using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Renovators
{
    public class Catalog
    {
        public Catalog(string name, int neededRenovators, string project)
        {
            Name = name;
            NeededRenovators = neededRenovators;
            Project = project;
            Renovators = new List<Renovator>();
        }

        public string Name { get; set; }
        public int NeededRenovators { get; set; }
        public string Project { get; set; }
        public List<Renovator> Renovators { get; set; }
        public int Count => Renovators.Count;

        public string AddRenovator(Renovator renovator)
        {
            if (NeededRenovators > Count)
            {
                if (string.IsNullOrEmpty(renovator.Name) || string.IsNullOrEmpty(renovator.Type))
                {
                    return "Invalid renovator's information.";
                }
                else
                {
                    if (renovator.Rate > 350)
                    {
                        return "Invalid renovator's rate.";
                    }
                    else
                    {
                        Renovators.Add(renovator);
                        return $"Successfully added {renovator.Name} to the catalog.";
                    }
                }
            }
            else
            {
                return "Renovators are no more needed.";
            }
        }
        public bool RemoveRenovator(string name) => Renovators.Remove(Renovators.FirstOrDefault(x => x.Name == name));

        public int RemoveRenovatorBySpecialty(string type)
        {
            int removed = 0;
            for (int i = 0; i < Renovators.Count; i++)
            {
                if (Renovators[i].Type == type)
                {
                    removed++;
                    Renovators.RemoveAt(i);
                    i--;
                }
            }
            return removed;
        }
        public Renovator HireRenovator(string name)
        {
            Renovator renovator = Renovators.FirstOrDefault(x => x.Name == name);
            if (renovator != null)
            {
                renovator.Hired = true;
            }
            return renovator;
        }
        public List<Renovator> PayRenovators(int days)
        {
            List<Renovator> renovators = new List<Renovator>();
            renovators = Renovators.FindAll(x => x.Days >= days).ToList();
            //foreach (var renovator in Renovators)
            //{
            //    if (renovator.Days >= days)
            //    {
            //        renovators.Add(renovator);
            //    }
            //}
            return renovators;
        }
        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Renovators available for Project {this.Project}:");
            foreach (var renovator in Renovators.Where(x => x.Hired != true))
            {
                sb.AppendLine(renovator.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
