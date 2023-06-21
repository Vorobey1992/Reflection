
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Loader;
using Reflection.Task1.configurationAttributes;
using Reflection.Task1.interfaces;

namespace Reflection.Task1
{
    public class ConfigurationComponentBase
    {
        public ProgramSettings? Settings { get; set; }

        private readonly List<Type> providerTypes;

        public ConfigurationComponentBase()
        {
            providerTypes = GetAvailableProviderTypes();
        }

        /*public void SaveSettings()
        {
            try
            {
                var properties = typeof(ProgramSettings).GetProperties();
                foreach (var property in properties)
                {
                    if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                    {
                        var value = property.GetValue(Settings);

                        if (value != null && value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(GenericSetting<>))
                        {
                            // Получение значения свойства Value
                            var genericSettingValue = value.GetType().GetProperty("Value")?.GetValue(value);

                            // Создание экземпляра провайдера
                            if (Activator.CreateInstance(attribute.ProviderType) is IConfigurationProvider provider)
                            {
                                Type providerType = attribute.ProviderType;
                                MethodInfo? setSettingMethod = providerType.GetMethod("SetSetting");
                                MethodInfo? genericSetSettingMethod = setSettingMethod.MakeGenericMethod(genericSettingValue.GetType());

                                // Вызов метода SetSetting<T> с передачей значения
                                genericSetSettingMethod?.Invoke(provider, new object?[] { attribute.SettingName, genericSettingValue });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving settings: " + ex.Message);
            }
        }


        public void LoadSettings()
        {
            try
            {
                var properties = typeof(ProgramSettings).GetProperties();

                foreach (var property in properties)
                {
                    if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                    {
                        if (Activator.CreateInstance(attribute.ProviderType) is IConfigurationProvider provider)
                        {
                            Type providerType = attribute.ProviderType;
                            MethodInfo? getSettingMethod = providerType.GetMethod("GetSetting");

                            // Получаем обобщенный аргумент типа T
                            Type genericArgumentType = property.PropertyType.GetGenericArguments()[0];

                            // Создание обобщенного метода GetSetting<T>
                            MethodInfo? genericGetSettingMethod = getSettingMethod.MakeGenericMethod(genericArgumentType);

                            // Создание делегата для вызова метода GetSetting<T>
                            var getSettingDelegate = (Func<string, object>)Delegate.CreateDelegate(typeof(Func<string, object>), provider, genericGetSettingMethod);

                            // Вызов метода GetSetting<T> с указанием типа T
                            dynamic value = getSettingDelegate.Invoke(attribute.SettingName);

                            // Создание экземпляра GenericSetting<T> и установка значения свойства Value
                            var genericSetting = Activator.CreateInstance(typeof(GenericSetting<>).MakeGenericType(genericArgumentType));
                            genericSetting?.GetType().GetProperty("Value")?.SetValue(genericSetting, value.Value);

                            // Установка значения свойства объекта ProgramSettings
                            property.SetValue(Settings, genericSetting);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when loading settings: " + ex.Message);
            }
        }*/
        public void SaveSettings()
        {
            try
            {
                var properties = typeof(ProgramSettings).GetProperties();
                foreach (var property in properties)
                {
                    if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                    {
                        var value = property.GetValue(Settings);

                        if (value != null && value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(GenericSetting<>))
                        {
                            var genericSettingValue = value.GetType().GetProperty("Value")?.GetValue(value);

                            if (genericSettingValue != null)
                            {
                                foreach (var providerType in providerTypes)
                                {
                                    var provider = Activator.CreateInstance(providerType) as IConfigurationProvider;

                                    MethodInfo? setSettingMethod = providerType.GetMethod("SetSetting");
                                    MethodInfo? genericSetSettingMethod = setSettingMethod.MakeGenericMethod(genericSettingValue.GetType());

                                    genericSetSettingMethod?.Invoke(provider, new object[] { attribute.SettingName, genericSettingValue });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving settings: " + ex.Message);
            }
        }


        public void LoadSettings()
        {
            try
            {
                var properties = typeof(ProgramSettings).GetProperties();

                foreach (var property in properties)
                {
                    if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                    {
                        // Получение обобщенного аргумента типа T
                        var genericArgumentType = property.PropertyType.GetGenericArguments()[0];
                        
                        foreach (var providerType in providerTypes)
                        {
                            var provider = Activator.CreateInstance(providerType) as IConfigurationProvider;

                            MethodInfo? getSettingMethod = providerType.GetMethod("GetSetting");

                            MethodInfo? genericGetSettingMethod = getSettingMethod.MakeGenericMethod(genericArgumentType);

                            var getSettingDelegate = Delegate.CreateDelegate(typeof(Func<string, object>), provider, genericGetSettingMethod) as Func<string, object>;

                            dynamic value = getSettingDelegate.Invoke(attribute.SettingName);

                            if (value != null)
                            {
                                var genericSetting = Activator.CreateInstance(typeof(GenericSetting<>).MakeGenericType(genericArgumentType), attribute.SettingName);
                                genericSetting?.GetType().GetProperty("Value")?.SetValue(genericSetting, value.Value);
                                property.SetValue(Settings, genericSetting);
                            }
                            else
                            {
                                // Значение по умолчанию, если строка настроек не найдена
                                var defaultValue = ""; 
                                var genericSetting = Activator.CreateInstance(typeof(GenericSetting<>).MakeGenericType(genericArgumentType), attribute.SettingName);
                                genericSetting?.GetType().GetProperty("Value")?.SetValue(genericSetting, defaultValue);
                                property.SetValue(Settings, genericSetting);
                            }

                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when loading settings: " + ex.Message);
            }
        }

        private static List<Type> GetAvailableProviderTypes()
        {
            var providerTypes = new List<Type>();

            var pluginDirectory = new DirectoryInfo("..\\..\\..\\Task2\\Plugins");

            foreach (var pluginAssemblyFile in pluginDirectory.GetFiles("*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(pluginAssemblyFile.FullName);
                    var types = assembly.GetExportedTypes();

                    foreach (var type in types)
                    {
                        if (typeof(IConfigurationProvider).IsAssignableFrom(type))
                        {
                            providerTypes.Add(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when loading assembly {pluginAssemblyFile.Name}: {ex.Message}");
                }
            }

            return providerTypes;
        }

    }
}


