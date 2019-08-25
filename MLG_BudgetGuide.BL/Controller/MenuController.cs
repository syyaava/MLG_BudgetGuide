using MLG_BudgetGuide.BL.Model;
using System;


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

            SecondaryMainMenu(userController.CurrentUser, incomeController, expenseController);


            Console.WriteLine(userController.CurrentUser.Name);
            Console.ReadLine();

        }

        public void SecondaryMainMenu(User currentUser, IncomeController incomeController, ExpenseController expenseController)
        {
            var flag = true;
            while(flag)
            {
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
                        TotalIncomeExpense(currentUser, incomeController, expenseController);
                        break;

                    case 0:
                        flag = false;
                        break;
                }
            }
        }

        public void ListCommands(User currentUser)
        {
            Console.WriteLine("Текущий пользователь - " + currentUser.Name);
            Console.WriteLine("1 - Сводка о доходах и расходах.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
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

            Console.WriteLine("Нажмите \"Enter\" чтобы выйти в меню.");
            Console.ReadLine();
        }
    }
}
