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
using De01.BUS;

namespace De01.TacVu
{
    /// <summary>
    /// Interaction logic for FrmThemSach.xaml
    /// </summary>
    public partial class FrmThemSach : Window
    {
        public delegate void ThemSach(Sach s);
        public ThemSach EventThem;
        public FrmThemSach()
        {
            InitializeComponent();
            QLThuVienEntities ql = new QLThuVienEntities();

            txtTheLoai.ItemsSource = ql.TheLoais.ToList();
            txtTheLoai.DisplayMemberPath = "TenTL";
            txtTheLoai.SelectedValuePath = "ID";
            txtTheLoai.SelectedIndex = 0;

            txtNhanVien.ItemsSource = NhanVienBUS.LayBoPhan(2);
            txtNhanVien.DisplayMemberPath = "HoTen";
            txtNhanVien.SelectedValuePath = "ID";
            txtNhanVien.SelectedIndex = 0;
            if (Session.GetUser().ID_BoPhan != 0)
            {
                txtNhanVien.IsReadOnly = true;
                txtNhanVien.SelectedValue = Session.GetUser().ID;
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtTenSach.Text) || txtTenSach.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(txtTacGia.Text) || txtTenSach.Text.Trim() == "")
            {
                MessageBox.Show("Tác giả không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(txtNXB.Text) || txtNXB.Text.Trim() == "")
            {
                MessageBox.Show("NXB không hợp lệ");
                return;
            }
            if (DateTime.Now.Year - Convert.ToInt32(txtNamXB.EditValue) > 9)
            {
                MessageBox.Show("Năm XB không hợp lệ");
                return;
            }
            Sach sa = new Sach();
            sa.TenSach = txtTenSach.Text;
            sa.TacGia = txtTacGia.Text;
            sa.NXB = txtNXB.Text;
            sa.NamXB =Convert.ToInt32( txtNamXB.EditValue);
            sa.Gia = Convert.ToInt32(txtGia.EditValue);
            sa.ID_NhanVien =(int) txtNhanVien.SelectedValue;
            sa.ID_TheLoai =(int) txtTheLoai.SelectedValue;
            sa.NgayNhap = DateTime.Now;
            sa.ID =AllFunc.GetMaxID(sa.ID_TheLoai);
            sa.State = true;
            sa.TinhTrang = true;
            RaiseEvent(sa);
            Close();
        }
        void RaiseEvent(Sach sa)
        {
            if (EventThem != null)
                EventThem(sa);
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
    }
}
