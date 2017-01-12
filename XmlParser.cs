namespace MicroFramework.Xml
{
    using System;

    public class XmlParser
    {
        private string inputString;
        private int currPos = 0;

        public XmlParser(string input)
        {
            this.inputString = input;
            this.currPos = 0;
        }

        public void ReadElement(ref XElem root)
        {
            int posSpace = 0;
            int posNameEnd = 0;
            XElem node;

            while (currPos < this.inputString.Length)
            {
                switch (this.inputString[currPos])
                {
                    case '<': // Start of Element or end of long Element
                        if (this.inputString[currPos + 1] == '/')
                        {
                            // Check for end of long Element
                            if (this.inputString.Substring(currPos, root.Name.Length + 3) == "</" + root.Name + ">")
                            {
                                // long tag form element close
                                currPos = currPos + (root.Name.Length + 3);
                                return;
                            }

                            throw new InvalidOperationException("Unmatched elements at " + currPos + ". Expected '" + root.Name + "', found '" + this.inputString.Substring(currPos, 10));
                        }
                        else
                        {
                            // Start of Element
                            if (this.inputString[currPos + 1] == '?')
                            {
                                // Skip Xml declaration
                                posNameEnd = this.inputString.IndexOf(">", currPos);
                                currPos = currPos + posNameEnd;
                            }
                            else
                            {
                                node = new XElem();

                                posNameEnd = this.inputString.IndexOf(">", currPos);
                                posSpace = this.inputString.IndexOf(" ", currPos);

                                // If there is a space between opening and the closing bracket...
                                if ((posSpace > 0) && (posSpace < posNameEnd))
                                {
                                    // ... then attributes are present, so read them
                                    int off = 0;
                                    node.Name = this.inputString.Substring(currPos + 1, posSpace - currPos - 1);
                                    currPos = posSpace + 1;
                                    if (this.inputString[posNameEnd - 1] == '/')
                                    {
                                        // Account for short form
                                        off = 1;
                                    }

                                    node.Attributes = ReadAttributes(this.inputString.Substring(currPos, posNameEnd - currPos - off));
                                    currPos = posNameEnd + 1;
                                }
                                else
                                {
                                    // .. otherwise no attributes are present
                                    node.Name = this.inputString.Substring(currPos + 1, posNameEnd - currPos - 1);
                                    currPos = posNameEnd + 1;
                                    node.Attributes = null;
                                }

                                // Did we just read the root node?
                                if (root == null)
                                {
                                    // Yes, so set.
                                    root = node;
                                }
                                else
                                {
                                    // No, so add as child to passed in node.
                                    root.AddChild(node);
                                }

                                // If we are not a shortform, recurse down to read the next element.
                                if (this.inputString[posNameEnd - 1] != '/')
                                {
                                    // Not a shortform tag
                                    ReadElement(ref node);
                                }
                            }
                        }
                        break;

                    case '/':
                        // Single tag element closing
                        if (this.inputString[currPos + 1] == '>')
                        {
                            currPos = currPos + 2;
                            return;
                        }
                        else
                        {
                            currPos = currPos + 1;
                        }
                        break;

                    default:
                        currPos = currPos + 1;
                        break;
                }
            }
        }

        private XAttributeList ReadAttributes(string attrString)
        {
            string[] attrParts = null;
            XAttributeList objAttributes = new XAttributeList();

            foreach (string attrPair in attrString.Trim().Split(' '))
            {
                if (attrPair != string.Empty)
                {
                    attrParts = attrPair.Split('=');
                    if (attrParts.Length == 2)
                    {
                        // remove quotes from value
                        objAttributes.Add(new XAttribute(attrParts[0], attrParts[1].Trim("\"".ToCharArray())));
                    }
                }
            }

            return objAttributes;
        }
    }
}

