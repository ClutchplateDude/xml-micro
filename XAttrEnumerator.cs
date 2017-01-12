namespace MicroFramework.Xml
{
    using System;
    using System.Collections;

    public class XAttrEnumerator : IEnumerator
    {
        ArrayList enumList;
        IEnumerator enumerator;
        public XAttrEnumerator(ArrayList list)
        {
            this.enumList = list;
            Reset();
        }

        public object Current
        {
            get { return this.enumerator.Current; }
        }

        public bool MoveNext()
        {
            return this.enumerator.MoveNext();
        }

        public void Reset()
        {
            this.enumerator = this.enumList.GetEnumerator();
        }
    }

}
