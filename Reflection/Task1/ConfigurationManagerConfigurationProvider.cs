using System.Configuration;

namespace Reflection.Task1
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public object? GetSetting(string settingName)
        {
            // Чтение значения настройки из ConfigurationManager
            string? settingValue = ConfigurationManager.AppSettings[settingName];
            return settingValue != null ? (object?)settingValue : null;
        }

        public void SetSetting(string settingName, object value)
        {
            string configFilePath = "C:\\EPAM\\C#\\.NET Mentoring Program Basics 2023\\Reflection\\Reflection\\App.config";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var settings = config.AppSettings.Settings;

            if (settings[settingName] != null)
            {
                settings[settingName].Value = value.ToString();
                try
                {
                    //config.Save(ConfigurationSaveMode.Modified);
                    //ConfigurationManager.RefreshSection("appSettings");
                    config.SaveAs(configFilePath, ConfigurationSaveMode.Modified);
                    Console.WriteLine("The settings have been successfully saved.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when saving the settings: " + ex.Message);
                }
            }
            else
            {
                // Обработка случая, когда настройка не найдена
                // Можно выбросить исключение или выполнить другие действия
                Console.WriteLine($"Setting '{settingName}' not found in the configuration file.");
            }
        }

    }
}
