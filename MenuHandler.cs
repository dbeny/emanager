using System;
using System.Collections.Generic;

namespace Aprojekt {
    class MenuHandler {
        private List<Menu> menus;
        private Menu currentMenu;
        private bool admin;

        public MenuHandler() {
            menus = new List<Menu>();
            admin = false;
        }

        public void GiveAdmin() {
            admin = true;
        }

        public void RevokeAdmin() {
            admin = false;
        }

        public bool HasAdmin() {
            return admin;
        }

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

        public void HandlePage() {
            ClearConsole();
            if (currentMenu.IsFunctional()) {
                bool returned = currentMenu.InvokeFunction();
                if (returned) {
                    SwitchMenu(currentMenu.GetParent());
                    return;
                }
            }
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

        public void AddMenu(Menu menu) {
            menus.Add(menu);
            if (menus.Count == 1) {
                this.currentMenu = menu;
            }
        }

        public static void ClearConsole() {
            Console.Clear();
        }

        public Menu GetMenu(string id) {
            foreach (Menu m in menus) {
                if (id.Equals(m.GetId())) return m;
            }
            return null;
        }

    }
}
