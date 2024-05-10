using System.Collections.Generic;
using System.IO;

namespace Aprojekt {
    /*
    Admin adatok osztálya (alkalmazott id. és jelszó)
    */
    class Admin {
        private int id;
        private string password;

        public Admin(int id, string password) {
            this.id = id;
            this.password = password;
        }

        //egy megadott fájlból egy listába betölti az adminok adatait
        public static List<Admin> LoadFromFile(string filename) {
            List<Admin> admins = new();
            StreamReader sr = new(filename);
            while (!sr.EndOfStream) {
                string line = sr.ReadLine();
                if (line.StartsWith("#")) continue;
                string[] lineSplit = line.Split(";", 2);
                if (lineSplit.Length != 2) continue;
                Admin admin = new(int.Parse(lineSplit[0]), lineSplit[1]);
                admins.Add(admin);
            }
            sr.Close();
            return admins;
        }

        //egy megadott jelszóhoz egyezteti az admin jelszavát
        public bool Authenticate(string password) {
            return (password == this.password);
        }

        public int GetId() { 
            return id; 
        }

        public string GetPassword() {
            return password;
        }
    }
}
