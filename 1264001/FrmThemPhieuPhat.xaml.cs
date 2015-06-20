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
using DevExpress.Xpf.Editors;
using De01.BUS;

namespace De01.TacVu
{
    /// <summary>
    /// Interaction logic for FrmThemPhieuPhat.xaml
    /// </summary>
    public partial class FrmThemPhieuPhat : Window
    {
        public FrmThemPhieuPhat()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            txtNgayTra.Text = DateTime.Now.ToString("dd/MM/yyyy");
            QLThuVienEntities ql = new QLThuVienEntities();
            txtDocGia.DataContext = ql.TheDocGias.Where(i => i.Active == true && i.State == true && i.TienNo>0).ToList();
            txtNhanVien.ItemsSource = NhanVienBUS.LayBoPhan(3);
            txtNhanVien.SelectedIndex = 0;
            grid1.IsEnabled = false;
            if (Session.GetUser().ID_BoPhan != 0)
            {
                txtNhanVien.IsReadOnly = true;
                txtNhanVien.IsEnabled = false;
                txtNhanVien.SelectedItem = Session.GetUser();
            }

        }
        private void txtDocGia_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            grid1.IsEnabled = true;
            TheDocGia dg = (txtDocGia.SelectedItem as TheDocGia);
            txtTienNo.Text =Convert.ToInt32( dg.TienNo).ToString("#,##0");
            txtTienTra.MaxValue = dg.TienNo;
            txtConLai.Text = Convert.ToInt32(dg.TienNo-(decimal?)txtTienTra.EditValue ).ToString("#,##0");
        }

        private void txtTienTra_EditValueChanging(object sender, EditValueChangingEventArgs e)
        {
            TheDocGia dg = (txtDocGia.SelectedItem as TheDocGia);
            txtConLai.Text = Convert.ToInt32(dg.TienNo - (decimal?)txtTienTra.EditValue).ToString("#,##0");
        }
        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            TheDocGia dg = (txtDocGia.SelectedItem as TheDocGia);
            if (dg == null)
            {
                MessageBox.Show("Dữ liệu trống");
                return;
            }
            PhieuPhat pp = new PhieuPhat();
            pp.ID_DocGia = (txtDocGia.SelectedItem as TheDocGia).ID;
            pp.ID_NhanVien = (txtNhanVien.SelectedItem as NhanVien).ID;
            pp.State = true;
            pp.TienThu =(decimal?) txtTienTra.EditValue;
            pp.ConLai = dg.TienNo - (decimal?)txtTienTra.EditValue;
            pp.Ngay = DateTime.Now;
            QLThuVienEntities ql = new QLThuVienEntities();
            ql.TheDocGias.Where(i => i.ID == pp.ID_DocGia).FirstOrDefault().TienNo -= pp.TienThu;
            ql.PhieuPhats.AddObject(pp);
            ql.SaveChanges();
            Close();
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
