﻿using MLG_BudgetGuide.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace MLG_BudgetGuide.BL.Controller
{
    public class BasedController
    {
        /// <summary>
        /// Сериализовать данные.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="item">Предмет сериализации.</param>
        protected void SaveData<T>(string fileName, T item)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }

        /// <summary>
        /// Десериализовать данные.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <returns></returns>
        protected List<T> LoadData<T>(string fileName)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0 && formatter.Deserialize(fs) is List<T> user) 
                {
                    return user;
                }
                else
                {
                    return new List<T>();
                }
            }
        }

        /// <summary>
        /// Получить средний ежемесячный результат.
        /// </summary>
        /// <param name="currentUser">Текущий пользователь.</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        protected long GetAverageMonthlyResult(User currentUser, long amount)
        {
            var currentDate = DateTime.Now;
            TimeSpan span = currentDate - currentUser.RegistrationDate;
            if (Math.Round(span.TotalDays) == 0)
            {
                return amount;
            }
            else
            {
                return (long)(amount / Math.Round(span.TotalDays) * 30);
            }
        }
    }
}
