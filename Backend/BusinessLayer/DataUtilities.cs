using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public static class DataUtilities
    {
        public const int EMPTYSEQ = 1;

        /// <summary>
        /// loads the data of every board(id,board_name,board_owner,backlog_limit etc)
        /// </summary>
        /// <param name="boardDalController">// the boards dal controller</param>
        /// <returns>dictionary of [boardId, board]</returns>
        internal static Dictionary<int, Board> loadData(BoardDalController boardDalController)
        {
            Dictionary<int, Board> boardsLoaded = new Dictionary<int, Board>();
            List<BoardDTO> boardsDtos = boardDalController.SelectAllBoards();
            foreach (BoardDTO boardDTO in boardsDtos)
            {
                boardsLoaded.Add(boardDTO.id, new Board(boardDTO.BoardName, boardDTO.id));
            }
            return boardsLoaded;
        }

        /// <summary>
        /// loads the data of every task(id,title,description, board_id,creation_time,due_date)
        /// </summary>
        /// <param name="taskDalController">// the tasks dal controller</param>
        /// <returns>list of tasks</returns>
        internal static List<Task> loadData(TaskDalController taskDalController)
        {
            List<Task> tasksLoaded = new List<Task>();
            List<TaskDTO> tasksDTOs = taskDalController.SelectAllTasks();
            foreach (TaskDTO taskDto in tasksDTOs)
            {
                tasksLoaded.Add(new Task(taskDto.Title, taskDto.Description, taskDto.DueDate, taskDto.id, taskDto.BoardId, taskDto.ColumnOrdinal, taskDto.Assignee));
            }
            return tasksLoaded;
        }

        /// <summary>
        /// loads the data of every user(id,email,password)
        /// </summary>
        /// <param name="userDalController">// the users dal controller</param>
        /// <returns>dictionary of [dict of [email, User], dict of [userId, User]]</returns>
        internal static Dictionary<Dictionary<string, User>, Dictionary<int, User>> loadData(UserDalController userDalController)
        {
            Dictionary<Dictionary<string, User>, Dictionary<int, User>> returnValue = new Dictionary<Dictionary<string, User>, Dictionary<int, User>>();
            Dictionary<string, User> userByName = new Dictionary<string, User>();
            Dictionary<int, User> userById = new Dictionary<int, User>();
            List<UserDTO> userDtos = userDalController.SelectAllUsers();
            foreach (UserDTO userDto in userDtos)
            {
                User user = new User(userDto.Email, userDto.Password, userDto.id);
                userByName.Add(userDto.Email, user);
                userById.Add(userDto.id, user);
            }
            returnValue.Add(userByName,userById);
            return returnValue;
        }

        /// <summary>
        /// loads the data of boardMembers (board_id, member_id)
        /// </summary>
        /// <param name="boardsMembersDalController">// the board members dal controller</param>
        /// <returns>list of tuple (board_id, member_id)</returns>
        internal static List<(int boardId, int memberId)> loadData(BoardsMembersDalController boardsMembersDalController)
        {
            List<(int boardId, int memberId)> boardsMembers = new List<(int boardId, int memberId)>();
            List<BoardsMembersDTO> BoardsMembersDtos = boardsMembersDalController.SelectAllBoardsMembersDtos();
            foreach (BoardsMembersDTO boardMembersDto in BoardsMembersDtos)
            {
                boardsMembers.Add((boardMembersDto.BoardId, boardMembersDto.MemberID));
            }
            return boardsMembers;
        }
    }
}
