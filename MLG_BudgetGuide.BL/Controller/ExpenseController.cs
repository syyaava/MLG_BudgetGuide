using MLG_BudgetGuide.BL.Model;
using System;
using System.Linq;

namespace MLG_BudgetGuide.BL.Controller
{
    [Serializable]
    public class ExpenseController : BasedController
    {
        private string FILE_USERS_DATA = "users.dat";

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
        public void SetExpenseOfType(UserController userController)
        {
            var flag = true;
            while(flag)
            {
                Console.Clear();
                OutputTypesExpense();
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    var expense = 0;

                    switch(result)
                    {
                        case 0:
                            flag = false;
                            break;

                        case 1:
                            AddType(userController);
                            break;

                        case 2:
                            DeleteType(userController);
                            break;

                        default:
                            expense = InputExpense();
                            CurrentUser.Expense.CurrentMonthExpenses += expense;
                            CurrentUser.Expense.TotalExpense += expense;
                            CurrentUser.Expense.TypesExpense[result - 3].ExpensesAmount += expense;
                            userController.Save();
                            break;
                    }
                }
                else
                {
                    IncorrectInput();
                    OutputTypesExpense();
                }
            }
        }

        /// <summary>
        /// Удалить тип.
        /// </summary>
        /// <param name="userController"></param>
        private void DeleteType(UserController userController)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Введите номер типа, который хотите удалить:");
                for (var i = 0; i < CurrentUser.Expense.TypesExpense.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - Расходы типа \"{CurrentUser.Expense.TypesExpense[i].Name}\"");
                }
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input < CurrentUser.Expense.TypesExpense.Count && input > 0)
                    {
                        CurrentUser.Expense.TypesExpense.RemoveAt(input - 1);
                        break;
                    }
                    else
                    {
                        IncorrectInput();
                    }
                }
                else
                {
                    IncorrectInput();
                }
                userController.Save();
            }
        }

        private static void IncorrectInput()
        {
            Console.Clear();
            Console.WriteLine("Некорректный ввод. Попробуйте еще раз.\n");
        }

        /// <summary>
        /// Добавление типа.
        /// </summary>
        /// <param name="userController"></param>
        private void AddType(UserController userController)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите название типа расхода.");
                var name = Console.ReadLine();
                Console.Clear();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    CurrentUser.Expense.TypesExpense.Add(new TypeOfExpense(name));
                    Console.WriteLine("Тип успешно добавлен.");
                    userController.Save();
                    break;
                }
                else
                {
                    Console.WriteLine("Название типа не может быть пустым. Введите ");
                }
            }
        }

        /// <summary>
        /// Ввод размера расхода.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Вывод типов расходов.
        /// </summary>
        public void OutputTypesExpense()
        {
            Console.WriteLine("Выберите к какому типу относятся ваши расходы:");
            Console.WriteLine("0 - Выход.");
            Console.WriteLine("1 - Добавить тип расходов.");
            Console.WriteLine("2 - Удалить тип расходов.");
            for(var i = 0; i<CurrentUser.Expense.TypesExpense.Count; i++)
            {
                Console.WriteLine($"{i + 3} - Расходы типа \"{CurrentUser.Expense.TypesExpense[i].Name}\"");
            }
            
        }

    }
}
