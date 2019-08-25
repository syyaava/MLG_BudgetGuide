using MLG_BudgetGuide.BL.Model;
using System;


namespace MLG_BudgetGuide.BL.Controller
{
    [Serializable]
    public class ExpenseController
    {
        private User CurrentUser { get; set; }

        public ExpenseController(User currentUser)
        {
            if (!(currentUser is User))
            {
                throw new ArgumentException("Неверный формат пользователя", nameof(currentUser));
            }
            CurrentUser = currentUser;
        }

        /// <summary>
        /// Увеличение суммарного расхода.
        /// </summary>
        /// <param name="income">Расход.</param>
        public void AddExpense(int expense)
        {
            if (expense <= 0)
            {
                throw new ArgumentException("Расход не может быть меньше, либо равен 0.", nameof(expense));
            }

            CurrentUser.Income.TotalIncome += expense;
        }

        /// <summary>
        /// Получить средний расход в месяц.
        /// </summary>
        /// <returns></returns>
        public long GetAverageMonthlyExpense()
        {
            var currentDate = DateTime.Now;
            TimeSpan span = currentDate - CurrentUser.RegistrationDate;
            return (long)Math.Abs(CurrentUser.Expense.TotalExpense / (span.TotalDays * 30));
        }
    }
}
