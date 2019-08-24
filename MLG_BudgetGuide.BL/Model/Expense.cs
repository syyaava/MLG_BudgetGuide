using System;


namespace MLG_BudgetGuide.BL.Model
{
    class Expense
    {
        /// <summary>
        /// Суммарный расход.
        /// </summary>
        public long TotalExpense { get; set; } = 0;


        public Expense()
        {

        }

        public override string ToString()
        {
            return TotalExpense.ToString();
        }
    }
}
