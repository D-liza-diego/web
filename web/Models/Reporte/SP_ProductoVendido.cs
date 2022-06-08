using System.Data.SqlClient;

namespace web.Models.Reporte
{
    public class SP_ProductoVendido
    {
        public List<ProductoVendido> RetonarProductos()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();

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
                        lista.Add(new ProductoVendido()
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
