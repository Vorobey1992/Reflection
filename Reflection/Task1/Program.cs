using Reflection.Task1;

class Program : ConfigurationComponentBase
{

    public Program()
    {
        Settings = new ProgramSettings();
    }

    static void Main()
    {
        var program = new Program
        {
            Settings = new ProgramSettings()
        };

        // Загрузка настроек
        program.LoadSettings();

        Console.WriteLine("Before saving setting:");
        Console.WriteLine("MyFileStringSetting value: " + program.Settings.MyFileStringSetting.Value);
        Console.WriteLine("MyConfigurationManagerStringSetting value: " + program.Settings.MyConfigurationManagerStringSetting.Value);

        Console.WriteLine("MyFileIntSetting value: " + program.Settings.MyFileIntSetting.Value);
        Console.WriteLine("MyConfigurationManagerIntSetting value: " + program.Settings.MyConfigurationManagerIntSetting.Value);

        Console.WriteLine("MyFileFloatSetting value: " + program.Settings.MyFileFloatSetting.Value);
        Console.WriteLine("MyConfigurationManagerFloatSetting value: " + program.Settings.MyConfigurationManagerFloatSetting.Value);

        Console.WriteLine("MyFileTimeSpanSetting value: " + program.Settings.MyFileTimeSpanSetting.Value);
        Console.WriteLine("MyConfigurationManagerTimeSpanSetting value: " + program.Settings.MyConfigurationManagerTimeSpanSetting.Value);


        // Изменение настроек
        program.Settings.MyFileStringSetting.Value = "New file's value N3";
        program.Settings.MyConfigurationManagerStringSetting.Value = "New ConfigurationManager's value N3";
        program.Settings.MyFileIntSetting.Value = 56;
        program.Settings.MyConfigurationManagerIntSetting.Value = 64;
        program.Settings.MyFileFloatSetting.Value = 5.643f;
        program.Settings.MyConfigurationManagerFloatSetting.Value = 3.53f;
        program.Settings.MyFileTimeSpanSetting.Value = TimeSpan.MinValue;
        program.Settings.MyConfigurationManagerTimeSpanSetting.Value = TimeSpan.MaxValue;

        // Сохранение настроек
        program.SaveSettings(); 
        
        Console.WriteLine("After saving setting:");
        Console.WriteLine("MyFileStringSetting value: " + program.Settings.MyFileStringSetting.Value);
        Console.WriteLine("MyConfigurationManagerStringSetting value: " + program.Settings.MyConfigurationManagerStringSetting.Value);

        Console.WriteLine("MyFileIntSetting value: " + program.Settings.MyFileIntSetting.Value);
        Console.WriteLine("MyConfigurationManagerIntSetting value: " + program.Settings.MyConfigurationManagerIntSetting.Value);

        Console.WriteLine("MyFileFloatSetting value: " + program.Settings.MyFileFloatSetting.Value);
        Console.WriteLine("MyConfigurationManagerFloatSetting value: " + program.Settings.MyConfigurationManagerFloatSetting.Value);

        Console.WriteLine("MyFileTimeSpanSetting value: " + program.Settings.MyFileTimeSpanSetting.Value);
        Console.WriteLine("MyConfigurationManagerTimeSpanSetting value: " + program.Settings.MyConfigurationManagerTimeSpanSetting.Value);

    }
}