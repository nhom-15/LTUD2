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
using System.Text.RegularExpressions;
using DevExpress.Xpf.Editors;
using System.Collections.ObjectModel;
using De01.BUS;
namespace De01.TabItems
{
    /// <summary>
    /// Interaction logic for tabDocGia.xaml
    /// </summary>
    public partial class tabDocGia : UserControl
    {
        public tabDocGia()
        {
            InitializeComponent();
            modified = false;
            stringEmty = true;
            SetVisibility(true, false, false, false, false);
        }
        List<TheDocGia> list;
        public bool modified;
        bool stringEmty;
        public void LoadData()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork+= new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            gridDocGia.ShowLoadingPanel = true;
            bw.RunWorkerAsync();
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();

            list = DocGiaBUS.LayTonTai();
            
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            gridDocGia.ItemsSource = list;
            colLoaiDG.DataContext =new ObservableCollection<LoaiDocGia>(  LoaiDocGiaBUS.LayHet()) ;
            colNhanVien.DataContext = new ObservableCollection<NhanVien>(NhanVienBUS.LayHet());
            gridDocGia.ShowLoadingPanel = false;
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
            for (int i = 0; i < gridDocGia.Columns.Count; i++)
                gridDocGia.Columns[i].ReadOnly = value;
            gridDocGia.Columns[" "].Visible = !value;
            gridDocGia.Columns["NgayLap"].ReadOnly = true;
            gridDocGia.Columns["ID_NhanVien"].ReadOnly = true;
        }
        private void gridDocGia_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
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
            if (e.Column.Name == "colEmail" && e.Value != null)
            {
                if (!Regex.IsMatch(e.Value.ToString(), @"^[\w.]+@[\w.-]+.[a-zA-Z]$"))
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
            FrmThemDocGia frm = new FrmThemDocGia();
            frm.EventThem += new FrmThemDocGia.ThemDocGia(ThemDG);
            frm.ShowDialog();
        }
        public void ThemDG(TheDocGia dg)
        {
            list.Add(dg);
            gridDocGia.ItemsSource =new ObservableCollection<TheDocGia>( list);
            modified = true;
   
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            list = DocGiaBUS.LayTonTai();
            AllFunc.CheckActive();
            gridDocGia.ItemsSource = new ObservableCollection<TheDocGia>(list);
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
            List<TheDocGia> defaulList = ql.TheDocGias.ToList();
            foreach (TheDocGia dg in list)
            {
                 TheDocGia n= defaulList.Where(i => i.ID == dg.ID).FirstOrDefault();
                 if (n == null)
                 {
                     ql.TheDocGias.AddObject(dg);
                     continue;
                 }
 
                 n.HoTen = dg.HoTen;
                 n.Email = dg.Email;
                 n.ID_LoaiDG = dg.ID_LoaiDG;
                 n.DiaChi = dg.DiaChi;
                 n.TienNo = dg.TienNo;
                 if (n.Active != dg.Active)
                     if (n.Active == false)
                         n.MocNgay = DateTime.Now;
                 n.Active = dg.Active;
                
                 n.NgaySinh = dg.NgaySinh;
            }
            ql.SaveChanges();
            foreach (TheDocGia dg in defaulList)
            {
                TheDocGia n = list.Where(i => i.ID == dg.ID).FirstOrDefault();
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

                int id = (cell.RowData.Row as TheDocGia).ID;
                TheDocGia dg = list.Where(i => i.ID == id).FirstOrDefault();
                list.Remove(dg);
                gridDocGia.ItemsSource = new ObservableCollection<TheDocGia>(list);
            
                modified = true;
            }
        }

        private void colActive_Validate(object sender, GridCellValidationEventArgs e)
        {
            if (e.Value != e.CellValue && modified == false)
                modified = true;
        }

        private void gridDocGia_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!btEdit.IsEnabled)
                gridDocGia.View.ShowEditor();
            //TableViewHitInfo hit = ((TableView)gridDocGia.View).CalcHitInfo(e.OriginalSource as DependencyObject);
            //if (hit.InRowCell)
            //{
            //    gridDocGia.View.FocusedRowHandle = hit.RowHandle;
            //    if (hit.Column.Name == "colActive")
            //    {
            //        bool value=(bool)gridDocGia.GetCellValue(hit.RowHandle,hit.Column);
            //        gridDocGia.SetCellValue(hit.RowHandle, hit.Column, !value);
            //        modified = true;
            //    }
            //}
        }



 

    }
}
