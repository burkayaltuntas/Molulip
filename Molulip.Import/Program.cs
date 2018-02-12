using Molulip.Import.Extentions;
using Molulip.Import.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Molulip.Import
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {

                // SET KEY
                var meals = GetMeals();
                string serializedMeals = JsonConvert.SerializeObject(meals);
                client.Set("meal", serializedMeals);

            }
        }
        public static List<Meal> GetMeals()
        {
            var mealList = new List<Meal>();

            try
            {
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var fileName = "Feb.xlsx";

                var filePath = Path.GetFullPath(Path.Combine(baseDirectory, fileName));

                FileInfo file = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    mealList = worksheet.ConvertSheetToObjects<Meal>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mealList;
        }
    }
}
