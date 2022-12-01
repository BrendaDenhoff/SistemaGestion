using System.Data.SqlClient;
using SistemaGestion.Models;

namespace SistemaGestion.Repositories
{
    public class UsuariosRepositories
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=brendenhoff_sistema__gestion;User Id =brendenhoff_sistema__gestion; Password=bren123;";

        public UsuariosRepositories()
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

        public List<Usuario> listarUsuario()
        {
            List<Usuario> lista = new List<Usuario>(); ;
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID, NOMBRE, APELLIDO, NOMBREUSUARIO, MAIL FROM USUARIO", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = int.Parse(reader["Id"].ToString());
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                usuario.Mail = reader["Mail"].ToString();
                                lista.Add(usuario);

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

        //public Usuario obtener()
        //{

        //}
    }
}
