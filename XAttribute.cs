namespace MicroFramework.Xml
{
    using System;

    public class XAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public XAttribute()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        public XAttribute(string name, string val)
        {
            this.Name = name;
            this.Value = val;
        }
    }
}
