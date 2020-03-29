using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace OptimalMenuSelection
{
    internal class DataReader
    {

        public MealCollection GetMealCollectionFromFile()
        {
            List<Meal> meals = new List<Meal>();
            using (TextFieldParser parser = new TextFieldParser("menu.csv"))
            {
               
                parser.Delimiters = new string[] { "," };
                while (true)
                {
                    Meal meal = new Meal();
                    string[] parts = parser.ReadFields();
                    if (parts == null)
                    {
                        break;
                    }
                    meal.Category = parts[0];
                    meal.Name = parts[1];
                    meal.ServingSize = getServing(parts[2]);
                    meal.Calories = Int32.Parse(parts[3]) / 10;
                    meal.Fat = float.Parse(parts[5]);
                    meal.Sodium = Int32.Parse(parts[12]);
                    meal.Carbohidrate = Int32.Parse(parts[14]);
                    meal.Protein = Int32.Parse(parts[19]);
                    meal.VitaminA = Int32.Parse(parts[20]);
                    meal.VitaminC = Int32.Parse(parts[21]);
                    meal.Calcium = Int32.Parse(parts[22]);
                    meal.Iron = Int32.Parse(parts[23]);

                    meals.Add(meal);
                }
            }
            return new MealCollection(meals);
        }

        private float getServing(string value)
        {
            int startIndex = value.IndexOf('(');
            int endIndex = value.IndexOf(')');
            string sub = value.Substring(startIndex + 1, endIndex - startIndex - 2);
            return float.Parse(sub);
        }
    }
}