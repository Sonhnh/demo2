using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBe
{
    public class QLBB
    {
        public DataHelper DataHelper { get; set; }

        public QLBB()
        {
            this.DataHelper = new DataHelper();
        }

        public BanBe GetBanBe(DataRow dataRow)
        {
            BanBe BanBe = new BanBe();
            BanBe.STT = Convert.ToInt32(dataRow["STT"].ToString());
            BanBe.HoTen = dataRow["HoTen"].ToString();
            BanBe.Email = dataRow["Email"].ToString();
            BanBe.DiaChi = dataRow["DiaChi"].ToString();
            BanBe.NgaySinh = Convert.ToDateTime(dataRow["NgaySinh"].ToString());
            BanBe.SoDienThoai = dataRow["SoDienThoai"].ToString();
            BanBe.Facebook = dataRow["Facebook"].ToString();
            BanBe.Skype = dataRow["Skype"].ToString();
            BanBe.GioiTinh = Convert.ToBoolean(dataRow["GioiTinh"].ToString());
            return BanBe;
        }

        public List<BanBe> GetListBanBe()
        {
            string query = "select * from BanBe";
            DataTable dataTable = this.DataHelper.DB_select(query);
            List<BanBe> list = new List<BanBe>();
            foreach (DataRow item in dataTable.Rows)
            {
                list.Add(GetBanBe(item));
            }
            return list;
        }


        public BanBe GetBanBeBySTT(int STT)
        {
            string query = "select * from BanBe where BanBe.STT = " + STT + "";
            DataTable dataTable = this.DataHelper.DB_select(query);
            return GetBanBe(dataTable.Rows[0]);
        }


        public void DelBanBe(int STT)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "delete from BanBe where STT = @STT";
            Cmd.Parameters.Add("@STT", SqlDbType.NVarChar);
            Cmd.Parameters["@STT"].Value = STT;
            this.DataHelper.DB_ExecuteNonQuery(Cmd);
        }

        public void AddInformationBanBe(BanBe banBe, SqlCommand Cmd)
        {
            Cmd.Parameters.Add("@STT", SqlDbType.Int);
            Cmd.Parameters["@STT"].Value = banBe.STT;

            Cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar);
            Cmd.Parameters["@HoTen"].Value = banBe.HoTen;

            Cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            Cmd.Parameters["@Email"].Value = banBe.Email;

            Cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime);
            Cmd.Parameters["@NgaySinh"].Value = banBe.NgaySinh;

            Cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar);
            Cmd.Parameters["@SoDienThoai"].Value = banBe.SoDienThoai;

            Cmd.Parameters.Add("@Facebook", SqlDbType.NVarChar);
            Cmd.Parameters["@Facebook"].Value = banBe.Facebook;

            Cmd.Parameters.Add("@Skype", SqlDbType.NVarChar);
            Cmd.Parameters["@Skype"].Value = banBe.Skype;

            Cmd.Parameters.Add("@GioiTinh", SqlDbType.Bit);
            Cmd.Parameters["@GioiTinh"].Value = banBe.GioiTinh;

            Cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
            Cmd.Parameters["@DiaChi"].Value = banBe.DiaChi;

        }
        public void AddbanBe(BanBe banBe)
        {
            SqlCommand Cmd = new SqlCommand();
            string query = "insert into BanBe values(@HoTen,@Email,@NgaySinh,@SoDienThoai,@Facebook,@Skype,@GioiTinh,@DiaChi)";
            Cmd.CommandText = query;
            AddInformationBanBe(banBe, Cmd);
            this.DataHelper.DB_ExecuteNonQuery(Cmd);
        }

        public void UpdatebanBe(BanBe banBe)
        {
            SqlCommand Cmd = new SqlCommand();
            string query = "update BanBe set HoTen = @HoTen,Email=@Email,NgaySinh=@NgaySinh,SoDienThoai=@SoDienThoai," +
                           "Facebook=@Facebook,Skype=@Skype,GioiTinh=@GioiTinh, DiaChi=@DiaChi where STT = @STT";
            Cmd.CommandText = query;
            AddInformationBanBe(banBe, Cmd);
            this.DataHelper.DB_ExecuteNonQuery(Cmd);
        }
        
    }
}
