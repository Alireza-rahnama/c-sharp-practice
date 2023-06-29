using System;
using System.Collections.Generic;

namespace WineInformationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of Wine objects
            Wine chardonnay = new Wine("Chardonnay", WineType.White, 10, "Rich, buttery, and oaky");
            Wine cabernetSauvignon = new Wine("Cabernet Sauvignon", WineType.Red, 18, "Bold, full-bodied, and tannic");
            Wine rosé = new Wine("Rosé", WineType.Rosé, 8, "Light, refreshing, and fruity");
            Wine pinotNoir = new Wine("Pinot Noir", WineType.Red, 16, "Elegant, earthy, and medium-bodied");
            Wine sauvignonBlanc = new Wine("Sauvignon Blanc", WineType.White, 8, "Crisp, herbal, and citrusy");
            Wine syrah = new Wine("Syrah", WineType.Red, 18, "Spicy, dark fruit flavors, and full-bodied");
            Wine riesling = new Wine("Riesling", WineType.White, 9, "Aromatic, fruity, and off-dry");
            Wine merlot = new Wine("Merlot", WineType.Red, 17, "Soft, ripe fruit flavors, and medium-bodied");

            // Create a list of wines
            List<Wine> wines = new List<Wine> { chardonnay, cabernetSauvignon, rosé, pinotNoir, sauvignonBlanc, syrah, riesling, merlot };

            // Display information about each wine
            foreach (Wine wine in wines)
            {
                Console.WriteLine($"Name: {wine.Name}");
                Console.WriteLine($"Type: {wine.Type}");
                Console.WriteLine($"Best Serving Temperature: {wine.BestServingTemperature}°C");
                Console.WriteLine($"Characteristics: {wine.Characteristics}");
                Console.WriteLine();
            }

            // Wait for user input before closing the console window
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    // Enum to represent the wine types
    enum WineType
    {
        Red,
        White,
        Rosé
    }

    // Class to represent a Wine
    class Wine
    {
        public string Name { get; set; }
        public WineType Type { get; set; }
        public int BestServingTemperature { get; set; }
        public string Characteristics { get; set; }

        // Constructor
        public Wine(string name, WineType type, int bestServingTemperature, string characteristics)
        {
            Name = name;
            Type = type;
            BestServingTemperature = bestServingTemperature;
            Characteristics = characteristics;
        }
    }
}

