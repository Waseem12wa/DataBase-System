using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogisticsShipping
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Logistics and Shipping Interface";
            this.Size = new Size(600, 500);
            this.BackColor = Color.LightPink;

            Label titleLabel = new Label()
            {
                Text = "Logistics and Shipping Interface",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(120, 20),
                AutoSize = true
            };

            Button orderAssignmentButton = new Button()
            {
                Text = "Order Assignment and Tracking",
                Location = new Point(150, 100),
                Width = 300
            };
            orderAssignmentButton.Click += (sender, e) =>
            {
                var assignOrderForm = new AssignOrderForm();
                assignOrderForm.Show();
                this.Hide();
            };

            Button deliveryNotificationsButton = new Button()
            {
                Text = "Delivery Notifications",
                Location = new Point(150, 160),
                Width = 300
            };
            deliveryNotificationsButton.Click += (sender, e) =>
            {
                var notificationsForm = new DeliveryNotificationsForm();
                notificationsForm.Show();
                this.Hide();
            };

            Button shipmentSchedulingButton = new Button()
            {
                Text = "Shipment Scheduling",
                Location = new Point(150, 220),
                Width = 300
            };
            shipmentSchedulingButton.Click += (sender, e) =>
            {
                var schedulingForm = new ScheduleDeliveryForm();
                schedulingForm.Show();
                this.Hide();
            };

            Button exitButton = new Button()
            {
                Text = "Exit",
                Location = new Point(150, 280),
                Width = 300
            };
            exitButton.Click += (sender, e) => Application.Exit();

            this.Controls.Add(titleLabel);
            this.Controls.Add(orderAssignmentButton);
            this.Controls.Add(deliveryNotificationsButton);
            this.Controls.Add(shipmentSchedulingButton);
            this.Controls.Add(exitButton);
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }

    public class AssignOrderForm : Form
    {
        public AssignOrderForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Logistics and Shipping Interface - Order Assignment";
            this.Size = new Size(600, 500);
            this.BackColor = Color.LightBlue;

            Label titleLabel = new Label()
            {
                Text = "Order Assignment and Tracking",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(150, 20),
                AutoSize = true
            };

            Label orderIdLabel = new Label() { Text = "Order ID:", Location = new Point(20, 100) };
            TextBox orderIdTextBox = new TextBox() { Location = new Point(150, 100), Width = 200 };
            Label agentLabel = new Label() { Text = "Delivery Agent:", Location = new Point(20, 140) };
            TextBox agentTextBox = new TextBox() { Location = new Point(150, 140), Width = 200 };

            Button assignButton = new Button()
            {
                Text = "Assign Order",
                Location = new Point(150, 200),
                Width = 150
            };
            assignButton.Click += (sender, e) =>
            {
                string orderId = orderIdTextBox.Text;
                string agent = agentTextBox.Text;

                if (!string.IsNullOrWhiteSpace(orderId) && !string.IsNullOrWhiteSpace(agent))
                {
                    MessageBox.Show($"Order {orderId} assigned to {agent}.", "Success");
                }
                else
                {
                    MessageBox.Show("Please enter valid Order ID and Delivery Agent.", "Error");
                }
            };

            Button backButton = new Button()
            {
                Text = "Back",
                Location = new Point(20, 400),
                Width = 100
            };
            backButton.Click += (sender, e) =>
            {
                var mainMenu = new MainMenuForm();
                mainMenu.Show();
                this.Hide();
            };

            this.Controls.Add(titleLabel);
            this.Controls.Add(orderIdLabel);
            this.Controls.Add(orderIdTextBox);
            this.Controls.Add(agentLabel);
            this.Controls.Add(agentTextBox);
            this.Controls.Add(assignButton);
            this.Controls.Add(backButton);
        }
    }

    public class DeliveryNotificationsForm : Form
    {
        public DeliveryNotificationsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Logistics and Shipping Interface - Delivery Notifications";
            this.Size = new Size(600, 500);
            this.BackColor = Color.LightYellow;

            Label titleLabel = new Label()
            {
                Text = "Delivery Notifications",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(180, 20),
                AutoSize = true
            };

            Button notifyButton = new Button()
            {
                Text = "Send Notifications",
                Location = new Point(150, 150),
                Width = 200
            };
            notifyButton.Click += (sender, e) =>
            {
                MessageBox.Show("Delivery notifications sent successfully.", "Success");
            };

            Button backButton = new Button()
            {
                Text = "Back",
                Location = new Point(20, 400),
                Width = 100
            };
            backButton.Click += (sender, e) =>
            {
                var mainMenu = new MainMenuForm();
                mainMenu.Show();
                this.Hide();
            };

            this.Controls.Add(titleLabel);
            this.Controls.Add(notifyButton);
            this.Controls.Add(backButton);
        }
    }

    public class ScheduleDeliveryForm : Form
    {
        public ScheduleDeliveryForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Logistics and Shipping Interface - Shipment Scheduling";
            this.Size = new Size(600, 500);
            this.BackColor = Color.LightGreen;

            Label titleLabel = new Label()
            {
                Text = "Shipment Scheduling",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(180, 20),
                AutoSize = true
            };

            Label dateLabel = new Label() { Text = "Select Date:", Location = new Point(20, 100) };
            DateTimePicker datePicker = new DateTimePicker() { Location = new Point(150, 100), Width = 200 };
            Label timeLabel = new Label() { Text = "Select Time:", Location = new Point(20, 140) };
            DateTimePicker timePicker = new DateTimePicker()
            {
                Location = new Point(150, 140),
                Width = 200,
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true
            };

            Button scheduleButton = new Button()
            {
                Text = "Schedule",
                Location = new Point(150, 200),
                Width = 150
            };
            scheduleButton.Click += (sender, e) =>
            {
                string date = datePicker.Text;
                string time = timePicker.Text;
                MessageBox.Show($"Scheduled for {date} at {time}.", "Success");
            };

            Button backButton = new Button()
            {
                Text = "Back",
                Location = new Point(20, 400),
                Width = 100
            };
            backButton.Click += (sender, e) =>
            {
                var mainMenu = new MainMenuForm();
                mainMenu.Show();
                this.Hide();
            };

            this.Controls.Add(titleLabel);
            this.Controls.Add(dateLabel);
            this.Controls.Add(datePicker);
            this.Controls.Add(timeLabel);
            this.Controls.Add(timePicker);
            this.Controls.Add(scheduleButton);
            this.Controls.Add(backButton);
        }
    }
}
