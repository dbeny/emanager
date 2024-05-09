using System;
using System.Collections.Generic;

namespace Aprojekt {
    internal class Program {
        // https://media.discordapp.net/attachments/899955220468097065/1228283713021018172/rn_image_picker_lib_temp_2bf3f291-c801-48c3-84e3-06bb10001db3.jpg?ex=662b7b59&is=66190659&hm=66c89dc5b07c50b214b8ced650e0c75b38057abe667f8c220c025110530f7e48&=&format=webp&width=910&height=683
        //opciók után ott van az opció ID-je ha be van kapcsolva
        public static bool debug = false;
        private static List<Employee> employees;

        static void Main(string[] args) {
            string employeeFilepath = "data/employee.data";
            employees = Employee.LoadFromFile(employeeFilepath);
            string adminFilepath = "data/admin.data";
            List<Admin> admins = Utils.LoadAdminsFromFile(adminFilepath);
            
            bool login = false;
            Employee e = null;

            MenuHandler handler = new();

            //globális cuccos
            MenuOption backOption = new MenuOption("back", "Visszalépés")
                .SetMenuHandler(handler);

            //főmenü
            MenuOption personalOption = new MenuOption("personal", "Adataim")
                .SetMenuHandler(handler);
            MenuOption adminOption = new MenuOption("admin", "Admin panel")
                .SetMenuHandler(handler);
            MenuOption exitOption = new MenuOption("exit", "Kilépés")
                .SetMenuHandler(handler);
            List<MenuOption> startOptions = new() {
                personalOption, adminOption, exitOption
            };
            Menu startMenu = new Menu("start").AddOptions(startOptions);
            handler.AddMenu(startMenu);

            //saját adataim oldal
            Menu personalMenu = new Menu("personal", () => {
                Utils.ShowEmployee(e);
                Utils.Spacer(20);
                return false;
            }).AddOption(backOption).SetParent(startMenu);
            handler.AddMenu(personalMenu);

            //admin oldal
            MenuOption employeeOption = new MenuOption("employee", "Dolgozók megtekintése")
                .SetMenuHandler(handler);
            MenuOption employeeCreateOption = new MenuOption("employee_create", "Új dolgozó felvétele")
                .SetMenuHandler(handler);
            MenuOption employeeRemoveOption = new MenuOption("employee_remove", "Dolgozó eltávolítása")
                .SetMenuHandler(handler);
            List<MenuOption> adminOptions = new() {
                employeeOption, employeeCreateOption, employeeRemoveOption, backOption
            };
            Menu adminMenu = new Menu("admin").AddOptions(adminOptions).SetParent(startMenu);
            handler.AddMenu(adminMenu);

            //kilistázott dolgozók oldal
            Menu employeeMenu = new Menu("employee", () => {
                Utils.ShowEmployees(employees);
                return false;
            }).AddOption(backOption).SetParent(adminMenu);
            handler.AddMenu(employeeMenu);

            //dolgozó felvétele oldal
            Menu employeeCreateMenu = new Menu("employee_create", () => {
                ec_start:
                int id = employees.Count;
                Console.Write("Név: ");
                string name = Console.ReadLine();
                Console.Write("Születési év (ÉÉÉÉ/HH/NN): ");
                string dateOfBirth = Console.ReadLine();
                Console.Write("Telefonszám: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Pozíció: ");
                string position = Console.ReadLine();
                Console.Write("Admin (i/n): ");
                bool admin = (Console.ReadLine().Equals("i"));
                string adminPass = "NONE";
                if (admin) {
                    adminPw:
                    Console.Write("Admin jelszó (min. 6 karakter): ");
                    adminPass = Console.ReadLine();
                    if (adminPass.Length < 6) {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Utils.ClearLine();
                        goto adminPw;
                    }
                }
                Employee e = new(id, name, dateOfBirth, phoneNumber, position, admin);
                MenuHandler.ClearConsole();
                Utils.ShowEmployee(e);
                // menüopciók??
                Console.Write("Megfelelőek az adatok (i/n): ");
                string approved = Console.ReadLine();
                if (approved.Equals("i")) {
                    employees.Add(e);
                    Utils.SaveEmployeesToFile(employeeFilepath, employees);
                    if (admin) {
                        Admin a = new(id, adminPass);
                        admins.Add(a);
                        Utils.SaveAdminsToFile(adminFilepath, admins);
                    }
                } else {
                    Console.Write("Újrakezdés (i/n): ");
                    string restart = Console.ReadLine();
                    if (restart.Equals("i")) {
                        MenuHandler.ClearConsole();
                        goto ec_start;
                    }
                }
                return true;
            }).SetParent(adminMenu);
            handler.AddMenu(employeeCreateMenu);

            Menu employeeRemoveMenu = new Menu("employee_remove", () => {
                // todo
                Utils.ShowEmployee(new Employee(99, "asd", "asd", "asd", "asd", true));
                Console.ReadKey();
                return true;
            }).SetParent(adminMenu);
            handler.AddMenu(employeeRemoveMenu);

            while (true) {
                if (!login) {
                    e_login: 
                    Console.Write("Teljes név (Példa János): ");
                    string name = Console.ReadLine();
                    Console.Write("Születési dátum (ÉÉÉÉ/HH/NN): ");
                    string dateOfBirth = Console.ReadLine();
                    e = Utils.GetEmployee(employees, name, dateOfBirth);
                    if (e == null) {
                        MenuHandler.ClearConsole();
                        Console.WriteLine("Hibás adatok!");
                        goto e_login;
                    }
                    if (e.IsAdmin()) {
                    a_login:
                        Console.Write("Admin jelszó: ");
                        string password = Console.ReadLine();
                        Admin admin = Utils.GetAdmin(admins, e.GetId());
                        if (!admin.Authenticate(password)) {
                            MenuHandler.ClearConsole();
                            Console.WriteLine("Hibás jelszó!");
                            goto a_login;
                        }
                        handler.GiveAdmin();
                    } else adminOption.SetLabel(adminOption.GetLabel() + " (NEM ELÉRHETŐ)");
                    login = true;
                } else {
                    handler.HandlePage();
                }
            }

            // - dolgozó kirúgás
            // program személyre szabás (random cég) - üres verzió + random verzió
            // kód dokumentáció (MIELOTT ELKEZDENEM ki kell torolni mindent ami nem az + /**/ -t hasznalni
            // github readme (+kód +használati dokumentáció)
        }
    }
}