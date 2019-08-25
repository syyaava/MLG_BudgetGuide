using MLG_BudgetGuide.BL.Model;
using System;


namespace MLG_BudgetGuide.BL.Controller
{
    [Serializable]
    public class ExpenseController : BasedController
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

            CurrentUser.Expense.TotalExpense += expense; 
        }

        /// <summary>
        /// Получить средний расход в месяц.
        /// </summary>
        /// <returns></returns>
        public long GetAverageMonthlyExpense()
        {
            return GetAverageMonthlyResult(CurrentUser, CurrentUser.Expense.TotalExpense);
        }

        /// <summary>
        /// Распределение расходов на каждый день.
        /// </summary>
        public void GetEveryDayExpense()
        {
            var flag = true;
            while (flag)
            {
                Console.WriteLine("Введите сумму, которую нужно распределить.");
                int sum;
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    sum = result;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Нажмите \"Enter\" чтобы выйти в меню.");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Введите количество дней, на которое будет распределена сумма.");
                int day;
                if (int.TryParse(Console.ReadLine(), out int resultday))
                {
                    day = resultday;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Нажмите \"Enter\" чтобы выйти в меню.");
                    Console.ReadLine();
                    break;
                }
                Console.Clear();
                Console.WriteLine($"Неприкосновенный запас: {0.2 * sum} \nЕжедневные расходы:");
                Console.WriteLine($"Затраты на питание - {0.6 * sum / day} \nОстальные траты - {0.2 * sum / day}");

                Console.WriteLine("Нажмите \"Enter\" чтобы выйти в меню.");
                Console.ReadLine();
                flag = false;
            }
        }
    }
}
