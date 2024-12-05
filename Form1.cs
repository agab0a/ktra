using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VuTuanAnh_163
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XmlDocument doc = new XmlDocument();
        string path = @"D:\VuTuanAnh_163\DSNhanVien.xml";
        int d;
        private void HienThi()
        {
            dataNhanVien.Rows.Clear();
            doc.Load(path);

            XmlNodeList ds = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;
            dataNhanVien.ColumnCount = 5;
            dataNhanVien.Rows.Add();
            foreach (XmlNode nv in ds) 
            {
                XmlNode manv = nv.SelectSingleNode("@manv");
                dataNhanVien.Rows[sd].Cells[0].Value = manv.InnerText.ToString();
                XmlNode hoten = nv.SelectSingleNode("hoten");
                dataNhanVien.Rows[sd].Cells[1].Value = hoten.InnerText.ToString();
                XmlNode gioitinh = nv.SelectSingleNode("gioitinh");
                dataNhanVien.Rows[sd].Cells[2].Value = gioitinh.InnerText.ToString();
                XmlNode trinhdo = nv.SelectSingleNode("trinhdo");
                dataNhanVien.Rows[sd].Cells[3].Value = trinhdo.InnerText.ToString();
                XmlNode diachi = nv.SelectSingleNode("diachi");
                dataNhanVien.Rows[sd].Cells[4].Value = diachi.InnerText.ToString();
                dataNhanVien.Rows.Add();
                sd++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void dataNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txt_manv.Text = dataNhanVien.Rows[d].Cells[0].Value.ToString();
            txt_hoten.Text = dataNhanVien.Rows[d].Cells[1].Value.ToString();
            if (dataNhanVien.Rows[d].Cells[2].Value.ToString() == "Nam")
            {
                radioButNam.Select();
            } else
            {
                radioButNu.Select();
            }
            if (dataNhanVien.Rows[d].Cells[3].Value.ToString().ToLower() == "đại học")
            {
                comboBox_TrinhDo.SelectedIndex = 1;
            } else if (dataNhanVien.Rows[d].Cells[3].Value.ToString().ToLower() == "cao đẳng") 
            {
                comboBox_TrinhDo.SelectedIndex = 2;
            }
            else if (dataNhanVien.Rows[d].Cells[3].Value.ToString().ToLower() == "trung cấp")
            {
                comboBox_TrinhDo.SelectedIndex = 3;
            } else
            {
                comboBox_TrinhDo.SelectedIndex = 0;
            }
            txt_diachi.Text = dataNhanVien.Rows[d].Cells[4].Value.ToString();
        }

        private void them_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv = doc.CreateElement("nhanvien");
            XmlAttribute manv = doc.CreateAttribute("manv");
            manv.InnerText = txt_manv.Text;
            nv.Attributes.Append(manv);
            XmlNode hoten = doc.CreateElement("hoten");
            hoten.InnerText = txt_hoten.Text;
            nv.AppendChild(hoten);
            XmlNode gioitinh = doc.CreateElement("gioitinh");
            if (radioButNam.Checked)
            {
                gioitinh.InnerText = "Nam";
            }
            else gioitinh.InnerText = "Nữ";
            nv.AppendChild (gioitinh);
            XmlNode trinhdo = doc.CreateElement("trinhdo");
            trinhdo.InnerText = comboBox_TrinhDo.Text;
            nv.AppendChild(trinhdo);
            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txt_diachi.Text;
            nv.AppendChild(diachi);
            goc.AppendChild(nv);
            doc.Save(path);
            HienThi();
        }

        private void sua_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv_cu = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            XmlNode nv_moi = doc.CreateElement("nhanvien");
            XmlAttribute manv = doc.CreateAttribute("manv");
            manv.InnerText = txt_manv.Text;
            nv_moi.Attributes.Append(manv);
            XmlNode hoten = doc.CreateElement("hoten");
            hoten.InnerText = txt_hoten.Text;
            nv_moi.AppendChild(hoten);
            XmlNode gioitinh = doc.CreateElement("gioitinh");
            if (radioButNam.Checked)
            {
                gioitinh.InnerText = "Nam";
            }
            else gioitinh.InnerText = "Nữ";
            nv_moi.AppendChild(gioitinh);
            XmlNode trinhdo = doc.CreateElement("trinhdo");
            trinhdo.InnerText = comboBox_TrinhDo.Text;
            nv_moi.AppendChild(trinhdo);
            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txt_diachi.Text;
            nv_moi.AppendChild(diachi);
            goc.ReplaceChild(nv_moi, nv_cu);
            doc.Save(path);
            HienThi();
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv_xoa = doc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            goc.RemoveChild(nv_xoa);
            doc.Save(path);
            HienThi();
        }

        private void tim_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv_tim = doc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            if (nv_tim == null) 
            {
               MessageBox.Show("Không có nhân viên nào có mã NV này!", "Thông báo");
                txt_manv.Text = "";
                return;
            }

            XmlNode hoten = nv_tim.SelectSingleNode("hoten");
            txt_hoten.Text = hoten.InnerText;
            XmlNode gioitinh = nv_tim.SelectSingleNode("gioitinh");
            if (gioitinh.InnerText == "Nam") radioButNam.Select();
            else radioButNu.Select();
            XmlNode trinhdo = nv_tim.SelectSingleNode("trinhdo");
            if (trinhdo.InnerText.ToLower() == "đại học") comboBox_TrinhDo.SelectedIndex = 1;
            else if (trinhdo.InnerText.ToLower() == "cao đẳng") comboBox_TrinhDo.SelectedIndex = 2;
            else if (trinhdo.InnerText.ToLower() == "trung cấp") comboBox_TrinhDo.SelectedIndex = 3;
            else comboBox_TrinhDo.SelectedIndex = 0;
            XmlNode diachi = nv_tim.SelectSingleNode("diachi");
            txt_diachi.Text = diachi.InnerText;
            
        }
    }
}
