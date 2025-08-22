using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Managment_System_Forms
{
    public partial class EditItemForm : Form
    {
        public InventoryItem UpdatedItem { get; private set; }

        public EditItemForm(InventoryItem item)
        {
            InitializeComponent();
            // Pre-fill fields
            txtName.Text = item.Name;
            txtCategory.Text = item.Category;
            nudQuantity.Value = item.Quantity;
            txtPrice.Text = item.Price.ToString(CultureInfo.InvariantCulture);

            // Store the original ID
            UpdatedItem = new InventoryItem(item.Id, item.Name, item.Category, item.Quantity, item.Price);
        }

        private void EditItemForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string category = txtCategory.Text;
            int quantity = (int)nudQuantity.Value;

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price!");
                return;
            }

            // Update item
            UpdatedItem.Name = name;
            UpdatedItem.Category = category;
            UpdatedItem.Quantity = quantity;
            UpdatedItem.Price = price;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
