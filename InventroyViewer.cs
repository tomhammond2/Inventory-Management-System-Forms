using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory_Managment_System_Forms
{
    public partial class InventroyViewer : Form
    {

        private Inventory inventory;

        public InventroyViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            inventory = new Inventory();

            // Load items from CSV
            foreach (var item in FileHandler.Load())
                inventory.AddItem(item.Name, item.Category, item.Quantity, item.Price);

            DataGrid.AutoGenerateColumns = true;
            RefreshGrid();
            EditItemBtn.Click += EditItemBtn_Click;
        }

        private void RefreshGrid()
        { 
            DataGrid.DataSource = null;
            DataGrid.DataSource = inventory.GetAllItems();

            // Adjust column sizes
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGrid.AutoResizeColumnHeadersHeight();
            DataGrid.RowTemplate.Height = 30; // optional, makes rows taller
        }

 

        private void AddItemBtn_Click(object sender, EventArgs e)
        {
            using (var form = new AddItemForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    inventory.AddItem(form.NewItem.Name, form.NewItem.Category, form.NewItem.Quantity, form.NewItem.Price);
                    RefreshGrid();
                }
            }
        }

        private void EditItemBtn_Click(object sender, EventArgs e)
        {
            if (DataGrid.CurrentRow == null) return;

            var selectedItem = (InventoryItem)DataGrid.CurrentRow.DataBoundItem;

            using (var form = new EditItemForm(selectedItem))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Update inventory with new values
                    inventory.EditItem(
                        form.UpdatedItem.Id,
                        form.UpdatedItem.Name,
                        form.UpdatedItem.Category,
                        form.UpdatedItem.Quantity,
                        form.UpdatedItem.Price
                    );

                    RefreshGrid();
                }
            }
        }

 

        private void DeleteItemBtn_Click_1(object sender, EventArgs e)
        {
            if (DataGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            var selectedItem = (InventoryItem)DataGrid.CurrentRow.DataBoundItem;

            var confirm = MessageBox.Show($"Are you sure you want to delete '{selectedItem.Name}'?",
                                          "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                inventory.DeleteItem(selectedItem.Id);
                RefreshGrid();
                FileHandler.Save(inventory.GetAllItems());
            }
        }

        private void SaveDataBtn_Click_1(object sender, EventArgs e)
        {
            FileHandler.Save(inventory.GetAllItems());
            MessageBox.Show("Inventory saved!");
        }

        private void SearchDataBtn_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text;
            List<InventoryItem> results;

            if (cmbSearchType.SelectedItem.ToString() == "Name")
                results = inventory.SearchByName(query);
            else // Category
                results = inventory.SearchByCategory(query);

            DataGrid.DataSource = null;
            DataGrid.DataSource = results;
        }
    }
}
