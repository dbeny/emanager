﻿namespace Aprojekt {
    /*
    Egy adott MenuOption egy adott Menu osztályhoz kötődik az `id`-vel
    */
    class MenuOption {
        private readonly string id;
        private string label;
        private MenuHandler handler;

        public MenuOption(string id, string label) {
            this.id = id;
            this.label = label;
        }

        public MenuOption SetMenuHandler(MenuHandler handler) {
            this.handler = handler;
            return this;
        }

        public MenuHandler GetMenuHandler() {
            return handler;
        }

        //visszaadja az id-t ha nem kilépés történik
        public string Trigger() {
            switch (id) {
                case "exit":
                    System.Environment.Exit(0);
                    return "";
                default:
                    return id;
            }
        }

        public string GetId() { return id; }
        public string GetLabel() { return label; }

        public void SetLabel(string label) { this.label = label; }
    }
}
