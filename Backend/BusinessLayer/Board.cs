using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Board
    {
        public const int UNLIMITED = -1;

        private string name;
        public string Name
        {
            get { return name; }
        }

        private readonly int id;
        public int Id
        {
            get { return id; }
        }

        private int owner;
        public int Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private HashSet<string> memeberList; // each board holds its members
        public HashSet<string> MemeberList
        {
            get { return memeberList; }
        }

        public int LimitBacklog { get; set; }
        public int limitInProgress { get; set; }
        public int LimitDone { get; set; }

        public Dictionary<string, List<Task>> columns { get; } // dictionary <column name, tasks list>
        public readonly Dictionary<int, string> columnsId = new Dictionary<int, string> // dictionary< columnsId, ColumnsTitle>
        {
            { 0, "backlog" },
            { 1, "in progress" },
            { 2, "done" }
        };

        public Board(String boardName, int id)
        {
            this.name = boardName;
            this.id = id;
            this.owner = id;
            memeberList = new HashSet<string>();

            columns = new Dictionary<string, List<Task>>();
            initColumns();
            
            this.LimitBacklog = UNLIMITED;
            this.limitInProgress = UNLIMITED;
            this.LimitDone = UNLIMITED;

        }

        public void initColumns()
        {
            columns.Add("backlog", new List<Task>());
            columns.Add("in progress", new List<Task>());
            columns.Add("done", new List<Task>());
        }


        public string getColumnName(int id)
        {
            return columnsId[id];
        }

        public int getColumnLimit(int columnId)
        {
            if (columnId == getColumnNumber("backlog"))
            {
                return LimitBacklog;
            }

            else if (columnId == getColumnNumber("in progress"))
            {
                return limitInProgress;
            }
            else
            {
                return LimitDone;
            }
        }

        public void setColumnLimit(int columnId, int newLimit)
        {
            if (columnId == getColumnNumber("backlog"))
            {
                LimitBacklog = newLimit;
            }

            if (columnId == getColumnNumber("in progress"))
            {
                limitInProgress = newLimit;
            }

            if (columnId == getColumnNumber("done"))
            {
                LimitDone = newLimit;
            }
            
        }

        public List<Task> getColumn(int id)
        {
            if (id > 2 || id < 0)
            {
                throw new Exception("Id out of bounds");
            }

            return columns[columnsId[id]];
        }


        public bool isColumnFull(int colID)
        {
            return getColumn(colID).Count == getColumnLimit(colID);
        }

        public Task findTaskById(int id, int columnOrdinal)
        {
            List<Task> currentTaskList = getColumn(columnOrdinal);
            foreach (Task currentTask in currentTaskList)
            {
                if (currentTask.Id == id)
                {
                    return currentTask;
                }
            }

            throw new Exception("This Task Does not exist in this column");
        }

        public int getColumnNumber(string columnName)
        {
            if (columnName.Equals("backlog"))
            {
                return 0;
            }

            if (columnName.Equals("in progress"))
            {
                return 1;
            }

            if (columnName.Equals("done"))
            {
                return 2;
            }

            else
            {
                throw new Exception(columnName + "is invalid");
            }
        }

    }
}