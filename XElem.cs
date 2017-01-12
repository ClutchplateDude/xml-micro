namespace MicroFramework.Xml
{
    using System;

    public class XElem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public XAttributeList Attributes { get; set; }
        public XElemList Children { get; set; }

        public XElem()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        internal void AddChild(XElem node)
        {
            if (this.Children == null)
            {
                this.Children = new XElemList();
            }

            this.Children.Add(node);
        }

        public bool HasChildren { get { return (this.Children != null) && (this.Children.Count > 0); } }

        public XElem Element(string elemName)
        {
            foreach (XElem elem in this.Children)
            {
                if (elem.Name.Equals(elemName))
                {
                    return elem;
                }
            }

            return null;
        }

        public XElemList Elements(string elemName)
        {
            var result = new XElemList();
            foreach (XElem elem in this.Children)
            {
                if (elem.Name.Equals(elemName))
                {
                    result.Add(elem);
                }
            }

            return result;
        }

        public XElemList Elements()
        {
            var result = new XElemList();
            foreach (XElem elem in this.Children)
            {
                result.Add(elem);
            }

            return result;
        }

        public XAttribute Attribute(string attrName)
        {
            return this.Attributes[attrName] as XAttribute;
        }

        public override string ToString()
        {
            return this.Name + (this.HasChildren ? " (" + this.Children.Count + " children)" : (this.Value.Length > 0) ? " = '" + this.Value + "'" : string.Empty);
        }
    }
}
