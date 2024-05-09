namespace Aprojekt {
    class Admin {
        private int id;
        private string password;

        public Admin(int id, string password) {
            this.id = id;
            this.password = password;
        }

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
