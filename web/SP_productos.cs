using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace web
{
    public class SP_productos
    {
        public List<producto_vendido> RetonarProductos(){
            List<producto_vendido> lista = new List<producto_vendido>();

            using (SqlConnection x = new SqlConnection("Data Source=DESKTOP-QAG3240; Initial Catalog=web; Integrated Security=True"))
            {
                string query = "producto_vendido";
                SqlCommand cmd = new SqlCommand(query, x);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                x.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new producto_vendido()
                        {
                            producto = dr["nameproduct"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }
            return lista;
        }

    }
}
