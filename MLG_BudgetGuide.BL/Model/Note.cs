using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class Note
    {
        /// <summary>
        /// Дата расхода.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Размер расходов в данный день.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Создать новую записку в истории.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="amount">Размер дохода/расхода.</param>
        public Note(DateTime date, long amount = 0)
        {
            Date = date;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Amount}";
        }
    }
}
