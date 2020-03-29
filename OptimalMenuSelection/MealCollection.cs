using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalMenuSelection
{
    internal class MealCollection
    {
        private List<Meal> meals;

        public MealCollection(List<Meal> meals)
        {
            this.meals = meals;
        }

        public List<Meal> ToList()
        {
            return meals;
        }

        public Meal this[int index]
        {
            get { return meals[index]; }
            set { meals[index] = value; }
        }

        public int Count
        {
            get { return meals.Count; }
        }
    }
}
