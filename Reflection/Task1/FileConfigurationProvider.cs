

namespace Reflection.Task1
{
    public class FileConfigurationProvider : IConfigurationProvider
    {
        private readonly string filePath = "C:\\EPAM\\C#\\.NET Mentoring Program Basics 2023\\Reflection\\Reflection\\config.txt";

        public object GetSetting(string settingName)
        {
            // Чтение значения настройки из файла
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2 && parts[0].Trim() == settingName)
                {
                    return parts[1].Trim();
                }
            }

            return null!;
        }

        public void SetSetting(string settingName, object value)
        {
            // Запись значения настройки в файл
            string settingValue = value?.ToString() ?? string.Empty;
            string[] lines = File.ReadAllLines(filePath);
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

            using StreamWriter writer = new(filePath, false);
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }
        }

    }
}
