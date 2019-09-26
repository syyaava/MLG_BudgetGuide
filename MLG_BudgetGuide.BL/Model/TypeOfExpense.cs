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
        public string Name { get; }

        public long ExpensesAmount { get; set; }

        public int Percent { get; set; }

        public TypeOfExpense(string name)
        {
            Name = name;
            ExpensesAmount = 0;
        }

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
