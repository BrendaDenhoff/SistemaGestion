using System.Data;
using System.Data.SqlClient;
using SistemaGestion.Models;

namespace SistemaGestion.Repositories
{
    public class VentasRepositories
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=brendenhoff_sistema__gestion;User Id =brendenhoff_sistema__gestion; Password=bren123;";

        public VentasRepositories()
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

        public List<Venta> listarVentas()
        {
            List<Venta> lista = new List<Venta>(); ;
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM VENTA", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = obtenerVentaDesdeReader(reader);
                                lista.Add(venta);

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

        public Venta? obtenerVenta(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM VENTA where id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Venta venta = obtenerVentaDesdeReader(reader);
                            return venta;
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

        public void crearVenta(Venta venta)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO venta(comentarios, idUsuario) Values(@comentarios, @idUsuario)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario });
                    cmd.ExecuteNonQuery();
                }

                conexion.Close();
            }
            catch
            {
                throw;
            }
        }

        public Venta? actualizarVenta(int id, Venta ventaAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Venta? venta = obtenerVenta(id);
                if (venta == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (venta.Comentarios != ventaAActualizar.Comentarios && !string.IsNullOrEmpty(ventaAActualizar.Comentarios))
                {
                    camposAActualizar.Add("comentarios = @comentarios");
                    venta.Comentarios = ventaAActualizar.Comentarios;
                }
                if (venta.IdUsuario != ventaAActualizar.IdUsuario && ventaAActualizar.IdUsuario > 0)
                {
                    camposAActualizar.Add("idUsuario = @idUsuario");
                    venta.IdUsuario = ventaAActualizar.IdUsuario;
                }          
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE venta SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = ventaAActualizar.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = ventaAActualizar.IdUsuario });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return venta;
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

        public bool eliminaVenta(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM venta WHERE ID = @id", conexion))
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

        private Venta obtenerVentaDesdeReader(SqlDataReader reader)
        {
            Venta venta = new Venta();
            venta.Id = int.Parse(reader["Id"].ToString());
            venta.Comentarios = reader["Comentarios"].ToString();
            return venta;
        }
    }

    
}
