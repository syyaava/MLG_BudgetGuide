using MLG_BudgetGuide.BL.Model;
using System;
using System.Collections.Generic;
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
                throw new Exception("Неверный формат пользователя.");
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
        public void GetEveryDayExpense(UserController userController)
        {
            var flag = true;
            while (flag)
            {
                Console.Clear();
                CalcTypesMenu();

                if (int.TryParse(Console.ReadLine(), out int result))
                {

                    switch (result)
                    {
                        case 0:
                            flag = false;
                            break;

                        case 1:
                            Console.Clear();
                            Console.WriteLine("Введите сумму, которую нужно распределить.");
                            var sum = Input();
                            if (sum == default)
                            {
                                break;
                            }

                            Console.Clear();
                            Console.WriteLine("Распределение суммы размером: " + sum);
                            var day = CalcInputDays();
                            if (day == default)
                            {
                                break;
                            }

                            CalculatedDaylyExpenses(sum, day);
                            Console.ReadLine();
                            break;

                        case 2:
                            ChangePercent();
                            userController.Save();
                            break;

                        case 3:
                            AddType(userController, CurrentUser.Expense.CalculatorTypeExpense, true);
                            userController.Save();
                            break;

                        case 4:
                            DeleteType(userController, CurrentUser.Expense.CalculatorTypeExpense);
                            userController.Save();
                            break;

                        default:

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Показать сумму процентных соотношений.
        /// </summary>
        private void ShowSumPersent()
        {
            var percent = 0;
            foreach(var item in CurrentUser.Expense.CalculatorTypeExpense)
            {
                percent += item.Percent;
            }
            Console.WriteLine("Сумма всех процентных долей = " + percent);
            if(percent < 100 || percent > 100)
            {
                Console.WriteLine("////////////////////////////////////////////////////////////");
                Console.WriteLine("Некорректное поведение калькулятора.\n" +
                    "Измените процентные соотношения так, чтобы их сумма была = 100");
                Console.WriteLine("////////////////////////////////////////////////////////////");
            }
        }

        /// <summary>
        /// Расчёт ежедневных расходов по типам.
        /// </summary>
        /// <param name="sum">Сумма для распределения.</param>
        /// <param name="day">Количество дней на которое идет распределение.</param>
        private void CalculatedDaylyExpenses(int sum, int day)
        {
            Console.Clear();
            Console.WriteLine("Распределение ежедневных расходов на " + day + " дней:");
            foreach(var item in CurrentUser.Expense.CalculatorTypeExpense)
            {
                Console.WriteLine($"{item.Name}({item.Percent}%): {((item.Percent/100.0) * sum) / day}");
            }
            Console.WriteLine("\n\n\nНажмите \"Enter\" чтобы продолжить.");
        }

        /// <summary>
        /// Изменить процентное соотношение типа.
        /// </summary>
        private void ChangePercent()
        {
            while (true)
            {
                Console.Clear();
                ShowSumPersent();
                Console.WriteLine("Изменение процентного соотношения.");

                Console.WriteLine("Выберите пункт для изменения");
                Console.WriteLine("0 - Выход.");
                for (var i = 0; i < CurrentUser.Expense.CalculatorTypeExpense.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {CurrentUser.Expense.CalculatorTypeExpense[i].Name}({CurrentUser.Expense.CalculatorTypeExpense[i].Percent}%).");
                }

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if(result == 0)
                    {
                        break;
                    }
                    else if (result > CurrentUser.Expense.CalculatorTypeExpense.Count || result < 0)
                    {
                        IncorrectInputWithBackToMenu();
                    }
                    else
                    {
                        Console.WriteLine("Введите количество процентов (Пример: 20)");
                        var percent = Input();

                        CurrentUser.Expense.CalculatorTypeExpense[result - 1].Percent = percent;
                    }
                }
                else
                {
                    IncorrectInputWithBackToMenu();
                }
            }
        }

        private static void IncorrectInputWithBackToMenu()
        {
            Console.WriteLine("Некорректный ввод. Нажмите \"Enter\" чтобы выйти в меню.");
            Console.ReadLine();
        }

        /// <summary>
        /// Ввод количества дней для расчета.
        /// </summary>
        /// <returns></returns>
        private int CalcInputDays()
        {
            Console.WriteLine("Введите количество дней, на которое будет распределена сумма.");
            if (int.TryParse(Console.ReadLine(), out int resultday))
            {
                Console.Clear();
                return resultday;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Нажмите \"Enter\" чтобы выйти в меню.");
                Console.ReadLine();
                return default;
            }
        }

        private int Input()
        {
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            else
            {
                IncorrectInputWithBackToMenu();
                return default;
            }
        }

        private void CalcTypesMenu()
        {
            ShowSumPersent();
            Console.WriteLine("Выберите дальнейшее действие:");
            Console.WriteLine("0 - Выход.");
            Console.WriteLine("1 - Рассчитать ежедневный расход.");
            Console.WriteLine("2 - Изменить процентное соотношение типов.");
            Console.WriteLine("3 - Добавить тип расхода.");
            Console.WriteLine("4 - Удалить тип расхода.");
        }

        /// <summary>
        /// Ввод расходов по типам.
        /// </summary>
        public void SetExpenseOfType(UserController userController)
        {
            var flag = true;
            var good = false;
            while(flag)
            {
                Console.Clear();

                if(good)
                {
                    Console.WriteLine("Расходы успешно добавлены.");
                }

                good = false;
                OutputTypesExpense();
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch(result)
                    {
                        case 0:
                            flag = false;
                            break;

                        case 1:
                            AddType(userController, CurrentUser.Expense.TypesExpense, false);
                            break;

                        case 2:
                            DeleteType(userController, CurrentUser.Expense.TypesExpense);
                            break;

                        default:
                            if (result < 0 || result >= CurrentUser.Expense.TypesExpense.Count + 3)
                            {
                                IncorrectInputWithBackToMenu();
                                break;
                            }
                            AddExpense(userController, result);
                            good = true;
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

        private void AddExpense(UserController userController, int result)
        {
            int expense = InputExpense();
            CurrentUser.Expense.CurrentMonthExpenses += expense;
            CurrentUser.Expense.TotalExpense += expense;
            CurrentUser.Expense.TypesExpense[result - 3].ExpensesAmount += expense;
            AddInHistoryExpense(expense);
            userController.Save();
        }

        /// <summary>
        /// Удалить тип.
        /// </summary>
        /// <param name="userController"></param>
        private void DeleteType(UserController userController, List<TypeOfExpense> listOfExpenses)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Введите номер типа, который хотите удалить:");
                Console.WriteLine("0 - Выход.");
                for (var i = 0; i < listOfExpenses.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - Расходы типа \"{listOfExpenses[i].Name}\"");
                }
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                    {
                        break;
                    }
                    if (input <= listOfExpenses.Count && input > 0)
                    {
                        listOfExpenses.RemoveAt(input - 1);
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

        private void IncorrectInput()
        {
            Console.Clear();
            Console.WriteLine("Некорректный ввод. Попробуйте еще раз.\n");
        }

        /// <summary>
        /// Добавление типа расходов.
        /// </summary>
        /// <param name="userController"></param>
        private void AddType(UserController userController, List<TypeOfExpense> listOfExpenses, bool withPersent)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите название типа расхода.");
                var name = Console.ReadLine();
                Console.Clear();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if(withPersent)
                    {
                        listOfExpenses.Add(new TypeOfExpense(name, 0));
                    }
                    else
                    {
                        listOfExpenses.Add(new TypeOfExpense(name));
                    }
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
        private int InputExpense()
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

        /// <summary>
        /// Добавить расход в историю.
        /// </summary>
        /// <param name="expense">Размер расхода.</param>
        private void AddInHistoryExpense(long expense)
        {
            AddInHistory(CurrentUser.Expense.History, expense);
        }
    }
}
