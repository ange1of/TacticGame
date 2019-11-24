using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace game {

    static class Config {
        private static string configPath = Path.Join(Environment.CurrentDirectory, "Config\\appsettings.json");
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile(configPath).Build();

        public static string GetValue(string property) {
            return configuration.GetSection(property).Value;
        }
    }
}
