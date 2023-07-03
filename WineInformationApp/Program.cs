using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using DotNetEnv;

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
            // ChardonnayWine chardonnay = new ChardonnayWine();
            // chardonnay.getDescription();
            CreateDataBaseGateway1 createDataBaseGate = new CreateDataBaseGateway1();
            // createDataBaseGate.InsertNewWine(chardonnay);

            string[] searchFilter = { "Buttery", "rich" };
            // createDataBaseGate.getWineInfoByCharacteristics(searchFilter).Wait();

            CreateDataBaseGateway2 createDataBaseGate2 = new CreateDataBaseGateway2();

            createDataBaseGate2.getWineInfoByCharacteristics(searchFilter).Wait();


            // Console.WriteLine("Name the characteristics you would like in your wine and hit enter:");
            // String desiredCharacteristics = Console.ReadLine();
            // chardonnay.MatchesCharacteristics(desiredCharacteristics);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


    }

    // Enum to represent the wine types
    public enum WineType
    {
        Red,
        White,
        Rose
    }

    // Class to represent a Wine
    public class Wine
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
            foreach (string word in Characteristics.Split(","))
            {
                if (word == characteristic)
                {
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

    public class CreateDataBaseGateway1
    {
        IMongoCollection<BsonDocument> collection;

        public CreateDataBaseGateway1()
        {
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();

            string connectionUri = DotNetEnv.Env.GetString("MONGO_URI2", "MONGO_URI not found");

            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            var serverApi = settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            MongoClient client = new MongoClient(settings);

            var wineInfoDatabase = client.GetDatabase("WineInformationApp");

            collection = wineInfoDatabase.GetCollection<BsonDocument>("wine-characteristics");
        }

        public void InsertNewWine(Wine newWine)
        {
            // Create a document
            var newWineDocument = new BsonDocument
            {
                { "name", $"{newWine.Name}" },
                { "type", $"{newWine.Type}" },
                { "bestServingTemperature", $"{newWine.BestServingTemperature}" },
                { "characteristics", $"{newWine.Characteristics}" },
            };

            // Insert the document into the collection
            collection.InsertOne(newWineDocument);
            Console.WriteLine("Document inserted.");
        }

        public void getWineInfoByName(string wineName)
        {

            // Retrieve documents from the collection
            var filter = Builders<BsonDocument>.Filter.Eq("name", $"{wineName}");
            var documents = collection.Find(filter).ToList();
            Console.WriteLine("Retrieved documents:");

            foreach (var doc in documents)
            {
                Console.WriteLine(doc);
            }
        }

        public void updateWineCharacteristicsByName(string wineName, string newCharacteristics)
        {
            // Update a document in the collection
            var updateFilter = Builders<BsonDocument>.Filter.Eq("name", $"{wineName}");
            var update = Builders<BsonDocument>.Update.Set("characteristics", $"{newCharacteristics}");
            collection.UpdateOne(updateFilter, update);
            Console.WriteLine($"{wineName}'s characteristics were updated successfully.");
        }

        public void deleteWineByName(string wineName)
        {
            // Delete a document from the collection
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("name", $"{wineName}");
            collection.DeleteOne(deleteFilter);
            Console.WriteLine($"{wineName} deleted successfully.");
        }

        public async Task getWineInfoByCharacteristics(string[] inputCharacteristics)
        {
            var projection = Builders<BsonDocument>.Projection.Include("name").Include("characteristics");
            var filter = Builders<BsonDocument>.Filter.Empty;
            var options = new FindOptions<BsonDocument, BsonDocument> { Projection = projection };

            using (var cursor = await collection.FindAsync(filter, options))
            {
                List<string> inputCharacteristicsToLower = new List<string>();
                foreach(string characteristic in inputCharacteristics){
                    inputCharacteristicsToLower.Add(characteristic.ToLower());
                }

                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var doc in batch)
                    {
                        var characteristics = doc["characteristics"].AsString.ToLower();
                        var wineName = doc["name"].AsString;
                        
                        foreach (var inputChar in inputCharacteristics)
                        {
                            if (characteristics.Contains(inputChar))
                            {
                                Console.WriteLine($"Hooray! Found a match in our database. You should try {wineName}.");
                                break;
                            }
                        }
                    }
                }
            }
        }   
    }

    public class CreateDataBaseGateway2
    {
        IMongoCollection<BsonDocument> collection;

        public CreateDataBaseGateway2()
        {
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();

            string connectionUri = DotNetEnv.Env.GetString("MONGO_URI2", "MONGO_URI not found");

            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            var serverApi = settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            MongoClient client = new MongoClient(settings);

            var wineInfoDatabase = client.GetDatabase("WineInformationApp");

            collection = wineInfoDatabase.GetCollection<BsonDocument>("wine-reviews");
        }

        // public void InsertNewWine(Wine newWine)
        // {
        //     // Create a document
        //     var newWineDocument = new BsonDocument
        //     {
        //         { "name", $"{newWine.Name}" },
        //         { "type", $"{newWine.Type}" },
        //         { "bestServingTemperature", $"{newWine.BestServingTemperature}" },
        //         { "characteristics", $"{newWine.Characteristics}" },
        //     };

        //     // Insert the document into the collection
        //     collection.InsertOne(newWineDocument);
        //     Console.WriteLine("Document inserted.");
        // }

        public void getWineInfoByName(string wineName)
        {

            // Retrieve documents from the collection
            var filter = Builders<BsonDocument>.Filter.Eq("name", $"{wineName}");
            var documents = collection.Find(filter).ToList();
            Console.WriteLine("Retrieved documents:");

            foreach (var doc in documents)
            {
                Console.WriteLine(doc);
            }
        }

        // public void updateWineCharacteristicsByName(string wineName, string newCharacteristics)
        // {
        //     // Update a document in the collection
        //     var updateFilter = Builders<BsonDocument>.Filter.Eq("name", $"{wineName}");
        //     var update = Builders<BsonDocument>.Update.Set("characteristics", $"{newCharacteristics}");
        //     collection.UpdateOne(updateFilter, update);
        //     Console.WriteLine($"{wineName}'s characteristics were updated successfully.");
        // }

        // public void deleteWineByName(string wineName)
        // {
        //     // Delete a document from the collection
        //     var deleteFilter = Builders<BsonDocument>.Filter.Eq("name", $"{wineName}");
        //     collection.DeleteOne(deleteFilter);
        //     Console.WriteLine($"{wineName} deleted successfully.");
        // }

        public async Task getWineInfoByCharacteristics(string[] inputCharacteristics)
        {
            var projection = Builders<BsonDocument>.Projection.Include("title").Include("description");
            var filter = Builders<BsonDocument>.Filter.Empty;
            var options = new FindOptions<BsonDocument, BsonDocument> { Projection = projection };

            using (var cursor = await collection.FindAsync(filter, options))
            {
                List<string> inputCharacteristicsToLower = new List<string>();
                foreach(string characteristic in inputCharacteristics){
                    inputCharacteristicsToLower.Add(characteristic.ToLower());
                }

                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var doc in batch)
                    {
                        var wineDescription = doc["description"].AsString.ToLower();
                        var wineName = doc["title"].AsString;
                        
                        foreach (var inputChar in inputCharacteristicsToLower)
                        {
                            if (wineDescription.Contains(inputChar))
                            {
                                Console.WriteLine($"We Found a match in our database. You should try {wineName}. {wineDescription}");
                                break;
                            }
                        }
                    }
                }
            }
        }   
    }
}

