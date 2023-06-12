
namespace Reflection.Task1
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
