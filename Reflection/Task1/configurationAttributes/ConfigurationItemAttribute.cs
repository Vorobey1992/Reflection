namespace Reflection.Task1.configurationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; }
        public Type ProviderType { get; }

        public ConfigurationItemAttribute(string settingName, Type providerType)
        {
            SettingName = settingName;
            ProviderType = providerType;
        }
    }
}
