using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CustomerInterfaceApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RegistrationForm());
        }
    }

    

public class RegistrationForm : Form
    {
        private TextBox txtName, txtEmail, txtAddress, txtPayment, txtPassword;
        private Button btnRegister, btnBack;

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "User Registration";
            this.ClientSize = new Size(400, 350);
            this.BackColor = Color.Pink;

            txtName = new TextBox { Location = new Point(150, 30), Width = 200 };
            txtEmail = new TextBox { Location = new Point(150, 70), Width = 200 };
            txtAddress = new TextBox { Location = new Point(150, 110), Width = 200 };
            txtPayment = new TextBox { Location = new Point(150, 150), Width = 200 };
            txtPassword = new TextBox { Location = new Point(150, 190), Width = 200, UseSystemPasswordChar = true };

            btnRegister = new Button { Text = "Register", Location = new Point(150, 230), BackColor = Color.Gold, Size = new Size(100, 40) };
            btnRegister.Click += (s, e) =>
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Email and Password are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!LoginForm.RegisteredUsers.ContainsKey(email))
                {
                    LoginForm.RegisteredUsers[email] = password;
                    MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    new LoginForm().Show();
                }
                else
                {
                    MessageBox.Show("This email is already registered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnBack = new Button { Text = "Back", Location = new Point(50, 290), Size = new Size(100, 40) };
            btnBack.Click += (s, e) => { this.Hide(); new LoginForm().Show(); };

            this.Controls.Add(new Label { Text = "Name:", Location = new Point(50, 30), Width = 100 });
            this.Controls.Add(new Label { Text = "Email:", Location = new Point(50, 70), Width = 100 });
            this.Controls.Add(new Label { Text = "Address:", Location = new Point(50, 110), Width = 100 });
            this.Controls.Add(new Label { Text = "Payment:", Location = new Point(50, 150), Width = 100 });
            this.Controls.Add(new Label { Text = "Password:", Location = new Point(50, 190), Width = 100 });

            this.Controls.Add(txtName);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtAddress);
            this.Controls.Add(txtPayment);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnRegister);
            this.Controls.Add(btnBack);
        }
    }

    public class LoginForm : Form
    {
        public static Dictionary<string, string> RegisteredUsers = new Dictionary<string, string>();

        private TextBox txtEmail, txtPassword;
        private Button btnLogin, btnBack;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Login";
            this.ClientSize = new Size(400, 300);
            this.BackColor = Color.Pink;

            txtEmail = new TextBox { Location = new Point(150, 30), Width = 200 };
            txtPassword = new TextBox { Location = new Point(150, 70), Width = 200, UseSystemPasswordChar = true };

            btnLogin = new Button { Text = "Login", Location = new Point(150, 110), BackColor = Color.Gold, Size = new Size(100, 40) };
            btnLogin.Click += (s, e) =>
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (RegisteredUsers.ContainsKey(email) && RegisteredUsers[email] == password)
                {
                    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    new ProductSearchForm().Show();
                }
                else
                {
                    MessageBox.Show("Invalid Email or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnBack = new Button { Text = "Back", Location = new Point(50, 250), Size = new Size(100, 40) };
            btnBack.Click += (s, e) => { this.Hide(); new RegistrationForm().Show(); };

            this.Controls.Add(new Label { Text = "Email:", Location = new Point(50, 30), Width = 100 });
            this.Controls.Add(new Label { Text = "Password:", Location = new Point(50, 70), Width = 100 });

            this.Controls.Add(txtEmail);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnBack);
        }
    }




    // Product Search Form
    public class ProductSearchForm : Form
    {
        private TextBox txtSearch;
        private ComboBox cmbCategory, cmbBrand, cmbShippingOptions;
        private NumericUpDown nudMinPrice, nudMaxPrice;
        private ListBox lstProducts;
        private Button btnSearch, btnAddToCart, btnBack, btnNext;

        private List<Product> products = new List<Product> {
        new Product("Laptop", "Electronics", 1000, "Brand A"),
        new Product("Smartphone", "Electronics", 800, "Brand B"),
        new Product("TV", "Electronics", 1200, "Brand A"),
        new Product("Headphones", "Electronics", 200, "Brand C"),
        new Product("Sofa", "Furniture", 500, "Brand B"),
        new Product("Table", "Furniture", 150, "Brand A"),
        new Product("Shirt", "Clothing", 50, "Brand C"),
        new Product("Shoes", "Clothing", 80, "Brand B")
    };

        private List<string> cart = new List<string>();

        public ProductSearchForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Product Search";
            this.ClientSize = new Size(600, 600);
            this.BackColor = Color.Pink;

            // Textbox for search
            txtSearch = new TextBox { Location = new Point(150, 20), Width = 300 };

            // ComboBox for Category
            cmbCategory = new ComboBox { Location = new Point(150, 60), Width = 300 };
            cmbCategory.Items.AddRange(new string[] { "All Products", "Electronics", "Furniture", "Clothing" });
            cmbCategory.SelectedIndex = 0;

            // ComboBox for Brand
            cmbBrand = new ComboBox { Location = new Point(150, 100), Width = 300 };
            cmbBrand.Items.AddRange(new string[] { "All Brands", "Brand A", "Brand B", "Brand C" });
            cmbBrand.SelectedIndex = 0;

            // NumericUpDown for Price Range
            nudMinPrice = new NumericUpDown { Location = new Point(150, 140), Width = 100, Minimum = 0, Maximum = 10000, Value = 0 };
            nudMaxPrice = new NumericUpDown { Location = new Point(260, 140), Width = 100, Minimum = 0, Maximum = 10000, Value = 10000 };

            // ComboBox for Shipping Options
            cmbShippingOptions = new ComboBox { Location = new Point(150, 180), Width = 300 };
            cmbShippingOptions.Items.AddRange(new string[] { "Standard", "Express", "Overnight" });

            // Search Button
            btnSearch = new Button { Text = "Search", Location = new Point(150, 220), Size = new Size(100, 40), BackColor = Color.Gold };
            btnSearch.Click += (s, e) => PerformSearch();

            // Product List
            lstProducts = new ListBox { Location = new Point(50, 280), Size = new Size(500, 200) };

            // Add to Cart Button
            btnAddToCart = new Button { Text = "Add to Cart", Location = new Point(50, 500), Size = new Size(100, 40) };
            btnAddToCart.Click += (s, e) =>
            {
                foreach (var item in lstProducts.SelectedItems)
                {
                    cart.Add(item.ToString());
                }
                MessageBox.Show("Product(s) added to cart.");
            };

            // Back Button
            btnBack = new Button { Text = "Back", Location = new Point(50, 550), Size = new Size(100, 40) };
            btnBack.Click += (s, e) => { this.Hide(); new LoginForm().Show(); };

            // Next Button
            btnNext = new Button { Text = "Next", Location = new Point(450, 550), Size = new Size(100, 40) };
            btnNext.Click += (s, e) => { this.Hide(); new ShoppingCartForm(cart).Show(); };

            // Labels
            this.Controls.Add(new Label { Text = "Search:", Location = new Point(50, 20), Width = 100 });
            this.Controls.Add(new Label { Text = "Category:", Location = new Point(50, 60), Width = 100 });
            this.Controls.Add(new Label { Text = "Brand:", Location = new Point(50, 100), Width = 100 });
            this.Controls.Add(new Label { Text = "Price Range:", Location = new Point(50, 140), Width = 100 });
            this.Controls.Add(new Label { Text = "Shipping:", Location = new Point(50, 180), Width = 100 });

            // Adding Controls
            this.Controls.Add(txtSearch);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(cmbBrand);
            this.Controls.Add(nudMinPrice);
            this.Controls.Add(nudMaxPrice);
            this.Controls.Add(cmbShippingOptions);
            this.Controls.Add(btnSearch);
            this.Controls.Add(lstProducts);
            this.Controls.Add(btnAddToCart);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnNext);
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.ToLower();
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "All Products";
            string selectedBrand = cmbBrand.SelectedItem?.ToString() ?? "All Brands";
            decimal minPrice = nudMinPrice.Value;
            decimal maxPrice = nudMaxPrice.Value;

            if (minPrice > maxPrice)
            {
                MessageBox.Show("Minimum price cannot be greater than Maximum price!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var filteredProducts = products.Where(p =>
                (string.IsNullOrEmpty(searchTerm) || p.Name.ToLower().Contains(searchTerm)) &&
                (selectedCategory == "All Products" || p.Category == selectedCategory) &&
                (selectedBrand == "All Brands" || p.Brand == selectedBrand) &&
                p.Price >= minPrice &&
                p.Price <= maxPrice).ToList();

            lstProducts.Items.Clear();
            if (filteredProducts.Any())
            {
                lstProducts.Items.AddRange(filteredProducts.Select(p => $"{p.Name} - {p.Price:C}").ToArray());
            }
            else
            {
                lstProducts.Items.Add("No products found.");
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }

        public Product(string name, string category, decimal price, string brand)
        {
            Name = name;
            Category = category;
            Price = price;
            Brand = brand;
        }
    }


    // Shopping Cart Form
    public class ShoppingCartForm : Form
    {
        private ListBox lstCart;
        private Button btnRemoveFromCart, btnBack, btnNext, btnPayment;
        private Label lblTotalAmount;
        private List<string> cart;
        private Dictionary<string, decimal> productPrices;
        private decimal totalAmount;
        private static List<string> orderHistory = new List<string>();  // Static list to maintain order history

        public ShoppingCartForm(List<string> cart)
        {
            this.cart = cart;
            this.productPrices = InitializeProductPrices();
            this.totalAmount = CalculateTotalAmount();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Shopping Cart";
            this.ClientSize = new Size(500, 500);
            this.BackColor = Color.Pink;

            lstCart = new ListBox { Location = new Point(50, 20), Size = new Size(400, 200) };
            lstCart.Items.AddRange(cart.ToArray());
            lstCart.SelectedIndexChanged += LstCart_SelectedIndexChanged;  // Add the event handler for item click

            lblTotalAmount = new Label
            {
                Text = $"Total Amount: ${totalAmount}",
                Location = new Point(50, 230),
                Size = new Size(200, 30),
                ForeColor = Color.Black
            };

            btnRemoveFromCart = new Button { Text = "Remove from Cart", Location = new Point(50, 270), Size = new Size(150, 40) };
            btnRemoveFromCart.Click += (s, e) =>
            {
                if (lstCart.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select an item to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var item in lstCart.SelectedItems)
                {
                    cart.Remove(item.ToString());
                    totalAmount -= productPrices[item.ToString()];
                }
                lstCart.Items.Clear();
                lstCart.Items.AddRange(cart.ToArray());
                lblTotalAmount.Text = $"Total Amount: ${totalAmount}";
                MessageBox.Show("Product(s) removed from cart.");
            };

            btnPayment = new Button { Text = "Proceed to Payment", Location = new Point(50, 330), Size = new Size(150, 40) };
            btnPayment.Click += (s, e) =>
            {
                if (cart.Count == 0)
                {
                    MessageBox.Show("Cart is empty. Please add products to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Pass totalAmount and cart to the Payment Form
                this.Hide();
                new ProceedToPaymentForm(totalAmount, cart, orderHistory).Show();
            };

            btnBack = new Button { Text = "Back", Location = new Point(50, 390), Size = new Size(100, 40) };
            btnBack.Click += (s, e) => { this.Hide(); new ProductSearchForm().Show(); };

            btnNext = new Button { Text = "Order History", Location = new Point(350, 390), Size = new Size(100, 40) };
            btnNext.Click += (s, e) =>
            {
                // Show order history
                if (orderHistory.Count == 0)
                {
                    MessageBox.Show("No order history available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Hide();
                new OrderHistoryForm(orderHistory).Show();
            };

            this.Controls.Add(new Label { Text = "Your Cart:", Location = new Point(50, 0), Width = 100 });
            this.Controls.Add(lstCart);
            this.Controls.Add(lblTotalAmount);
            this.Controls.Add(btnRemoveFromCart);
            this.Controls.Add(btnPayment);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnNext);
        }

        private void LstCart_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the total amount on each selection change
            totalAmount = 0;
            foreach (var item in lstCart.Items)
            {
                if (productPrices.ContainsKey(item.ToString()))
                {
                    totalAmount += productPrices[item.ToString()];
                }
            }
            lblTotalAmount.Text = $"Total Amount: ${totalAmount}";  // Update the total amount label
        }

        private Dictionary<string, decimal> InitializeProductPrices()
        {
            return new Dictionary<string, decimal>
        {
            { "Laptop", 1000 },
            { "Smartphone", 700 },
            { "TV", 600 },
            { "Headphones", 100 },
            { "Washing Machine", 800 },
            { "Refrigerator", 1200 },
            { "Sofa", 400 },
            { "Table", 150 },
            { "Shirt", 30 },
            { "Shoes", 50 }
        };
        }

        private decimal CalculateTotalAmount()
        {
            decimal amount = 0;
            foreach (var item in cart)
            {
                if (productPrices.ContainsKey(item))
                {
                    amount += productPrices[item];
                }
            }
            return amount;
        }
    }



    public class ProceedToPaymentForm : Form
    {
        private decimal totalAmount;
        private List<string> cart;
        private List<string> orderHistory;

        public ProceedToPaymentForm(decimal totalAmount, List<string> cart, List<string> orderHistory)
        {
            this.totalAmount = totalAmount;
            this.cart = cart;
            this.orderHistory = orderHistory;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Proceed to Payment";
            this.ClientSize = new Size(400, 300);
            this.BackColor = Color.LightGreen;

            Label lblTotalAmount = new Label
            {
                Text = $"Total Amount: ${totalAmount}",
                Location = new Point(50, 50),
                Size = new Size(200, 30)
            };

            Button btnPay = new Button { Text = "Pay", Location = new Point(50, 100), Size = new Size(100, 40) };
            btnPay.Click += (s, e) =>
            {
                MessageBox.Show($"Payment of ${totalAmount} successful. Thank you for shopping!", "Payment Successful", MessageBoxButtons.OK);
                orderHistory.Add($"Purchased items: {string.Join(", ", cart)} for ${totalAmount}");
                this.Hide();
                new ShoppingCartForm(cart).Show();  // Go back to the shopping cart
            };

            Button btnBack = new Button { Text = "Back", Location = new Point(200, 100), Size = new Size(100, 40) };
            btnBack.Click += (s, e) =>
            {
                this.Hide();
                new ShoppingCartForm(cart).Show();  // Go back to the cart
            };

            this.Controls.Add(lblTotalAmount);
            this.Controls.Add(btnPay);
            this.Controls.Add(btnBack);
        }
    }
    public class OrderHistoryForm : Form
    {
        private List<string> orderHistory;

        public OrderHistoryForm(List<string> orderHistory)
        {
            this.orderHistory = orderHistory;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Order History";
            this.ClientSize = new Size(400, 300);
            this.BackColor = Color.LightYellow;

            ListBox lstOrderHistory = new ListBox
            {
                Location = new Point(50, 50),
                Size = new Size(300, 150)
            };

            lstOrderHistory.Items.AddRange(orderHistory.ToArray());

            Button btnBack = new Button { Text = "Back", Location = new Point(150, 220), Size = new Size(100, 40) };
            btnBack.Click += (s, e) =>
            {
                this.Hide();
                new ShoppingCartForm(new List<string>()).Show();  
            };

            this.Controls.Add(lstOrderHistory);
            this.Controls.Add(btnBack);
        }
    }


}
