using Reflection.Task1.configurationAttributes;

namespace Reflection.Task1
{
    public class ProgramSettings
    {
        [FileConfigurationItem("StringSetting")]
        public GenericSetting<string> MyFileStringSetting { get; set; }

        [ConfigurationManagerConfigurationItem("StringSetting")]
        public GenericSetting<string> MyConfigurationManagerStringSetting { get; set; }
        
        [FileConfigurationItem("IntSetting")]
        public GenericSetting<int> MyFileIntSetting { get; set; }

        [ConfigurationManagerConfigurationItem("IntSetting")]
        public GenericSetting<int> MyConfigurationManagerIntSetting { get; set; }

        [FileConfigurationItem("FloatSetting")]
        public GenericSetting<float> MyFileFloatSetting { get; set; }

        [ConfigurationManagerConfigurationItem("FloatSetting")]
        public GenericSetting<float> MyConfigurationManagerFloatSetting { get; set; }

        [FileConfigurationItem("TimeSpanSetting")]
        public GenericSetting<TimeSpan> MyFileTimeSpanSetting { get; set; }

        [ConfigurationManagerConfigurationItem("TimeSpanSetting")]
        public GenericSetting<TimeSpan> MyConfigurationManagerTimeSpanSetting { get; set; }

        public ProgramSettings()
        {
            MyFileStringSetting = new GenericSetting<string>("StringSetting");
            MyConfigurationManagerStringSetting = new GenericSetting<string>("StringSetting");
            MyFileIntSetting = new GenericSetting<int>("IntSetting");
            MyConfigurationManagerIntSetting = new GenericSetting<int>("IntSetting");
            MyFileFloatSetting = new GenericSetting<float>("FloatSetting");
            MyConfigurationManagerFloatSetting = new GenericSetting<float>("FloatSetting");
            MyFileTimeSpanSetting = new GenericSetting<TimeSpan>("TimeSpanSetting");
            MyConfigurationManagerTimeSpanSetting = new GenericSetting<TimeSpan>("TimeSpanSetting");
        }
    }
}