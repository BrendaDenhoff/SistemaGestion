using System.Data.SqlClient;
using SistemaGestion.Models;

namespace SistemaGestion.Repositories
{
    public class ProductosVendidosRepositories
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=brendenhoff_sistema__gestion;User Id =brendenhoff_sistema__gestion; Password=bren123;";

        public ProductosVendidosRepositories()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProductoVendido> listarProductoVendido()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>(); ;
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PRODUCTOVENDIDO", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();
                                productoVendido.Id = int.Parse(reader["Id"].ToString());
                                productoVendido.IdProducto = int.Parse(reader["IdProducto"].ToString());
                                productoVendido.Stock = int.Parse(reader["Stock"].ToString());
                                productoVendido.IdVenta = int.Parse(reader["IdVenta"].ToString());
                                lista.Add(productoVendido);

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
