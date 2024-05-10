using System;
using System.Collections.Generic;

namespace Aprojekt {
    /*
    A programnak egy darab MenuHandler-je van ami irányítja, hogy a felhasználó mikor, hogy és milyen menüket lát a képernyőn.
    */
    class MenuHandler {
        private List<Menu> menus;
        private Menu currentMenu;
        private bool admin;

        public MenuHandler() {
            menus = new List<Menu>();
            admin = false;
        }

        //beállítja, hogy a bejelentkezett felhasználó admin
        public void GiveAdmin() {
            admin = true;
        }

        //visszaadja, hogy megkapta-e az admin jogosultságot a MenuHandler
        public bool HasAdmin() {
            return admin;
        }

        //átvált egy új Menu-re paraméterből majd meghívja a HandlePage függvényt
        public bool SwitchMenu(Menu newMenu) {
            if (!newMenu.GetId().Equals(currentMenu)) {
                if (newMenu.GetId() == "admin" && !HasAdmin()) {
                    HandlePage();
                    return false;
                }
                currentMenu = newMenu;
                HandlePage();
                return true;
            }
            return false;
        } 

        /*
        irányítja, hogy a felhasználó léppen mit lát a képernyőn
        a navigációt is itt kezeljük amivel a felh. 
        vagy kilép a programból vagy egy másik menüre vált
        */
        public void HandlePage() {
            ClearConsole();
            Console.WriteLine("----- EMPLOYEE MANAGER -----");
            Console.WriteLine();
            /*
            ha funkciós a menü (lásd: Menu.cs) akkor azt hívja meg 
            és ha visszakap egy értéket akkor visszamegy a szülőmenübe
            */
            if (currentMenu.IsFunctional()) {
                bool returned = currentMenu.InvokeFunction();
                if (returned) {
                    SwitchMenu(currentMenu.GetParent());
                    return;
                }
            }
            //navigáció
            List<MenuOption> options = currentMenu.GetOptions();
            for (int i = 0; i < options.Count; i++) {
                MenuOption opt = options[i];
                Console.WriteLine(i + 1 + ". " + opt.GetLabel() + (Program.debug ? " (" + opt.GetId() + ")" : ""));
            }
            if (options.Count > 0) {
                Console.WriteLine();
                Console.Write("Navigáció (1" + (options.Count == 1 ? "" : "-" + options.Count) + "): ");
                int selected = int.Parse(Console.ReadLine());
                if (selected > 0 && selected <= options.Count) {
                    MenuOption opt = options[selected - 1];
                    string id = opt.Trigger();
                    Console.WriteLine("id: " + id);
                    //ha a visszatérés menü lett meghívva akkor a szülőmenüre megy vissza
                    if (id == "back") {
                        SwitchMenu(currentMenu.GetParent());
                        return;
                    }
                    Menu newMenu = GetMenu(id);
                    if (newMenu == null) {
                        HandlePage();
                        return;
                    }
                    SwitchMenu(newMenu);
                }
            } else HandlePage();
        }

        //menü hozzáadása a listába. ha ez az első akkor ki is mutatja
        public void AddMenu(Menu menu) {
            menus.Add(menu);
            if (menus.Count == 1) {
                this.currentMenu = menu;
            }
        }

        public static void ClearConsole() {
            Console.Clear();
        }

        //menü lekérése id alapján
        public Menu GetMenu(string id) {
            foreach (Menu m in menus) {
                if (id.Equals(m.GetId())) return m;
            }
            return null;
        }

    }
}
