using System.Data.SqlClient;

namespace web.Models.Reporte
{
    public class SP_VentasMes
    {
        public List<VentasMes> RetonarVentas()
        {
            List<VentasMes> lista = new List<VentasMes>();

            using (SqlConnection x = new SqlConnection("Data Source=DESKTOP-QAG3240; Initial Catalog=web; Integrated Security=True"))
            {
                string query = "Ventas_Mes";
                SqlCommand cmd = new SqlCommand(query, x);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                x.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new VentasMes()
                        {
                            Total =decimal.Parse(dr["total"].ToString()),
                            Mes = dr["Mes"].ToString(),
                        });
                    }
                }
            }
            return lista;
        }
    }
}
