using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.BusinessLayer.Task;


namespace Frontend.Model
{
    public class BoardModel : NotifiableModelObject
    {
        private UserModel user;
        // public ObservableCollection<IntModel> BoardsIds { get; set; }
        // public int Id { get; set; }

        private ObservableCollection<TaskModel> backlog;
        public ObservableCollection<TaskModel> Backlog { get { return backlog; } set { backlog = value; } }
        
        private ObservableCollection<TaskModel> inProgress;
        public ObservableCollection<TaskModel> InProgress { get { return inProgress; } set { inProgress = value; } }

        private ObservableCollection<TaskModel> done { get; set; }
        public ObservableCollection<TaskModel> Done { get { return done; } set { done = value; } }

        private string name { get; set; }
        public string Name { get { return name; } set { name = value; } }
        
        public BoardModel(BackendController bc, UserModel UserModel, string boardName) : base(bc)
        {
            this.user = UserModel;
            this.name = boardName;
            // Id=1000;

            // List<int> list = (List<int>)(bc.getBoards(user.Email).ReturnValue);
            // BoardsIds = new ObservableCollection<IntModel>(list.
            // Select((c, i) => new IntModel(bc, list[i])));

            List<Task> backlogList = (List<Task>)(bc.getColumn(user.Email, this.name, 0).ReturnValue);
            backlog = new ObservableCollection<TaskModel>(backlogList.
            Select((c, i) => new TaskModel(bc, backlogList[i].title, backlogList[i].Description, backlogList[i].DueDate, backlogList[i].Assignie)));

            List<Task> inProgressList = (List<Task>)(bc.getColumn(user.Email, this.name, 1).ReturnValue);
            inProgress = new ObservableCollection<TaskModel>(inProgressList.
                Select((c, i) => new TaskModel(bc, inProgressList[i].title, inProgressList[i].Description, inProgressList[i].DueDate, inProgressList[i].Assignie)));

            List<Task> doneList = (List<Task>)(bc.getColumn(user.Email, this.name, 2).ReturnValue);
            done = new ObservableCollection<TaskModel>(doneList.
                Select((c, i) => new TaskModel(bc, doneList[i].title, doneList[i].Description, doneList[i].DueDate, doneList[i].Assignie)));


            // BoardsIds = new ObservableCollection<IntModel>();
            // IntModel model = new IntModel(bc, 100);
            // BoardsIds.Add(model);
            // boardIds = (List<int>)(bc.getBoards(user.Email).ReturnValue);
        }
    }
}
