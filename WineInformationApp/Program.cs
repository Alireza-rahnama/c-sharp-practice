using System;
using System.Collections.Generic;

namespace WineInformationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of Wine objects
            // Wine chardonnay = new Wine("Chardonnay", WineType.White, 10, "Rich, buttery, and oaky");
            // Wine cabernetSauvignon = new Wine("Cabernet Sauvignon", WineType.Red, 18, "Bold, full-bodied, and tannic");
            // Wine rosé = new Wine("Rosé", WineType.Rose, 8, "Light, refreshing, and fruity");
            // Wine pinotNoir = new Wine("Pinot Noir", WineType.Red, 16, "Elegant, earthy, and medium-bodied");
            // Wine sauvignonBlanc = new Wine("Sauvignon Blanc", WineType.White, 8, "Crisp, herbal, and citrusy");
            // Wine syrah = new Wine("Syrah", WineType.Red, 18, "Spicy, dark fruit flavors, and full-bodied");
            // Wine riesling = new Wine("Riesling", WineType.White, 9, "Aromatic, fruity, and off-dry");
            // Wine merlot = new Wine("Merlot", WineType.Red, 17, "Soft, ripe fruit flavors, and medium-bodied");

            // // Create a list of wines
            // List<Wine> wines = new List<Wine> { chardonnay, cabernetSauvignon, rosé, pinotNoir, sauvignonBlanc, syrah, riesling, merlot };

            // // Display information about each wine
            // foreach (Wine wine in wines)
            // {
            //     Console.WriteLine($"Name: {wine.Name}");
            //     Console.WriteLine($"Type: {wine.Type}");
            //     Console.WriteLine($"Best Serving Temperature: {wine.BestServingTemperature}°C");
            //     Console.WriteLine($"Characteristics: {wine.Characteristics}");
            //     Console.WriteLine();
            // }

            // // Wait for user input before closing the console window
            // Console.WriteLine("Press any key to exit...");
            // Console.ReadKey();
            ChardonnayWine chardonnay = new ChardonnayWine();
            chardonnay.getDescription();
            Console.WriteLine("Name the characteristics you would like in your wine and hit enter:");
            String desiredCharacteristics = Console.ReadLine();
            chardonnay.MatchesCharacteristics(desiredCharacteristics);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


    }

    // Enum to represent the wine types
    enum WineType
    {
        Red,
        White,
        Rose
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

        public bool MatchesCharacteristics(string characteristic)
        {
            Boolean doesMatch = false;
            foreach (string word in Characteristics.Split(",")){
                if(word==characteristic){
                    Console.WriteLine($"{Name} is {characteristic}");
                    doesMatch = true;
                }
            }
            return doesMatch;

        }
    }

    //practice inheritance
    class ChardonnayWine : Wine
    {
        public ChardonnayWine(string name = "Chardonnay",
        WineType type = WineType.White,
        int bestServingTemperature = 10,
        string characteristics = "Rich, buttery, and oaky") : base(name, type, bestServingTemperature, characteristics) { }

        public void getDescription()
        {
            Console.WriteLine($"{Name} is {Type} wine, with {BestServingTemperature} degrees best serving temperature and has {Characteristics} characteristics.");
        }
    }
}

