using Microsoft.Data.SqlClient;
using ShiftLogger.Models;
using System.Data.Common;

namespace ShiftLogger.Services
{
    public class ShiftService
    {   
        private IConfiguration _configuration;
        public ShiftService(IConfiguration configuration) { 
            _configuration = configuration;
        }

        public async Task<List<Shift>> GetShifts()
        {
            List<Shift> allShifts = new List<Shift>();
            using (SqlConnection sqlConnection =
                new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from ShiftLogger", sqlConnection);
                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Shift shift = new Shift();
                        shift.ShiftId = reader.GetGuid(0);
                        shift.StartTime = reader.GetDateTime(1);
                        shift.EndTime = reader.GetDateTime(2);
                        allShifts.Add(shift);
                    }
                }
                catch (DbException ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return allShifts;
        }

        public async Task<Shift> GetSingleShift(Guid shiftId)
        {
            Shift shift = new Shift();
            using (SqlConnection sqlConnection =
                new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from ShiftLogger WHERE shiftId = @id", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id",shiftId);
                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        shift.ShiftId = reader.GetGuid(0);
                        shift.StartTime = reader.GetDateTime(1);
                        shift.EndTime = reader.GetDateTime(2);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
            return shift;
        }

        public async Task<Shift> AddShift(Shift shift)
        {
            Guid guid = Guid.NewGuid();
            shift.ShiftId = guid;
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand =
                        new SqlCommand("Insert INTO ShiftLogger (shiftID, startTime, endTime) VALUES (@shiftID, @startTime, @endTime)", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@shiftID", shift.ShiftId);
                    sqlCommand.Parameters.AddWithValue("@startTime", shift.StartTime);
                    sqlCommand.Parameters.AddWithValue("@endTime", shift.EndTime);
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
            return shift;
        }

        public async Task DeleteShift(Guid shiftId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand =
                        new SqlCommand("DELETE FROM ShiftLogger WHERE shiftID = @shiftID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@shiftID", shiftId);
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
