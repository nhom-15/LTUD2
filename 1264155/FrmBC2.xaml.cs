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
    /// Interaction logic for FrmBC2.xaml
    /// </summary>
    public partial class FrmBC2 : Window
    {
        public FrmBC2()
        {
            InitializeComponent();
            QLThuVienEntities ql=new QLThuVienEntities();
            List<BCTraTre> list = new List<BCTraTre>();
            List<ChiTietTra> l=ql.ChiTietTras.Where(i=>i.TienPhat>0).ToList();
            for (int i = 0; i < l.Count; i++)
            {
                BCTraTre tt = new BCTraTre();
                tt.S = l[i].Sach;
                tt.STT = i + 1;
                tt.NgayMuon =l[i].PhieuTra.PhieuMuon.NgayMuon.ToString("dd/MM/yyyy ss:mm:HH");
                tt.SoNgay =((int)l[i].TienPhat)/1000;
                list.Add(tt);
            }
            dataGrid1.ItemsSource = list;
            txtNgay.Text ="NGÀY : "+ DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
