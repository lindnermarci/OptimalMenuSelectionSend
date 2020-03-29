using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalMenuSelection
{
    class Program
    {
        private const int INITIALPOPULATIONSIZE = 100;
        private const int CROSSOVERPERCENTAGE = 90;
        private const int MUTATIONPERCENTAGE = 10;


        private const int CALORIES = 3000;
        private const int PROTEIN = 140;
        private const int CARBS = 358;
        private const int FAT = 117;
        private const int VITAMINA = 900;
        private const int VITAMINC = 90;
        private const int CALCIUM = 100;
        private const int IRON = 100;



        static void Main(string[] args)
        {
            Console.Write("Initialising engine...");
            GeneticAlgorithEngine ge = new GeneticAlgorithEngine(INITIALPOPULATIONSIZE, CROSSOVERPERCENTAGE, MUTATIONPERCENTAGE);
            Console.Write("\nReading data...");
            DataReader dataReader = new DataReader();
            MealCollection mealCollection = dataReader.GetMealCollectionFromFile();

            NutritionConstraints constraints = new NutritionConstraints(PROTEIN, CARBS, FAT, CALORIES, VITAMINA, VITAMINC, CALCIUM, IRON);
            Console.Write("\nRunning optimalisation...");
            MealCollection solution = ge.FindOptimalItems(mealCollection, constraints);

            WriteSolutionToConsole(solution);
        }

        private static void WriteSolutionToConsole(MealCollection solution)
        {
            Meal totalMeal = new Meal();
            Console.WriteLine("\nYour meals:");
            foreach (var item in solution.ToList())
            {
                WriteMeal(item);
                totalMeal.ServingSize += item.ServingSize;
                totalMeal.Calories += item.Calories;
                totalMeal.Carbohidrate += item.Carbohidrate;
                totalMeal.Fat += item.Fat;
                totalMeal.Protein += item.Protein;
                totalMeal.Sodium += item.Sodium;
                totalMeal.VitaminA += item.VitaminA;
                totalMeal.VitaminC += item.VitaminC;
            }
            Console.WriteLine("============================");
            Console.WriteLine("Your total daily consumption:");
            WriteMeal(totalMeal);
            Console.ReadLine();
        }

        private static void WriteMeal(Meal meal)
        {
            Console.WriteLine("category: {0}, name: {1}, serving: {2}, calories: {3}, fat: {4}, carbs: {5}, protein: {6}, VitaminA: {7}, VitaminC: {8}, Calcium: {9}, Iron: {10}", meal.Category, meal.Name, meal.ServingSize, meal.Calories * 10, meal.Fat, meal.Carbohidrate, meal.Protein, meal.VitaminA, meal.VitaminC, meal.Calcium, meal.Iron);
        }

    }
}
