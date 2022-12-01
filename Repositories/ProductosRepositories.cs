using System.Data.SqlClient;
using SistemaGestion.Models;

namespace SistemaGestion.Repositories
{
    public class ProductosRepositories
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=brendenhoff_sistema__gestion;User Id =brendenhoff_sistema__gestion; Password=bren123;";

        public ProductosRepositories() 
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch(Exception ex) 
            {
                throw;    
            }
        }

        public List<Producto> listarProducto()
        {
            List<Producto> lista = new List<Producto>(); ;
            if (conexion == null) 
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PRODUCTO", conexion))
                {
                    conexion.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = int.Parse(reader["Id"].ToString());
                                producto.Descripcion = reader["Descripciones"].ToString();
                                producto.Costo = double.Parse(reader["Costo"].ToString());
                                producto.PrecioVenta = double.Parse(reader["PrecioVenta"].ToString());
                                producto.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                                lista.Add(producto);

                            }
                        }
                    }
                }
                conexion.Close();
            } 
            catch 
            {
                throw;
            }
            return lista;
        }
    }
}
