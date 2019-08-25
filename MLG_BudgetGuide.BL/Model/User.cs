using System;


namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class User
    {
        #region Свойства
        /// <summary>
        /// Имя польщователя.
        /// </summary>
        public string Name { get; }

        public Income Income { get; }

        public Expense Expense { get; }


        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; set; }
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
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
