using exampleAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace exampleAPI2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        [HttpGet]
        [Route("/User/detail")]
        public ActionResult<User> Get(int idUser)
        {
            try
            {
                using (MySqlConnection conex = new(config.GetConnectionString("MYSQL_FINANCE")))
                {
                    conex.Open();
                    MySqlCommand comando = conex.CreateCommand();
                    comando.CommandText = "select * from financedb.usuario where id = ?id";
                    comando.Parameters.AddWithValue("id", idUser);
                    MySqlDataReader reader = comando.ExecuteReader();
                    User userData = new();

                    while (reader.Read())
                    {
                        userData.id = (int)reader["id"];
                        userData.name = (string)reader["nombreUsuario"];
                        userData.email = (string)reader["email"];
                        userData.fechaCreacion = (DateTime)reader["fechaCreacion"];
                        try
                        {
                            userData.imgURL = (string)reader["imgURL"];
                        }
                        catch
                        {
                            userData.imgURL = null;
                        }
                        userData.activo = (bool)reader["activo"];
                    }
                    conex.Close();
                    Console.WriteLine("Hooola mundo recargado");
                    return userData;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Respuesta de consulta ", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("/User/list")]
        public ActionResult<IEnumerable<User>> GetUserList()
        {
            try
            {
                using (MySqlConnection conex = new(config.GetConnectionString("MYSQL_FINANCE")))
                {
                    conex.Open();
                    MySqlCommand comando = conex.CreateCommand();
                    comando.CommandText = "select * from financedb.usuario where activo = 1";
                    MySqlDataReader reader = comando.ExecuteReader();
                    List<User> userDataList = new List<User>();

                    while (reader.Read())
                    {
                        User userData = new User();
                        userData.id = (int)reader["id"];
                        userData.name = (string)reader["nombreUsuario"];
                        userData.email = (string)reader["email"];
                        userData.fechaCreacion = (DateTime)reader["fechaCreacion"];
                        try
                        {
                            userData.imgURL = (string)reader["imgURL"];
                        }
                        catch
                        {
                            userData.imgURL = null;
                        }
                        userData.activo = (bool)reader["activo"];
                        userDataList.Add(userData);
                    }
                    conex.Close();
                    Console.WriteLine("data " + userDataList);
                    return userDataList;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Respuesta de consulta ", ex);
                throw;
            }
        }
    }
}
