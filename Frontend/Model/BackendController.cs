using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using Task = IntroSE.Kanban.Backend.BusinessLayer.Task;

namespace Frontend.Model
{
    public class BackendController
    {
        private ServiceFactory serviceFactory;


        public BackendController(ServiceFactory service)
        {
            this.serviceFactory = service;
        }

        public BackendController()
        {
            this.serviceFactory = new ServiceFactory();
            serviceFactory.LoadData();
        }

        public Response<string> Login(string username, string password)
        {
            Response<string> response = JsonController<string>.fromJson(serviceFactory.userService.login(username, password));
            
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }

            // return new UserModel(this, username);
            return response;
        }

        public Response<string> Register(string username, string password)
        {
            Response<string> response = JsonController<string>.fromJson(serviceFactory.userService.createUser(username, password));
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }

            return response;
        }

        public Response<string> logOut(string username)
        {
            Response<string> response = JsonController<string>.fromJson(serviceFactory.userService.logout(username));
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }

            // return new UserModel(this, username);
            return response;
        }

        public Response<List<int>> getBoards(string username)
        {
            Response<List<int>> response = JsonController<List<int>>.fromJson(serviceFactory.userService.GetUserBoards(username));
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }

            // return new UserModel(this, username);
            return response;
        }

        public Response<List<string>> getBoardsNames(string username)
        {
            Response<List<string>> response = JsonController<List<string>>.fromJson(serviceFactory.userService.GetUserBoardsNames(username));
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }

            // return new UserModel(this, username);
            return response;
        }

        public Response<List<Task>> getColumn(string email, string boardName, int columnOrdinal)
        {
            Response<List<Task>> response = JsonController<List<Task>>.fromJson(serviceFactory.boardService.getColumn(email, boardName, columnOrdinal));
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }
        
            // return new UserModel(this, username);
            return response;
        }
    }
}