using Reflection.Task1.interfaces;
using System.Text;

namespace Reflection.Task1
{
    public class FileConfigurationProvider : IConfigurationProvider
    {
        private readonly string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\config.txt"));

        public GenericSetting<T> GetSetting<T>(string settingName)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && parts[0].Trim() == settingName)
                        {
                            string value = parts[1].Trim();

                            T settingValue = ParseSettingValue<T>(value.Trim());
                            return new GenericSetting<T>(settingName) { Value = settingValue };
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading configuration file: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting the setting: " + ex.Message);
            }

            return new GenericSetting<T>(settingName) { Value = default };
        }


        public void SetSetting<T>(string settingName, T value)
        {
            try
            {
                string settingValue = value?.ToString() ?? string.Empty;

                if (typeof(T) == typeof(TimeSpan))
                {
                    TimeSpan timeSpanValue = (TimeSpan)(object)value;
                    settingValue = timeSpanValue.ToString(); // Используйте стандартное форматирование TimeSpan
                }

                string[] lines = File.Exists(filePath) ? File.ReadAllLines(filePath) : Array.Empty<string>();
                bool settingExists = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split('=');
                    if (parts.Length == 2 && parts[0].Trim() == settingName)
                    {
                        lines[i] = settingName + " = " + settingValue;
                        settingExists = true;
                        break;
                    }
                }

                if (!settingExists)
                {
                    string newLine = settingName + " = " + settingValue;
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[^1] = newLine;
                }

                using StreamWriter writer = new(filePath, false, Encoding.UTF8);
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing configuration file: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while setting the value: " + ex.Message);
            }
        }

        private static T ParseSettingValue<T>(string value)
        {
            Type valueType = typeof(T);

            if (valueType == typeof(string))
            {
                return (T)(object)value;
            }
            else if (valueType == typeof(int))
            {
                return (T)(object)int.Parse(value);
            }
            else if (valueType == typeof(float))
            {
                return (T)(object)float.Parse(value);
            }
            else if (valueType == typeof(TimeSpan))
            {
                if (TimeSpan.TryParse(value, out TimeSpan timeSpanValue))
                {
                    return (T)(object)timeSpanValue;
                }
            }

            throw new NotSupportedException($"Unsupported setting type: {typeof(T).FullName}");
        }
    }
}