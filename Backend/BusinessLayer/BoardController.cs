using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class BoardController
    {
        private Dictionary<int, Board> boards;
        private UserController userController;
        private int boardIdCOunter;

        private BoardDalController boardDalController;
        private BoardsMembersDalController boardsMembersDalController;
        private TaskDalController taskDalController;

        public BoardController(ServiceFactory serviceFactory)
        {
            boardsMembersDalController = new BoardsMembersDalController();
            boardDalController = new BoardDalController();
            taskDalController = new TaskDalController();

            userController = serviceFactory.UserController;

            boardIdCOunter = (int)boardDalController.getSeq() + 1;

            boards = new Dictionary<int, Board>();
        }


        /// <summary>
        ///  This method adds a board to a user and to the global boards list.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>The created Board</returns>
        public Board addBoard(string boardName, string email)
        {
            if (string.IsNullOrWhiteSpace(boardName)) // in case the user tries to enter an empty title
            {
                throw new Exception("board name is not valid");
            }

            User user = userController.getUserAndLogeddin(email);

            Dictionary<string, Board> userBoardsbyName = user.getBoardListByName();
            Dictionary<int, Board> userBoardsbyId = user.getBoardListById();

            if (userBoardsbyName.ContainsKey(boardName)) // check if user has board with given name
            {
                throw new Exception("A board named " + boardName + " already exist");
            }

            boardDalController.Insert(new BoardDTO(boardIdCOunter, boardName, user.Id,
                Board.UNLIMITED, Board.UNLIMITED, Board.UNLIMITED));
            boardsMembersDalController.Insert(new BoardsMembersDTO(boardIdCOunter, user.Id));

            Board toAdd = new Board(boardName, boardIdCOunter);
            //assigne the board owner to the board boardId 
            // boardsOwnerId.Add(boardIdCOunter, user.Id);
            toAdd.Owner = user.Id; //assigns the board owner to be the User
            userBoardsbyName.Add(boardName, toAdd); // adds the board to users board list by name
            userBoardsbyId.Add(boardIdCOunter, toAdd); // adds the board to users board list by ID
            boards.Add(boardIdCOunter, toAdd); // adds the board to the global boards list
            toAdd.MemeberList.Add(user.email); //adds the board owner to members of the board

            boardIdCOunter++; // advances the global board boardId counter

            return toAdd;
        }


        /// <summary>
        /// This method lets a user join a board. 
        /// </summary>
        /// <param name="email">// the email of the user that wants to join</param>
        /// <param name="boardId">// the boardId of the board that wants to join</param>
        /// <returns></returns>
        public void joinBoard(string email, int boardId)
        {
            User currentUser = userController.getUserAndLogeddin(email);

            if (boards.ContainsKey(boardId))
            {
                Board boardToAdd = boards[boardId]; // the board to Add
                Dictionary<string, Board>
                    userBoardsByName = currentUser.getBoardListByName(); // the users boards by name
                Dictionary<int, Board> userBoardsById = currentUser.getBoardListById(); // the user boards by Id

                if (!userBoardsByName.ContainsKey(boardToAdd.Name)) // in case the user is not a part of this board 
                {
                    userBoardsByName.Add(boardToAdd.Name, boardToAdd); // adds the board to boards dict by name
                }
                else // else the user is already part of this board
                {
                    throw new Exception("user: " + email + " is already a member of a board " + boardToAdd.Name);
                }

                if (!userBoardsById.ContainsKey(boardId))
                {
                    userBoardsById.Add(boardId, boardToAdd); // adds the board to the users boards dict by Id
                }
                else // else the user is already part of this board
                {
                    throw new Exception("user: " + email + " is already part of this board");
                }

                boardsMembersDalController.Insert(new BoardsMembersDTO(boardToAdd.Id, currentUser.Id));

                boardToAdd.MemeberList.Add(email); //adds the user to the members list of the board
            }
            else
            {
                throw new Exception("board with: " + boardId + "does not exist");
            }
        }

        /// <summary>
        /// This method lets the owner of the board to transfer it's ownership. 
        /// </summary>
        /// <param name="currentOwnerEmail">// the mail of the owner of the board</param>
        /// <param name="newOwnerEmail">// the mail of the user that will be the new owner of the board</param>
        /// <param name="boardName">// the name of the board </param>
        /// <returns></returns>
        public void transferOwnerShip(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            User currentOwner = userController.getUserAndLogeddin(currentOwnerEmail);
            User newOwner = userController.getUser(newOwnerEmail);

            if (!currentOwner.getBoardListByName()
                    .ContainsKey(boardName)) // checks if the user is a member of this board
            {
                throw new Exception("this board does not exist");
            }

            Board currentBoard = currentOwner.getBoardListByName()[boardName];

            if (currentBoard.Owner !=
                currentOwner.Id) // if the user who's trying to perform the action is not the owner
            {
                throw new Exception("a user who is not the owner tried to transfer board ownership");
            }

            if (!currentBoard.MemeberList.Contains(newOwnerEmail))
            {
                throw new Exception(newOwnerEmail + " isn't a member of " + boardName);
            }

            //updating in database the new owner's id
            boardDalController.Update(currentBoard.Id, "board_owner", newOwner.Id);
            currentBoard.Owner = newOwner.Id;
        }


        /// <summary>
        /// This method lets a user leave a board unless he's an owner. 
        /// </summary>
        /// <param name="email">// the mail of the user that wants to leave the board</param>
        /// <param name="boardId">// the Id of the board the user wants to leave </param>
        /// <returns></returns>
        public void leaveBoard(string email, int boardId)
        {
            User leavingUser = userController.getUserAndLogeddin(email);

            if (!leavingUser.getBoardListById().ContainsKey(boardId)) // if the user is already not part of this board
            {
                throw new Exception("The user: " + email + " is already not part of board: " + boardId);
            }

            Board boardToLeave = boards[boardId];
            if (leavingUser.Id == boardToLeave.Owner) // in case the user who's trying to leave is the owner
            {
                throw new Exception("user: " + leavingUser + "who's a board owner tried to leave board: " + boardId);
            }

            boardToLeave.MemeberList.Remove(email); // removes the user from the boards users list
            leavingUser.getBoardListById().Remove(boardId); // removes the board from the boardList by ID
            leavingUser.getBoardListByName().Remove(boardToLeave.Name); // removes board from the users list by name

            foreach (Task task in boardToLeave.getColumn(boardToLeave.columnsId
                         .FirstOrDefault(x => x.Value == "backlog").Key))
            {
                if (task.Assignie.Equals(email))
                {
                    task.Assignie = null;
                }
            }

            foreach (Task task in boardToLeave.getColumn(boardToLeave.columnsId
                         .FirstOrDefault(x => x.Value == "in progress").Key))
            {
                if (task.Assignie.Equals(email))
                {
                    task.Assignie = null;
                }
            }

            boardsMembersDalController.Delete(new BoardsMembersDTO(boardId, leavingUser.Id));
        }

        /// <summary>
        /// This method lets a user remove a board if he's the owner
        /// </summary>
        /// <param name="boardName">// the mail of the user that wants to remove the board</param>
        /// <param name="email">// the Id of the board the user wants to remove </param>
        /// <returns></returns>
        public void removeBoard(string boardName, string email)
        {
            User owner = userController.getUserAndLogeddin(email);

            Dictionary<string, Board> userBoardsbyName = owner.getBoardListByName();
            Dictionary<int, Board> userBoardsbyId = owner.getBoardListById();

            if (!userBoardsbyName.ContainsKey(boardName))
            {
                throw new Exception("Try to remove a board with the name " + boardName +
                                    " which doesn't exist to the email: " + email);
            }

            Board board = userBoardsbyName[boardName];
            if (!board.Owner.Equals(owner.Id))
            {
                throw new Exception(email + " is not the owner of " + boardName);
            }

            if (board.MemeberList != null) //no need for exception, if null then only owner is in the board
            {
                foreach (string username in board.MemeberList) //remove board from all members 
                {
                    User user = userController.getUser(username);
                    user.getBoardListByName().Remove(boardName);
                    user.getBoardListById().Remove(board.Id);
                }
            }

            boardDalController.Delete(new BoardDTO(board.Id, board.Name, board.Owner, board.LimitBacklog,
                board.limitInProgress, board.LimitDone));

            boardsMembersDalController.DeleteBoard(board.Id);
            taskDalController.DeleteBoard(board.Id);

            userBoardsbyName.Remove(boardName); //board has been removed from userBoardByName
            userBoardsbyId.Remove(board.Id); // board has been removed from the userBoardById
            boards.Remove(board.Id); // removes the board from the global board list
        }

        /// <summary>
        ///  This method sets the column limit of a given column in a given board.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The id of the column</param>
        /// <param name="limit">The wanted limit for the column</param>
        /// <returns></returns>
        public void setColumnLimit(string email, string boardName, int columnOrdinal, int limit)
        {
            if (columnOrdinal > 2 || columnOrdinal < 0)
            {
                throw new Exception(columnOrdinal + " is invalid");
            }

            if (limit < -1)
            {
                throw new Exception("invalid column limit");
            }


            User user = userController.getUserAndLogeddin(email);
            Board board = user.hasBoardByName(boardName);

            if (board.getColumn(columnOrdinal).Count > limit)
            {
                throw new Exception("there are already too many tasks in this column");
            } 

            board.setColumnLimit(columnOrdinal, limit);
            // boardDalController.Update(board.Id, board.columnsId[columnOrdinal], limit);
            boardDalController.setColumnLimit(board.Id, board.columnsId[columnOrdinal], limit);
        }


        /// <summary>
        ///  This method returns the column limit of a given column in a given board.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnId">The id of the column</param>
        /// <returns>Limit of the given column</returns>
        public int getColumnLimit(string email, string boardName, int columnId)
        {
            User user = userController.getUserAndLogeddin(email); //check if exists and if logged in is in getUser

            if (columnId < 0 || columnId > 2)
            {
                throw new Exception("invalid columnId");
            }

            Board board = user.hasBoardByName(boardName);
            return board.getColumnLimit(columnId);
        }

        /// <summary>
        ///  This method returns the column name of a given column in a given board.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnId">The id of the column</param>
        /// <returns>Name of the given column</returns>
        public string getColumnName(string email, string boardName, int columnId)
        {
            User user = userController.getUserAndLogeddin(email); //check if exists and if logged in is in getUser

            if (columnId < 0 || columnId > 2)
            {
                throw new Exception("invalid columnId");
            }

            Board board = user.hasBoardByName(boardName);
            return board.getColumnName(columnId);
        }


        /// <summary>
        ///  This method returns a column from a board.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnId">The id of the column</param>
        /// <returns>List of tasks representing the column</returns>
        public List<Task> getColumn(string email, string boardName, int columnId)
        {
            User user = userController.getUserAndLogeddin(email); //check if exists and if logged in is in getUser

            if (columnId < 0 || columnId > 2)
            {
                throw new Exception("invalid columnId");
            }

            Board board = user.hasBoardByName(boardName);
            return board.getColumn(columnId);
        }


        /// <summary>
        /// This method loads data related to board- boards and board members. 
        /// </summary>
        /// <returns></returns>
        public void loadData()
        {
            boards = DataUtilities.loadData(boardDalController); // loading boards info from db

            List<(int boardId, int memberId)> boardsMembers = DataUtilities.loadData(boardsMembersDalController);

            //adding the boards of every user
            foreach ((int boardId, int memberId) entry in boardsMembers)
            {
                Board board = boards[entry.boardId];
                User user = userController.getUser(entry.memberId);
                user.getBoardListById().Add(board.Id, board);
                user.getBoardListByName().Add(board.Name, board);
            }

            //loading all tasks from db
            List<Task> tasks = DataUtilities.loadData(taskDalController);

            //adding tasks to its respective board in its respective column
            foreach (Task task in tasks)
            {
                Board board = boards[task.boardId];
                board.getColumn(board.getColumnNumber(task.columnOrdinal)).Add(task);
            }
        }


        /// <summary>
        /// This method returns the name of a board. 
        /// </summary>
        /// <param name="boardId">// the boardId of the board to get its' name</param>
        /// <returns>string with the name of the board</returns>
        public string getBoardName(int boardId)
        {
            if (!boards.ContainsKey(boardId))
            {
                throw new Exception("board with id = " + boardId + " doesn't exist!");
            }

            return boards[boardId].Name;
        }

        /// <summary>
        /// This method deletes the data from board related tables 
        /// </summary>
        /// <returns></returns>
        public void resetData()
        {
            boardDalController.resetTable();
            boardsMembersDalController.resetTable();
            boards.Clear();
            boardIdCOunter = DataUtilities.EMPTYSEQ;
        }
    }
}