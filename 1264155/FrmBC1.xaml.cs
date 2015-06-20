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

namespace De01.TacVu
{
    /// <summary>
    /// Interaction logic for FrmBC1.xaml
    /// </summary>
    public partial class FrmBC1 : Window
    {
        public FrmBC1()
        {
            InitializeComponent();
            QLThuVienEntities ql=new QLThuVienEntities();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            List<TheLoai> l = ql.TheLoais.ToList();
            List<BCTheLoai> list = new List<BCTheLoai>();
            int total = ql.ChiTietMuons.ToList().Count;
            for (int j = 0; j < l.Count; j++)
            {
                BCTheLoai tl = new BCTheLoai();
                tl.STT = j + 1;
                tl.Tl = l[j];
                tl.Soluong = ql.ChiTietMuons.Where(i => i.Sach.ID_TheLoai == tl.Tl.ID ).ToList().Count;
                tl.Tile = total==0?0: Math.Round( tl.Soluong * 100 * 1.0 / total,2);
                list.Add(tl);
            }
            dataGrid1.ItemsSource = list;
            txtNgay.Text ="THÁNG : "+ month.ToString() + "/" + year.ToString();
            txtTong.Text ="Tổng số lượt mượn : "+ total.ToString();
        }
    }
}
