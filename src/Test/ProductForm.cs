using System;
using System.Linq;
using System.Windows.Forms;
using Test.Models;

namespace Test
{
    public partial class ProductForm : Form
    {
        private ProductContext db = new ProductContext();

        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvProducts.DataSource = db.Products.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                Name = txtName.Text,
                Price = decimal.TryParse(txtPrice.Text, out decimal p) ? p : 0
            };
            db.Products.Add(product);
            db.SaveChanges();
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            var id = (int)dgvProducts.CurrentRow.Cells[0].Value;
            var product = db.Products.Find(id);
            if (product != null)
            {
                product.Name = txtName.Text;
                product.Price = decimal.TryParse(txtPrice.Text, out decimal p) ? p : 0;
                db.SaveChanges();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            var id = (int)dgvProducts.CurrentRow.Cells[0].Value;
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                LoadData();
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                txtName.Text = dgvProducts.CurrentRow.Cells[1].Value?.ToString();
                txtPrice.Text = dgvProducts.CurrentRow.Cells[2].Value?.ToString();
            }
        }
    }
}
