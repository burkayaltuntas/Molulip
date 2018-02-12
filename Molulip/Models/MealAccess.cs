using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceStack.Redis;
using Molulip.Import;


namespace Molulip.Models
{
    public class MealAccess
    {
        public Import.Models.Meal GetTodaysSpecial()
        {
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                // GET FROM REDIS
                var mealValue = client.GetValue("meal");


                mealValue = mealValue.TrimStart('\"');
                mealValue = mealValue.TrimEnd('\"');
                mealValue = mealValue.Replace("\\", "");

                var mealList = JsonConvert.DeserializeObject<List<Import.Models.Meal>>(mealValue);
                mealList.RemoveAll(x => x.Col2 == null && x.Col1.Year == 1);

                var todaysSpecial = mealList.Where(x => x.Col1.DayOfYear == DateTime.Now.DayOfYear).FirstOrDefault();

                return todaysSpecial;


            }
        }
    }
}
