
namespace Reflection.Task1.interfaces
{
    public interface IConfigurationProvider
    {
        GenericSetting<T> GetSetting<T>(string settingName);
        void SetSetting<T>(string settingName, T value);
    }
}
