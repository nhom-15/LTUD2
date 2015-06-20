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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Bars;
using De01.TacVu;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using De01.BUS;


namespace De01.TabItems
{
    /// <summary>
    /// Interaction logic for tabNhanVien.xaml
    /// </summary>
    public partial class tabNhanVien : UserControl
    {
        List<NhanVien> list;
        public bool modified;
        bool stringEmty;
        public tabNhanVien()
        {
            InitializeComponent();
            modified = false;
            stringEmty = true;
            SetVisibility(true, false, false, false, false);
        }
        public void LoadData()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork+= new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            gridNhanVien.ShowLoadingPanel = true;
            bw.RunWorkerAsync();
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();

            list = NhanVienBUS.LayUsers();
            
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            gridNhanVien.ItemsSource = new ObservableCollection<NhanVien>(list);
            colBangCap.DataContext =new ObservableCollection<BangCap>(BangCapBUS.LayHet()) ;
            colBoPhan.DataContext =new ObservableCollection<BoPhan>(BoPhanBUS.LayHet()) ;
            colChucVu.DataContext = new ObservableCollection<ChucVu>(ChucVuBUS.LayHet());
            gridNhanVien.ShowLoadingPanel = false;
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
            for (int i = 0; i < gridNhanVien.Columns.Count; i++)
                gridNhanVien.Columns[i].ReadOnly = value;
            gridNhanVien.Columns[" "].Visible = !value;
        }
        private void gridNhanVien_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Header.ToString() == "STT")
                e.DisplayText = (e.RowHandle + 1).ToString();
            
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            SetVisibility(false, true, true, true, true);
            
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

        private void GridColumn_Validate(object sender, GridCellValidationEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString().Trim()))
            {
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                stringEmty = false;
                e.IsValid = false;
                
            }
            else
                stringEmty = true;
            if (e.Column.Name == "colDienThoai" && e.Value!=null)
            {
                if (!Regex.IsMatch(e.Value.ToString(), @"^\d+$"))
                {
                    stringEmty = false;
                    e.IsValid = false;
                }
            }
            if (e.Value != e.CellValue && modified==false)
                modified = true;
        }
        
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!stringEmty)
            {
                MessageBox.Show("Dữ liệu bị trống");
                return;
            }
            FrmThemNhanVien frm = new FrmThemNhanVien();
            frm.EventThem += new FrmThemNhanVien.ThemNhanVien(ThemNV);
            frm.ShowDialog();
        }
        public void ThemNV(NhanVien nv)
        {
            list.Add(nv);
            gridNhanVien.ItemsSource = new ObservableCollection<NhanVien>(list);
            modified = true;
        
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            list = NhanVienBUS.LayUsers();
            gridNhanVien.ItemsSource = new ObservableCollection<NhanVien>(list);
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

        public void SaveData()
        {
            QLThuVienEntities ql=new QLThuVienEntities();
            List<NhanVien> defaulList = ql.NhanViens.ToList();
            foreach (NhanVien nv in list)
            {
                 NhanVien n= defaulList.Where(i => i.ID == nv.ID).FirstOrDefault();
                 if (n == null)
                 {
                     ql.NhanViens.AddObject(nv);
                     continue;
                 }
               
                 n.HoTen = nv.HoTen;
                 n.ID_BangCap = nv.ID_BangCap;
                 n.DienThoai = nv.DienThoai;
                 n.DiaChi = nv.DiaChi;
                 n.ID_ChucVu = nv.ID_ChucVu;
                 n.ID_BoPhan = nv.ID_BoPhan;
                 n.NgaySinh = nv.NgaySinh;
            }
            ql.SaveChanges();
            foreach (NhanVien nv in defaulList)
            {
                NhanVien n = list.Where(i => i.ID == nv.ID).FirstOrDefault();
                if (n == null)
                {
                    nv.State = false;
                   
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

                int id = (cell.RowData.Row as NhanVien).ID;
                NhanVien nv = list.Where(i => i.ID == id).FirstOrDefault();
                list.Remove(nv);
                gridNhanVien.ItemsSource = new ObservableCollection<NhanVien>(list);
    
                modified = true;
            }
        }

        private void gridNhanVien_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!btEdit.IsEnabled)
                gridNhanVien.View.ShowEditor();
            
        }
        
    }
}
