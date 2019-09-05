using System;
using System.Collections.Generic;

namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class Expense
    {
        /// <summary>
        /// Суммарный расход.
        /// </summary>
        public long TotalExpense { get; set; } = 0;


        /// <summary>
        /// Список типов расходов.
        /// </summary>
        public List<TypeOfExpense> TypesExpense { get; }

        /// <summary>
        /// Расходы за текущий месяц.
        /// </summary>
        public long CurrentMonthExpenses { get; set; } = 0;

        public Expense()
        {
            TypesExpense = new List<TypeOfExpense>();
        }

        public override string ToString()
        {
            return TotalExpense.ToString();
        }
    }
}
