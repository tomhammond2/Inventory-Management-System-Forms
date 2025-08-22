using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Managment_System_Forms
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public InventoryItem(int id, string name, string category, int quantity, decimal price)
        {
            Id = id;
            Name = name;
            Category = category;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Category} | Qty: {Quantity} | £{Price}";
        }
    }
}

