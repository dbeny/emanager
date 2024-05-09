using System;
using System.Collections.Generic;

namespace Aprojekt {
    class Menu {
        private string id;
        private List<MenuOption> options;
        private bool functional;
        private Func<bool> function;
        private Menu parent;

        public Menu(string id) {
            this.id = id;
            options = new List<MenuOption>();
        }

        public Menu(string id, Func<bool> function) {
            this.id = id;
            options = new List<MenuOption>();
            functional = true;
            this.function = function;
        }

        public bool InvokeFunction() {
            return function.Invoke();
        }

        public bool IsFunctional() { 
            return functional; 
        }

        public Menu GetParent() {
            return parent;
        }

        public Menu SetParent(Menu parent) {
            this.parent = parent;
            return this;
        }

        public Menu AddOption(MenuOption option) {
            options.Add(option);
            return this;
        }

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

        public MenuOption GetOption(string id) {
            foreach (MenuOption opt in options) {
                if (opt.GetId().Equals(id)) return opt;
            }
            return null;
        } 
    }
}
