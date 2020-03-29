using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalMenuSelection
{
    class GeneticAlgorithEngine
    {
        private const int MaxGenerationsWithNoChange = 100;


        private int populationSize;
        private double crossoverRate;
        private double mutationRate;

        private List<CandidateSolution> currentGeneration;
        private int generationNumber = 0;
        private float totalFitnessThisGeneration = 0;
        private double totalInverseFitnessThisGeneration = 0;

        private float bestFitnessScoreAllTime = Int32.MaxValue;
        private CandidateSolution bestSolution = null;
        private int bestSolutionGenerationNumber;


        public GeneticAlgorithEngine(int initialPopulationSize, int crossoverPercentage, double mutationPercentage)
        {
            this.populationSize = initialPopulationSize;
            this.crossoverRate = crossoverPercentage / 100D;
            this.mutationRate = mutationPercentage / 100D;
        }

        public MealCollection FindOptimalItems(MealCollection meals, NutritionConstraints constraints)
        {
            currentGeneration = new List<CandidateSolution>(populationSize);
            for (int i = 0; i < populationSize; i++)
            {
                currentGeneration.Add(new CandidateSolution(meals, constraints));
            }

            generationNumber = 1;


            //main loop
            while (true)
            {
                float bestFitnessScoreThisGeneration = System.Int32.MaxValue;
                CandidateSolution bestSolutionThisGeneration = null;

                foreach (var candidate in currentGeneration)
                {
                    candidate.Repair();
                    float fitness = candidate.Fitness;

                    //sum up fitness scores for the roulette wheel selection
                    totalFitnessThisGeneration += fitness;
                    totalInverseFitnessThisGeneration += 1 / (double)fitness;

                    if(fitness < bestFitnessScoreThisGeneration)
                    {
                        bestFitnessScoreThisGeneration = fitness;
                        bestSolutionThisGeneration = candidate;
                    }
                }


                Debug.WriteLine("Iteration count {0}, best fitness: {1}", generationNumber, bestFitnessScoreAllTime);
                //compare this generation's best to to the best we had so far
                if(bestFitnessScoreThisGeneration < bestFitnessScoreAllTime)
                {
                    //save the best score
                    bestFitnessScoreAllTime = bestFitnessScoreThisGeneration;

                    //and save possible solution
                    bestSolution = bestSolutionThisGeneration.DeepClone();
                    bestSolutionGenerationNumber = generationNumber;
                }
                else
                {
                    if ((generationNumber - bestSolutionGenerationNumber) > MaxGenerationsWithNoChange)
                        break;
                }

                List<CandidateSolution> nextGeneration = new List<CandidateSolution>();
                while (nextGeneration.Count < populationSize)
                {
                    //Select two parents(the lower the fitness, the higher the chance of selection)
                    var parent1 = SelectCandidate();
                    var parent2 = SelectCandidate();

                    //cross them over to generate two new children
                    CandidateSolution child1, child2;
                    CrossOverParents(parent1, parent2, out child1, out child2);

                    //appy mutation if needed
                    child1.AddPossibleMutation(mutationRate);
                    child2.AddPossibleMutation(mutationRate);

                    //add them to next generation
                    nextGeneration.Add(child1);
                    nextGeneration.Add(child2);
                }

                currentGeneration = nextGeneration;
                generationNumber++;
            }

            return bestSolution.GetSelectedItems();
        }

        private CandidateSolution SelectCandidate()
        {
            //roulette wheel selection
            double randomValue = Randomizer.GetRandomNumberUpTo(totalInverseFitnessThisGeneration);
            for (int i = 0; i < populationSize; i++)
            {
                randomValue -= 1 / (double)currentGeneration[i].Fitness;
                if (randomValue <= 0)
                {
                    return currentGeneration[i];
                }
            }
            return currentGeneration[populationSize - 1];
        }

        private void CrossOverParents(CandidateSolution parent1, CandidateSolution parent2, out CandidateSolution child1, out CandidateSolution child2)
        {
            child1 = parent1.DeepClone();
            child2 = parent2.DeepClone();

            //use exact copies or do crossover
            if (Randomizer.GetDoubleFromZeroToOne() < crossoverRate)
            {
                int numItems = parent1.Meals.Count;
                int crossoverPoint = Randomizer.IntLessThan(numItems);

                for (int i = 0; i < crossoverPoint; i++)
                {
                    child1.SetSelected(i, parent1.IsSelected(i));
                    child2.SetSelected(i, parent2.IsSelected(i));
                }
                for (int i = crossoverPoint; i < numItems; i++)
                {
                    child1.SetSelected(i, parent2.IsSelected(i));
                    child2.SetSelected(i, parent1.IsSelected(i));
                }
            }
        }
    }
}
