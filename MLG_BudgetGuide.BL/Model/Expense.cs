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
        /// История расходов.
        /// </summary>
        public List<Note> History { get; }

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
            DefaultTypes();
            History = new List<Note>();
        }

        private void DefaultTypes()
        {
            TypesExpense.Add(new TypeOfExpense("Продукты питания"));
            TypesExpense.Add(new TypeOfExpense("Транспорт"));
            TypesExpense.Add(new TypeOfExpense("Коммунальные платежи"));
            TypesExpense.Add(new TypeOfExpense("Кредит"));
            TypesExpense.Add(new TypeOfExpense("Досуг"));
        }

        public override string ToString()
        {
            return TotalExpense.ToString();
        }
    }
}
