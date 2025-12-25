using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Windows;
using System.Data.Common;

namespace Mazada.Services
{
    class MySQLRepository<T> : IRepository<T>
    {
        private const string ConnectionString = "server=127.0.0.1;uid=root;pwd=admin;database=mazada";

        public void Add(T entity)
        {
            //Get the actual info from generic class(e.g class name, properties, methods, ...)
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<ColumnAttribute>().Name;
            //Get all properties(getters-setters) with column names and values from marked column attribute excluding that is primary key and auto increment
            var properties = t.GetProperties()
                .Where(attr => attr.GetCustomAttribute<ColumnAttribute>() != null && !attr.GetCustomAttribute<ColumnAttribute>().AutoIncrement && !attr.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            //We will use columns, parameterNames(placeholder names), and parameters(mysql) later in a few line
            var columns = new List<string>();
            var paramaterNames = new List<string>();
            var parameters = new List<MySqlParameter>();

            foreach (var prop in properties)
            {
                string col = prop.GetCustomAttribute<ColumnAttribute>().Name; //Get columnn name from marked column attribute
                var paramName = $"@{col}";
                columns.Add(col);
                paramaterNames.Add(paramName);
                parameters.Add(new MySqlParameter(paramName, prop.GetValue(entity) ?? DBNull.Value));
            }

            var commandText = $"INSERT INTO {tableName} ({string.Join(",", columns)}) VALUES ({string.Join(",", paramaterNames)})";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    connection.Open();
                    command.Prepare();
                    command.Parameters.AddRange(parameters.ToArray()); //Add parameters to the actual command parameters
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public async Task AddAsync(T entity)
        {
            //Get the actual info from generic class(e.g class name, properties, methods, ...)
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;
            //Get all properties(getters-setters) with column names and values from marked column attribute excluding that is primary key and auto increment
            var properties = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null && !propInfo.GetCustomAttribute<ColumnAttribute>().AutoIncrement && !propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            //We will use columns, parameterNames(placeholder names), and parameters(mysql) later in a few line
            var columns = new List<string>();
            var paramaterNames = new List<string>();
            var parameters = new List<MySqlParameter>();

            foreach (var prop in properties)
            {
                string col = prop.GetCustomAttribute<ColumnAttribute>().Name; //Get columnn name from marked column attribute
                string paramName = $"@{col}";
                columns.Add(col);
                paramaterNames.Add(paramName);
                parameters.Add(new MySqlParameter(paramName, prop.GetValue(entity) ?? DBNull.Value)); //Adding placeholder for command
            }

            string commandText = $"INSERT INTO {tableName} ({string.Join(",", columns)}) VALUES ({string.Join(",", paramaterNames)})";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.AddRange(parameters.ToArray()); //Add parameters to the actual command parameters
                    await command.ExecuteNonQueryAsync();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Delete(T entity)
        {
            //Get the actual info from generic class
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;
            //Get a property that is a primaryKey
            var primaryKeyProp = t.GetProperties()
                .FirstOrDefault(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null && propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            var primaryKeyName = primaryKeyProp.GetCustomAttribute<ColumnAttribute>().Name;
            var primaryKeyValue = primaryKeyProp.GetValue(entity) ?? DBNull.Value;
            //Placeholder name for sql syntax
            var paramName = $"@{primaryKeyName}";
            var parameter = new MySqlParameter(paramName, primaryKeyValue);

            string commandText = $"DELETE FROM {tableName} WHERE {primaryKeyName} = {paramName}";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public async Task DeleteAsync(T entity)
        {
            //Get the actual info from generic class
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;
            //Find the property that is a primaryKey
            var primaryKeyProp = t.GetProperties()
                .FirstOrDefault(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null && propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            var primaryKeyName = primaryKeyProp.GetCustomAttribute<ColumnAttribute>().Name;
            var primaryKeyValue = primaryKeyProp.GetValue(entity) ?? DBNull.Value;
            //Placeholder name for sql syntax
            var paramName = $"@{primaryKeyName}";
            var parameter = new MySqlParameter(paramName, primaryKeyValue);

            string commandText = $"DELETE FROM {tableName} WHERE {primaryKeyName} = {paramName}";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(parameter);
                    await command.ExecuteNonQueryAsync();

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            //Get the actual info from generic class
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;
            //Get all properties(getters, setters)
            var properties = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null);

            List<T> entities = new List<T>();

            string commandText = $"SELECT * FROM {tableName}";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            entities.Add(CreateEntityFromDBReader(properties, reader));
                    }
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return entities;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //Get the actual info from generic class
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;
            //Get all properties(getters, setters)
            var properties = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null);

            var entities = new List<T>();
            var commandText = $"SELECT * FROM {tableName}";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        entities.Add(CreateEntityFromDBReader(properties, reader));

            }
            return entities;
        }

        private T CreateEntityFromDBReader(IEnumerable<PropertyInfo> properties, DbDataReader reader)
        {
            //Create entity object
            T entity = Activator.CreateInstance<T>();

            //Fill the entity object with the properties
            foreach (var prop in properties)
            {
                string column = prop.GetCustomAttribute<ColumnAttribute>().Name;
                object value = reader[column]; //DB table column value

                if (value == DBNull.Value)
                    value = null;

                prop.SetValue(entity, value); //entity.SomeProperty = value;
            }

            return entity;
        }

        public T GetById(int id)
        {
            //Get the actual info from generic class
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;

            //Get all properties(getters, setters)
            var props = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null);

            //Find the property from marked column attribute that is a primary key
            var primaryKeyProp = props.FirstOrDefault(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);

            string column = primaryKeyProp.GetCustomAttribute<ColumnAttribute>().Name;

            var parameterName = $"@{column}";
            var commandText = $"SELECT * FROM {tableName} WHERE {column} = {parameterName}";

            T entity = default;

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue(parameterName, id);
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        entity = CreateEntityFromDBReader(props, reader);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return entity;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            //Get the actual info from generic class
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;

            //Get all properties(getters, setters)
            var props = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null);

            //Find the property from marked column attribute that is a primary key
            var primaryKeyProp = props.FirstOrDefault(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);

            string column = primaryKeyProp.GetCustomAttribute<ColumnAttribute>().Name;
            var parameterName = $"@{column}";
            var commandText = $"SELECT * FROM {tableName} WHERE {column} = {parameterName}";

            T entity = default(T);

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue(parameterName, id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        entity = CreateEntityFromDBReader(props, reader);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return entity;
        }

        public void Update(T entity)
        {
            /*'Get the actual info from generic class Question: Why I decide not to use entity.getType()? 
             * because using T works for every case and doesn't require argument*/
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;

            //Get all properties from marked column attribute that is not primary key and autoincrement
            var properties = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null &&
                !propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey &&
                !propInfo.GetCustomAttribute<ColumnAttribute>().AutoIncrement
                );

            //Find the property from marked column attribute that is a primary key
            var primaryKeyProp = t.GetProperties()
                .FirstOrDefault(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            //Name of the id column
            string idColumn = primaryKeyProp.GetCustomAttribute<ColumnAttribute>().Name;

            var columns = new List<string>();
            var assignments = new List<string>(); //e.g col1 = @value, col2 = @value, ...
            var parameters = new List<MySqlParameter>();

            foreach(var prop in properties)
            {
                string col = prop.GetCustomAttribute<ColumnAttribute>().Name;
                string paramName = $"@{col}";
                columns.Add(col);
                assignments.Add($"{col} = {paramName}");
                parameters.Add(new MySqlParameter(paramName, prop.GetValue(entity) ?? DBNull.Value));
            }

            var commandText = $"UPDATE {tableName} SET {string.Join(",", assignments)} WHERE {idColumn} = @{idColumn}";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddRange(parameters.ToArray());
                    command.Parameters.AddWithValue($"@{idColumn}", primaryKeyProp.GetValue(entity));
                    command.ExecuteNonQuery();

                }catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        public async Task UpdateAsync(T entity)
        {
            /*'Get the actual info from generic class Question: Why I decide not to use entity.getType()? 
             * because using T works for every case and doesn't require argument*/
            Type t = typeof(T);
            //Get the table name from marked table attribute
            string tableName = t.GetCustomAttribute<TableAttribute>().Name;

            //Get all properties from marked column attribute that is not primary key and autoincrement
            var properties = t.GetProperties()
                .Where(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>() != null &&
                !propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey &&
                !propInfo.GetCustomAttribute<ColumnAttribute>().AutoIncrement
                );

            //Find the property from marked column attribute that is a primary key
            var primaryKeyProp = t.GetProperties()
                .FirstOrDefault(propInfo => propInfo.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            //Name of the id column
            string idColumn = primaryKeyProp.GetCustomAttribute<ColumnAttribute>().Name;

            var columns = new List<string>();
            var assignments = new List<string>(); //e.g col1 = @value, col2 = @value, ...
            var parameters = new List<MySqlParameter>();

            foreach (var prop in properties)
            {
                string col = prop.GetCustomAttribute<ColumnAttribute>().Name;
                string paramName = $"@{col}";
                columns.Add(col);
                assignments.Add($"{col} = {paramName}");
                parameters.Add(new MySqlParameter(paramName, prop.GetValue(entity) ?? DBNull.Value));
            }

            var commandText = $"UPDATE {tableName} SET {string.Join(",", assignments)} WHERE {idColumn} = @{idColumn}";

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(commandText, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.AddRange(parameters.ToArray());
                    command.Parameters.AddWithValue($"@{idColumn}", primaryKeyProp.GetValue(entity));
                    await command.ExecuteNonQueryAsync();

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
    }
}
