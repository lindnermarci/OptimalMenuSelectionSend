using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OptimalMenuSelection
{
    internal class CandidateSolution
    {
        private List<bool> isSelected;

        public MealCollection Meals { get; }

        public NutritionConstraints NutritionConstraints { get; }

        public float Fitness { get; private set; } = 0;

        //generation 0, creates random solution
        public CandidateSolution(MealCollection mealCollection, NutritionConstraints nutritionConstraints)
        {
            NutritionConstraints = nutritionConstraints;
            Meals = mealCollection;

            var numMeals = Meals.Count;
            isSelected = new List<bool>(numMeals);

            //randomly initilase membership flag
            for (int i = 0; i < numMeals; i++)
            {
                isSelected.Add(Randomizer.GetDoubleFromZeroToOne() >= 0.5);
            }

            CalcFitness();
        }

        public CandidateSolution(MealCollection meals, NutritionConstraints nutritionConstraints, List<bool> isSelected)
        {
            this.isSelected = new List<bool>(isSelected);
            Meals = meals;
            NutritionConstraints = nutritionConstraints;
         
            CalcFitness();
        }

        public void Repair()
        {
            int numItems = Meals.Count;
            int numSelected = isSelected.Where(v => v).Count();

            while (GetCaloriesOfSelected() > NutritionConstraints.Calories * 1.1)
            {
                int deselectWich = Randomizer.IntLessThan(numItems);
                for (int i = 0; i < numItems; i++)
                {
                    if (isSelected[i])
                    {
                        if (deselectWich == 0)
                        {
                            isSelected[i] = false;
                            numSelected--;
                            break;
                        }
                        deselectWich--;
                    }
                }

                // Since the selected items have changed, recalculate the fitness
                CalcFitness();
            }
        }

        public float GetCaloriesOfSelected()
        {
            int numItems = Meals.Count;
            float totalCalories = 0;
            for (int i = 0; i < numItems; i++)
                if (isSelected[i])
                {
                    totalCalories += Meals[i].Calories;
                }

            return totalCalories;
        }

        internal CandidateSolution DeepClone()
        {
            var clone = new CandidateSolution(Meals, NutritionConstraints, isSelected);
            return clone;
        }

        private void CalcFitness()
        {
            Meal total = new Meal();
            int numItems = Meals.Count;
            for (int i = 0; i < numItems; i++)
            {
                if (isSelected[i])
                {
                    total.Calories += Meals[i].Calories;
                    total.Protein = Meals[i].Protein;
                    total.Carbohidrate = Meals[i].Carbohidrate;
                    total.Fat = Meals[i].Fat;
                    total.VitaminA = Meals[i].VitaminA;
                    total.Calcium = Meals[i].Calcium;
                    total.Iron = Meals[i].Iron;
                }
            }
            Fitness = calcAbsoluteDifference(NutritionConstraints.Calories, total.Calories)
                                + calcAbsoluteDifference(NutritionConstraints.Protein, total.Protein)
                                + calcAbsoluteDifference(NutritionConstraints.Carbohydrate, total.Carbohidrate)
                                + calcAbsoluteDifference(NutritionConstraints.Fat, total.Fat)
                                + calcAbsoluteDifference(NutritionConstraints.VitaminA, total.VitaminA)
                                + calcAbsoluteDifference(NutritionConstraints.VitaminC, total.VitaminC)
                                + calcAbsoluteDifference(NutritionConstraints.Calcium, total.Calcium)
                                + calcAbsoluteDifference(NutritionConstraints.Iron, total.Iron);
        }

        private float calcAbsoluteDifference(int a, float b)
        {
            return (System.Math.Abs(a - b));
        }

        public void SetSelected(int i, bool include)
        {
            isSelected[i] = include;
        }

        public bool IsSelected(int i)
        {
            return isSelected[i];
        }

        internal MealCollection GetSelectedItems()
        {
            // return only the selected items 
            List<Meal> results = new List<Meal>();
            int numItems = Meals.Count;
            var itemlist = Meals.ToList();

            for (int i = 0; i < numItems; i++)
                if (isSelected[i])
                {
                    results.Add(itemlist[i]);
                }

            return new MealCollection(results);
        }

        public void AddPossibleMutation(double mutationRate)
        {
            int numItems = Meals.Count;
            for (int i = 0; i < numItems; i++)
            {
                if (Randomizer.GetDoubleFromZeroToOne() < mutationRate)
                    isSelected[i] = !isSelected[i];
            }
            CalcFitness();
        }

    }
}