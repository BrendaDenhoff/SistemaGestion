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
                                Venta venta = new Venta();
                                venta.Id = int.Parse(reader["Id"].ToString());
                                venta.Comentarios = reader["Comentarios"].ToString();
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
    }
}
