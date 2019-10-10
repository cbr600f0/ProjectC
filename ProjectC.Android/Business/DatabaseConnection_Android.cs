using System;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using System.IO;
using SQLite;
using ProjectC.Droid.Business;

[assembly: Dependency(typeof(DatabaseConnection_Android))]
namespace ProjectC.Droid.Business
{
    class DatabaseConnection_Android : ISQLiteInterface
    {
        public DatabaseConnection_Android()
        {
        }
        public SQLiteConnection GetConnection()
        {
            String fileName = "UserDatabase.db3";
            String documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            String path = Path.Combine(documentPath, fileName);
            SQLiteConnection connection = new SQLiteConnection(path, false);
            return connection;
        }
    }
}