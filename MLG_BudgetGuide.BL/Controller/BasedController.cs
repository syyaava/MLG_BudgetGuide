using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace MLG_BudgetGuide.BL.Controller
{
    class BasedController
    {
        protected void SaveData<T>(string fileName, T item)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }

        protected List<T> LoadData<T>(string fileName)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0 && formatter.Deserialize(fs) is List<T> user) //TODO: ошибка (попытка десериализации пустого потока)
                {
                    return user;
                }
                else
                {
                    return new List<T>();
                }
            }
        }
    }
}
