using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Repos.DatabaseSizeRepo;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Aktitic.HrProject.BL.Managers;
public class DatabaseSizeService(IConfiguration configuration,GetDatabaseSize databaseSize)
{
    private readonly string _connectionString = configuration.GetConnectionString("MonsterDb");

    public async Task<string> GetDatabaseSizeAsync()
    {
        string query = "EXEC sp_spaceused;";
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await using SqlCommand command = new SqlCommand(query, connection);
        await using SqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return reader["database_size"].ToString();
        }

        return "No data";
    }
    
    public async Task<long> GetQueryDataSizeAsync()
    {
        string query = @"
        SELECT 
            SUM(DATALENGTH(Name)) AS TotalSizeInBytes
        FROM 
             Departments 
        WHERE 
             IsDeleted == 1 ";

        await using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await using SqlCommand command = new SqlCommand(query, connection);
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt64(result);
    }

    public async Task<long> GetActiveDataSizeAsync()
    {
        return await databaseSize.GetTotalActiveDataSize();
    }
    
    public async Task<long> GetNonActiveDataSizeAsync()
    {
        return await databaseSize.GetTotalNonActiveDataSize();
    }

    public void DeleteNonActiveData()
    {
        databaseSize.DeleteNonActiveData();
    }
}