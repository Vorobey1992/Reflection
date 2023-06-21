using Reflection.Task1.interfaces;
using System.Configuration;

namespace Reflection.Task1
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        private readonly string configFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\App.config"));

        public GenericSetting<T> GetSetting<T>(string settingName)
        {
            ExeConfigurationFileMap configFileMap = new()
            {
                ExeConfigFilename = configFilePath
            };

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            var settings = config.AppSettings.Settings;

            if (settings[settingName] != null)
            {
                string? settingValue = settings[settingName].Value;
                T? value = default;

                if (settingValue != null && typeof(T) == typeof(TimeSpan))
                {
                    if (TimeSpan.TryParse(settingValue, out var timeSpanValue))
                    {
                        value = (T)(object)timeSpanValue;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid value for setting '{settingName}': {settingValue}");
                    }
                }
                else
                {
                    value = settingValue != null ? (T)Convert.ChangeType(settingValue, typeof(T)) : default;
                }

                return new GenericSetting<T>(settingName) { Value = value };
            }
            else
            {
                Console.WriteLine($"Setting '{settingName}' not found in the configuration file.");
                return null; // Возвращаем null или выбрасываем исключение в зависимости от требований
            }
        }

        public void SetSetting<T>(string settingName, T value)
        {
            ExeConfigurationFileMap configFileMap = new()
            {
                ExeConfigFilename = configFilePath
            };

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            var settings = config.AppSettings.Settings;

            if (settings[settingName] != null)
            {
                string? settingValue = value != null ? value.ToString() : string.Empty;
                settings[settingName].Value = settingValue;
                try
                {
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    Console.WriteLine("The settings have been successfully saved.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when saving the settings: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"Setting '{settingName}' not found in the configuration file.");
            }
        }
    }
}