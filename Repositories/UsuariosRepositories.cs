using System.Data;
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
                                Usuario usuario = obtenerUsuarioDesdeReader(reader);           
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

        public Usuario? obtenerUsuario(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM USUARIO where id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario= obtenerUsuarioDesdeReader(reader);
                            return usuario;
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

        public void crearUusario(Usuario usuario)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO usuario(nombre, apellido, nombreUsuario, contrasenia, mail) Values(@nombre, @apellido, @nombreUsuario, @contrasenia, @mail)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    cmd.ExecuteNonQuery();
                }
                conexion.Close();
            }
            catch
            {
                throw;
            }
        }

        public Usuario? actualizarUsuario(int id, Usuario usuarioAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (usuario.Nombre != usuarioAActualizar.Nombre && !string.IsNullOrEmpty(usuarioAActualizar.Nombre))
                {
                    camposAActualizar.Add("nombre = @nombre");
                    usuario.Nombre = usuarioAActualizar.Nombre;
                }
                if (usuario.Apellido != usuarioAActualizar.Apellido && !string.IsNullOrEmpty(usuarioAActualizar.Apellido))
                {
                    camposAActualizar.Add("apellido = @apellido");
                    usuario.Apellido = usuarioAActualizar.Apellido;
                }
                if (usuario.NombreUsuario != usuarioAActualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioAActualizar.NombreUsuario))
                {
                    camposAActualizar.Add("NombreUsuario  = @NombreUsuario ");
                    usuario.NombreUsuario = usuarioAActualizar.NombreUsuario;
                }
                if (usuario.Contrasenia != usuarioAActualizar.Contrasenia && !string.IsNullOrEmpty(usuarioAActualizar.Contrasenia))
                {
                    camposAActualizar.Add("contrasenia = @contrasenia");
                    usuario.Contrasenia = usuarioAActualizar.Contrasenia;
                }
                if (usuario.Mail != usuarioAActualizar.Mail && !string.IsNullOrEmpty(usuarioAActualizar.Mail))
                {
                    camposAActualizar.Add("mail = @mail");
                    usuario.Mail = usuarioAActualizar.Mail;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioAActualizar.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioAActualizar.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuarioAActualizar.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuarioAActualizar.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioAActualizar.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return usuario;
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


        public bool eliminarUsuario(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no valida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM usuario WHERE ID = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }

        private Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = int.Parse(reader["Id"].ToString());
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;
        }
    }
}
