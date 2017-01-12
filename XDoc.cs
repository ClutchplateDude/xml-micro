namespace MicroFramework.Xml
{
    using System;

    public class XDoc
    {
        private XElem rootElem;

        public static XDoc Parse(string input)
        {
            var xmlParser = new XmlParser(input);
            XElem root = null;
            xmlParser.ReadElement(ref root);

            return new XDoc(root);
        }

        public XDoc(XElem root)
        {
            this.rootElem = root;
        }

        public XElem Element(string name)
        {
            if (this.rootElem.Name == name)
            {
                return this.rootElem;
            }

            return null;
        }
    }
}
