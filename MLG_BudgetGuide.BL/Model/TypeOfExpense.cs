using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class TypeOfExpense
    {
        /// <summary>
        /// Название типа расхода.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Размер расхода.
        /// </summary>
        public long ExpensesAmount { get; set; }

        /// <summary>
        /// Процент данного расхода от общего.
        /// </summary>
        public int Percent { get; set; }

        /// <summary>
        /// Создать тип расхода без процентного соотношения.
        /// </summary>
        /// <param name="name"></param>
        public TypeOfExpense(string name)
        {
            Name = name;
            ExpensesAmount = 0;
        }

        /// <summary>
        /// Создать тип расхода с процентным соотношением.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="percent"></param>
        public TypeOfExpense(string name, int percent)
        {
            Name = name;
            Percent = percent;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
