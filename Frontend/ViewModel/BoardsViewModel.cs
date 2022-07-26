using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;
using Frontend.View;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Frontend.ViewModel
{
    internal class BoardsViewModel : NotifiableObject
    {
        public UserModel UserModel;

        public BackendController controller;

        // public BoardModel Board { get; private set; }
        public UserModel User { get; private set; }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Title { get; private set; }

        private BoardModel selectedBoard;

        public BoardModel SelectedBoard
        {
            get { return selectedBoard; }
            set
            {
                selectedBoard = value;
                // EnableForward = value != null;
                // RaisePropertyChanged("SelectedTask");
            }
        }

        public BoardsViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.UserModel = user;
            Title = "Boards of " + user.Email;
            Email = user.Email;
            User = user;
        }

        public Response<string> logout()
        {
            try
            {
                return UserModel.Controller.logOut(UserModel.Email);
            }
            catch (Exception e)
            {
                // Message = e.Message;
                return new Response<string>(e.Message, null);
            }
        }

        // public viewBoard()
        // {
        //
        // }
    }
}