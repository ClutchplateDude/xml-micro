namespace MicroFramework.Xml
{
    using System;
    using System.Collections;

    public class XElemList
    {
        ArrayList contents = new ArrayList();

        public XElem this[string name]
        {
            get
            {
                foreach (XElem elem in this.contents)
                {
                    if (elem.Name.Equals(name))
                    {
                        return elem;
                    }
                }

                return null;
            }
        }

        internal void Add(XElem node)
        {
            this.contents.Add(node);
        }

        public IEnumerator GetEnumerator()
        {
            ArrayList newList = new ArrayList();
            foreach (XElem elem in this.contents)
            {
                newList.Add(elem);
            }

            return new XElemEnumerator(newList);
        }

        public int Count { get { return this.contents.Count; } }
    }


}
