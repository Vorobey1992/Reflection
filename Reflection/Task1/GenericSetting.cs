namespace Reflection.Task1
{
    public class GenericSetting<T>
    {
        public string? Name { get; }
        public T? Value { get; set; }

        public GenericSetting(string? name)
        {
            Name = name;
        }

        public GenericSetting()
        {
            Name = null;
        }

        public GenericSetting(string? name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}
