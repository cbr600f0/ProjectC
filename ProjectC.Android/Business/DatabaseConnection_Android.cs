using System;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net;
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
            SQLitePlatformAndroid platform = new SQLitePlatformAndroid();
            SQLiteConnection connection = new SQLiteConnection(platform, path);
            return connection;
        }
    }
}