using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class User
    {
        #region Свойства

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Доходы.
        /// </summary>
        public Income Income { get; set; }

        /// <summary>
        /// Расходы.
        /// </summary>
        public Expense Expense { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        public List<DateTime> Months { get; set; }
        #endregion

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public User(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым", nameof(name));
            }
            Name = name;
            Expense = new Expense();
            Income = new Income();
            RegistrationDate = DateTime.Now;
            Months = new List<DateTime>();
        }

        public User()
        {
            Expense = new Expense();
            Income = new Income();
            RegistrationDate = DateTime.Now;
            Months = new List<DateTime>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
