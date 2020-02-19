using MousePlayback.Db;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MousePlayback
{
    public class Database
    {
        /// <summary>
        /// Contains the path to the SQLite Database of RemindMe
        /// </summary>
        public static readonly string databaseFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MousePlayBack\\Database.db";

        //The neccesary query to execute to create the table settings
        private const string TABLE_SETTINGS = "CREATE TABLE [Settings] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [RepeatTimes] bigint DEFAULT(0) NOT NULL, [RepeatForever] bigint DEFAULT(0) NOT NULL, [RandomizeInput] bigint DEFAULT(0) NOT NULL, [HotKeyStartStopRecording] text DEFAULT('F1') NOT NULL, [HotKeyPlaybackRecording] text DEFAULT('F2') NOT NULL, [RndSleepTime] bigint NULL, [RndPixels] bigint NULL);";

        private static Settings settings;

        private Database() { }

        /// <summary>
        /// If the user deleted the .db file, or if the user is using this app for the first time, this method created the neccesary database + tables.
        /// </summary>
        public static void CreateDatabaseIfNotExist()
        {            
            if (!System.IO.File.Exists(databaseFile))
                CreateDatabase();
            else
            {
                //great! the .db file exists. Now lets check if the user's .db file is up-to-date. let's see if the reminder table has all the required columns.
                if (HasAllTables())
                {
                    if (!HasAllColumns())
                        InsertNewColumns(); //not up to date. insert !
                }
                else
                {
                    InsertMissingTables();
                    //re-run the method, since the .db file **should** now have all the tables.
                    CreateDatabaseIfNotExist();
                }

            }        
        }

        public static Settings Settings
        {
            get
            {
                using (DbEntities db = new DbEntities())
                {
                    int count = db.Settings.Where(o => o.Id >= 0).Count();
                    if (count == 0)
                    {
                        settings = new Settings();
                        settings.RepeatTimes = 0;
                        settings.RepeatForever = 0;
                        settings.RandomizeInput = 0;
                        settings.HotKeyStartStopRecording = "F1";
                        settings.HotKeyPlaybackRecording = "F2";
                        settings.RndPixels = 5;
                        settings.RndSleepTime = 4000;
                        UpdateSettings(settings);
                    }
                    else
                        settings = (from s in db.Settings select s).ToList().FirstOrDefault();

                    db.Dispose();
                }

                return settings;
            }
        }

        public static string HotKeyStartStopRecording
        {
            get { return Settings.HotKeyStartStopRecording; }
            set
            {
                settings = Settings;
                settings.HotKeyStartStopRecording = value;
                UpdateSettings(settings);
            }
        }

        public static string HotKeyPlaybackRecording
        {
            get { return Settings.HotKeyPlaybackRecording; }
            set
            {
                settings = Settings;
                settings.HotKeyPlaybackRecording = value;
                UpdateSettings(settings);
            }
        }

        public static int RepeatTimes
        {
            get { return (int)Settings.RepeatTimes; }
            set
            {
                settings = Settings;
                settings.RepeatTimes = value;
                UpdateSettings(settings);
            }
        }

        public static bool RepeatForever
        {
            get { return Settings.RepeatForever == 1; }
            set
            {
                settings = Settings;
                settings.RepeatForever = Convert.ToInt32(value);
                UpdateSettings(settings);
            }
        }

        public static bool RandomizeInput
        {
            get { return Settings.RandomizeInput == 1; }
            set
            {
                settings = Settings;
                settings.RandomizeInput = Convert.ToInt32(value);
                UpdateSettings(settings);
            }
        }

        public static long? RndSleepTime
        {
            get
            {
                if (Settings.RndSleepTime.HasValue)
                    return Settings.RndSleepTime;
                else
                    return 4000;
            }
            set
            {
                settings = Settings;
                settings.RndSleepTime = value;
                UpdateSettings(settings);
            }
        }

        public static long? RndPixels
        {
            get
            {
                if (Settings.RndPixels.HasValue)
                    return Settings.RndPixels;
                else
                    return 5;
            }
            set
            {
                settings = Settings;
                settings.RndPixels = value;
                UpdateSettings(settings);
            }
        }      

        private static void UpdateSettings(Settings set)
        {
            using (DbEntities db = new DbEntities())
            {

                var count = db.Settings.Where(o => o.Id >= 0).Count();
                if (count > 0)
                {
                    db.Settings.Attach(set);
                    var entry = db.Entry(set);
                    entry.State = System.Data.Entity.EntityState.Modified; //Mark it for update                                
                    db.SaveChanges();                                      //push to database
                    db.Dispose();
                }
                else
                {//The settings table is still empty
                    db.Settings.Add(set);
                    db.SaveChanges();
                    db.Dispose();
                }
            }
        }

        private static void CreateDatabase()
        {
            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = "data source = " + databaseFile;
            conn.Open();

            SQLiteCommand tableSettings = new SQLiteCommand();

            tableSettings.CommandText = TABLE_SETTINGS;

            tableSettings.Connection = conn;

            tableSettings.ExecuteNonQuery();


            conn.Close();
            conn.Dispose();

        }
        /// <summary>
        /// Checks wether the table x has column x
        /// </summary>
        /// <param name="columnName">The column you want to check on</param>
        /// <param name="table">The table you want to check on</param>
        /// <returns></returns>
        private static bool HasColumn(string columnName, string table)
        {
            using (DbEntities db = new DbEntities())
            {
                try
                {
                    var t = db.Database.SqlQuery<object>("SELECT " + columnName + " FROM " + table).ToList();
                    db.Dispose();
                    return true;
                }
                catch (SQLiteException)
                {
                    db.Dispose();
                    //if (ex.Message.ToLower().Contains("no such column"))
                    //{
                    return false;
                    //}                                        
                }
            }
        }

        /// <summary>
        /// Checks if the user's .db file has all the columns from the database model.
        /// </summary>
        /// <returns></returns>
        private static bool HasAllColumns()
        {

            var settingNames = typeof(Settings).GetProperties().Select(property => property.Name).ToList();

            foreach (string columnName in settingNames)
            {
                if (!HasColumn(columnName, "settings"))
                    return false; //aww damn! the user has an outdated .db file!                
            }            

            return true;
        }

        /// <summary>
        /// Checks if the database has the given table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static bool HasTable(string table, DbEntities db)
        {
            try
            {
                var result = db.Database.ExecuteSqlCommand("select * from " + table);
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if the user's .db file has all the tables from the database model.
        /// </summary>
        /// <returns></returns>
        private static bool HasAllTables()
        {
            using (DbEntities db = new DbEntities())
            {
                if (HasTable("settings", db))
                    return true;
                else
                    return false;
            }

        }

        /// <summary>
        /// Inserts all missing tables into the user's .db file 
        /// </summary>
        private static void InsertMissingTables()
        {
            using (DbEntities db = new DbEntities())
            {
                SQLiteConnection conn = new SQLiteConnection();
                conn.ConnectionString = "data source = " + databaseFile;
                conn.Open();
                
                SQLiteCommand tableSettings = new SQLiteCommand();

                tableSettings.CommandText = TABLE_SETTINGS;
                
                tableSettings.Connection = conn;

                if (!HasTable("Settings", db))
                    tableSettings.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
                db.Dispose();
            }
        }

        /// <summary>
        /// This method will insert every missing column for each table into the database. Will only be called if HasallColumns() returns false. This means the user has an outdated .db file
        /// </summary>
        private static void InsertNewColumns()
        {
            using (DbEntities db = new DbEntities())
            {
                //every column that SHOULD exist                
                var settingNames = typeof(Settings).GetProperties().Select(property => property.Name).ToArray();                

                foreach (string column in settingNames)
                {
                    if (!HasColumn(column, "settings"))
                        db.Database.ExecuteSqlCommand("ALTER TABLE SETTINGS ADD COLUMN " + column + " " + GetSettingColumnSqlType(column));
                }
                
                db.SaveChanges();
                db.Dispose();
            }
        }

        /// <summary>
        /// Gets the SQLite data types of the settings columns, "text not null", "bigint null", etc
        /// </summary>
        /// <param name="columnName">The column you want to know the data type of</param>
        /// <returns>Data type of the column</returns>
        private static string GetSettingColumnSqlType(string columnName)
        {
            //Yes, this is not really the "correct" way of dealing with a problem, but after a lot of searching it's quite a struggle
            //to get the data types of the sqlite columns, especially when they're nullable.
            switch (columnName)
            {
                case "Id": return "INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL";
                case "RepeatTimes": return "bigint DEFAULT (0) NOT NULL";
                case "RepeatForever": return "bigint DEFAULT (0) NOT NULL";
                case "RandomizeInput": return "bigint DEFAULT (0) NOT NULL";
                case "HotKeyStartStopRecording": return "text DEFAULT 'F1' NOT NULL";
                case "HotKeyPlaybackRecording": return "text DEFAULT 'F2' NOT NULL";
                case "RndSleepTime": return "bigint DEFAULT 2000 NULL";
                case "RndPixels": return "bigint DEFAULT 5 NULL";
                default: return "text NULL";
            }
        }
    }
}
