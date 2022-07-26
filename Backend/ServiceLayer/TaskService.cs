using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private readonly UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly TaskController taskController;

        public TaskService(ServiceFactory serviceFactory)
        {
            this.userController = serviceFactory.UserController;
            this.taskController = serviceFactory.TaskController;
            // this.taskController = new TaskController(userController);
        }


        /// <summary>
        /// This method creates and adds a new task. 
        /// </summary>
        /// <param name="user">The username that wants to add a new task</param>
        /// <param name="title">The title of the new task</param>
        /// <param name="description">The description of the new task</param>
        /// <param name="dueTime">The dueDate of the new task</param>
        /// <param name="boardId">The boardId of the new task</param>
        /// <returns>creates a new task and adds it to the users task list</returns>
        public string add(string title, string description, DateTime dueTime, string boardName, string email)
        {
            try
            {
                email = email.ToLower();
                taskController.addTask(title, description, dueTime, boardName, email);
                log.Debug("a task was added to user: " + email);
                return JsonController<string>.toJson(new Response<string>(null, null));
            }
            catch (Exception e)
            {
                return JsonController<string>.toJson(new Response<string>(e.Message, null));
            }
        }


        /// <summary>
        /// This method is called when the user tries to edit the task title. 
        /// </summary>
        /// <param name="title">The title of the new task</param>
        /// <param name="task">The task that it's title needs to be edited</param>
        /// <param name="user">The username that wants to edit the task title</param>
        /// <returns>The string "{}", unless an error occurs</returns>
        public string editTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                email = email.ToLower();
                taskController.editTitle(email, boardName, columnOrdinal, taskId, title);
                log.Debug("User: " + email + "edited the task's title");
                return JsonController<string>.toJson(new Response<string>(null, null));
            }
            catch (Exception e)
            {
                return JsonController<string>.toJson(new Response<string>(e.Message, null));
            }
        }

        /// <summary>
        /// This method is called when the user tries to edit the task description. 
        /// </summary>
        /// <param name="description">The task that it's description is edited</param>
        /// <param name="task">The task that it's description needs to be edited</param>
        /// <param name="user">The username that wants to edit the task description</param>
        /// <returns>The string "{}", unless an error occurs</returns>
        public string editTaskDescription(string email, string boardName, int columnOrdinal, int taskId,
            string description)
        {
            try
            {
                email = email.ToLower();
                taskController.editDescription(email, boardName, columnOrdinal, taskId, description);
                log.Debug("User: " + email + "edited thier task title");
                return JsonController<string>.toJson(new Response<string>(null, null));
            }
            catch (Exception e)
            {
                return JsonController<string>.toJson(new Response<string>(e.Message, null));
            }
        }

        /// <summary>
        /// This method is called when the user tries to change the dueDate. 
        /// </summary>
        /// <param name="dateTime">the new due date that the user wants to change to</param>
        /// <param name="task">The task that it's description needs to be edited</param>
        /// <param name="user">The username that wants to edit the task description</param>
        /// <returns>The string "{}", unless an error occurs</returns>
        public string editTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                email = email.ToLower();
                taskController.editDueDate(email, boardName, columnOrdinal, taskId, dueDate);
                log.Debug("User: " + email + "edited the task title");
                return JsonController<string>.toJson(new Response<string>(null, null));
            }
            catch (Exception e)
            {
                return JsonController<string>.toJson(new Response<string>(e.Message, null));
            }
        }

        public string listTasksInProgress(string email)
        {
            try
            {
                email = email.ToLower();
                List<Task> inProgress = taskController.listTaskInProgress(email);
                log.Info("in progress tasks were listed successfully");
                return JsonController<List<Task>>.toJson(new Response<List<Task>>(null, inProgress));
            }
            catch (Exception e)
            {
                return JsonController<List<Task>>.toJson(new Response<List<Task>>(e.Message, null));
            }
        }

        /// <summary>
        /// This method changes the state of task. 
        /// </summary>
        /// <param name="id">The id of the board in which the task is in</param>
        /// <param name="taskTitle">The title of the task of which to change state</param>
        /// <returns>The string "{}", unless an error occurs</returns>
        public string changeState(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                email = email.ToLower();
                taskController.changeState(email, boardName, columnOrdinal, taskId);
                log.Info("taks: " + taskId + " was advanced by " + email);
                return JsonController<string>.toJson(new Response<string>(null, null));
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return JsonController<string>.toJson(new Response<string>(e.Message, null));
            }
        }

        public string AssignTask(string email, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                email = email.ToLower();
                emailAssignee = emailAssignee.ToLower();
                taskController.assignTask(email, boardName, columnOrdinal, taskId, emailAssignee);
                log.Info("task " + taskId + " was assigned to " + emailAssignee);
                return JsonController<string>.toJson(new Response<string>(null, null));
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return JsonController<string>.toJson(new Response<string>(e.Message, null));
            }
        }
    }
}