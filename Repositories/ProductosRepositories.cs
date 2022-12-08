using System.Data;
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
                                Producto producto = obtenerProductoDesdeReader(reader);
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

        public Producto? obtenerProducto(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PRODUCTO where id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = obtenerProductoDesdeReader(reader);
                            return producto;
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

        public void crearProducto(Producto producto)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripciones, costo, precioVenta, stock, idUsuario) Values(@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.BigInt) { Value = producto.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.BigInt) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    cmd.ExecuteNonQuery();
                }
                conexion.Close();
            }
            catch
            {
                throw;
            }
        }  

        public Producto? actualizarProducto(int id, Producto productoAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Producto? producto = obtenerProducto(id);
                if (producto == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (producto.Descripciones != productoAActualizar.Descripciones && !string.IsNullOrEmpty(productoAActualizar.Descripciones))
                {
                    camposAActualizar.Add("descripciones = @descripcion");
                    producto.Descripciones = productoAActualizar.Descripciones;
                }
                if (producto.Costo != productoAActualizar.Costo && productoAActualizar.Costo > 0)
                {
                    camposAActualizar.Add("costo = @costo");
                    producto.Costo = productoAActualizar.Costo;
                }
                if (producto.PrecioVenta != productoAActualizar.PrecioVenta && productoAActualizar.PrecioVenta > 0)
                {
                    camposAActualizar.Add("precioVenta = @precioVenta");
                    producto.PrecioVenta = productoAActualizar.PrecioVenta;
                }
                if (producto.Stock != productoAActualizar.Stock && productoAActualizar.Stock > 0)
                {
                    camposAActualizar.Add("stock = @stock");
                    producto.Stock = productoAActualizar.Stock;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = productoAActualizar.Descripciones });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = productoAActualizar.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = productoAActualizar.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return producto;
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

        public bool eliminarProducto(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM PRODUCTO WHERE ID = @id", conexion))
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

        private Producto obtenerProductoDesdeReader(SqlDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = int.Parse(reader["Id"].ToString());
            producto.Descripciones = reader["Descripciones"].ToString();
            producto.Costo = double.Parse(reader["Costo"].ToString());
            producto.PrecioVenta = double.Parse(reader["PrecioVenta"].ToString());
            producto.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
            return producto;
        }
    }
}
