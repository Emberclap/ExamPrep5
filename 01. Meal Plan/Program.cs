using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Meal_Plan
{
    public class Program
    {
        static void Main(string[] args)
        {

            Queue<string> meals = new Queue<string>(Console.ReadLine()
               .Split(" ", StringSplitOptions.RemoveEmptyEntries));

            Stack<int> calories = new Stack<int>(Console.ReadLine()
                 .Split(" ")
                 .Select(int.Parse));
            Dictionary<string, int> mealMenu = new Dictionary<string, int>
            {
                { "salad", 350 },
                { "soup", 490 },
                { "pasta", 680},
                { "steak", 790 },
            };
            
            int mealsEaten = 0;
            while (meals.Any() && calories.Any())
            {
                string meal = meals.Dequeue();
                int mealCal = mealMenu[meal];
                
                if (mealCal <= calories.Peek())
                {
                    int tempCalories = calories.Pop() - mealCal;
                    calories.Push(tempCalories);
                    mealsEaten++;
                }
                else
                {
                    int tempCalories = mealCal - calories.Pop();
                    mealsEaten++;
                    if (calories.Any())
                    {
                        int nextDayCalories = calories.Pop() - tempCalories;
                        calories.Push(nextDayCalories); 

                    }
                    
                }
            }
            
            
            if (meals.Any())
            {
                Console.WriteLine($"John ate enough, he had {mealsEaten} meals.");
                Console.WriteLine($"Meals left: {string.Join(", ", meals)}.");
                
            }
            else
            {
                Console.WriteLine($"John had {mealsEaten} meals.");
                Console.WriteLine($"For the next few days, he can eat {string.Join(", ",calories)} calories.");
            }
        }
    }
}
