// using System;
// using System.Linq;
// using System.Collections.Generic;
// using System.Reflection.Metadata.Ecma335;
// using IntroSE.Kanban.Backend.BusinessLayer;
//
//
// namespace IntroSE.Kanban.Backend.ServiceLayer
// {
//     /// <summary>
//     /// A class for grading your work <b>ONLY</b>. The methods are not using good SE practices and you should <b>NOT</b> infer any insight on how to write the service layer/business layer. 
//     /// <para>
//     /// Each of the class' methods should return a JSON string with the following structure (see <see cref="System.Text.Json"/>):
//     /// <code>
//     /// {
//     ///     "ErrorMessage": &lt;string&gt;,
//     ///     "ReturnValue": &lt;object&gt;
//     /// }
//     /// </code>
//     /// Where:
//     /// <list type="bullet">
//     ///     <item>
//     ///         <term>ReturnValue</term>
//     ///         <description>
//     ///             The return value of the function.
//     ///             <para>
//     ///                 The value may be either a <paramref name="primitive"/>, a <paramref name="Task"/>, or an array of of them. See below for the definition of <paramref name="Task"/>.
//     ///             </para>
//     ///             <para>If the function does not return a value or an exception has occorred, then the field is undefined.</para>
//     ///         </description>
//     ///     </item>
//     ///     <item>
//     ///         <term>ErrorMessage</term>
//     ///         <description>If an exception has occorred, then this field will contain a string of the error message.</description>
//     ///     </item>
//     /// </list>
//     /// </para>
//     /// <para>
//     /// The structure of the JSON of a Task, is:
//     /// <code>
//     /// {
//     ///     "Id": &lt;int&gt;,
//     ///     "CreationTime": &lt;DateTime&gt;,
//     ///     "Title": &lt;string&gt;,
//     ///     "Description": &lt;string&gt;,
//     ///     "DueDate": &lt;DateTime&gt;
//     /// }
//     /// </code>
//     /// </para>
//     /// </summary>
//     public class GradingService
//     {
//         // UserService userService;
//         // BoardService boardService;
//         // TaskService taskService;
//         private ServiceFactory serviceFactory;
//
//         public GradingService()
//         {
//             serviceFactory = new ServiceFactory();
//             // userService = new UserService();
//             // boardService = new BoardService(userService);
//             // taskService = new TaskService(userService);
//         }
//
//
//         /// <summary>
//         /// This method registers a new user to the system.
//         /// </summary>
//         /// <param name="email">The user email address, used as the username for logging the system.</param>
//         /// <param name="password">The user password.</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string Register(string email, string password)
//         {
//             return (JsonController.toJson(serviceFactory.userService.createUser(email, password)));
//         }
//
//
//         //
//         //         /// <summary>
//         //         ///  This method logs in an existing user.
//         //         /// </summary>
//         //         /// <param name="email">The email address of the user to login</param>
//         //         /// <param name="password">The password of the user to login</param>
//         //         /// <returns>Response with user email, unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string Login(string email, string password)
//         {
//             return (JsonController.toJson(serviceFactory.userService.login(email, password)));
//         }
//         //
//         //
//         /// <summary>
//         /// This method logs out a logged in user. 
//         /// </summary>
//         /// <param name="email">The email of the user to log out</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string Logout(string email)
//         {
//             // Response response = serviceFactory.userService.logout(email);
//             // string s = (JsonController.toJson(response));
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//
//             return (JsonController.toJson(serviceFactory.userService.logout(email)));
//         }
//
//         /// <summary>
//         /// This method limits the number of tasks in a specific column.
//         /// </summary>
//         /// <param name="email">The email address of the user, must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
//         {
//             Response response = (serviceFactory.boardService.limitColumn(email, boardName, columnOrdinal, limit));
//             string s = JsonController.toJson(response);
//             if (response.ErrorMessage == null)
//             {
//                 return (string)response.ReturnValue;
//             }
//
//             return (JsonController.toJson(serviceFactory.boardService.limitColumn(email, boardName, columnOrdinal, limit)));
//         }
//
//         /// <summary>
//         /// This method gets the limit of a specific column.
//         /// </summary>
//         /// <param name="email">The email address of the user, must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string GetColumnLimit(string email, string boardName, int columnOrdinal)
//         {
//             return JsonController.toJson(serviceFactory.boardService.getColumnLimit(email, boardName, columnOrdinal));
//         }
//
//
//         /// <summary>
//         /// This method gets the name of a specific column
//         /// </summary>
//         /// <param name="email">The email address of the user, must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <returns>Response with column name value, unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string GetColumnName(string email, string boardName, int columnOrdinal)
//         {
//             return JsonController.toJson(serviceFactory.boardService.getColumnName(email, boardName, columnOrdinal));
//         }
//
//
//         /// <summary>
//         /// This method adds a new task.
//         /// </summary>
//         /// <param name="email">Email of the user. The user must be logged in.</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="title">Title of the new task</param>
//         /// <param name="description">Description of the new task</param>
//         /// <param name="dueDate">The due date if the new task</param>
//         /// <returns>Response with user-email, unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
//         {
//             return JsonController.toJson(serviceFactory.taskService.add(title, description, dueDate, boardName, email));
//         }
//
//
//         /// <summary>
//         /// This method updates the due date of a task
//         /// </summary>
//         /// <param name="email">Email of the user. Must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <param name="taskId">The task to be updated identified task ID</param>
//         /// <param name="dueDate">The new due date of the column</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
//         {
//             // Response response = serviceFactory.taskService.editTaskDueDate(email, boardName, columnOrdinal, taskId,
//             //     dueDate);
//             // string s = JsonController.toJson(response);
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//
//             return JsonController.toJson(serviceFactory.taskService.editTaskDueDate(email, boardName, columnOrdinal, taskId,
//                 dueDate));
//         }
//
//
//         /// <summary>
//         /// This method updates task title.
//         /// </summary>
//         /// <param name="email">Email of user. Must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <param name="taskId">The task to be updated identified task ID</param>
//         /// <param name="title">New title for the task</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
//         {
//             // Response response = serviceFactory.taskService.editTaskTitle(email, boardName, columnOrdinal, taskId,
//             //     title);
//             // string s = JsonController.toJson(response);
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//
//             return JsonController.toJson(serviceFactory.taskService.editTaskTitle(email, boardName, columnOrdinal, taskId,
//                 title));
//         }
//
//
//         /// <summary>
//         /// This method updates the description of a task.
//         /// </summary>
//         /// <param name="email">Email of user. Must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <param name="taskId">The task to be updated identified task ID</param>
//         /// <param name="description">New description for the task</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId,
//             string description)
//         {
//             // Response response = serviceFactory.taskService.editTaskDescription(email, boardName, columnOrdinal, taskId,
//             //     description);
//             // string s = JsonController.toJson(response);
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//
//             return JsonController.toJson(serviceFactory.taskService.editTaskDescription(email, boardName, columnOrdinal, taskId,
//                 description));
//         }
//
//
//         /// <summary>
//         /// This method advances a task to the next column
//         /// </summary>
//         /// <param name="email">Email of user. Must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <param name="taskId">The task to be updated identified task ID</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
//         {
//             // Response response = serviceFactory.taskService.changeState(email, boardName, columnOrdinal, taskId);
//             // string s = JsonController.toJson(response);
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//
//             return JsonController.toJson(serviceFactory.taskService.changeState(email, boardName, columnOrdinal, taskId));
//         }
//
//
//         /// <summary>
//         /// This method returns a column given it's name
//         /// </summary>
//         /// <param name="email">Email of the user. Must be logged in</param>
//         /// <param name="boardName">The name of the board</param>
//         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
//         /// <returns>Response with a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string GetColumn(string email, string boardName, int columnOrdinal)
//         {
//             return (JsonController.toJson(serviceFactory.boardService.getColumn(email, boardName, columnOrdinal)));
//         }
//
//
//         /// <summary>
//         /// This method adds a board to the specific user.
//         /// </summary>
//         /// <param name="email">Email of the user. Must be logged in</param>
//         /// <param name="name">The name of the new board</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string AddBoard(string email, string name)
//         {
//             // Response response = serviceFactory.boardService.createBoard(name, email);
//             // string s = JsonController.toJson(response);
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//             //JsonController.toJson(serviceFactory.userService.login(email, password))
//             return JsonController.toJson(serviceFactory.boardService.createBoard(name, email));
//             // return s;
//         }
//
//
//         /// <summary>
//         /// This method removes a board to the specific user.
//         /// </summary>
//         /// <param name="email">Email of the user. Must be logged in</param>
//         /// <param name="name">The name of the board</param>
//         /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string RemoveBoard(string email, string name)
//         {
//             // Response response = serviceFactory.boardService.remove(name, email);
//             // string s = JsonController.toJson(response);
//             // if (response.ErrorMessage == null)
//             // {
//             //     return (string)response.ReturnValue;
//             // }
//
//             return JsonController.toJson(serviceFactory.boardService.remove(name, email));
//         }
//
//
//         /// <summary>
//         /// This method returns all the In progress tasks of the user.
//         /// </summary>
//         /// <param name="email">Email of the user. Must be logged in</param>
//         /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
//         public string InProgressTasks(string email)
//         {
//             return (JsonController.toJson(serviceFactory.taskService.listTasksInProgress(email)));
//         }
//     }
// }