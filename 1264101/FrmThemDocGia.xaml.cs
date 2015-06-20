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
using De01.BUS;

namespace De01.TacVu
{
    /// <summary>
    /// Interaction logic for FrmThemDocGia.xaml
    /// </summary>
    public partial class FrmThemDocGia : Window
    {
        public delegate void ThemDocGia(TheDocGia dg);
        public ThemDocGia EventThem;
        public FrmThemDocGia()
        {
            InitializeComponent();
            txtNgaySinh.EditValue = DateTime.Now;
            QLThuVienEntities ql = new QLThuVienEntities();
            txtLDG.ItemsSource = ql.LoaiDocGias.ToList();
            txtLDG.DisplayMemberPath = "TenLDG";
            txtLDG.SelectedValuePath = "ID";
            txtLDG.SelectedIndex = 0;

            txtNhanVien.ItemsSource = NhanVienBUS.LayBoPhan(1);
            txtNhanVien.DisplayMemberPath = "HoTen";
            txtNhanVien.SelectedValuePath = "ID";
            txtNhanVien.SelectedIndex = 0;
            if (Session.GetUser().ID_BoPhan != 0)
            {
                txtNhanVien.IsReadOnly = true;
                txtNhanVien.IsEnabled = false;
                txtNhanVien.SelectedValue = Session.GetUser().ID;
            }
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
            if (!Regex.IsMatch(txtEmail.Text, @"^[\w.]+@[\w.-]+.[a-zA-Z]$"))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }
            if (txtNgaySinh.EditValue == null)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                return;
            }
            DateTime born=Convert.ToDateTime(txtNgaySinh.EditValue);
            int age=DateTime.Now.Date.Year-born.Date.Year;
            if (DateTime.Now.Date < born.AddYears(age)) age--;
            if (age < 18 || age > 55)
            {
                MessageBox.Show("Tuổi không hợp lệ.\nTừ 18 đến 55.");
                return;
            }
            TheDocGia dg = new TheDocGia();
            dg.HoTen = txtHoTen.Text;
            dg.DiaChi = txtDiaChi.Text;
            dg.Email = txtEmail.Text;
            dg.NgaySinh = Convert.ToDateTime(txtNgaySinh.EditValue);
            dg.ID_LoaiDG = (int)txtLDG.SelectedValue;
            dg.ID_NhanVien = (int)txtNhanVien.SelectedValue;
            dg.NgayLap = DateTime.Now;
            dg.State = true;
            dg.Active = true;
            dg.TienNo = 0;
            dg.MocNgay = DateTime.Now;
            RaiseEvent(dg);
            Close();
        }

        void RaiseEvent(TheDocGia dg)
        {
            if (EventThem != null)
                EventThem(dg);
        }
    }
}
