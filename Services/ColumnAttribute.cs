using System;

namespace Mazada.Services
{
    //Use for column metadata
    [AttributeUsage(AttributeTargets.Property)]
    class ColumnAttribute : Attribute
    {
        public string Name { get; }
        public bool IsPrimaryKey { get; set; } = false;
        public bool AutoIncrement { get; set; } = false;

        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
