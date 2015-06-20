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
    /// Interaction logic for FrmThemMatSach.xaml
    /// </summary>
    public partial class FrmThemMatSach : Window
    {
        public FrmThemMatSach(int mode,MatSach m)
        {
            InitializeComponent();
            if (mode == 1)
                LoadDataAdd();
            else
                LoadDataView(m);
        }
        void LoadDataAdd()
        {
            QLThuVienEntities ql=new QLThuVienEntities();
            txtDocGia.ItemsSource = ql.TheDocGias.ToList();
            txtNhanVien.ItemsSource = NhanVienBUS.LayBoPhan(1);
            txtSach.ItemsSource = ql.Saches.ToList();
            txtNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtDocGia.SelectedIndex = 0;
            txtNhanVien.SelectedIndex = 0;
            txtSach.SelectedIndex = 0;
            txtTienPhat.MinValue = (txtSach.SelectedItem as Sach).Gia;
            txtTienPhat.EditValue = (txtSach.SelectedItem as Sach).Gia;
            if (Session.GetUser().ID_BoPhan != 0)
            {
                txtNhanVien.IsReadOnly = true;
                txtNhanVien.IsEnabled = false;
                txtNhanVien.EditValue = Session.GetUser().ID;
             
            }
        }

        void LoadDataView(MatSach m)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            txtDocGia.ItemsSource = ql.TheDocGias.ToList();
            txtNhanVien.ItemsSource = ql.NhanViens.Where(i => i.ID_BoPhan == 1).ToList();
            txtSach.ItemsSource = ql.Saches.ToList();
            txtDocGia.EditValue = m.ID_DocGia;
            txtNhanVien.EditValue = m.ID_NhanVien;
            txtNgay.Text = m.Ngay.ToString("dd/MM/yyyy");
            txtSach.EditValue = m.ID_Sach;
            txtTienPhat.EditValue = m.TienPhat;

            txtTienPhat.IsReadOnly = true;
            txtSach.IsReadOnly = true;
            txtDocGia.IsReadOnly = true;
            txtNhanVien.IsReadOnly = true;

            btCancel.Visibility = Visibility.Collapsed;
            btThem.Visibility = Visibility.Collapsed;

            Title = "Thông tin chi tiết";
        }
        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            MatSach ms = new MatSach();
            ms.ID_DocGia = (txtDocGia.SelectedItem as TheDocGia).ID;
            ms.ID_NhanVien = (txtNhanVien.SelectedItem as NhanVien).ID;
            ms.TienPhat =(decimal?) txtTienPhat.EditValue;
            ms.ID_Sach = (txtSach.SelectedItem as Sach).ID;
            ms.Ngay = DateTime.Now;
            ms.State = true;

            QLThuVienEntities ql = new QLThuVienEntities();
            ql.Saches.Where(i => i.ID == ms.ID_Sach).FirstOrDefault().State = false;
            ql.MatSaches.AddObject(ms);
            ql.SaveChanges();
            Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtSach_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            //txtTienPhat.MinValue = (txtSach.SelectedItem as Sach).Gia;
            //txtTienPhat.EditValue = (txtSach.SelectedItem as Sach).Gia;
        }

        private void txtSach_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            txtTienPhat.MinValue = (txtSach.SelectedItem as Sach).Gia;
            txtTienPhat.EditValue = (txtSach.SelectedItem as Sach).Gia;
        }
    }
}
