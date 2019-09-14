using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanBe
{
    public partial class Form1 : Form
    {
        public QLBB qLBB { get; set; }
        public Form1()
        {
            InitializeComponent();
            qLBB = new QLBB();
            LoadCbSearch();
        }

        public void LoadData()
        {
            gv_DsBanBe.DataSource = null;
            gv_DsBanBe.DataSource = qLBB.GetListBanBe();
        }

        private void LoadCbSearch()
        {
            cbTimKiem.Items.Clear();
            cbTimKiem.Items.Add("Tất cả");
            cbTimKiem.Items.Add("Họ tên");
            cbTimKiem.Items.Add("Email");
            cbTimKiem.Items.Add("Địa chỉ");
            cbTimKiem.Items.Add("Ngày sinh");
            cbTimKiem.Items.Add("Số điện thoại");
            cbTimKiem.Items.Add("Facebook");
            cbTimKiem.Items.Add("Skype");
            cbTimKiem.Items.Add("Giới tính");
        }

        private void bthHienThi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtHoTen.Text) || String.IsNullOrEmpty(txtEmail.Text) ||
               String.IsNullOrEmpty(txtDiaChi.Text) ||  String.IsNullOrEmpty(txtSdt.Text) ||
               (rbGtNam.Checked == false && rbGtNu.Checked == false))
            {
                MessageBox.Show("Các trường không được để trống");
                return;
            }

            try
            {
                Convert.ToDouble(txtSdt.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số điện thoại không chứa kí tự");
                return;
            }
            qLBB.AddbanBe(new BanBe(0, txtHoTen.Text, txtEmail.Text,
                txtDiaChi.Text, Convert.ToDateTime(dtNgaySinh.Value.ToShortDateString()), txtSdt.Text, txtFb.Text, txtSkype.Text, rbGtNam.Checked));
            MessageBox.Show("Thêm thành công");
            LoadData();
        }

        private void gv_DsBanBe_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BanBe banBe = qLBB.GetBanBeBySTT(Convert.ToInt32(gv_DsBanBe.SelectedRows[0].Cells["STT"].Value.ToString()));
            Hien_thi_thong_tin(banBe);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gv_DsBanBe.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Chưa chọn bản ghi để sửa");
                return;
            }
            if (String.IsNullOrEmpty(txtHoTen.Text) || String.IsNullOrEmpty(txtEmail.Text) ||
               String.IsNullOrEmpty(txtDiaChi.Text) || String.IsNullOrEmpty(txtSdt.Text) ||
               (rbGtNam.Checked == false && rbGtNu.Checked == false))
            {
                MessageBox.Show("Các trường không được để trống");
                return;
            }

            try
            {
                Convert.ToDouble(txtSdt.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số điện thoại không chứa kí tự");
                return;
            }

            qLBB.UpdatebanBe(new BanBe(Convert.ToInt32(gv_DsBanBe.SelectedRows[0].Cells["STT"].Value), txtHoTen.Text, txtEmail.Text,
                txtDiaChi.Text, Convert.ToDateTime(dtNgaySinh.Value.ToShortDateString()), txtSdt.Text, txtFb.Text, txtSkype.Text, rbGtNam.Checked));

            MessageBox.Show("Sửa thành công");
            LoadData();

        }

        private void Xoa_Thong_Tin()
        {
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtSdt.Text = "";
            txtSkype.Text = "";
            txtFb.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            rbGtNam.Checked = false;
            rbGtNu.Checked = false;
        }

        private void Hien_thi_thong_tin(BanBe banBe)
        {
            txtHoTen.Text = banBe.HoTen;
            txtEmail.Text = banBe.Email;
            txtDiaChi.Text = banBe.DiaChi;
            txtSdt.Text = banBe.SoDienThoai;
            txtSkype.Text = banBe.Skype;
            txtFb.Text = banBe.Facebook;
            dtNgaySinh.Value = banBe.NgaySinh;
            rbGtNam.Checked = banBe.GioiTinh;
            rbGtNu.Checked = !banBe.GioiTinh;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gv_DsBanBe.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xóa");
                return;
            }
            if (gv_DsBanBe.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Chọn dòng dữ liệu để xóa");
                return;
            }

            foreach (DataGridViewRow row in gv_DsBanBe.SelectedRows)
            {
                int Stt = Convert.ToInt32(row.Cells["STT"].Value.ToString());
                qLBB.DelBanBe(Stt);
            }
            LoadData();
            Xoa_Thong_Tin();
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                MessageBox.Show("Nhập từ khóa tìm kiếm");
            }
            else if(gv_DsBanBe.DataSource == null)
            {
                MessageBox.Show("Dữ liệu rỗng");
            }

            else
            {
                string search = txtTimKiem.Text;
                List<BanBe> list = (List<BanBe>)gv_DsBanBe.DataSource;
                int id = 0;
                switch (cbTimKiem.SelectedItem.ToString())
                {
                    case "Tất cả":
                        break;
                    case "Họ tên":
                        id = 1;
                        break;
                    case "Địa chỉ":
                        id = 2;
                        break;
                    case "Email":
                        id = 3;
                        break;
                    case "Số điện thoại":
                        id = 4;
                        break;
                    case "Facebook":
                        id = 5;
                        break;
                    case "Skype":
                        id = 6;
                        break;
                    case "Ngày sinh":
                        id = 7;
                        break;
                    case "Giới tính":
                        id = 8;
                        break;
                    
                    default:
                        break;
                }
                List<BanBe> result = new List<BanBe>();
                foreach (BanBe item in list)
                {
                   if ((item.HoTen.ToLower().Contains(search.ToLower()) && (id == 0 || id == 1)) || (item.DiaChi.ToLower().Contains(search.ToLower()) && (id == 0 || id == 2)) ||
                           (item.Email.ToLower().Contains(search.ToLower()) && (id == 0 || id == 3)) || (item.SoDienThoai.ToLower().Contains(search.ToLower()) && (id == 0 || id == 4)) ||
                           (item.Facebook.ToLower().Contains(search.ToLower()) && (id == 0 || id == 5)) || (item.Skype.ToLower().Contains(search.ToLower()) && (id == 0 || id == 6)) ||
                           (item.NgaySinh.ToShortDateString().Contains(search) && (id == 0 || id == 7)))
                        result.Add(item);
                   if(id == 8 || id == 0)
                    {
                        if (search.ToLower().Equals("nam") && item.GioiTinh)
                            result.Add(item);
                        else if ((search.ToLower().Equals("nữ") && !item.GioiTinh))
                            result.Add(item);
                    }
                }
                gv_DsBanBe.DataSource = null;
                gv_DsBanBe.DataSource = result;

            }
        }
        
    }
}
