using System;
using System.Collections.Generic;
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

        public TypeOfExpense(string name)
        {
            Name = name;
            ExpensesAmount = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
