using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using log4net;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Task
    {
        public const string UNASSIGNED = "unassigned";

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string assignie;
        public string Assignie
        {
            get { return assignie; }
            set { assignie = value; }
        }


        private readonly DateTime creationTime;
        public DateTime CreationTime
        {
            get { return creationTime; }
        }

        public string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        
        private DateTime dueDate;
        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        [JsonIgnore]
        public string columnOrdinal { get; set; }

        [JsonIgnore]
        public int boardId { get; set; }

        /// <summary>
        ///  constructor
        /// </summary>
        /// <param name="dueDate">The dueDate of the task</param>
        /// <param name="title">The title of the task</param>
        /// <param name="description">The description of the task</param>
        /// <param name="boardId">The id of the board that the task is in</param>
        /// <param name="id">The id of the task</param>
        /// <returns> creates a new task >
        // public Task(string title, string description, DateTime dueDate, int id,int boardId)
        // {
        //     this.creationTime = DateTime.Now;
        //     this.title = title;
        //     this.description = description;
        //     this.dueDate = dueDate;
        //     this.id = id;
        //     this.columnOrdinal = "backlog";
        //     this.boardId = boardId;
        //     this.assignie = "unassigned";
        // }

        public Task(string title, string description, DateTime dueDate, int id, int boardId, string columnOrdinal, string assignie)
        {
            this.creationTime = DateTime.Now;
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.id = id;
            this.columnOrdinal = "backlog";
            this.boardId = boardId;
            // this.assignie = "unassigned";
            this.Assignie = assignie;
            this.columnOrdinal = columnOrdinal;
        }

    }
}