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
    public partial class EditNhanVien : Window
    {
        private Employee _employee;

        public EditNhanVien(Employee employee)
        {
            InitializeComponent();
            if (employee == null)
            {
                MessageBox.Show("Không có dữ liệu nhân viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            _employee = employee;
            DataContext = _employee;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QlksContext())
            {
                var existingEmployee = db.Employees.Find(_employee.EmployeeId);
                if (existingEmployee != null)
                {
                    existingEmployee.FullName = txtFullName.Text;
                    existingEmployee.Position = txtPosition.Text;
                    existingEmployee.Phone = txtPhone.Text;
                    existingEmployee.Email = txtEmail.Text;
                    existingEmployee.Address = txtAddress.Text;
                    existingEmployee.HireDate = dpHireDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpHireDate.SelectedDate.Value) : (DateOnly?)null; // Fix: Convert DateTime to DateOnly
                    db.SaveChanges();
                }
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