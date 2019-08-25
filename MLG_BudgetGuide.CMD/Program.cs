using System;
using MLG_BudgetGuide.BL.Model;
using MLG_BudgetGuide.BL.Controller;


namespace MLG_BudgetGuide.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuController = new MenuController();
            menuController.MainMenu();
        }
    }
}
