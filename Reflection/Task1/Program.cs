using Reflection.Task1;

class Program : ConfigurationComponentBase
{
    [FileConfigurationItem("MySetting")]
    public string MyFileSetting { get; set; }

    [ConfigurationManagerConfigurationItem("MySetting")]
    public string MyConfigurationManagerSetting { get; set; }

    public Program()
    {
        MyFileSetting = string.Empty;
        MyConfigurationManagerSetting = string.Empty;
    }


    static void Main()
    {
        var program = new Program();

        // Загрузка настроек
        program.LoadSettings();

        Console.WriteLine("MyFileSetting value: " + program.MyFileSetting);
        Console.WriteLine("MyConfigurationManagerSetting value: " + program.MyConfigurationManagerSetting);

        // Изменение настроек
        program.MyFileSetting = "New file's value";
        program.MyConfigurationManagerSetting = "New ConfigurationManager's value";

        // Сохранение настроек
        program.SaveSettings();
    }
}