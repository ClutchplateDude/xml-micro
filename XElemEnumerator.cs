namespace MicroFramework.Xml
{
    using System;
    using System.Collections;

    public class XElemEnumerator : IEnumerator
    {
        int index = -1;
        ArrayList enumList;
        public XElemEnumerator(ArrayList list)
        {
            this.enumList = list;
        }

        public object Current
        {
            get { return this.enumList[this.index]; }
        }

        public bool MoveNext()
        {
            this.index++;
            return this.index < this.enumList.Count;
        }

        public void Reset()
        {
            this.index = -1;
        }
    }
}
