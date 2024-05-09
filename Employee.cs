using System.Collections.Generic;
using System.IO;

namespace Aprojekt {
    internal class Employee {
        private readonly int id;
        private readonly string name;
        private string dateOfBirth;
        private string phoneNumber;
        private string position;
        private bool admin;

        public Employee(int id, string name, string dateOfBirth, string phoneNumber, string position, bool admin) {
            this.id = id;
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.position = position;
            this.admin = admin;
        }

        public static Employee GetById(List<Employee> employees, int id) {
            foreach (Employee emp in employees) {
                if (emp.GetId() == id) return emp;
            }
            return null;
        }

        public static List<Employee> LoadFromFile(string file) {
            List<Employee> employees = new();
            StreamReader sr = new(file);
            while (!sr.EndOfStream) {
                string line = sr.ReadLine();
                if (line.StartsWith("#")) continue;
                string[] lineSplit = line.Split(";", 6);
                if (lineSplit.Length != 6) continue;
                Employee e = new(
                    //azonosító
                    int.Parse(lineSplit[0]),
                    //név
                    lineSplit[1],
                    //szül.év
                    lineSplit[2],
                    //tel.szám
                    lineSplit[3],
                    //pozíció
                    lineSplit[4],
                    //admin-e? - ha igen akkor true, ha bármi más false
                    (lineSplit[5] == "igen")
                );
                employees.Add(e);
            }
            sr.Close();
            return employees;
        }

        public int GetId() { return id; }
        public string GetName() { return name; }
        public string GetDateOfBirth() { return dateOfBirth; }
        public string GetPhoneNumber() { return phoneNumber; }
        public string GetPosition() { return position; }
        public bool IsAdmin() { return admin; }
        public string IsAdminString() { return (admin ? "igen" : "nem"); }

        public void SetDateOfBirth(string dateOfBirth) { this.dateOfBirth = dateOfBirth; }
        public void SetPhoneNumber(string phoneNumber) { this.phoneNumber = phoneNumber; }
        public void SetPosition(string position) { this.position = position; }
        public void SetAdmin(bool admin) { this.admin = admin; }
    }
}