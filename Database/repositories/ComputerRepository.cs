using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

   public IEnumerable<Computer> GetAll()
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var computers = connection.Query<Computer>("SELECT * FROM Computers");
        return computers;
    }


    public Computer Save(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)",computer);
        return computer;
    }

    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var computer = connection.QueryFirstOrDefault<Computer>("SELECT * FROM Computers WHERE id = @Id;", new {Id = id});
       return computer;        
    }

    public Computer Update(Computer computer)
    {   
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id = @Id;",computer);
        return computer;
    }

    public void Delete(int id){
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("DELETE FROM Computers WHERE id = @Id;",new {Id = id});
    }

    public bool ExitsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        var result = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Computers WHERE id = @Id;",new {Id = id});
        return result;
    }    

}