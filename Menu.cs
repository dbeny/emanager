using System;
using System.Collections.Generic;

namespace Aprojekt {
    /*
    a program egyik fő eleme a Menu. ezek tartalmaznak esetleges szöveget (funkcionalitás) 
    vagy még több opciót

    funkcióval rendelkező menük:
    - bármilyen kód lefuttatására képesek (szöveg kiírása, szöveg bekérése, stb)
    funkcióval NEM rendelkező menük: 
    - egyéb menüket tudnak tartalmazni
    */
    class Menu {
        private string id;
        private List<MenuOption> options;
        private bool functional;
        private Func<bool> function;
        private Menu parent;

        //normál menü
        public Menu(string id) {
            this.id = id;
            options = new List<MenuOption>();
        }

        //funkciós menü
        public Menu(string id, Func<bool> function) {
            this.id = id;
            options = new List<MenuOption>();
            functional = true;
            this.function = function;
        }

        //funkció meghívása
        public bool InvokeFunction() {
            return function.Invoke();
        }

        //megkérdezi hogy funkciós-e a menü
        public bool IsFunctional() { 
            return functional; 
        }

        //szülőmenü lekérése
        public Menu GetParent() {
            return parent;
        }

        //szülőmenü beállítása
        public Menu SetParent(Menu parent) {
            this.parent = parent;
            return this;
        }

        //opció hozzáadása
        public Menu AddOption(MenuOption option) {
            options.Add(option);
            return this;
        }

        //opciók hozzáadása listából
        public Menu AddOptions(List<MenuOption> options) {
            this.options = options;
            return this;
        }

        public String GetId() {
            return id;
        }

        public List<MenuOption> GetOptions() {
            return options;
        }

        //opciók lekérése id alapján
        public MenuOption GetOption(string id) {
            foreach (MenuOption opt in options) {
                if (opt.GetId().Equals(id)) return opt;
            }
            return null;
        } 
    }
}
