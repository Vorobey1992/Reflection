namespace Reflection.Task1.configurationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationManagerConfigurationItemAttribute : ConfigurationItemAttribute
    {
        public ConfigurationManagerConfigurationItemAttribute(string settingName)
            : base(settingName, typeof(ConfigurationManagerConfigurationProvider))
        {
        }
    }
}
