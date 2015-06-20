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
    /// Interaction logic for FrmThemPhieuTra.xaml
    /// </summary>
    public partial class FrmThemPhieuTra : Window
    {
        List<PhieuMuon> listID;
        public  int count = 0;
        public FrmThemPhieuTra()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            txtNgayTra.Text = DateTime.Now.ToString("dd/MM/yyyy");
            QLThuVienEntities ql = new QLThuVienEntities();
            txtDocGia.DataContext = ql.TheDocGias.Where(i => i.Active == true && i.State == true).ToList();
            
            gridControl1.IsEnabled = false;
            listID = new List<PhieuMuon>();
           
        }
        private void CheckEdit_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e)
        {
            CheckEdit bt = sender as CheckEdit;
            EditGridCellData cell = bt.Tag as EditGridCellData;
            PhieuMuon id = (cell.RowData.Row as PhieuMuon);
            TheDocGia dg = txtDocGia.SelectedItem as TheDocGia;
            if ((bool)e.NewValue)
            {
                    listID.Add(id);
            }
            else
            {
                PhieuMuon it = listID.Where(i => i.ID == id.ID).FirstOrDefault();
                if (it != null)
                {
                    listID.Remove(id);
                }

            }
        }

        private void txtDocGia_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            gridControl1.IsEnabled = true;
            QLThuVienEntities ql=new QLThuVienEntities();
            int id = (txtDocGia.SelectedItem as TheDocGia).ID;
            gridControl1.ItemsSource = ql.PhieuMuons.Where(i => i.State == true && i.TinhTrang == false && i.ID_DocGia==id).ToList();
         for (int i = 0; i < gridControl1.VisibleRowCount; i++)
            {
             PhieuMuon pm=gridControl1.GetRow(i) as PhieuMuon;
             decimal? nm=pm.ChiTietMuons.Where(a=>a.Sach.TinhTrang==false).ToList().Count();
                gridControl1.SetCellValue(i, gridControl1.Columns["TheDocGia.DiaChi"], nm.ToString());
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (listID.Count == 0)
            {
                MessageBox.Show("Dữ liệu trống");
                return;
            }
            count = listID.Count;
            foreach (PhieuMuon pm in listID)
            {
                FrmChiTietTra frm = new FrmChiTietTra(pm,ref frmfrm);
                frm.Show();
            }
            this.Hide();
            if (count == 0)
                Close();
            
        }
        public void SetClose()
        {
            count--;
            if (count <= 0)
                Close();
            
                
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
