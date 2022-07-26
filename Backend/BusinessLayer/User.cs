using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class User
    {
        private readonly int id;
        public int Id
        {
            get { return id; }
        }
        public string email;
        private string password;
        private bool isLoggedIn;
        public bool IsLoggedIn { get; set; }
        private Dictionary<string, Board> boardListByName; // Dict <boardName , Board Object>
        private Dictionary<int, Board> boardListById; // Dict < board Id , BoardObject>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //when we create user, we assume that all the fields are valid
        public User(string email, string password, int id)
        {
            this.id = id; //consider dropping id for user (ONLY)
            this.email = email;
            this.password = password;
            isLoggedIn = false;
            boardListByName = new Dictionary<string, Board>();
            boardListById = new Dictionary<int, Board>();
        }

        public bool isPassword(string possiblePassword)
        {
            if (possiblePassword.Equals(password))
                return true;
            return false;
        }

        public Dictionary<string, Board> getBoardListByName()
        {
            return boardListByName;
        }

        public Dictionary<int, Board> getBoardListById()
        {
            return boardListById;
        }

        public Board hasBoardByName(string boardName)
        {
            if (boardListByName.ContainsKey(boardName))
            {
                return boardListByName[boardName];
            }
            else
            {
                throw new Exception("The user doesn't own board with given name");
            }
        }

        public Board hasBoardById(int boardId)
        {
            if (boardListById.ContainsKey(boardId))
            {
                return boardListById[boardId];
            }
            else
            {
                throw new Exception("The user doesn't own board with given id");
            }
        }
    }
}