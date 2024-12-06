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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private List<Device> devices = new List<Device>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Nhap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrEmpty(txtMaThietBi.Text) || string.IsNullOrEmpty(txtViTri.Text) || dpNgayBatDau.SelectedDate == null || cbLoaiThietBi.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra trùng mã thiết bị
                if (devices.Any(d => d.MaThietBi == txtMaThietBi.Text))
                {
                    MessageBox.Show("Mã thiết bị không được trùng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra ngày bắt đầu sử dụng
                if (dpNgayBatDau.SelectedDate > DateTime.Now)
                {
                    MessageBox.Show("Ngày bắt đầu sử dụng phải trước hoặc bằng ngày hiện tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Tính số ngày sử dụng
                DateTime ngayBatDau = dpNgayBatDau.SelectedDate.Value;
                int soNgaySuDung = (DateTime.Now - ngayBatDau).Days;

                // Thêm vào danh sách
                var device = new Device
                {
                    MaThietBi = txtMaThietBi.Text,
                    ViTri = txtViTri.Text,
                    NgayBatDau = ngayBatDau.ToString("dd/MM/yyyy"),
                    LoaiThietBi = ((ComboBoxItem)cbLoaiThietBi.SelectedItem).Content.ToString(),
                    SoNgaySuDung = soNgaySuDung
                };
                devices.Add(device);

                // Cập nhật DataGrid
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = devices;

                // Xóa trắng các trường
                txtMaThietBi.Clear();
                txtViTri.Clear();
                dpNgayBatDau.SelectedDate = null;
                cbLoaiThietBi.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Window2_Click(object sender, RoutedEventArgs e)
        {
            var window2 = new Window2(devices);
            window2.Show();
        }
    }
}
