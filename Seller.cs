using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SellerInterface
{
    public class Program : Form
    {
        private TabControl tabControl;
        private TabPage tabAccount, tabProducts, tabOrders, tabReports;
        private TabControl tabLoginRegister;
        private TabPage tabLogin, tabRegister;

        private TextBox txtLoginEmail, txtLoginPassword;
        private TextBox txtRegisterStoreName, txtRegisterSellerName, txtRegisterEmail, txtRegisterContactInfo, txtRegisterProfileInfo, txtRegisterPassword;
        private Button btnLogin, btnRegister;

        private TextBox txtProductName, txtProductPrice, txtProductDescription, txtProductStock;
        private ListBox lstProducts;
        private Button btnAddProduct, btnUpdateProduct, btnDeleteProduct;

        // Order Fulfillment fields
        private List<Order> orderList = new List<Order>();
        private ListBox lstOrders;
        private Button btnPrintLabel, btnMarkShipped, btnMarkDelivered;
        private TextBox txtOrderDetails;

        // Sales Reports fields
        private List<Sale> salesList = new List<Sale>();
        private ListBox lstBestSellingProducts;
        private TextBox txtRevenueReport;
        private Button btnGenerateReport;

        // A simple dictionary to store registered users
        private Dictionary<string, string> registeredUsers = new Dictionary<string, string>();
        private List<Product> productList = new List<Product>();

        // Track login status
        private bool isLoggedIn = false;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Program());
        }

        public Program()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Initialize components
            this.tabControl = new TabControl();
            this.tabAccount = CreateTabPageWithPinkBackground("Account Management");
            this.tabProducts = CreateTabPageWithPinkBackground("Product Listings");
            this.tabOrders = CreateTabPageWithPinkBackground("Order Fulfillment");
            this.tabReports = CreateTabPageWithPinkBackground("Sales Reports");

            // Account Management: Login & Register Tabs
            this.tabLoginRegister = new TabControl() { Dock = DockStyle.Fill };
            this.tabLogin = CreateTabPageWithPinkBackground("Login");
            this.tabRegister = CreateTabPageWithPinkBackground("Register");

            // Login Controls
            this.txtLoginEmail = CreatePlaceholderTextBox("Enter Email");
            this.txtLoginEmail.Location = new Point(100, 50);
            this.txtLoginPassword = CreatePlaceholderTextBox("Enter Password");
            this.txtLoginPassword.Location = new Point(100, 100);
            this.txtLoginPassword.PasswordChar = '*'; // Mask password input
            this.btnLogin = new Button() { Text = "Login", Location = new Point(100, 150), Width = 100 };
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.tabLogin.Controls.Add(new Label() { Text = "Email:", Location = new Point(100, 30) });
            this.tabLogin.Controls.Add(this.txtLoginEmail);
            this.tabLogin.Controls.Add(new Label() { Text = "Password:", Location = new Point(100, 80) });
            this.tabLogin.Controls.Add(this.txtLoginPassword);
            this.tabLogin.Controls.Add(this.btnLogin);

            // Register Controls
            this.txtRegisterStoreName = CreatePlaceholderTextBox("Enter Store Name");
            this.txtRegisterStoreName.Location = new Point(100, 50);
            this.txtRegisterSellerName = CreatePlaceholderTextBox("Enter Seller Name");
            this.txtRegisterSellerName.Location = new Point(100, 100);
            this.txtRegisterEmail = CreatePlaceholderTextBox("Enter Email");
            this.txtRegisterEmail.Location = new Point(100, 150);
            this.txtRegisterContactInfo = CreatePlaceholderTextBox("Enter Contact Info");
            this.txtRegisterContactInfo.Location = new Point(100, 200);
            this.txtRegisterProfileInfo = CreatePlaceholderTextBox("Enter Profile Info");
            this.txtRegisterProfileInfo.Location = new Point(100, 250);
            this.txtRegisterPassword = CreatePlaceholderTextBox("Enter Password");
            this.txtRegisterPassword.Location = new Point(100, 300);
            this.txtRegisterPassword.PasswordChar = '*'; // Mask password input
            this.btnRegister = new Button() { Text = "Register", Location = new Point(100, 370), Width = 100 };
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);
            this.tabRegister.Controls.Add(new Label() { Text = "Store Name:", Location = new Point(100, 30) });
            this.tabRegister.Controls.Add(this.txtRegisterStoreName);
            this.tabRegister.Controls.Add(new Label() { Text = "Seller Name:", Location = new Point(100, 80) });
            this.tabRegister.Controls.Add(this.txtRegisterSellerName);
            this.tabRegister.Controls.Add(new Label() { Text = "Email:", Location = new Point(100, 130) });
            this.tabRegister.Controls.Add(this.txtRegisterEmail);
            this.tabRegister.Controls.Add(new Label() { Text = "Contact Info:", Location = new Point(100, 180) });
            this.tabRegister.Controls.Add(this.txtRegisterContactInfo);
            this.tabRegister.Controls.Add(new Label() { Text = "Profile Info:", Location = new Point(100, 230) });
            this.tabRegister.Controls.Add(this.txtRegisterProfileInfo);
            this.tabRegister.Controls.Add(new Label() { Text = "Password:", Location = new Point(100, 280) });
            this.tabRegister.Controls.Add(this.txtRegisterPassword);
            this.tabRegister.Controls.Add(this.btnRegister);

            // Add tabs to Login/Register control
            this.tabLoginRegister.Controls.Add(this.tabLogin);
            this.tabLoginRegister.Controls.Add(this.tabRegister);
            this.tabAccount.Controls.Add(this.tabLoginRegister);

            // Product Listings Tab
            this.txtProductName = CreatePlaceholderTextBox("Enter Product Name");
            this.txtProductName.Location = new Point(50, 50);
            this.txtProductPrice = CreatePlaceholderTextBox("Enter Price");
            this.txtProductPrice.Location = new Point(50, 100);
            this.txtProductDescription = CreatePlaceholderTextBox("Enter Description");
            this.txtProductDescription.Location = new Point(50, 150);
            this.txtProductStock = CreatePlaceholderTextBox("Enter Stock");
            this.txtProductStock.Location = new Point(50, 200);
            this.lstProducts = new ListBox() { Location = new Point(300, 50), Size = new Size(400, 300) };
            this.btnAddProduct = new Button() { Text = "Add Product", Location = new Point(50, 250), Width = 100 };
            this.btnUpdateProduct = new Button() { Text = "Update Product", Location = new Point(160, 250), Width = 100 };
            this.btnDeleteProduct = new Button() { Text = "Delete Product", Location = new Point(50, 300), Width = 100 };

            this.btnAddProduct.Click += new EventHandler(this.btnAddProduct_Click);
            this.btnUpdateProduct.Click += new EventHandler(this.btnUpdateProduct_Click);
            this.btnDeleteProduct.Click += new EventHandler(this.btnDeleteProduct_Click);

            this.tabProducts.Controls.Add(new Label() { Text = "Product Name:", Location = new Point(50, 30) });
            this.tabProducts.Controls.Add(this.txtProductName);
            this.tabProducts.Controls.Add(new Label() { Text = "Price:", Location = new Point(50, 80) });
            this.tabProducts.Controls.Add(this.txtProductPrice);
            this.tabProducts.Controls.Add(new Label() { Text = "Description:", Location = new Point(50, 130) });
            this.tabProducts.Controls.Add(this.txtProductDescription);
            this.tabProducts.Controls.Add(new Label() { Text = "Stock:", Location = new Point(50, 180) });
            this.tabProducts.Controls.Add(this.txtProductStock);
            this.tabProducts.Controls.Add(this.lstProducts);
            this.tabProducts.Controls.Add(this.btnAddProduct);
            this.tabProducts.Controls.Add(this.btnUpdateProduct);
            this.tabProducts.Controls.Add(this.btnDeleteProduct);

            // Order Fulfillment Tab
            this.tabOrders = CreateTabPageWithPinkBackground("Order Fulfillment");

            lstOrders = new ListBox() { Location = new Point(50, 50), Size = new Size(300, 300) };
            txtOrderDetails = new TextBox() { Location = new Point(400, 50), Size = new Size(300, 100), Multiline = true, ReadOnly = true };
            btnPrintLabel = new Button() { Text = "Print Shipping Label", Location = new Point(400, 160), Width = 150 };
            btnMarkShipped = new Button() { Text = "Mark as Shipped", Location = new Point(400, 200), Width = 150 };
            btnMarkDelivered = new Button() { Text = "Mark as Delivered", Location = new Point(400, 240), Width = 150 };

            btnPrintLabel.Click += new EventHandler(this.btnPrintLabel_Click);
            btnMarkShipped.Click += new EventHandler(this.btnMarkShipped_Click);
            btnMarkDelivered.Click += new EventHandler(this.btnMarkDelivered_Click);
            lstOrders.SelectedIndexChanged += new EventHandler(this.lstOrders_SelectedIndexChanged);

            this.tabOrders.Controls.Add(new Label() { Text = "New Orders:", Location = new Point(50, 30) });
            this.tabOrders.Controls.Add(lstOrders);
            this.tabOrders.Controls.Add(new Label() { Text = "Order Details:", Location = new Point(400, 30) });
            this.tabOrders.Controls.Add(txtOrderDetails);
            this.tabOrders.Controls.Add(btnPrintLabel);
            this.tabOrders.Controls.Add(btnMarkShipped);
            this.tabOrders.Controls.Add(btnMarkDelivered);

            // Sales Reports Tab
            this.tabReports = CreateTabPageWithPinkBackground("Sales Reports");

            lstBestSellingProducts = new ListBox() { Location = new Point(50, 50), Size = new Size(300, 300) };
            txtRevenueReport = new TextBox() { Location = new Point(400, 50), Size = new Size(300, 100), Multiline = true, ReadOnly = true };
            btnGenerateReport = new Button() { Text = "Generate Report", Location = new Point(400, 160), Width = 150 };

            btnGenerateReport.Click += new EventHandler(this.btnGenerateReport_Click);

            this.tabReports.Controls.Add(new Label() { Text = "Best Selling Products:", Location = new Point(50, 30) });
            this.tabReports.Controls.Add(lstBestSellingProducts);
            this.tabReports.Controls.Add(new Label() { Text = "Revenue Report:", Location = new Point(400, 30) });
            this.tabReports.Controls.Add(txtRevenueReport);
            this.tabReports.Controls.Add(btnGenerateReport);

            // Simulate receiving new orders and sales data for demonstration purposes
            SimulateReceivingOrders();
            SimulateSalesData();

            // Add tabs to main tab control
            this.tabControl.Controls.Add(this.tabAccount);
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Controls.Add(this.tabOrders);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Dock = DockStyle.Fill;

            // Add main tab control to form
            this.Controls.Add(this.tabControl);

            // Form properties
            this.Text = "Seller Interface - ShopVerse";
            this.ClientSize = new Size(800, 600);

            // Initially hide the product, order, and report tabs
            this.tabControl.TabPages.Remove(this.tabProducts);
            this.tabControl.TabPages.Remove(this.tabOrders);
            this.tabControl.TabPages.Remove(this.tabReports);
        }

        private TabPage CreateTabPageWithPinkBackground(string title)
        {
            TabPage tabPage = new TabPage(title)
            {
                BackColor = Color.Pink
            };
            return tabPage;
        }

        private TextBox CreatePlaceholderTextBox(string placeholder)
        {
            TextBox textBox = new TextBox();
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };

            return textBox;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(txtProductDescription.Text) || string.IsNullOrWhiteSpace(txtProductStock.Text))
            {
                MessageBox.Show("Please fill in all product details.");
                return;
            }

            if (!decimal.TryParse(txtProductPrice.Text, out decimal price) || !int.TryParse(txtProductStock.Text, out int stock))
            {
                MessageBox.Show("Invalid price or stock value.");
                return;
            }

            Product newProduct = new Product
            {
                Name = txtProductName.Text,
                Price = price,
                Description = txtProductDescription.Text,
                Stock = stock
            };

            productList.Add(newProduct);
            lstProducts.Items.Add(newProduct.Name);
            MessageBox.Show("Product added successfully.");
            ClearProductFields();
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            Product selectedProduct = productList[lstProducts.SelectedIndex];
            if (!string.IsNullOrWhiteSpace(txtProductName.Text)) selectedProduct.Name = txtProductName.Text;
            if (!string.IsNullOrWhiteSpace(txtProductPrice.Text) && decimal.TryParse(txtProductPrice.Text, out decimal price)) selectedProduct.Price = price;
            if (!string.IsNullOrWhiteSpace(txtProductDescription.Text)) selectedProduct.Description = txtProductDescription.Text;
            if (!string.IsNullOrWhiteSpace(txtProductStock.Text) && int.TryParse(txtProductStock.Text, out int stock)) selectedProduct.Stock = stock;

            lstProducts.Items[lstProducts.SelectedIndex] = selectedProduct.Name;
            MessageBox.Show("Product updated successfully.");
            ClearProductFields();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            productList.RemoveAt(lstProducts.SelectedIndex);
            lstProducts.Items.RemoveAt(lstProducts.SelectedIndex);
            MessageBox.Show("Product deleted successfully.");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (registeredUsers.TryGetValue(txtLoginEmail.Text, out string password) && password == txtLoginPassword.Text)
            {
                MessageBox.Show("Login successful.");
                isLoggedIn = true;

                // Show the other tabs after successful login
                this.tabControl.TabPages.Add(this.tabProducts);
                this.tabControl.TabPages.Add(this.tabOrders);
                this.tabControl.TabPages.Add(this.tabReports);
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (registeredUsers.ContainsKey(txtRegisterEmail.Text))
            {
                MessageBox.Show("Email already registered.");
                return;
            }

            registeredUsers.Add(txtRegisterEmail.Text, txtRegisterPassword.Text);
            MessageBox.Show("Registration successful.");
        }

        private void ClearProductFields()
        {
            txtProductName.Text = "Enter Product Name";
            txtProductPrice.Text = "Enter Price";
            txtProductDescription.Text = "Enter Description";
            txtProductStock.Text = "Enter Stock";
            txtProductName.ForeColor = txtProductPrice.ForeColor = txtProductDescription.ForeColor = txtProductStock.ForeColor = Color.Gray;
        }

        private void SimulateReceivingOrders()
        {
            // Example orders
            orderList.Add(new Order { OrderId = "ORD001", ProductName = "Product A", Quantity = 2, IsShipped = false });
            orderList.Add(new Order { OrderId = "ORD002", ProductName = "Product B", Quantity = 1, IsShipped = false });

            foreach (var order in orderList)
            {
                lstOrders.Items.Add(order.OrderId);
            }
        }

        private void SimulateSalesData()
        {
            // Example sales data
            salesList.Add(new Sale { ProductName = "Product A", QuantitySold = 10, Revenue = 100 });
            salesList.Add(new Sale { ProductName = "Product B", QuantitySold = 5, Revenue = 50 });

            foreach (var sale in salesList)
            {
                lstBestSellingProducts.Items.Add($"{sale.ProductName} - Sold: {sale.QuantitySold}");
            }
        }

        private void lstOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOrders.SelectedIndex != -1)
            {
                var selectedOrder = orderList[lstOrders.SelectedIndex];
                txtOrderDetails.Text = $"Order ID: {selectedOrder.OrderId}\nProduct: {selectedOrder.ProductName}\nQuantity: {selectedOrder.Quantity}\nShipped: {selectedOrder.IsShipped}";
            }
        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            if (lstOrders.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an order to print the shipping label.");
                return;
            }

            // Simulate printing the shipping label
            MessageBox.Show($"Shipping label printed for Order ID: {orderList[lstOrders.SelectedIndex].OrderId}");
        }

        private void btnMarkShipped_Click(object sender, EventArgs e)
        {
            if (lstOrders.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an order to mark as shipped.");
                return;
            }

            var selectedOrder = orderList[lstOrders.SelectedIndex];
            selectedOrder.IsShipped = true;
            MessageBox.Show($"Order ID: {selectedOrder.OrderId} marked as shipped.");
            lstOrders.Items[lstOrders.SelectedIndex] = selectedOrder.OrderId; // Refresh the list
            lstOrders_SelectedIndexChanged(sender, e); // Update order details
        }


        private void btnMarkDelivered_Click(object sender, EventArgs e)
        {
            if (lstOrders.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an order to mark as delivered.");
                return;
            }

            var selectedOrder = orderList[lstOrders.SelectedIndex];
            if (!selectedOrder.IsShipped)
            {
                MessageBox.Show("Order must be marked as shipped before it can be marked as delivered.");
                return;
            }

            // Simulate marking the order as delivered
            MessageBox.Show($"Order ID: {selectedOrder.OrderId} marked as delivered.");
            lstOrders.Items.RemoveAt(lstOrders.SelectedIndex); // Remove from the list after delivery
            txtOrderDetails.Clear(); // Clear order details
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            // Calculate total revenue
            decimal totalRevenue = 0;
            foreach (var sale in salesList)
            {
                totalRevenue += sale.Revenue;
            }

            // Display revenue report
            txtRevenueReport.Text = $"Total Revenue: {totalRevenue:C}\n\nBest Selling Products:\n";
            foreach (var sale in salesList)
            {
                txtRevenueReport.AppendText($"{sale.ProductName} - Sold: {sale.QuantitySold}, Revenue: {sale.Revenue:C}\n");
            }
        }

        private class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int Stock { get; set; }
        }

        private class Order
        {
            public string OrderId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public bool IsShipped { get; set; }
        }

        private class Sale
        {
            public string ProductName { get; set; }
            public int QuantitySold { get; set; }
            public decimal Revenue { get; set; }
        }
    }
}