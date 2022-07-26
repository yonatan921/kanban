using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.ViewModel;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Frontend.Model
{
    internal class MainViewModel : NotifiableObject
    {
        public BackendController Controller { get; private set; }

        public MainViewModel()
        {
            Controller = new BackendController();
        }


        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                this.username = value;
                RaisePropertyChanged("Username");
            }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                this.password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                this.message = value;
                RaisePropertyChanged("Message");
            }
        }

        public Response<string> Login()
        {
            try
            {
                return Controller.Login(Username, Password);
            }
            catch (Exception e)
            {
                // Message = e.Message;
                return new Response<string>(e.Message,null);
            }
        }

        public Response<string> Register()
        {
            try
            {
                return Controller.Register(Username, Password);
            }
            catch (Exception e)
            {
                return new Response<string>(e.Message, null);
            }
        }
    }
}