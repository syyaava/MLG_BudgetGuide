using MLG_BudgetGuide.BL.Model;
using System;
using System.Linq;

namespace MLG_BudgetGuide.BL.Controller
{
    public class MenuController
    {
        public MenuController()
        {
            
        }

        public void MainMenu()
        {
            Console.WriteLine("Добро пожаловать в помощника по управлению доходами.");
            string name;
            while(true)
            {
                Console.Write("Введите ваше имя: ");
                name = Console.ReadLine();
                if (name == null)
                {
                    Console.WriteLine("Имя не может быть пустым. \n Попробуйте еще раз:");
                }
                else
                {
                    break;
                }
            }

            var userController = new UserController(name);
            var incomeController = new IncomeController(userController.CurrentUser);
            var expenseController = new ExpenseController(userController.CurrentUser);

            SecondaryMainMenu(userController.CurrentUser, incomeController, expenseController, userController);


            Console.WriteLine("До свидания " + userController.CurrentUser.Name);
            Console.WriteLine("Чтобы закрыть приложение нажмите \"Enter\".");
            Console.ReadLine();

        }

        public void SecondaryMainMenu(User currentUser, 
                                      IncomeController incomeController, 
                                      ExpenseController expenseController, 
                                      UserController userController)
        {
            var flag = true;
            while(flag)
            {
                ResetMonthlyExpenseAndIncome(currentUser);
                Console.Clear();
                ListCommands(currentUser);
                int input;
                if(int.TryParse(Console.ReadLine(), out int result))
                {
                    input = result;
                }
                else
                {
                    continue;
                }
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        TotalIncomeExpense(currentUser, incomeController, expenseController);
                        userController.Save();
                        break;

                    case 2:
                        Console.Clear();
                        expenseController.GetEveryDayExpense();
                        userController.Save();
                        break;

                    case 3:
                        Console.Clear();
                        int income;
                        while(true)
                        {
                            Console.WriteLine("Введите размер дохода.");
                            if(int.TryParse(Console.ReadLine(), out int resultIncome))
                            {
                                income = resultIncome;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод");
                            }
                        }
                        incomeController.AddIncome(income);
                        userController.Save();
                        break;

                    case 4:
                        Console.Clear();
                        expenseController.SetExpenseOfType(userController);
                        userController.Save();
                        break;

                    case 0:
                        flag = false;
                        userController.Save();
                        break;

                    default:
                        break;
                }
            }
        }

        public void ListCommands(User currentUser)
        {
            Console.WriteLine("Текущий пользователь - " + currentUser.Name);
            Console.WriteLine("1 - Сводка о доходах и расходах.");
            Console.WriteLine("2 - Рассчитать ежедневные расходы.");
            Console.WriteLine("3 - Ввести доход.");
            Console.WriteLine("4 - Ввести расход.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("0 - Выход.");
        }

        public void TotalIncomeExpense(User currentUser, IncomeController incomeController, ExpenseController expenseController)
        {
            Console.Clear();
            Console.WriteLine($"Суммарных доход за все время - {currentUser.Income.TotalIncome}");
            Console.WriteLine($"Суммарных расход за все время - {currentUser.Expense.TotalExpense}");
            Console.WriteLine($"Средний ежемесячный доход - {incomeController.GetAverageMonthlyIncome()}");
            Console.WriteLine($"Средний ежемесячный расход - {expenseController.GetAverageMonthlyExpense()}");
            Console.WriteLine($"Остаток от дохода за этот месяц - {currentUser.Income.CurrentMonthIncome - currentUser.Expense.CurrentMonthExpenses}");
            Console.WriteLine();
            for(var i = 0; i < currentUser.Expense.TypesExpense.Count; i++)
            {
                Console.WriteLine($"Расходы типа \"{currentUser.Expense.TypesExpense[i]}\" - {currentUser.Expense.TypesExpense[i].ExpensesAmount}");
            }
            Console.WriteLine("Нажмите \"Enter\" чтобы выйти в меню.");
            Console.ReadLine();
        }


        //Очень кривая реализация.
        /// <summary>
        /// Обнуление ежемесячных дохода и расхода при смене месяца.
        /// </summary>
        public void ResetMonthlyExpenseAndIncome(User currentUser)
        {
            if (currentUser.Months.Count != 0)
            {
                if (currentUser.Months.Last().Month != DateTime.Now.Month)
                {
                    currentUser.Months.Add(DateTime.Now);
                    currentUser.Income.CurrentMonthIncome = 0;
                    currentUser.Expense.CurrentMonthExpenses = 0;
                }
            }
            else
            {
                currentUser.Months.Add(DateTime.Now);
            }
        }
    }
}
