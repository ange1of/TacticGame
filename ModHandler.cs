using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

namespace game {
    static class ModHandler {
        public static IEnumerable<Type> GetAllUnits() {
            string mainPath = Path.Join(Environment.CurrentDirectory, Config.GetValue("BaseEntitiesPath"));
            string modePath = Path.Join(Environment.CurrentDirectory, Config.GetValue("ModsPath"));

            var units = new List<Type>();
            foreach (var type in Assembly.LoadFile(mainPath).GetTypes()) {
                if (type.IsSubclassOf(typeof(Unit))) {
                    units.Add(type);
                }
            }

            if (Directory.Exists(modePath)) {
                foreach (var file in Directory.GetFiles(modePath, "*.dll")) {
                    foreach (var type in Assembly.LoadFile(file).GetTypes()) {
                        if (type.IsSubclassOf(typeof(Unit))) {
                            units.Add(type);
                        }
                    }
                }
            }

            return units;
        }

        public static IEnumerable<Type> GetAllEffects() {
            string mainPath = Path.Join(Environment.CurrentDirectory, Config.GetValue("BaseEntitiesPath"));
            string modePath = Path.Join(Environment.CurrentDirectory, Config.GetValue("ModsPath"));

            var effects = new List<Type>();
            foreach (var type in Assembly.LoadFile(mainPath).GetTypes()) {
                if (type.IsSubclassOf(typeof(BaseEffect))) {
                    effects.Add(type);
                }
            }

            if (Directory.Exists(modePath)) {
                foreach (var file in Directory.GetFiles(modePath, "*.dll")) {
                    foreach (var type in Assembly.LoadFile(file).GetTypes()) {
                        if (type.IsSubclassOf(typeof(BaseEffect))) {
                            effects.Add(type);
                        }
                    }
                }
            }

            return effects;
        }

        public static IEnumerable<Type> GetAllCasts() {
            string mainPath = Path.Join(Environment.CurrentDirectory, Config.GetValue("BaseEntitiesPath"));
            string modePath = Path.Join(Environment.CurrentDirectory, Config.GetValue("ModsPath"));

            var casts = new List<Type>();
            foreach (var type in Assembly.LoadFile(mainPath).GetTypes()) {
                if (type.GetInterface("ICast") != null) {
                    casts.Add(type);
                }
            }

            if (Directory.Exists(modePath)) {
                foreach (var file in Directory.GetFiles(modePath, "*.dll")) {
                    foreach (var type in Assembly.LoadFile(file).GetTypes()) {
                        if (type.GetInterface("ICast") != null) {
                            casts.Add(type);
                        }
                    }
                }
            }

            return casts;
        }

        public static IEnumerable<Type> GetAllModifiers() {
            string mainPath = Path.Join(Environment.CurrentDirectory, Config.GetValue("BaseEntitiesPath"));
            string modePath = Path.Join(Environment.CurrentDirectory, Config.GetValue("ModsPath"));

            var modifiers = new List<Type>();
            foreach (var type in Assembly.LoadFile(mainPath).GetTypes()) {
                if (type.GetInterface("IModifier") != null) {
                    modifiers.Add(type);
                }
            }

            if (Directory.Exists(modePath)) {
                foreach (var file in Directory.GetFiles(modePath, "*.dll")) {
                    foreach (var type in Assembly.LoadFile(file).GetTypes()) {
                        if ((type.GetInterface("IModifier") != null)) {
                            modifiers.Add(type);
                        }
                    }
                }
            }

            return modifiers;
        }
    } 
}