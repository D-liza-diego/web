using System.Data.SqlClient;
using System;

namespace web.Models

{
    public class producto_vendido
    {
        public string producto { get; set; } = null!;
        public int cantidad { get; set; }
        
    }
}
