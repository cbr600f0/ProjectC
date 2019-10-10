using System;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using System.IO;
using SQLite;
using ProjectC.iOS.Business;

[assembly: Dependency(typeof(DatabaseConnection_IOS))]
namespace ProjectC.iOS.Business
{
    class DatabaseConnection_IOS : ISQLiteInterface
    {
        public DatabaseConnection_IOS()
        {
        }
        public SQLiteConnection GetConnection()
        {
            String fileName = "UserDatabase.db3";
            String documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String libraryPath = Path.Combine(documentsPath, "..", "Library");
            String path = Path.Combine(libraryPath, fileName);
            SQLiteConnection connection = new SQLiteConnection(path, false);
            return connection;
        }
    }
}