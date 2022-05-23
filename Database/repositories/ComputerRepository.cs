using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.repositories; 

class ComputerRepository
{
   private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;
  

    public List<Computer> GetAll()
    {
        var Computers = new List<Computer>();

        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Computers;";
        
        var reader = command.ExecuteReader();

        while (reader.Read())
        {   
            var computer = new Computer(
                reader.GetInt32(0), 
                reader.GetString(1), 
                reader.GetString(2)
            );

            Computers.Add(computer);            
        }
        connection.Close();

        return Computers;
    
    }
}