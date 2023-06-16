namespace Reflection.Task1.configurationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FileConfigurationItemAttribute : ConfigurationItemAttribute
    {
        public FileConfigurationItemAttribute(string settingName)
            : base(settingName, typeof(FileConfigurationProvider))
        {
        }
    }
}
