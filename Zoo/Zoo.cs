using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Zoo
{
    public class Zoo
    {
        
        public Zoo(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Animals = new List<Animal>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Animal> Animals { get; set; }
        


        public string AddAnimal(Animal animal)
        {
            if (string.IsNullOrWhiteSpace(animal.Species))
            {
                return "Invalid animal species.";
            }
            if (animal.Diet != "herbivore" && animal.Diet != "carnivore")
            {
                return "Invalid animal diet.";
            }
            if (Capacity <= this.Animals.Count)
            {
                return "The zoo is full.";
            }
            this.Animals.Add(animal);
            return $"Successfully added {animal.Species} to the zoo.";
        }

        public int RemoveAnimals(string species)
        {
            int removed = 0;
            for (int i = 0; i < Animals.Count; i++)
            {
                if (this.Animals[i].Species == species)
                {
                    removed++;
                    this.Animals.RemoveAt(i);
                    i--;
                }
            }
            return removed;
        }
        public List<Animal> GetAnimalsByDiet(string diet)
        {
            List<Animal> animals = new List<Animal>();
            animals = this.Animals.FindAll(x => x.Diet == diet).ToList();
            return animals;
        }

        public Animal GetAnimalByWeight(double weight)
        {
            Animal animal = this.Animals.First(x => x.Weight == weight);
            return animal;
        }
        public string GetAnimalCountByLength(double minLenght, double maxLenght)
        {
            int count = this.Animals.Where(x => x.Length >= minLenght && x.Length < maxLenght).Count();

            return $"There are {count} animals with a length between {minLenght} and {maxLenght} meters.";
        }
    }
}
