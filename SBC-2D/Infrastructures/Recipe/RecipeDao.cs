using Dapper;
using Dapper.FluentMap.Mapping;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SBC_2D.Infrastructures.Recipe
{
    public class RecipeDao
    {
        private readonly string _sqLiteFilePath;
        private readonly string _connectionString;

        public RecipeDao(string sqLiteFilePath)
        {
            if (string.IsNullOrWhiteSpace(sqLiteFilePath))
                throw new ArgumentException("SQLite file path is invalid.", nameof(sqLiteFilePath));

            _sqLiteFilePath = sqLiteFilePath;
            _connectionString = $@"Data Source={_sqLiteFilePath}";
        }

        public Recipe GetByName(string name)
        {
            Recipe result = null;
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM RECIPE WHERE NAME = @Name";
                result = conn.QuerySingleOrDefault<Recipe>(sql, new { Name = name });
            }
            return result;
        }

        public List<Recipe> GetAll()
        {
            List<Recipe> recipes = new List<Recipe>();
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sqlCmd = "SELECT * FROM RECIPE;";
                recipes = conn.Query<Recipe>(sqlCmd).AsList();
            }
            return recipes;
        }

        public List<string> GetAllNames()
        {
            List<string> names = new List<string>();
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sqlCmd = "SELECT NAME FROM RECIPE WHERE NAME != @Name;";
                names = conn.Query<string>(sqlCmd, new { Name = "Edit" }).AsList();
            }
            return names;
        }

        public bool Update(Recipe recipe)
        {
            bool isUpdated = false;
            DynamicParameters paramDict = new DynamicParameters();
            var propertyMaps = Dapper.FluentMap.FluentMapper.EntityMaps
                .FirstOrDefault(m => m.Key == typeof(Recipe))
                .Value.PropertyMaps
                .Where(p => p.ColumnName != "NAME").ToList();

            foreach (var propertyMap in propertyMaps)
            {
                var prop = propertyMap.PropertyInfo;
                var colName = propertyMap.ColumnName;
                var value = prop.GetValue(recipe);
                paramDict.Add($"@{colName}", value);
            }
            paramDict.Add("@Name", recipe.Name);
            string setClause = string.Join(", ", paramDict.ParameterNames.Select(p => $"{p} = @{p}"));
            string sql = $"UPDATE RECIPE SET {setClause}, UPDATED_AT = CURRENT_TIMESTAMP WHERE NAME = @Name;";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                isUpdated = conn.Execute(sql, paramDict) > 0;
            }

            return isUpdated;
        }

        public bool Update(string name, string columnName, object value)
        {
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sqlCmd = $@"UPDATE RECIPE SET {columnName} = @Value, UPDATED_AT = CURRENT_TIMESTAMP WHERE NAME = @Name";
                return conn.Execute(sqlCmd, new { Value = value, Name = name }) > 0;
            }
        }

        public bool Create(Recipe recipe)
        {
            bool isCreated = false;

            var propertyMaps = Dapper.FluentMap.FluentMapper.EntityMaps
                .FirstOrDefault(m => m.Key == typeof(Recipe)).Value.PropertyMaps;

            DynamicParameters paramDict = new DynamicParameters();

            foreach (var propertyMap in propertyMaps)
            {
                var prop = propertyMap.PropertyInfo;
                var colName = propertyMap.ColumnName;
                var value = prop.GetValue(recipe);
                paramDict.Add($"@{colName}", value);
            }

            string columnNames = string.Join(", ", propertyMaps.Select(p => p.ColumnName));
            string values = string.Join(", ", propertyMaps.Select(p => "@" + p.ColumnName));

            string sqlCmd = $"INSERT INTO RECIPE ({columnNames}, CREATED_AT, UPDATED_AT) VALUES ({values}, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                isCreated = conn.Execute(sqlCmd, paramDict) > 0;
            }

            return isCreated;
        }

        public bool Delete(string name)
        {
            bool isDelete = false;

            string sqlCmd = $"DELETE FROM RECIPE WHERE NAME = @Name";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                isDelete = conn.Execute(sqlCmd, new { Name = name }) > 0;
            }
            return isDelete;
        }
    }
}
