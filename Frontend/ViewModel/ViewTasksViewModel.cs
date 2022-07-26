using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace Frontend.ViewModel
{
    internal class ViewTasksViewModel : NotifiableObject
    {
        public BackendController controller;

        private UserModel userModel;
        public UserModel UserModel { get; set; }
        
        // private BoardModel boardModel;
        // public BoardModel BoardModel { get; private set; }

        public BoardModel Board { get; private set; }
        public ViewTasksViewModel(UserModel u, BoardModel b)
        {
            this.controller = u.Controller;
            this.UserModel = u;
            // this.Board = b;
            // Title = "Boards of " + user.Email;
            // Board = user.getBoards();
            Board = b;
        }

    }
}
