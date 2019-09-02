using System;


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
        /// Расходы за текущий месяц.
        /// </summary>
        public long CurrentMonthExpenses { get; set; } = 0;

        /// <summary>
        /// Расходы на еду.
        /// </summary>
        public long FoodExpense { get; set; } = 0;

        /// <summary>
        /// Расходы на медицину.
        /// </summary>
        public long MedicamentExpense { get; set; } = 0;

        /// <summary>
        /// Расходы на досуг.
        /// </summary>
        public long EntertamentExpense { get; set; } = 0;

        /// <summary>
        /// Расходы на транспорт.
        /// </summary>
        public long TransportExpense { get; set; } = 0;

        /// <summary>
        /// Расходы на кредит.
        /// </summary>
        public long CreditExpense { get; set; } = 0;

        /// <summary>
        /// Расходы на остальное.
        /// </summary>
        public long OtherExpense { get; set; } = 0;


        public Expense()
        {

        }

        public override string ToString()
        {
            return TotalExpense.ToString();
        }
    }
}
