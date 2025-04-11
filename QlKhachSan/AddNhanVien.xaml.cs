using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QlKhachSan.Models;

namespace QlKhachSan
{
    public partial class AddNhanVien : Window
    {
        private Employee _employee;

        public bool IsAdded { get; private set; }

        public AddNhanVien()
        {
            InitializeComponent();
            _employee = new Employee();
            DataContext = _employee;
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var db = new QlksContext())
            {
                var users = db.Users.ToList();
                cmbUsers.ItemsSource = users;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QlksContext())
            {
                var newEmployee = new Employee
                {
                    FullName = txtFullName.Text,
                    Position = txtPosition.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,
                    HireDate = dpHireDate.SelectedDate.HasValue
                        ? new DateOnly(dpHireDate.SelectedDate.Value.Year, dpHireDate.SelectedDate.Value.Month, dpHireDate.SelectedDate.Value.Day)
                        : (DateOnly?)null,
                    UserId = cmbUsers.SelectedValue != null ? (int)cmbUsers.SelectedValue : 0
                };

                db.Employees.Add(newEmployee);
                db.SaveChanges();
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}