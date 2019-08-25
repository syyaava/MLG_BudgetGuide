using MLG_BudgetGuide.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MLG_BudgetGuide.BL.Controller
{
    [Serializable]
    class UserController : BasedController
    {
        /// <summary>
        /// Пользователь.
        /// </summary>
        public List<User> Users { get; set; }

        private string FILE_USERS_DATA = "users.dat";

        public User CurrentUser { get; }

        /// <summary>
        /// Создание пользователя приложения.
        /// </summary>
        /// <param name="user"></param>
        public UserController(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым", nameof(name));
            }

            Users = GetUsersData();
            
            CurrentUser = Users.SingleOrDefault(u => u.Name == name);

            if(CurrentUser == null)
            {
                CurrentUser = new User(name);
                Users.Add(CurrentUser);
                Save();
            }
        }

        /// <summary>
        /// Загрузить список пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        private List<User> GetUsersData()
        {
            return LoadData<User>(FILE_USERS_DATA) ?? new List<User>();
        }

        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save()
        {
            SaveData(FILE_USERS_DATA, Users);
        }

    }
}
