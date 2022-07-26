using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class DataService
    {
        private UserController userController;
        private BoardController boardController;
        private TaskController taskController;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DataService(UserController userController, BoardController boardController, TaskController taskController)
        {
            this.userController = userController;
            this.boardController = boardController;
            this.taskController = taskController;
        }

        /// <summary>
        /// This method loads the data from db. 
        /// </summary>
        /// <returns></returns>
        public Response<string> LoadData()
        {
            try
            {
                userController.loadData();
                boardController.loadData();
                log.Info("Data has been loaded successfully");
                return new Response<string>(null, null);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return new Response<string>(e.Message, null);
            }
        }

        /// <summary>
        /// This method deletes the data from db. 
        /// </summary>
        /// <returns></returns>
        public Response<string> DeleteData()
        {
            try
            {
                userController.resetData();
                boardController.resetData();
                taskController.resetData();
                log.Info("Data has been deleted successfully");
                return new Response<string>(null, null);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return new Response<string>(e.Message, null);
            }
        }

    }
}
