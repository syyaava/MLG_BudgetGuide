﻿using MLG_BudgetGuide.BL.Model;
using System;
using System.Linq;

namespace MLG_BudgetGuide.BL.Controller
{
    [Serializable]
    public class IncomeController : BasedController
    {
        private User CurrentUser { get; set; }

        public IncomeController(User currentUser)
        {
            if (!(currentUser is User))
            {
                throw new ArgumentException("Неверный формат пользователя", nameof(currentUser));
            }
            CurrentUser = currentUser;
        }

        /// <summary>
        /// Увеличение суммарного дохода.
        /// </summary>
        /// <param name="income">Доход.</param>
        public void AddIncome(int income)
        {
            if (income <= 0)
            {
                throw new ArgumentException("Доход не может быть меньше, либо равен 0.", nameof(income));
            }

            CurrentUser.Income.TotalIncome += income;
            CurrentUser.Income.CurrentMonthIncome += income;
            AddInHistoryIncome(income);

        }

        /// <summary>
        /// Получить средний доход в месяц.
        /// </summary>
        /// <returns></returns>
        public long GetAverageMonthlyIncome()
        {
            return GetAverageMonthlyResult(CurrentUser, CurrentUser.Income.TotalIncome);
        }

        /// <summary>
        /// Добавить доход в историю.
        /// </summary>
        /// <param name="income">Размер дохода.</param>
        private void AddInHistoryIncome(long income)
        {
            AddInHistory(CurrentUser.Income.History, income);
        }
    }
}
