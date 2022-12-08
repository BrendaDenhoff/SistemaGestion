using System.Data;
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
                                ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
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

        public ProductoVendido? obtenerProductoVendido(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PRODUCTOVENDIDO where id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
                            return productoVendido;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void crearProductoVendido(ProductoVendido productoVendido)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(stock, idProducto, idVenta) Values(@stock, @idProducto, @idVenta)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.VarChar) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta });
                    cmd.ExecuteNonQuery();
                }
                conexion.Close();
            }
            catch
            {
                throw;
            }
        }

        public ProductoVendido? actualizarProductoVendido(int id, ProductoVendido productoVendidoAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                ProductoVendido? productoVendido = obtenerProductoVendido(id);
                if (productoVendido == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (productoVendido.Stock != productoVendidoAActualizar.Stock && productoVendidoAActualizar.Stock > 0)
                {
                    camposAActualizar.Add("stock = @stock");
                    productoVendido.Stock = productoVendidoAActualizar.Stock;
                }
                if (productoVendido.IdProducto != productoVendidoAActualizar.IdProducto && productoVendidoAActualizar.IdProducto > 0)
                {
                    camposAActualizar.Add("IdProducto = @idProducto");
                    productoVendido.IdProducto = productoVendidoAActualizar.IdProducto;
                }
                if (productoVendido.IdVenta != productoVendidoAActualizar.IdVenta && productoVendidoAActualizar.IdVenta > 0)
                {
                    camposAActualizar.Add("IdVenta = @IdVenta");
                    productoVendido.IdVenta = productoVendidoAActualizar.IdVenta;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE ProductoVendido SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.VarChar) { Value = productoVendidoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdVenta });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return productoVendido;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool eliminarProductoVendido(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM PRODUCTOVENDIDO WHERE ID = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }

        private ProductoVendido obtenerProductoVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = int.Parse(reader["Id"].ToString());
            productoVendido.IdProducto = int.Parse(reader["IdProducto"].ToString());
            productoVendido.Stock = int.Parse(reader["Stock"].ToString());
            productoVendido.IdVenta = int.Parse(reader["IdVenta"].ToString());
            return productoVendido;
        }
    }
}
