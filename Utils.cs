using System;
using System.Collections.Generic;
using System.IO;

namespace Aprojekt {
    internal class Utils {
        public static Employee GetEmployee(List<Employee> employees, string name, string dateOfBirth) {
            foreach (Employee e in employees) {
                if (e.GetName().Equals(name) && e.GetDateOfBirth().Equals(dateOfBirth)) {
                    return e;
                }
            }
            return null;
        }

        // soronként kilistázza a dolgozókat
        public static void ShowEmployeesCode(List<Employee> employees, bool spacer) {
            foreach (Employee e in employees) {
                Console.WriteLine(
                    e.GetId() + ";" +
                    e.GetName() + ";" +
                    e.GetDateOfBirth() + ";" +
                    e.GetPhoneNumber() + ";" +
                    e.GetPosition() + ";" +
                    e.IsAdminString()
                );
                if (spacer) Utils.Spacer(25);
            }
        }

        public static void ShowEmployees(List<Employee> employees) {
            foreach (Employee e in employees) {
                ShowEmployee(e);
                Utils.Spacer(25);
            }
        }

        // kilistáz egy adott dolgozót
        public static void ShowEmployee(Employee e) {
            Console.WriteLine("Azonosító: " + e.GetId());
            Console.WriteLine("Név: " + e.GetName());
            Console.WriteLine("Születési év: " + e.GetDateOfBirth());
            Console.WriteLine("Telefonszám: " + e.GetPhoneNumber());
            Console.WriteLine("Pozíció: " + e.GetPosition());
            Console.WriteLine("Admin: " + e.IsAdminString());
        }

        // elválasztó vonal a konzolba
        public static void Spacer(int size) {
            Console.Write(new string('-', size));
            Console.WriteLine();
        }

        // todo: SaveAdminsToFile
        public static void SaveAdminsToFile(string path, List<Admin> admins) {
            StreamWriter sw = new(path);
            foreach (Admin admin in admins) {
                sw.WriteLine(admin.GetId() + ";" + admin.GetPassword());
            }
            sw.Flush();
            sw.Close();
        }

        public static Admin GetAdmin(List<Admin> admins, int id) {
            foreach (Admin a in admins) {
                if (a.GetId() == id) return a;
            }
            return null;
        }

        public static void SaveEmployeesToFile(string path, List<Employee> employees) {
            StreamWriter sw = new(path);
            foreach (Employee e in employees) {
                sw.WriteLine(
                    e.GetId() + ";" +
                    e.GetName() + ";" +
                    e.GetDateOfBirth() + ";" +
                    e.GetPhoneNumber() + ";" +
                    e.GetPosition() + ";" +
                    e.IsAdminString()
                );
            }
            sw.Flush();
            sw.Close();
        }

        public static void ClearLine() {
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }

    }
}