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
using DevExpress.Xpf.Grid;

namespace De01.TacVu
{
    /// <summary>
    /// Interaction logic for FrmThemPhieuMuon.xaml
    /// </summary>
    public partial class FrmThemPhieuMuon : Window
    {
  
        List<string> listID;
        int quantity;
        bool IsNew;
        DateTime mocngay;
        public FrmThemPhieuMuon(int mode,string id)
        {
            InitializeComponent();
            if (mode == 1)
                LoadDataAdd();
            else
                LoadDataView(id);
            
        }
        void LoadDataAdd()
        {
            txtNgayMuon.Text = DateTime.Now.ToString("dd/MM/yyyy");
            QLThuVienEntities ql=new QLThuVienEntities();
            txtDocGia.DataContext = ql.TheDocGias.Where(i => i.Active == true && i.State == true).ToList();
            gridControl1.ItemsSource = ql.Saches.Where(i => i.State == true && i.TinhTrang==true).ToList();
            gridControl1.IsEnabled = false;
            IsNew = false;
            listID = new List<string>();
            quantity = 0;
            Title = "Thêm phiếu mượn";
        }
        void LoadDataView(string id)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            PhieuMuon pm = ql.PhieuMuons.Where(i => i.ID == id).FirstOrDefault();
            txtNgayMuon.Text = pm.NgayMuon.ToString("dd/MM/yyyy");
            txtDocGia.DataContext = ql.TheDocGias.Where(i => i.ID==pm.ID_DocGia).ToList();
            txtDocGia.SelectedIndex = 0;
            txtDocGia.IsReadOnly = true;
            colCheck.Visible = false ;
            List<Sach> ls = new List<Sach>();
            List<ChiTietMuon> l = ql.ChiTietMuons.Where(i => i.ID_PhieuMuon == id).ToList();
            foreach (ChiTietMuon ctm in l)
                ls.Add(ctm.Sach);
            gridControl1.ItemsSource = ls;
            btThem.Visibility = Visibility.Collapsed;
         
            Title = "Chi tiết phiêu mượn";
        }
        private void CheckEdit_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            CheckEdit bt = sender as CheckEdit;
            EditGridCellData cell = bt.Tag as EditGridCellData;
            string id = (cell.RowData.Row as Sach).ID;
            TheDocGia dg = txtDocGia.SelectedItem as TheDocGia;
            if ((bool)e.NewValue)
            {
                string it = listID.Where(i => i == id).FirstOrDefault();

                if (it == null)
                {
                    if (dg.LoaiDocGia.SoLuong <= quantity)
                    {
                        e.IsCancel = true;
                        e.Handled = false;
                        MessageBox.Show("Số lượng mượn không vượt quá " + dg.LoaiDocGia.SoLuong.ToString(), "Thông báo");
                        return;
                    }

                    listID.Add(id);
                    quantity++;
                }
            }
            else
            {
                string it = listID.Where(i => i == id).FirstOrDefault();
                if (it != null)
                {
                    listID.Remove(id);
                    quantity--;
                }

            }
        }

        private void txtDocGia_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            gridControl1.IsEnabled = true;
            quantity = 0;
            TheDocGia dg=txtDocGia.SelectedItem as TheDocGia;
            QLThuVienEntities ql=new QLThuVienEntities();
            DateTime neart=new DateTime(1,1,1);
            List<PhieuMuon> lpm= ql.PhieuMuons.Where(i=>i.ID_DocGia==dg.ID).ToList();
            if (lpm.Count == 0)
            {
                IsNew = true;
                return;
            }
            else
            {
                foreach (PhieuMuon pm in lpm)
                {
                    if (neart.CompareTo(pm.MocNgay) == -1)
                        neart = pm.MocNgay;
                }
                if ((DateTime.Now - neart).Days > dg.LoaiDocGia.SoNgay)
                {
                    IsNew = true;
                    quantity = 0;
                    return;
                }
                mocngay = neart;
                lpm = lpm.Where(i => i.MocNgay == neart).ToList();
                foreach (PhieuMuon pm in lpm)
                    quantity += ql.ChiTietMuons.Where(i => i.ID_PhieuMuon == pm.ID).ToList().Count;
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (listID.Count == 0)
            {
                MessageBox.Show("Dữ liệu trống");
                return;
            }
            QLThuVienEntities ql=new QLThuVienEntities();
            PhieuMuon pm=new PhieuMuon();
            if(IsNew)
            {
                pm.MocNgay=DateTime.Now;
            }
            else
                pm.MocNgay=mocngay;
            pm.ID_DocGia = (txtDocGia.SelectedItem as TheDocGia).ID ;
            pm.NgayMuon=DateTime.Now;
            pm.State=true;
            pm.ID=CreateID();
            pm.TinhTrang = false;
            ql.PhieuMuons.AddObject(pm);
            ql.SaveChanges();
            foreach (string id in listID)
            {
                ChiTietMuon ctm = new ChiTietMuon();
                ctm.ID_PhieuMuon = pm.ID;
                ctm.ID_Sach = id;
                Sach s = ql.Saches.Where(i => i.ID == id).FirstOrDefault();
                s.TinhTrang = false;
                ql.ChiTietMuons.AddObject(ctm);
            }
            ql.SaveChanges();
            Close();
        }
        string CreateID()
        {
            QLThuVienEntities ql=new QLThuVienEntities();
            int max = 0;
            List<PhieuMuon> l=ql.PhieuMuons.ToList();
            foreach (PhieuMuon pm in l)
            {
                int num = Convert.ToInt32(pm.ID.Remove(0, 2));
                if (num > max)
                    max = num;
            }
            return "PM" + (max + 1).ToString();
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckEdit_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            
        }
    }
}
