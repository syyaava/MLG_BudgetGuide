using MLG_BudgetGuide.BL.Model;
using System;
using System.Linq;

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
        /// Ввод величины расхода.
        /// </summary>
        /// <param name="income">Расход.</param>
        public void AddExpense(int expense)
        {
            if (expense <= 0)
            {
                throw new ArgumentException("Расход не может быть меньше, либо равен 0.", nameof(expense));
            }

            //TODO: ввод вида затрат.
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

        /// <summary>
        /// Ввод расходов по типам.
        /// </summary>
        public void SetExpenseOfType()
        {
            var flag = true;
            while(flag)
            {
                Console.Clear();
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    var expense = InputExpense();
                    switch(result)
                    {
                        case 1:
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.FoodExpense += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            break;

                        case 2:
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.MedicamentExpense += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            break;

                        case 3:
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.EntertamentExpense += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            break;

                        case 4:
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.TransportExpense += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            break;

                        case 5:
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.CreditExpense += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            break;

                        case 6:
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.OtherExpense += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            break;

                        case 0:
                            flag = false;
                            break;

                        default:
                            break;  
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.\n");
                    OutputTypesExpense();
                }
            }
        }

        private static int InputExpense()
        {
            while (true)
            {
                Console.WriteLine("Введите размер расхода.");
                if (int.TryParse(Console.ReadLine(), out int expense))
                {
                    if(expense >= 0)
                    {
                        return expense;
                    }
                    else
                    {
                        Console.WriteLine("Размер расхода не может быть меньше 0");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                }
            }

            
        }

        public void OutputTypesExpense()
        {
            Console.WriteLine("Выберите к какому типу относятся ваши расходы:");
            Console.WriteLine("0 - Выход.");
            Console.WriteLine("1 - Расходы на еду.");
            Console.WriteLine("2 - Расходы на лекарства/медицину.");
            Console.WriteLine("3 - Расходы на досуг.");
            Console.WriteLine("4 - Расходы на транспорт.");
            Console.WriteLine("5 - Расходы на кредит.");
            Console.WriteLine("6 - Расходы на остальное.");
        }

    }
}
