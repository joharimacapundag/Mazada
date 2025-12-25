using System;

namespace Mazada.Services
{

    //Use for making table metadata
    class TableAttribute : Attribute
    {
        public string Name { get; }
        public TableAttribute(string name) => Name = name;
    }
}
