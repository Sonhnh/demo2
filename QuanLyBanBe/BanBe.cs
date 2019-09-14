using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBe
{
    public class BanBe
    {
        public int STT { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Facebook { get; set; }
        public string Skype { get; set; }
        public bool GioiTinh { get; set; }

        public BanBe()
        {

        }

        public BanBe(int stt, string hoTen, string email, string diaChi, DateTime ngaySinh, string soDienThoai, string facebook, string skype, bool gioiTinh)
        {
            STT = stt;
            HoTen = hoTen;
            Email = email;
            DiaChi = diaChi;
            NgaySinh = ngaySinh;
            SoDienThoai = soDienThoai;
            Facebook = facebook;
            Skype = skype;
            GioiTinh = gioiTinh;
        }
    }
}
