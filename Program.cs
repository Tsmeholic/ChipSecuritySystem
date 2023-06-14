using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ChipSorter sorter = new ChipSorter();
            PathFinder pathFinder = new PathFinder(sorter);

            List<ColorChip> chips = InitializeChips();

            try
            {
                if (chips.Count == 0)
                {
                    throw new Exception();
                }
                sorter.SortChips(chips);
                pathFinder.FindPath();
                if (pathFinder.ValidatePath())
                {
                    Console.WriteLine($"The longest path is {pathFinder._longestPath.Count()} chips long and contains the following chips:");
                    foreach (ColorChip chip in pathFinder._longestPath)
                    {
                        Console.WriteLine(chip.ToString());
                    }
                    Console.ReadLine();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine(Constants.ErrorMessage);
                Console.ReadLine();
            }
        }

        private static List<ColorChip> InitializeChips()
        {
            Random rand = new Random();
            List<ColorChip> chips = new List<ColorChip>();
            var colors = (Color[])Enum.GetValues(typeof(Color));
            for (int i = 0; i < 100; i++)
            {
                chips.Add(new ColorChip(colors[rand.Next(0, 5)], colors[rand.Next(0, 5)]));
            }

            return chips;
        }
    }
}