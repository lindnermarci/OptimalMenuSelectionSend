using System.Collections.Generic;

namespace OptimalMenuSelection
{
    public class NutritionConstraints
    {
        public int Protein { get; set; }
        public int Carbohydrate { get; set; }
        public int Fat { get; set; }

        public int Calories { get; set; }

        public int VitaminA { get; set; }
        public int VitaminC { get; set; }
        public int Calcium { get; set; }
        public int Iron { get; set; }

        public NutritionConstraints(int protein, int carbohydrate, int fat, int calories, int vitaminA, int vitaminC, int calcium, int iron)
        {
            Protein = protein;
            Carbohydrate = carbohydrate;
            Fat = fat;
            Calories = calories / 10;
            VitaminA = vitaminA;
            VitaminC = vitaminC;
            Calcium = calcium;
            Iron = iron;
        }

        public List<double> normaliseConstraints()
        {
            return new List<double> { (double)Protein / 1880, (double)Carbohydrate / 141, (double)Fat / 182 };
        }
    }
}