using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        /// Список типов расходов в калькуляторе.
        /// </summary>
        public List<TypeOfExpense> CalculatorTypeExpense { get; }

        /// <summary>
        /// Расходы за текущий месяц.
        /// </summary>
        public long CurrentMonthExpenses { get; set; } = 0;

        public Expense()
        {
            TypesExpense = new List<TypeOfExpense>();
            DefaultTypes();
            CalculatorTypeExpense = new List<TypeOfExpense>();
            CalculatorDefaultTypes();
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

        private void CalculatorDefaultTypes()
        {
            CalculatorTypeExpense.Add(new TypeOfExpense("Неприкосновенный запас", 20));
            CalculatorTypeExpense.Add(new TypeOfExpense("Питание", 60));
            CalculatorTypeExpense.Add(new TypeOfExpense("Остальное", 20));
        }

        public override string ToString()
        {
            return TotalExpense.ToString();
        }
    }
}
