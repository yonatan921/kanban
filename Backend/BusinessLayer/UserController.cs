using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserController
    {
        private Dictionary<string, User> usersByName;
        private Dictionary<int, User> usersById;

        private int usersIdCount;

        private UserDalController userDalController;

        public UserController()
        {
            userDalController = new UserDalController();
            usersByName = new Dictionary<string, User>();
            usersById = new Dictionary<int, User>();
            usersIdCount = (int)userDalController.getSeq() + 1;
        }


        /// <summary>
        ///  This method creates a new user.
        /// </summary>
        /// <param name="email">The email address of the new user</param>
        /// <param name="password">The password of the new user</param>
        /// <returns>User object of the new user, unless an error occurs</returns>
        public User createUser(string email, string password)
        {
            if (!emailValidity(email))
            {
                throw new Exception("email: " + email + "is not a valid email");
            }

            if (userExists(email))
                throw new Exception("User with email: " + email + " already exists.");

            if (!passwordValidity(password))
                throw new Exception(
                    "Invalid password. Length should be between 6 to 20 characters, and should contain at least one Uppercase letter, one Lowercase letter and one number.");

            //if all the criteria above met- create new User object
            bool successInsert = userDalController.Insert(new UserDTO(usersIdCount, email, password));
            if (!successInsert)
            {
                throw new Exception("Problem occurred to add user: " + email + "  to DataBase");
            }

            User newUser = new User(email, password, usersIdCount);
            usersByName.Add(email, newUser);
            usersById.Add(usersIdCount, newUser);
            usersIdCount++;
            newUser.IsLoggedIn = true; //setting automatically the user is logged in

            return newUser;
        }

        /// <summary>
        ///  This method checks validity of a password.
        /// </summary>
        /// <param name="password">The password to check its' validity</param>
        /// <returns>True if the password is valid, false otherwise</returns>
        private bool passwordValidity(string password)
        {
            bool upperCap = false;
            bool lowerCap = false;
            bool num = false;

            if (password == null || (password.Length < 6 | password.Length > 20))
                return false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    upperCap = true;
                if (char.IsLower(c))
                    lowerCap = true;
                if (char.IsNumber(c))
                    num = true;
            }

            return upperCap && lowerCap && num;
        }

        /// <summary>
        ///  This method checks validity of an email.
        /// </summary>
        /// <param name="email">The email to check its' validity</param>
        /// <returns>True if the email is valid, false otherwise</returns>
        private bool emailValidity(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Please insert an input");
            }


            //regex for valid email
            bool regexValid = Regex.IsMatch(email, @"^\w+([.-]?\w+)@\w+([.-]?\w+)(.\w{2,3})+$");

            return regexValid;
        }

        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>User object of logged in user, unless an error occurs</returns>
        public User login(string email, string password)
        {
            if (!emailValidity(email))
            {
                throw new Exception("email: " + email + "is not a valid email");
            }

            // email = email.ToLower();
            if (!userExists(email))
                throw new Exception("Attempt to log in to account with email: " + email + " that doesn't exist!");

            User user = usersByName[email];

            if (!user.isPassword(password))
                throw new Exception("Attempt to log in to " + email + " with wrong password!");

            if (user.IsLoggedIn)
                throw new Exception("User already logged in"); //TODO: check with the guys about this one

            user.IsLoggedIn = true;

            return user;
        }

        /// <summary>
        ///  This method checks existence of a user.
        /// </summary>
        /// <param name="email">The email to check existence of a user with</param>
        /// <returns>True if user exists, false otherwise</returns>
        public bool userExists(string email)
        {
            return usersByName.ContainsKey(email);
        }

        /// <summary>
        ///  This method checks if an account is logged in.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>True if the user is logged in, false otherwise</returns>
        public void isLoggedIn(string email) //function in order to have access from the outside
        {
            if (!userExists(email))
                throw new Exception("An account with " + email + " doesn't even exist!");
            if (!usersByName[email].IsLoggedIn)
            {
                throw new Exception(email + " isn't Logged in");
            }
        }

        /// <summary>
        ///  This method logs out of a logged in user.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns></returns>
        public void logout(string email)
        {
            if (!emailValidity(email))
            {
                throw new Exception(email + " is invalid");
            }

            // email = email.ToLower(); //TODO: check if case-sensitivity is needed
            isLoggedIn(email);
            usersByName[email].IsLoggedIn = false;
        }

        /// <summary>
        ///  This method returns a user if its' logged in.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>User</returns>
        public User getUserAndLogeddin(string email)
        {
            isLoggedIn(email);
            return usersByName[email];
        }

        /// <summary>
        ///  This method returns a user.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>User</returns>
        public User getUser(string email)
        {
            if (!userExists(email))
                throw new Exception("An account with " + email + " doesn't even exist!");
            return usersByName[email];
        }

        /// <summary>
        ///  This method returns a user.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>User</returns>
        public User getUser(int id)
        {
            if (!usersById.ContainsKey(id))
                throw new Exception("An account with " + id + " does not exist!");
            return usersById[id];
        }

        /// <summary>
        ///  This method loads data related to users table from db.
        /// </summary>
        /// <returns>User</returns>
        public void loadData()
        {
            Dictionary<Dictionary<string, User>, Dictionary<int, User>> returnValue =
                DataUtilities.loadData(userDalController);
            usersByName = returnValue.Keys.First();
            usersById = returnValue.Values.First();
        }

        /// <summary>
        ///  This method returnes list of user's boards.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>list of board ids</returns>
        public List<int> getUserBoards(string email)
        {
            User user = getUserAndLogeddin(email);
            List<int> userBoardsIds = user.getBoardListById().Keys.ToList();
            return userBoardsIds;
        }


        /// <summary>
        ///  This method returnes list of user's boards names.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>list of board ids</returns>
        public List<string> getUserBoardsNames(string email)
        {
            User user = getUserAndLogeddin(email);
            List<string> userBoardsIds = user.getBoardListByName().Keys.ToList();
            return userBoardsIds;
        }

        /// <summary>
        ///  This method deletes the data related to users table in db.
        /// </summary>
        /// <returns>User</returns>
        public void resetData()
        {
            userDalController.resetTable();
            usersByName.Clear();
            usersById.Clear();
            usersIdCount = DataUtilities.EMPTYSEQ;
        }
    }
}