using Animals_DB.Models;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

namespace Animals_DB.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        List<Animal> animals = new List<Animal>();
        string query = $"SELECT IdAnimal, Name, Category, Area, Description FROM animal ORDER BY {orderBy}";
        
        
        using (SqlConnection connection = new
                   SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Animal animal = new Animal()
                        {
                            IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                            Name = reader["Name"].ToString(),
                            Category = reader["Category"].ToString(),
                            Area = reader["Area"].ToString(),
                            Description = reader["Description"].ToString()
                        };
                        animals.Add(animal);
                    }
                }
            }
        }

        return animals;
    }

    public Animal GetAnimal(int animalId)
    {
        using (SqlConnection connection = new
                   SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = 
                    "SELECT IdAnimal, Name, Category, Area, Description FROM ANIMAL WHERE IdAnimal = @IdAnimal";
                command.Parameters.AddWithValue("@IdAnimal", animalId);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                
                    Animal animal = new Animal()
                    {
                        IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Area = reader["Area"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                    return animal;
                }
            }
        }
    }

    public int AddAnimal(Animal animal)
    {
        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionString:MyConnectionString"]))
        {
            connection.Open();
            string query = "INSERT INTO ANIMAL VALUES(@Name, @Description, @Category, @Area)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Name", animal.Name);
            cmd.Parameters.AddWithValue("@Description", animal.Description);
            cmd.Parameters.AddWithValue("@Category", animal.Category);
            cmd.Parameters.AddWithValue("@Area", animal.Area);

            int affectedRows = cmd.ExecuteNonQuery();

            return affectedRows;
        }
    }

    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            "UPDATE ANIMAL SET Name=@Name, Desciption=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var counter = cmd.ExecuteNonQuery();
        return counter;
    }

    public int DeleteAnimal(int animalId)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:MyConnectionString"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM ANIMAL WHERE IdAnimal=@IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", animalId);
        
        var counter = cmd.ExecuteNonQuery();
        return counter;
    }
}