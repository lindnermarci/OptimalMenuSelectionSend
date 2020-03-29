using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalMenuSelection
{
    class Meal
    {

        public string Category { get; set; }
        public String Name { get; set; }
        public float ServingSize { get; set; }
        public float Calories { get; set; }

        public float Fat { get; set; }
        public float Carbohidrate { get; set; }
        public float Protein { get; set; }


        public float Sodium { get; set; }
        public float Calcium { get; set; }
        public float Iron { get; set; }

        public float VitaminA { get; set; }
        public float VitaminC { get; set; }

        public Meal(string category, string name, float servingSize, float calories, float fat, float carbohidrate, float protein, float sodium, float calcium, float iron, float vitaminA, float vitaminC)
        {
            Category = category;
            Name = name;
            ServingSize = servingSize;
            Calories = calories;
            Fat = fat;
            Carbohidrate = carbohidrate;
            Protein = protein;
            Sodium = sodium;
            Calcium = calcium;
            Iron = iron;
            VitaminA = vitaminA;
            VitaminC = vitaminC;
        }

        public Meal()
        {

        }

        //public static Meal operator +(Meal a, Meal b)
        //=> new Meal(b.Category + a.Category, a.Name, a.ServingSize + b.ServingSize, a.Calories + b.Calories);
    }
}
