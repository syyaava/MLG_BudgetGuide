using MLG_BudgetGuide.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MLG_BudgetGuide.BL.Controller
{
    class UserController
    {
        /// <summary>
        /// Пользователь.
        /// </summary>
        public List<User> Users { get; set; }

        private User CurrentUser { get; set; }

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

            CurrentUser = Users.SingleOrDefault(u => u.Name == name);

            if(CurrentUser == null)
            {
                CurrentUser = new User(name);
                Users.Add(CurrentUser);
                Save();
            }
        }



        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }

        /// <summary>
        /// Загрузить данные пользователя.
        /// </summary>
        /// <returns>Пользователь.</returns>
        public User Load()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                 if(formatter.Deserialize(fs) is User user)
                 {
                    return user;
                 }
                 else
                 {
                    throw new FileLoadException("Не удалось загрузить данные из файла.", "users.dat");
                 }
            }
        }
    }
}
