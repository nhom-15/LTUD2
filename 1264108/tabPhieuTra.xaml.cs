﻿using System;
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
using System.Collections.ObjectModel;
using De01.BUS;

namespace De01.TabItems
{
    /// <summary>
    /// Interaction logic for tabPhieuTra.xaml
    /// </summary>
    public partial class tabPhieuTra : UserControl
    {
        List<PhieuTra> list;
        public tabPhieuTra()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            gridPhieuTra.ShowLoadingPanel = true;
            bw.RunWorkerAsync();
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();

            list = PhieuTraBUS.LayTonTai();

        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gridPhieuTra.ItemsSource = new ObservableCollection<PhieuTra>(list);
            gridPhieuTra.ShowLoadingPanel = false;
        }
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            FrmThemPhieuTra frm = new FrmThemPhieuTra();
            frm.ShowDialog();
            btRefresh_Click(sender, e);
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            QLThuVienEntities ql = new QLThuVienEntities();
            list = PhieuTraBUS.LayTonTai();
            gridPhieuTra.ItemsSource = new ObservableCollection<PhieuTra>(list);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbr = MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.OK)
            {
                QLThuVienEntities ql = new QLThuVienEntities();
                Button bt = sender as Button;
                EditGridCellData cell = bt.Tag as EditGridCellData;
                string id = (cell.RowData.Row as PhieuTra).ID;
                PhieuTra pm = ql.PhieuTras.Where(i => i.ID == id).FirstOrDefault();
                pm.State = false;
                ql.SaveChanges();
                btRefresh_Click(sender, e);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            EditGridCellData cell = bt.Tag as EditGridCellData;
       
            FrmXemPhieuTra frm = new FrmXemPhieuTra(cell.RowData.Row as PhieuTra);
            frm.ShowDialog();
        }

        private void gridPhieuTra_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Header.ToString() == "STT")
                e.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
