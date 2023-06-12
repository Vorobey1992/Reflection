

namespace Reflection.Task1
{
    public class ConfigurationComponentBase
    {
        public void SaveSettings()
        {
            // Сохранение настроек для всех свойств с атрибутом ConfigurationItemAttribute
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                {
                    var value = property.GetValue(this);

                    if (Activator.CreateInstance(attribute.ProviderType) is IConfigurationProvider provider && value != null)
                    {
                        provider.SetSetting(attribute.SettingName, value);
                    }
                }
            }
        }

        public void LoadSettings()
        {
            // Загрузка настроек для всех свойств с атрибутом ConfigurationItemAttribute
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                {
                    if (Activator.CreateInstance(attribute.ProviderType) is IConfigurationProvider provider)
                    {
                        var value = provider.GetSetting(attribute.SettingName);
                        property.SetValue(this, value);
                    }
                }
            }
        }
    }
}
