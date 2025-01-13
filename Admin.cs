using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdminInterface
{
    public class AdminForm : Form
    {
        public AdminForm()
        {
            // Initialize Admin Interface
            InitializeAdminInterface();
        }

        private void InitializeAdminInterface()
        {
            // Create a TabControl for Admin Interface
            TabControl adminTabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // Create tabs for each functionality
            TabPage userSellerManagementTab = CreateTabWithPinkBackground("User & Seller Management");
            TabPage productCategoryManagementTab = CreateTabWithPinkBackground("Product & Category Management");
            TabPage orderOversightTab = CreateTabWithPinkBackground("Order Oversight");
            TabPage reportsAnalyticsTab = CreateTabWithPinkBackground("Reports & Analytics");
            TabPage feedbackModerationTab = CreateTabWithPinkBackground("Feedback Moderation");

            // Add buttons to "User & Seller Management" tab
            Button btnApproveSeller = new Button { Text = "Approve New Seller", Location = new Point(20, 20) };
            Button btnManageAccounts = new Button { Text = "Manage Accounts", Location = new Point(20, 60) };
            Button btnMonitorSuspiciousActivity = new Button { Text = "Monitor Suspicious Activity", Location = new Point(20, 100) };

            btnApproveSeller.Click += (s, e) => MessageBox.Show("Seller Approved!");
            btnManageAccounts.Click += (s, e) => MessageBox.Show("Managing Accounts...");
            btnMonitorSuspiciousActivity.Click += (s, e) => MessageBox.Show("Monitoring Suspicious Activity...");

            userSellerManagementTab.Controls.Add(btnApproveSeller);
            userSellerManagementTab.Controls.Add(btnManageAccounts);
            userSellerManagementTab.Controls.Add(btnMonitorSuspiciousActivity);

            // Add buttons to "Product & Category Management" tab
            Button btnModerateProducts = new Button { Text = "Moderate Products", Location = new Point(20, 20) };
            Button btnManageCategories = new Button { Text = "Manage Categories", Location = new Point(20, 60) };

            btnModerateProducts.Click += (s, e) => MessageBox.Show("Moderating Products...");
            btnManageCategories.Click += (s, e) => MessageBox.Show("Managing Categories...");

            productCategoryManagementTab.Controls.Add(btnModerateProducts);
            productCategoryManagementTab.Controls.Add(btnManageCategories);

            // Add buttons to "Order Oversight" tab
            Button btnMonitorOrders = new Button { Text = "Monitor Orders", Location = new Point(20, 20) };
            Button btnResolveConflicts = new Button { Text = "Resolve Conflicts", Location = new Point(20, 60) };

            btnMonitorOrders.Click += (s, e) => MessageBox.Show("Monitoring Orders...");
            btnResolveConflicts.Click += (s, e) => MessageBox.Show("Resolving Conflicts...");

            orderOversightTab.Controls.Add(btnMonitorOrders);
            orderOversightTab.Controls.Add(btnResolveConflicts);

            // Add buttons to "Reports & Analytics" tab
            Button btnGenerateReport = new Button { Text = "Generate Report", Location = new Point(20, 20) };

            btnGenerateReport.Click += (s, e) => MessageBox.Show("Generating Report...");

            reportsAnalyticsTab.Controls.Add(btnGenerateReport);

            // Add buttons to "Feedback Moderation" tab
            Button btnMonitorReviews = new Button { Text = "Monitor Reviews", Location = new Point(20, 20) };
            Button btnFlagInappropriate = new Button { Text = "Flag Inappropriate Content", Location = new Point(20, 60) };

            btnMonitorReviews.Click += (s, e) => MessageBox.Show("Monitoring Reviews...");
            btnFlagInappropriate.Click += (s, e) => MessageBox.Show("Flagging Inappropriate Content...");

            feedbackModerationTab.Controls.Add(btnMonitorReviews);
            feedbackModerationTab.Controls.Add(btnFlagInappropriate);

            // Add all tabs to TabControl
            adminTabControl.TabPages.Add(userSellerManagementTab);
            adminTabControl.TabPages.Add(productCategoryManagementTab);
            adminTabControl.TabPages.Add(orderOversightTab);
            adminTabControl.TabPages.Add(reportsAnalyticsTab);
            adminTabControl.TabPages.Add(feedbackModerationTab);

            // Add TabControl to the Form
            this.Controls.Add(adminTabControl);
        }

        // Helper method to create a TabPage with a pink background
        private TabPage CreateTabWithPinkBackground(string title)
        {
            return new TabPage
            {
                Text = title,
                BackColor = Color.Pink // Set the background color to pink
            };
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AdminForm());
        }
    }
}
