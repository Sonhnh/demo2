using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanBe
{

    public class DataHelper
    {
        public SqlConnection Cnn { get; set; }

        public DataHelper()
        {
            this.Cnn = new SqlConnection("Data Source=SON\\SQLEXPRESS;Initial Catalog=QuanLyBanBe;Integrated Security=True");
        }

        public void DB_ExecuteNonQuery(SqlCommand cmd)
        {
            cmd.Connection = this.Cnn;
            this.Cnn.Open();
            cmd.ExecuteNonQuery();
            this.Cnn.Close();
        }

        public DataTable DB_select(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, this.Cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}
