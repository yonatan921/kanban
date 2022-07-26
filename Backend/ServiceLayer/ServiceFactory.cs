using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ServiceFactory
    {
        public UserService userService;
        public TaskService taskService;
        public BoardService boardService;
        public DataService dataService;

        private UserController userController;
        private BoardController boardController;

        public BoardController BoardController
        {
            get { return boardController; }
        }

        public UserController UserController
        {
            get { return userController; }
        }

        private TaskController taskController;

        public TaskController TaskController
        {
            get { return taskController; }
        }

        public ServiceFactory()
        {
            create();
        }

        /// <summary>
        /// This method creates the services and controllers. 
        /// </summary>
        /// <returns></returns>
        private ServiceFactory create()
        {
            userController = new UserController();
            userService = new UserService(this);
            boardController = new BoardController(this);
            boardService = new BoardService(this);
            taskController = new TaskController(this);
            taskService = new TaskService(this);
            dataService = new DataService(userController, boardController, taskController);
            return this;
        }


        public void LoadData()
        {
            userController.loadData();
            boardController.loadData();
        }
    }
}