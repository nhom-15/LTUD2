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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using DevExpress.Xpf.Grid;
using De01.TacVu;
using DevExpress.Xpf.Bars;
using System.Collections.ObjectModel;
using De01.BUS;

namespace De01.TabItems
{
    /// <summary>
    /// Interaction logic for tabSachxaml.xaml
    /// </summary>
    public partial class tabSach : UserControl
    {
        public tabSach()
        {
            InitializeComponent();
            modified = false;
            stringEmty = true;
            SetVisibility(true, false, false, false, false);
        }
        public static List<Sach> list;
        public bool modified;
        bool stringEmty;
        public void LoadData()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            gridSach.ShowLoadingPanel = true;
            bw.RunWorkerAsync();
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();

            list = SachBUS.LayTonTai();

        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            gridSach.ItemsSource = new ObservableCollection<Sach>(list);
            colTheLoai.DataContext = new ObservableCollection<TheLoai>(TheLoaiBUS.LayHet());
            gridSach.ShowLoadingPanel = false;
        }
        void SetVisibility(bool Edit, bool Cancel, bool Add, bool Del, bool Save)
        {
            btEdit.IsEnabled = Edit;
            SetGridReadOnly(Edit);
            if (Cancel)
                btCancel.Visibility = Visibility.Visible;
            else
                btCancel.Visibility = Visibility.Collapsed;

            if (Add)
                btAdd.Visibility = Visibility.Visible;
            else
                btAdd.Visibility = Visibility.Collapsed;
            if (Save)
                btSave.Visibility = Visibility.Visible;
            else
                btSave.Visibility = Visibility.Collapsed;

        }
        void SetGridReadOnly(bool value)
        {
            for (int i = 0; i < gridSach.Columns.Count; i++)
                gridSach.Columns[i].ReadOnly = value;
            gridSach.Columns[" "].Visible = !value;

        }
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!stringEmty)
            {
                MessageBox.Show("Dữ liệu bị trống");
                return;
            }
            FrmThemSach frm = new FrmThemSach();
            frm.EventThem += new FrmThemSach.ThemSach(ThemS);
            frm.ShowDialog();
        }
        public void ThemS(Sach s)
        {
            list.Add(s);
            gridSach.ItemsSource = new ObservableCollection<Sach>(list);
            modified = true;

        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!stringEmty)
            {
                MessageBox.Show("Dữ liệu bị trống");
                return;
            }
            SetVisibility(true, false, false, false, false);
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            SetVisibility(false, true, true, true, true);
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            list = SachBUS.LayTonTai();
            gridSach.ItemsSource = new ObservableCollection<Sach>(list);
            modified = false;
            
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (!stringEmty)
            {
                MessageBox.Show("Dữ liệu bị trống");
                return;
            }
            if (modified)
            {
                MessageBoxResult mbr = MessageBox.Show("Bạn có muốn lưu ?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (mbr == MessageBoxResult.OK)
                {
                    SaveData();
                    modified = false;
                    stringEmty = true;
                    LoadData();
                }
            }
        }
        public static List<Sach> GetListSach()
        {
            return list;
        }
        public void SaveData()
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            List<Sach> defaulList = ql.Saches.ToList();
            List<Sach> listCopy = list.ToList();
            foreach (Sach dg in listCopy)
            {
                Sach n = defaulList.Where(i => i.ID == dg.ID).FirstOrDefault();
                if (n == null)
                {
                    ql.Saches.AddObject(dg);
                    ql.SaveChanges();
                    continue;
                }
                if (n.ID_TheLoai != dg.ID_TheLoai)
                {
                    Sach a = new Sach();
                    a.TenSach = dg.TenSach;
                    a.TacGia = dg.TacGia;
                    a.NamXB = dg.NamXB;
                    a.ID_NhanVien = dg.ID_NhanVien;
                    a.NXB = dg.NXB;
                    a.Gia = dg.Gia;
                    a.NgayNhap = dg.NgayNhap;
                    a.ID_TheLoai = dg.ID_TheLoai;
                    a.ID = AllFunc.GetMaxID(dg.ID_TheLoai);
                    a.TinhTrang = dg.TinhTrang;
                    a.State = true;
                    list.Add(a);
                    AllFunc.ChangeID(n.ID, a.ID);
                    ql.Saches.AddObject(a);
                }
                else
                {
                    n.TenSach = dg.TenSach;
                    n.TacGia = dg.TacGia;
                    n.NamXB = dg.NamXB;
                    n.NXB = dg.NXB;
                    n.Gia = dg.Gia;
                    n.ID_TheLoai = dg.ID_TheLoai;
                    n.TinhTrang = dg.TinhTrang;
                }
                ql.SaveChanges();
            }
            
            foreach (Sach dg in defaulList)
            {
                Sach n = list.Where(i => i.ID == dg.ID).FirstOrDefault();
                if (n == null)
                {
                    dg.State = false;

                }
            }
            ql.SaveChanges();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!stringEmty)
                return;
            MessageBoxResult mbr = MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.OK)
            {
                Button bt = sender as Button;
                EditGridCellData cell = bt.Tag as EditGridCellData;
                string id = (cell.RowData.Row as Sach).ID;
                Sach dg = list.Where(i => i.ID == id).FirstOrDefault();
                list.Remove(dg);
                gridSach.ItemsSource = new ObservableCollection<Sach>(list);
    
                modified = true;
            }
        }

        private void GridColumn_Validate(object sender, DevExpress.Xpf.Grid.GridCellValidationEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString().Trim()))
            {
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                stringEmty = false;
                e.IsValid = false;

            }
            else
                stringEmty = true;
           
            if (e.Value != e.CellValue && modified == false)
                modified = true;
        }

        private void gridSach_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Header.ToString() == "STT")
                e.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!btEdit.IsEnabled)
                gridSach.View.ShowEditor();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
     
                Button bt = sender as Button;
                EditGridCellData cell = bt.Tag as EditGridCellData;
                FrmThongTinSach frm = new FrmThongTinSach((cell.RowData.Row as Sach));
                frm.ShowDialog();
 
        }

       
    }
}
