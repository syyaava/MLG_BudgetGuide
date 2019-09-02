using System;


namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class Income
    {
        /// <summary>
        /// Суммарный доход за все время.
        /// </summary>
        public long TotalIncome { get; internal set; } = 0;

        public long CurrentMonthIncome { get; set; } = 0;

        public Income()
        {

        }


        public override string ToString()
        {
            return TotalIncome.ToString();
        }
    }
}
