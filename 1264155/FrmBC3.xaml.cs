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
    /// Interaction logic for FrmBC3.xaml
    /// </summary>
    public partial class FrmBC3 : Window
    {
        public FrmBC3()
        {
            InitializeComponent();
            QLThuVienEntities ql = new QLThuVienEntities();
            List<BCDocGia> list = new List<BCDocGia>();
            List<TheDocGia> l = ql.TheDocGias.Where(i => i.TienNo > 0).ToList();
            for (int i = 0; i < l.Count; i++)
            {
                BCDocGia tt = new BCDocGia();
                tt.STT = i + 1;
                tt.TenDG = l[i].HoTen;
                tt.TienNo = ((int)l[i].TienNo).ToString("#,##0");
                list.Add(tt);
            }
            dataGrid1.ItemsSource = list;
            txtNgay.Text = "NGÀY : " + DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
