using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class TaskModel : NotifiableModelObject
    {
        private string title;
        public string Title { get; set; }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private DateTime dueDate;
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }

        private string assignee;
        public string Assignee { get { return assignee; } set { { assignee = value; } } }


        public TaskModel(BackendController controller, string title, string desc, DateTime dueDate, string assignee) : base(controller)
        {
            this.Title = title;
            this.Description = desc;
            this.DueDate = dueDate;
            this.Assignee = assignee;
        }
    }
}
