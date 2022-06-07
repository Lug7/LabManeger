using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.repositories; 

class ComputerRepository
{
   private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;


    public List<Computer> All
    {
        get
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

 
public Computer Save(Computer computer)
{
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);"; 
        command.Parameters.AddWithValue("$id",computer.Id);
        command.Parameters.AddWithValue("$ram",computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();
        return computer;
}
 public Computer GetById(int id){        

        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Computers WHERE id = $id";
        command.Parameters.AddWithValue("$id",id);

        var reader = command.ExecuteReader();

        reader.Read();
        var computer = readerToComputer(reader);
        connection.Close();

        return computer;

    }

     public void Delete(int id){
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id",id);

        command.ExecuteNonQuery();
        connection.Close();
    }

      public void Update(Computer computer){
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = @"
            UPDATE 
                Computers   
            SET
                ram = $ram,
                processor = $processor
            WHERE
                id = $id
            "; 
            command.Parameters.AddWithValue("$id", computer.Id);
            command.Parameters.AddWithValue("$ram", computer.Ram);
            command.Parameters.AddWithValue("$processor", computer.Processor);

            command.ExecuteNonQuery();
            connection.Close();
    }

    public bool existsById(int id)
    {
         var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
         
         var command = connection.CreateCommand();

        command.CommandText = "Select count(id) FROM Computers WHERE id=$id";
        command.Parameters.AddWithValue("$id",id);

        bool result = Convert.ToBoolean(command.ExecuteScalar());

        return result;
       
    }

private Computer readerToComputer(SqliteDataReader reader)
{
    var computer = new Computer(
             reader.GetInt32(0), 
            reader.GetString(1), 
            reader.GetString(2)
    );

    return computer;
}

}
