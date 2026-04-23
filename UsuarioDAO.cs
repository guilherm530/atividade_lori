using APIUsuarios.DTO;
using MySql.Data.MySqlClient;

namespace APIUsuarios.DAO
{
    public class UsuarioDAO
    {
        public List<UsuarioDTO> ListarUsuarios()
        {
            var usuarios = new List<UsuarioDTO>();
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "SELECT*FROM Usuarios";
            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            while (dataReader.Read())
            {
                var usuario = new UsuarioDTO();
                usuario.ID = int.Parse(dataReader["ID"].ToString());
                usuario.Nome = dataReader["Nome"].ToString();
                usuario.Email = dataReader["Email"].ToString();
                usuario.Telefone = dataReader["Telefone"].ToString();

                usuarios.Add(usuario);
            }

            conexao.Close();

            return null; //Código temporário para evitar mensagem de erro
        }

        public void CadastrarUsuario(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Usuarios (Nome, Email, Telefone) 
                  VALUES (@nome, @email, @telefone)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@telefone", usuario.Telefone);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public int AtualizarUsuario(int id, UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE Usuarios
                  SET Nome = @nome,
                      Email = @email,
                      Telefone = @telefone
                  WHERE Id = @id";

            using var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@telefone", usuario.Telefone);
            comando.Parameters.AddWithValue("@id", id);

            var linhasAfetadas = comando.ExecuteNonQuery();
            conexao.Close();

            return linhasAfetadas;

        }

        public int DeletarUsuario(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"DELETE FROM Usuarios
                  WHERE Id = @id";

            using var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var linhasAfetadas = comando.ExecuteNonQuery();
            conexao.Close();

            return linhasAfetadas;

        }
    }
}
