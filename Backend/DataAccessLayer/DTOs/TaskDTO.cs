using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO : DTO
    {
        public const string TasksTitleColumnName = "title";
        public const string TasksDescriptionColumnName = "description";
        public const string TasksBoardIdColumnName = "board_id";
        public const string TasksCreationTimeColumnName = "creation_time";
        public const string TasksDueDateColumnName = "due_date";
        public const string TaskColumnOrdianlName = "column_ordinal";
        public const string TaskAssigneeName = "assignee";

        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                _controller.Update(id, TasksTitleColumnName, value);
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                _controller.Update(id, TasksDescriptionColumnName, value);
            }
        }

        private int boardId;

        public int BoardId
        {
            get => boardId;
            set
            {
                boardId = value;
                _controller.Update(id, TasksBoardIdColumnName, value);
            }
        }


        private DateTime creationTime;

        public DateTime CreationTime
        {
            get => creationTime;
            set
            {
                creationTime = value;
                _controller.Update(id, TasksCreationTimeColumnName, value.ToString());
            }
        }


        private DateTime dueDate;

        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                dueDate = value;
                _controller.Update(id, TasksDueDateColumnName, value.ToString());
            }
        }

        private string columnOrdinal;

        public string ColumnOrdinal
        {
            get => columnOrdinal;
            set
            {
                columnOrdinal = value;
                _controller.Update(id, TaskColumnOrdianlName, value);
            }
        }

        private string assignee;

        public string Assignee
        {
            get => assignee;
            set
            {
                assignee = value;
                _controller.Update(id, TaskColumnOrdianlName, value);
            }
        }

        public TaskDTO(int id, string title, string description, int boardId, DateTime creationTime, DateTime dueDate,
            string columnOrdinal, string assignee) : base(new TaskDalController())
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.boardId = boardId;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.columnOrdinal = columnOrdinal;
            this.assignee = assignee;
        }
    }
}