using Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace ClassDAL
{
    public class IdbRepository : dbRepository
    {
        //private readonly string _connection;

        //public IdbRepository(IDbConnection dbConnection)
        //{
        //    _connection = dbConnection.ConnectionString;
        //}
        private readonly string _connection;

        public IdbRepository(UserMgMtContext context)
        {
            // DbContext se connection string
            _connection = context.Database.GetConnectionString();
        }
        public void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            string connectionString = _connection;
            DataAccessHandeler DataAccessHandeler = new(connectionString);

            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public int InsertWithNoOfTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            int res = 0;
            string connectionString = _connection;
            DataAccessHandeler DataAccessHandeler = new(connectionString);

            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    res = command.ExecuteNonQuery();
                }
            }
            return res;
        }


        public DataSet ByProcedure(string ProcedureName)
        {
            try
            {
                DataSet dataset = new DataSet();

                string connectionString = Convert.ToString(_connection);
                DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
                using (var connection = DataAccessHandeler.CreateConnection())
                {
                    connection.Open();

                    using (var command = DataAccessHandeler.CreateCommand(ProcedureName, CommandType.StoredProcedure, connection))
                    {
                        command.CommandTimeout = 3600;

                        var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                        dataAdaper.Fill(dataset);

                        return dataset;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values)
        {
            try
            {
                DataSet dataset = new DataSet();

                if (Parameter.Length != Values.Length)
                {
                    DataTable dataTable = dataset.Tables.Add("Error");
                    dataTable.Columns.Add("Message");
                    dataTable.Rows.Add("Parameter and Values lengths are not equal.");
                    return dataset;
                }
                string connectionString = Convert.ToString(_connection);
                DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
                using (var connection = DataAccessHandeler.CreateConnection())
                {
                    connection.Open();

                    using (var command = DataAccessHandeler.CreateCommand(ProcedureName, CommandType.StoredProcedure, connection))
                    {
                        command.CommandTimeout = 3600;
                        if (Parameter != null)
                        {
                            for (int i = 0; i < Parameter.Length; i++)
                            {
                                ((SqlParameterCollection)command.Parameters).AddWithValue("@" + Parameter[i], Values[i]);
                            }
                        }

                        var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                        dataAdaper.Fill(dataset);

                        return dataset;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string[] TableParam, DataTable[] TableType)
        {
            try
            {
                DataSet dataset = new DataSet();

                if (Parameter.Length != Values.Length)
                {
                    DataTable dataTable = dataset.Tables.Add("Error");
                    dataTable.Columns.Add("Message");
                    dataTable.Rows.Add("Parameter and Values lengths are not equal.");
                    return dataset;
                }
                else if (TableParam.Length != TableType.Length)
                {
                    DataTable dataTable = dataset.Tables.Add("Error");
                    dataTable.Columns.Add("Message");
                    dataTable.Rows.Add("TableParam and TableType lengths are not equal.");
                    return dataset;
                }
                string connectionString = Convert.ToString(_connection);
                DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
                using (var connection = DataAccessHandeler.CreateConnection())
                {
                    connection.Open();

                    using (var command = DataAccessHandeler.CreateCommand(ProcedureName, CommandType.StoredProcedure, connection))
                    {
                        command.CommandTimeout = 3600;
                        if (Parameter != null)
                        {
                            for (int i = 0; i < Parameter.Length; i++)
                            {
                                ((SqlParameterCollection)command.Parameters).AddWithValue("@" + Parameter[i], Values[i]);
                            }

                            for (int j = 0; j < TableParam.Length; j++)
                            {
                                ((SqlParameterCollection)command.Parameters).AddWithValue("@" + TableParam[j], TableType[j]);
                            }
                        }

                        var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                        dataAdaper.Fill(dataset);

                        return dataset;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters, out int id)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            id = 0;
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    object generatedId = command.ExecuteScalar();
                    id = Convert.ToInt32(generatedId);
                }
            }
            return id;
        }
        public DataTable InsertWithReturnRecord(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var dataset = new DataSet();
                    var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                    dataAdaper.Fill(dataset);

                    if (dataset != null && dataset.Tables.Count > 0)
                        return dataset.Tables[0];
                    else
                        return new DataTable();
                }
            }
        }

        public DataTable UpdateWithReturnRecord(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataSet = new DataSet();
                    var dataAdapter = DataAccessHandeler.CreateAdapter(command);
                    dataAdapter.Fill(dataSet);

                    if (dataSet != null && dataSet.Tables.Count > 0)
                        return dataSet.Tables[0];
                    else
                        return new DataTable();
                }
            }
        }
        public void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();

                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        public object GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[]? parameters = null)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    return command.ExecuteScalar();
                }
            }
        }
        public DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            try
            {
                using (var connection = DataAccessHandeler.CreateConnection())
                {
                    connection.Open();

                    using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                    {
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                        dataAdaper.Fill(dataset);

                        if (dataset != null && dataset.Tables.Count > 0)
                            return dataset.Tables[0];
                        else
                            return new DataTable();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public DataSet GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            try
            {
                using (var connection = DataAccessHandeler.CreateConnection())
                {
                    connection.Open();

                    using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                    {
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                        dataAdaper.Fill(dataset);

                        if (dataset != null && dataset.Tables.Count > 0)
                            return dataset;
                        else
                            return new DataSet();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public int InsertwithUserDefineTableType(string commandText, CommandType commandType, IDbDataParameter[] parameters, string TableParam, DataTable TableType)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            int id = 0;
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                    }

                    //Assuming SQL Server, cast to SqlParameter
                    if (command is SqlCommand sqlCommand)
                    {
                        var tvpParameter = sqlCommand.Parameters.AddWithValue("@" + TableParam, TableType);
                        tvpParameter.SqlDbType = SqlDbType.Structured;
                        tvpParameter.TypeName = "dbo." + TableParam; // Replace with your actual TVP type
                    }

                    object generatedId = command.ExecuteScalar();
                    id = Convert.ToInt32(generatedId);
                }
            }
            return id;
        }

        public void InsertUDTableType(string commandText, CommandType commandType, IDbDataParameter[] parameters, string TableParam, DataTable TableType)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            int id = 0;
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                    }

                    //Assuming SQL Server, cast to SqlParameter
                    if (command is SqlCommand sqlCommand)
                    {
                        var tvpParameter = sqlCommand.Parameters.AddWithValue("@" + TableParam, TableType);
                        tvpParameter.SqlDbType = SqlDbType.Structured;
                        tvpParameter.TypeName = "dbo." + TableParam; // Replace with your actual TVP type
                    }

                    object generatedId = command.ExecuteScalar();
                    //  id = Convert.ToInt32(generatedId);
                }
            }

        }

        public DataTable InsertUDTableTypeReturnRecord(string commandText, CommandType commandType, IDbDataParameter[] parameters, string tableParam, DataTable tableType)
        {
            // Connection string
            string connectionString = Convert.ToString(_connection);

            // Create data access handler
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);

            // Create a DataTable to store the results
            DataTable resultTable = new DataTable();

            // Create a connection
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                // Open the connection
                connection.Open();

                // Create a command
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    // Add parameters if provided
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    // Add the table-valued parameter
                    if (command is SqlCommand sqlCommand)
                    {
                        // Add the table-valued parameter
                        var tvpParameter = sqlCommand.Parameters.AddWithValue("@" + tableParam, tableType);
                        tvpParameter.SqlDbType = SqlDbType.Structured;
                        tvpParameter.TypeName = "dbo." + tableParam; // Specify the TVP type name
                    }

                    // Execute the command and read the data
                    try
                    {
                        using (var dataReader = command.ExecuteReader())
                        {
                            // Load the data from the DataReader into the resultTable
                            resultTable.Load(dataReader);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException($"Error executing command: {ex.Message}", ex);
                    }
                }
            }

            // Return the result DataTable
            return resultTable;
        }

        public void UpdateWithUserDefineTableType(string commandText, CommandType commandType, IDbDataParameter[] parameters, string TableParam, DataTable TableType)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            int id = 0;
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                {
                    command.CommandTimeout = 0;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                    }

                    //Assuming SQL Server, cast to SqlParameter
                    if (command is SqlCommand sqlCommand)
                    {
                        var tvpParameter = sqlCommand.Parameters.AddWithValue("@" + TableParam, TableType);
                        tvpParameter.SqlDbType = SqlDbType.Structured;
                        tvpParameter.TypeName = "dbo." + TableParam; // Replace with your actual TVP type
                    }
                    object generatedId = command.ExecuteScalar();
                    //id = Convert.ToInt32(generatedId);


                }
            }

        }

        public DataTable InsertMulitpleUDTableTypeReturnRecord(string commandText, CommandType commandType, IDbDataParameter[] parameters, string[] tableParamNames, DataTable[] tableTypes)
        {
            if (tableParamNames.Length != tableTypes.Length)
            {
                throw new ArgumentException("The number of table parameter names must match the number of DataTables.");
            }
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            using (var connection = DataAccessHandeler.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandTimeout = 0;
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    for (int i = 0; i < tableTypes.Length; i++)
                    {
                        var tableParam = new SqlParameter
                        {
                            ParameterName = tableParamNames[i],
                            SqlDbType = SqlDbType.Structured,
                            TypeName = tableTypes[i].TableName,
                            Value = tableTypes[i]
                        };
                        command.Parameters.Add(tableParam);
                    }

                    var dataTable = new DataTable();
                    using (var adapter = new SqlDataAdapter((SqlCommand)command))
                    {
                        adapter.Fill(dataTable);
                    }

                    return dataTable;
                }
            }
        }

        public DataTable GetDataTableVehicleRegistration(string commandText, CommandType commandType, IDbDataParameter[]? parameters = null)
        {
            string connectionString = Convert.ToString(_connection);
            DataAccessHandeler DataAccessHandeler = new DataAccessHandeler(connectionString);
            try
            {
                using (var connection = DataAccessHandeler.CreateConnection())
                {
                    connection.Open();

                    using (var command = DataAccessHandeler.CreateCommand(commandText, commandType, connection))
                    {
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = DataAccessHandeler.CreateAdapter(command);
                        dataAdaper.Fill(dataset);

                        if (dataset != null && dataset.Tables.Count > 0)
                            return dataset.Tables[0];
                        else
                            return new DataTable();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
