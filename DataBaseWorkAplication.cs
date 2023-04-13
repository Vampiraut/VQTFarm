using Microsoft.Data.Sqlite;
using System.Reflection;
using System.Text.Json;

namespace VQTFarm
{
    public class DataBaseWorkAplication
    {
        public SqliteConnection connection;
        SqliteCommand command;
        SqliteDataReader reader;

        public DataBaseWorkAplication()
        {
            this.command = new SqliteCommand();
        }

        ~DataBaseWorkAplication()
        {
            this.connection.Close();
        }
        public void StartConnection(string connectionString)
        {
            this.connection = new SqliteConnection(connectionString);
            this.connection.Open();
            this.command.Connection = connection;
        }
        public int CreateTableByClass(object customClass)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";

            string columsNamesAndParametrs = "";

            foreach (FieldInfo field in customClass.GetType().GetFields())
            {
                if (field.Name.ToString() == "id")
                {
                    columsNamesAndParametrs += (field.Name.ToString() + " " + "INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, ");
                }
                else if ((field.FieldType == typeof(string)) || (field.FieldType == typeof(SortedDictionary<string, int>)))
                {
                    columsNamesAndParametrs += (field.Name.ToString() + " " + "TEXT NOT NULL, ");
                }
                else if (field.FieldType == typeof(int))
                {
                    columsNamesAndParametrs += (field.Name.ToString() + " " + "INTEGER NOT NULL, ");
                }
            }
            columsNamesAndParametrs = columsNamesAndParametrs[..^2];

            string commandText = "CREATE TABLE IF NOT EXISTS " + tableName + " (" + columsNamesAndParametrs + ")";
            this.command.CommandText = commandText;
            return this.command.ExecuteNonQuery();
        }
        public int AddClassToDB(object customClass)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";
            string columNames = "";
            string data = "";

            foreach (FieldInfo field in customClass.GetType().GetFields())
            {
                if (field.Name.ToString() != "id")
                {
                    columNames += (field.Name.ToString() + ", ");
                    if (field.FieldType == typeof(string))
                    {
                        data += "'" + field.GetValue(customClass).ToString() + "', ";
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        data += field.GetValue(customClass).ToString() + ", ";
                    }
                    else if (field.FieldType == typeof(SortedDictionary<string, int>))
                    {
                        data += "'" + JsonSerializer.Serialize<SortedDictionary<string, int>>(field.GetValue(customClass) as SortedDictionary<string, int>) + "', ";
                    }
                }
            }
            columNames = columNames[..^2];
            data = data[..^2];

            string commandText = "INSERT INTO " + tableName + " (" + columNames + ") VALUES (" + data + ")";
            this.command.CommandText = commandText;
            return this.command.ExecuteNonQuery();
        }
        public List<object>? ReadClassFromDB_AllClass(object customClass)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";

            string commandText = "SELECT * FROM " + tableName;
            this.command.CommandText = commandText;

            using (this.reader = command.ExecuteReader())
            {
                object genClass = new object();
                List<object> masOfGenClass = new List<object>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //------------------------------------------------------------------необходиа смена названий класса в зависимости от сборки
                        if (customClass.GetType() == typeof(CTFTeam))
                        {
                            genClass = new CTFTeam();
                        }
                        else if (customClass.GetType() == typeof(FlagHistory))
                        {
                            genClass = new FlagHistory();
                        }
                        //------------------------------------------------------------------
                        int i = 0;
                        foreach (FieldInfo field in genClass.GetType().GetFields())
                        {
                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(genClass, reader.GetString(i));
                            }
                            else if (field.FieldType == typeof(int))
                            {
                                field.SetValue(genClass, reader.GetInt32(i));
                            }
                            else if (field.FieldType == typeof(SortedDictionary<string, int>))
                            {
                                field.SetValue(genClass, JsonSerializer.Deserialize<SortedDictionary<string, int>>(reader.GetString(i)));
                            }
                            i++;
                        }
                        masOfGenClass.Add(genClass);
                    }
                    return masOfGenClass;
                }
                else
                {
                    return null;
                }
            }
        }
        public int DeleteClassFromDB_byParams(object customClass, List<string> paramsStrMas)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";

            string commandText = "DELETE FROM " + tableName + " WHERE ";

            foreach (string parametr in paramsStrMas)
            {
                commandText += parametr + " AND ";
            }
            commandText = commandText[..^5];

            this.command.CommandText = commandText;
            return this.command.ExecuteNonQuery();
        }
        public object? ReadClassFromDB_OneClass_byParams(object customClass, List<string> paramsStrMas)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";

            string commandText = "SELECT * FROM " + tableName + " WHERE ";

            foreach (string parametr in paramsStrMas)
            {
                commandText += parametr + " AND ";
            }
            commandText = commandText[..^5];

            this.command.CommandText = commandText;

            using (this.reader = command.ExecuteReader())
            {
                object genClass = new object();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //------------------------------------------------------------------необходиа смена названий класса в зависимости от сборки
                        if (customClass.GetType() == typeof(CTFTeam))
                        {
                            genClass = new CTFTeam();
                        }
                        else if (customClass.GetType() == typeof(FlagHistory))
                        {
                            genClass = new FlagHistory();
                        }
                        //------------------------------------------------------------------
                        int i = 0;
                        foreach (FieldInfo field in genClass.GetType().GetFields())
                        {
                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(genClass, reader.GetString(i));
                            }
                            else if (field.FieldType == typeof(int))
                            {
                                field.SetValue(genClass, reader.GetInt32(i));
                            }
                            else if (field.FieldType == typeof(SortedDictionary<string, int>))
                            {
                                field.SetValue(genClass, JsonSerializer.Deserialize<SortedDictionary<string, int>>(reader.GetString(i)));
                            }
                            i++;
                        }
                    }
                    return genClass;
                }
                else
                {
                    return null;
                }
            }
        }
        public int UpdateClassInDB_byParams(object customClass, List<string> paramsStrMas)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";

            string setInfo = "";

            foreach (FieldInfo field in customClass.GetType().GetFields())
            {
                if (field.Name.ToString() != "id")
                {
                    if (field.FieldType == typeof(string))
                    {
                        setInfo += field.Name.ToString() + "='" + field.GetValue(customClass).ToString() + "', ";
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        setInfo += field.Name.ToString() + "=" + field.GetValue(customClass).ToString() + ", ";
                    }
                    else if (field.FieldType == typeof(SortedDictionary<string, int>))
                    {
                        setInfo += field.Name.ToString() + "='" + JsonSerializer.Serialize<SortedDictionary<string, int>>(field.GetValue(customClass) as SortedDictionary<string, int>) + "', ";
                    }
                }
            }
            setInfo = setInfo[..^2];

            string commandText = "UPDATE " + tableName + " SET " + setInfo + " WHERE ";

            foreach (string parametr in paramsStrMas)
            {
                commandText += parametr + " AND ";
            }
            commandText = commandText[..^5];

            this.command.CommandText = commandText;
            return this.command.ExecuteNonQuery();
        }
        public List<object>? ReadClassFromDB_AllClass_byParams(object customClass, List<string> paramsStrMas)
        {
            string tableName = customClass.GetType().Name.ToString() + "s";

            string commandText = "SELECT * FROM " + tableName + " WHERE ";

            foreach (string parametr in paramsStrMas)
            {
                commandText += parametr + " AND ";
            }
            commandText = commandText[..^5];

            this.command.CommandText = commandText;

            using (this.reader = command.ExecuteReader())
            {
                object genClass = new object();
                List<object> masOfGenClass = new List<object>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //------------------------------------------------------------------необходиа смена названий класса в зависимости от сборки
                        if (customClass.GetType() == typeof(CTFTeam))
                        {
                            genClass = new CTFTeam();
                        }
                        else if (customClass.GetType() == typeof(FlagHistory))
                        {
                            genClass = new FlagHistory();
                        }
                        //------------------------------------------------------------------
                        int i = 0;
                        foreach (FieldInfo field in genClass.GetType().GetFields())
                        {
                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(genClass, reader.GetString(i));
                            }
                            else if (field.FieldType == typeof(int))
                            {
                                field.SetValue(genClass, reader.GetInt32(i));
                            }
                            else if (field.FieldType == typeof(SortedDictionary<string, int>))
                            {
                                field.SetValue(genClass, JsonSerializer.Deserialize<SortedDictionary<string, int>>(reader.GetString(i)));
                            }
                            i++;
                        }
                        masOfGenClass.Add(genClass);
                    }
                    return masOfGenClass;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
