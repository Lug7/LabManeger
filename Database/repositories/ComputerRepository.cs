using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.repositories; 

class ComputerRepository
{
    public List<Computer> GetAll()
    {
        var Computers = new List <Computer>(); 

        var connection = new SqliteConnection("Data Source=database.db");
       connection.Open();

       var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;"; 
        
        var reader = command.ExecuteReader();
        while(reader.Read())
        {

            var computer = new Computer(reader.GetInt32(0), 
            reader.GetString(1), 
            reader.GetString(2)
            );
            Computers.Add(computer);
          

        }

        reader.Close();
        connection.Close();

        return Computers;
    }
}