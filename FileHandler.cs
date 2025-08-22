namespace Inventory_Managment_System_Forms
{
    public static class FileHandler
    {
        private static string filePath = "inventory.csv";

        public static void Save(List<InventoryItem> items)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,Name,Category,Quantity,Price");


                foreach (var item in items)
                {
                    string name = EscapeCsv(item.Name);
                    string category = EscapeCsv(item.Category);

                    writer.WriteLine($"{item.Id},{name},{category},{item.Quantity},{item.Price.ToString(CultureInfo.InvariantCulture)}");
                }
            }
        }

        public static List<InventoryItem> Load()
        {
            var items = new List<InventoryItem>();

            //if (!File.Exists(filePath))
            //    return items; 

            //Create seeded data (Testing)
            if (!File.Exists(filePath))
            {
                SeedSampleData(); // Create the file with defaults
                return Load();    // Load the seeded data
            }

            try
            {
                var lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++) // Skip header
                {
                    var fields = SplitCsvLine(lines[i]);
                    if (fields.Length != 5)
                        continue; // Skip invalid rows

                    if (!int.TryParse(fields[0], out int id)) continue;
                    if (!int.TryParse(fields[3], out int quantity)) continue;
                    if (!decimal.TryParse(fields[4], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price)) continue;

                    items.Add(new InventoryItem(id, fields[1], fields[2], quantity, price));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading CSV: {ex.Message}");
            }

            return items;


        }

        //Create seeded data(Testing)
        public static void SeedSampleData()
        {
            var sampleItems = new List<InventoryItem>
            {
                new InventoryItem(1, "Red Apple", "Fruits", 100, 0.99m),
                new InventoryItem(2, "16GB USB Drive", "Electronics", 25, 12.50m),
                new InventoryItem(3, "Desk Chair", "Furniture", 10, 120.00m),
                new InventoryItem(4, "Notebook", "Stationery", 200, 2.49m),
                new InventoryItem(5, "LED Monitor 24inch", "Electronics", 8, 150.00m),
                new InventoryItem(6, "Banana", "Fruits", 80, 0.59m),
                new InventoryItem(7, "Office Desk", "Furniture", 5, 300.00m),
                new InventoryItem(8, "Blue Pen", "Stationery", 500, 0.25m),
                new InventoryItem(9, "Wireless Mouse", "Electronics", 15, 20.00m),
                new InventoryItem(10, "Orange", "Fruits", 120, 0.75m)
            };

            Save(sampleItems);
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains(",") || value.Contains("\""))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }
            return value;
        }


        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var current = "";

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '\"')
                {
                    inQuotes = !inQuotes; // Toggle quote mode
                    continue; // Don't include quotes
                }

                if (c == ',' && !inQuotes)
                {
                    result.Add(current.Trim());
                    current = "";
                }
                else
                {
                    current += c;
                }
            }

            result.Add(current.Trim());
            return result.ToArray();
        }
    }
}

