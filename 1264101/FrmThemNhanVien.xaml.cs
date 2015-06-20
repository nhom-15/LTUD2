using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace De01.TacVu
{
    /// <summary>
    /// Interaction logic for FrmThemNhanVien.xaml
    /// </summary>
    public partial class FrmThemNhanVien : Window
    {
        public delegate void ThemNhanVien(NhanVien nv);
        public ThemNhanVien EventThem;
        public FrmThemNhanVien()
        {
            InitializeComponent();
            txtNgaySinh.EditValue = DateTime.Now;
            QLThuVienEntities ql = new QLThuVienEntities();
            txtBangCap.ItemsSource = ql.BangCaps.ToList();
            txtBangCap.DisplayMemberPath = "TenBC";
            txtBangCap.SelectedValuePath = "ID";
            txtBangCap.SelectedIndex = 0;

            txtBoPhan.ItemsSource = ql.BoPhans.ToList();
            txtBoPhan.DisplayMemberPath = "TenBP";
            txtBoPhan.SelectedValuePath = "ID";
            txtBoPhan.SelectedIndex = 0;

            txtChucVu.ItemsSource = ql.ChucVus.ToList();
            txtChucVu.DisplayMemberPath = "TenCV";
            txtChucVu.SelectedValuePath = "ID";
            txtChucVu.SelectedIndex = 0;
        }

        


        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtHoTen.Text) || txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(txtDiaChi.Text) || txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ không hợp lệ");
                return;
            }
            if ( !Regex.IsMatch(txtSDT.Text,@"^\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }
            if (txtNgaySinh.EditValue==null)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                return;
            }
            NhanVien nv = new NhanVien();
            nv.HoTen = txtHoTen.Text;
            nv.DiaChi = txtDiaChi.Text;
            nv.DienThoai = txtSDT.Text;
            nv.NgaySinh =Convert.ToDateTime( txtNgaySinh.EditValue);
            nv.ID_BangCap = (int)txtBangCap.SelectedValue;
            nv.ID_ChucVu = (int)txtChucVu.SelectedValue;
            nv.ID_BoPhan = (int)txtBoPhan.SelectedValue;
            nv.State = true;
            nv.TaiKhoan = txtTaiKhoan.Text;
            nv.MatKhau = "123456";
            RaiseEvent(nv);
            Close();
        }

        void RaiseEvent(NhanVien nv)
        {
            if (EventThem != null)
                EventThem(nv);
        }
    }
}
