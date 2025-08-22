using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace Inventory_Managment_System_Forms
{
    public class Inventory
    {
        private List<InventoryItem> items = new List<InventoryItem>();
        private int nextId = 1;

        public void AddItem(string name, string category, int quantity, decimal price)
        {
            items.Add(new InventoryItem(nextId++, name, category, quantity, price));
        }

        public bool EditItem(int id, string name, string category, int quantity, decimal price)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;

            item.Name = name;
            item.Category = category;
            item.Quantity = quantity;
            item.Price = price;
            return true;
        }


        public void DeleteItem (int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
                items.Remove(item);
        }

        public List<InventoryItem> SearchByName(string name)
        {
            return items
                .Where(i => i.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<InventoryItem> SearchByCategory(string category)
        {
            return items
                .Where(i => i.Category.IndexOf(category, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
        //=> items.Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<InventoryItem> GetAllItems()
        {
            return new List<InventoryItem>(items);
        }

        public InventoryItem GetItemById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }
    }
}

