﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLG_BudgetGuide.BL.Model
{
    [Serializable]
    public class Income
    {
        /// <summary>
        /// Суммарный доход за все время.
        /// </summary>
        public long TotalIncome { get; internal set; } = 0;

        /// <summary>
        /// История доходов.
        /// </summary>
        public List<Note> History { get; }

        /// <summary>
        /// Размер дохода за текущий месяц.
        /// </summary>
        public long CurrentMonthIncome { get; set; } = 0;

        public Income()
        {
            History = new List<Note>();
        }


        public override string ToString()
        {
            return TotalIncome.ToString();
        }
    }
}
