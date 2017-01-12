namespace MicroFramework.Xml
{
    using System;
    using System.Collections;

    public class XAttributeList
    {
        ArrayList contents = new ArrayList();

        public XAttribute this[string name]
        {
            get
            {
                foreach (XAttribute attr in this.contents)
                {
                    if (attr.Name.Equals(name))
                    {
                        return attr;
                    }
                }

                return null;
            }
        }

        internal void Add(XAttribute attr)
        {
            this.contents.Add(attr);
        }

        public IEnumerator GetEnumerator()
        {
            return new XAttrEnumerator(this.contents);
        }
    }

}
