

namespace Reflection.Task1
{
    public interface IConfigurationProvider
    {
        object? GetSetting(string settingName);
        void SetSetting(string settingName, object value);
    }
}
