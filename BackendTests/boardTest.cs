// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using IntroSE.Kanban.Backend.ServiceLayer;
// using System.Text.Json;
// using IntroSE.Kanban.Backend.BusinessLayer;
//
// namespace BackendTests
// {
//     public class BoardTest
//     {
//         private readonly BoardService boardService;
//         private readonly UserService userService;
//         private readonly TaskService taskService;
//
//         public BoardTest(BoardService boardService, UserService userService, TaskService taskService)
//         {
//             this.boardService = boardService;
//             this.userService = userService;
//             this.taskService = taskService;
//         }
//
//         public void runBoardTests()
//         {
//             Response res = userService.createUser("yonatan@gamil.com", "Aa13456");
//             addBoard1();
//             addBoard2();
//             addBoard3();
//             addBoard4();
//             addBoard5();
//
//             removeBoard1();
//             removeBoard2();
//             removeBoard3();
//             removeBoard4();
//
//             limitColumn1();
//             limitColumn2();
//             limitColumn3();
//             limitColumn4();
//             limitColumn5();
//
//             getColumnLimit1();
//             getColumnLimit2();
//             getColumnLimit3();
//             getColumnLimit4();
//
//             getColumnName1();
//             getColumnName2();
//             getColumnName3();
//             getColumnName4();
//
//             getColumn1();
//             getColumn2();
//             getColumn3();
//             getColumn4();
//             getColumn5();
//
//             joinBoard1();
//             joinBoard2();
//             joinBoard3();
//
//             transferOwnership1();
//             transferOwnership2();
//             transferOwnership3();
//
//             leaveBoard1();
//             leaveBoard2();
//             leaveBoard3();
//
//             getBoardName1();
//             getBoardName2();
//         }
//
//         public void addBoard1() //should create a board successfully
//         {
//             Response res = boardService.createBoard("board1", "yonatan@gamil.com");
//             if (res.ErrorMessage.Equals("{}"))
//                 Console.WriteLine(
//                     "Account with email: yonatan@gamil.com has created a board with name \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void addBoard2() //should return that board with such name already exists
//         {
//             Response res = boardService.createBoard("board1", "yonatan@gamil.com"); //should create a board
//             if (res.ErrorMessage == null)
//                 Console.WriteLine(
//                     "Account with email: yonatan@gamil.com has created a board with name \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void addBoard3() //should return illegal board name
//         {
//             Response res = boardService.createBoard("", "yonatan@gamil.com"); //should create a board
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan@gamil.com has created a board with name \"\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void addBoard4() //should return attempt to create board to a logged-out user
//         {
//             userService.logout("yonatan@gamil.com");
//             Response res = boardService.createBoard("board2", "yonatan@gamil.com");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan@gamil.com has created a board with name \"board2\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void addBoard5() //should return attempt to create board to a non-existing user
//         {
//             userService.login("yonatan@gamil.com", "Aa13456");
//             Response res = boardService.createBoard("board222", "yonatan222@gamil.com");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan222@gamil.com has created a board with name \"board222\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void removeBoard1() //should remove successfully
//         {
//             Response res = boardService.removeBoard("board1", "yonatan@gamil.com");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan@gamil.com has removed a board with name \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void removeBoard2() //should return attempt to remove board that didn't exist in the first place
//         {
//             Response res = boardService.removeBoard("board2", "yonatan@gamil.com");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan@gamil.com has removed a board with name \"board2\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void removeBoard3() //should return the account doesn't even exist
//         {
//             Response res = boardService.removeBoard("board222", "yonatan222@gamil.com");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan222@gamil.com has removed a board with name \"board222\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void removeBoard4() //should return the account isn't even logged in
//         {
//             userService.logout("yonatan@gamil.com");
//             Response res = boardService.removeBoard("board1", "yonatan@gamil.com");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: yonatan222@gamil.com has removed a board with name \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn1() //should limit "backlog" column
//         {
//             userService.createUser("olga1@gmail.com", "123456Ab");
//             boardService.createBoard("board1", "olga1@gmail.com");
//             
//             Response res = boardService.limitColumn("olga@gmail.com", "board1", 0, 10);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column backlog in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn2() //should fail - board2 doesn't exist
//         {
//             Response res = boardService.limitColumn("olga@gmail.com", "board2", 0, 10);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column backlog in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn3() //should fail - illegal limit value
//         {
//             Response res = boardService.limitColumn("olga@gmail.com", "board1", 0, -20);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column backlog in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn4() //should succeed - limit inProgress column
//         {
//             Response res = boardService.limitColumn("olga@gmail.com", "board1", 1, 10);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column in progress in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn5() //should succeed - limit done column
//         {
//             Response res = boardService.limitColumn("olga@gmail.com", "board1", 2, 10);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column done in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn6() //should fail - illegal column ordnial
//         {
//             Response res = boardService.limitColumn("olga@gmail.com", "board1", -2, 10);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column done in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void limitColumn7() //should fail - illegal column ordnial
//         {
//             Response res = boardService.limitColumn("olga@gmail.com", "board1", 4, 10);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga1@gamil.com has limited column done in \"board1\" successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnLimit1() //should succeed - get limit of backlog
//         {
//             userService.createUser("olga2@gmail.com", "123456Ab");
//             boardService.createBoard("board2", "olga2@gmail.com");
//
//             Response res = boardService.getColumnLimit("olga2@gmail.com", "board2", 0);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga2@gamil.com has retrieved limit of backlog successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnLimit2() //should succeed - get limit of in progress
//         {
//             Response res = boardService.getColumnLimit("olga2@gmail.com", "board2", 1);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga2@gamil.com has retrieved limit of in progress successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnLimit3() //should succeed - get limit of done
//         {
//             Response res = boardService.getColumnLimit("olga2@gmail.com", "board2", 2);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga2@gamil.com has retrieved limit of done successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnLimit4() //should fail - illegal column ordinal
//         {
//             Response res = boardService.getColumnLimit("olga2@gmail.com", "board2", 3);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga2@gamil.com has retrieved limit successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnName1() //should succeed - get name of backlog
//         {
//             userService.createUser("olga3@gmail.com", "123456Ab");
//             boardService.createBoard("board3", "olga2@gmail.com");
//
//             Response res = boardService.getColumnLimit("olga3@gmail.com", "board3", 0);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga3@gamil.com has retrieved name of backlog successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnName2() //should succeed - get limit of in progress
//         {
//             Response res = boardService.getColumnLimit("olga3@gmail.com", "board3", 1);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga3@gamil.com has retrieved name of in progress successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnName3() //should succeed - get limit of done
//         {
//             Response res = boardService.getColumnLimit("olga3@gmail.com", "board3", 2);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga3@gamil.com has retrieved name of done successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumnName4() //should fail - illegal column ordinal
//         {
//             Response res = boardService.getColumnLimit("olga3@gmail.com", "board3", 3);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga3@gamil.com has retrieved name successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumn1() //should succeed
//         {
//             userService.createUser("olga4@gmail.com", "123456Ab");
//             boardService.createBoard("board4", "olga4@gmail.com");
//
//             Response res = boardService.getColumn("olga4@gmail.com", "board4", 0);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga4@gamil.com has retrieved column backlog successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumn2() //should succeed
//         {
//             Response res = boardService.getColumn("olga4@gmail.com", "board4", 1);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga4@gamil.com has retrieved column in progress successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumn3() //should succeed
//         {
//             Response res = boardService.getColumn("olga4@gmail.com", "board4", 2);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga4@gamil.com has retrieved column done successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumn4() //should fail - illegal column ordinal
//         {
//             Response res = boardService.getColumn("olga4@gmail.com", "board4", 4);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga4@gamil.com has retrieved column successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getColumn5() //should fail - board doesn't exist
//         {
//             Response res = boardService.getColumn("olga4@gmail.com", "board10", 0);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga4@gamil.com has retrieved column backlog successfully ");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void joinBoard1() //should succeed
//         {
//             userService.userController.resetData();
//             boardService.boardController.resetData();
//             userService.createUser("olga5@gmail.com", "123456Ab");
//             userService.createUser("danny@gmail.com", "123456Ab");
//             boardService.createBoard("board5", "olga5@gmail.com");
//
//             Response res = boardService.joinBoard("danny@gmail.com", 1);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny@gmail.com has joined board with id = 1 successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void joinBoard2() //should fail - board doesn't exist
//         {
//             Response res = boardService.joinBoard("danny@gmail.com", 8);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny@gmail.com has joined board with id = 8 successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void joinBoard3() //should fail - user doesn't exist
//         {
//             Response res = boardService.joinBoard("danny4@gmail.com", 1);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny4@gmail.com has joined board with id = 1 successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void transferOwnership1() // should succeed
//         {
//             userService.createUser("olga6@gmail.com", "123456Ab");
//             userService.createUser("danny2@gmail.com", "123456Ab");
//             boardService.createBoard("board6", "olga6@gmail.com");
//             boardService.joinBoard("danny2@gmail.com", 2);
//
//             Response res = boardService.transferOwnerShip("olga6@gmail.com", "danny2@gmail.com", "board6");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny2@gmail.com now owns board6 successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void transferOwnership2() // should fail - not member of the board
//         {
//             boardService.createBoard("board7", "olga6@gmail.com");
//
//             Response res = boardService.transferOwnerShip("olga6@gmail.com", "danny2@gmail.com", "board7");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny2@gmail.com now owns board7 successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void transferOwnership3() // should fail - board doesnt exist
//         {
//             boardService.createBoard("board7", "olga6@gmail.com");
//
//             Response res = boardService.transferOwnerShip("olga6@gmail.com", "danny2@gmail.com", "board100");
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny2@gmail.com now owns board7 successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void leaveBoard1() //should succeed
//         {
//             userService.createUser("olga7@gmail.com", "123456Ab");
//             userService.createUser("danny3@gmail.com", "123456Ab");
//             boardService.createBoard("board7", "olga7@gmail.com");
//             boardService.joinBoard("danny3@gmail.com", 3);
//
//             Response res = boardService.leaveBoard("danny3@gmail.com",3);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny3@gmail.com left successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void leaveBoard2() //should fail - owner can't leave board
//         {
//             Response res = boardService.leaveBoard("olga7@gmail.com", 3);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: olga7@gmail.com left successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void leaveBoard3() //should fail - not member of the bed
//         {
//             Response res = boardService.leaveBoard("danny3@gmail.com", 3);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: danny3@gmail.com left successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getBoardName1() // should succeed
//         {
//             userService.createUser("olga8@gmail.com", "123456Ab");
//             boardService.createBoard("board8", "olga8@gmail.com");
//             
//             Response res = boardService.getBoardName(4);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: board name with id = 4 retrieved successfully successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//
//         public void getBoardName2() //should fail - board with such id doesnt exist
//         { 
//             Response res = boardService.getBoardName(100);
//             if (res.ErrorMessage.Equals(null))
//                 Console.WriteLine(
//                     "Account with email: board name with id = 100 retrieved successfully successfully");
//             else
//                 Console.WriteLine(res.ErrorMessage);
//         }
//     }
// }