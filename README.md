# xml-micro
Simple Xml parser for .NET Micro Framework (made for Netduino)

I noticed that the .NET Microframework that's used on Netduinos does not support System.Xml, but I wanted to use Xml in my Netduino projects, so I wrote this simple parser.
It's very barebones (code size and memory usage are always a concern on the Micro Framework). It supports elements and attributes. NO namespaces or more esoteric features.

## Usage
It is quite simple to use this little library:
 1. Create an XDoc instance by calling the XDoc.Parse static function, giving it the Xml string.
 1. Use the Element() function to get the root node.
 1. Use a combination of Element(), Elements() and Attribute() functions to get at the data.
 
##Caveats
 - Attribute values may not contain spaces (parser is somewhat simplistic, but could probably be fixed to support this).
 - Only Elements and Attributes are supported.
 
##Example

Given the following Xml:
```xml
<House>
  <Rooms>
    <Room Name="LivingRoom" Floor="1" />
    <Room Name="MasterBedroom" Floor="2" />
    <Room Name="Kitchen" Floor="1">      
      <Furniture> 
        <Stove Type="Gas" Burners="4" /> 
        <Fridge Brand="GE" />      
      </Furniture>
    </Room>
  </Rooms>
</House>
````
This could be parse by code lide this:
```Csharp
XDoc dom = XDoc.Parse(xmlInputString);
XElem houseElem = dom.Element("House");

foreach (XElem roomElem in houseElem.Element("Rooms").Elements("Room"))
{
  string roomName = roomElem.Attribute("Name").Value;
  int roomFloor = int.Parse(roomElem.Attribute("Floor").Value);
  // ... create a room data structure...
  XElem furniture = roomElem.Element("Furniture");
  if (furniture != null) 
  {
    foreach (XElem furnitureElem in furniture.Children)
    {
      var furniturePiece = ReadFurniture(furnitureElem);
      // .. do something with the piece...
    }
  }
} 

private static object ReadFurniture(XElem furniture)
{
  switch (furniture.Name)
  {
    case "Stove": return ReadStove(furniture);
    case "Fridge": return ReadFridge(furniture);
  }
}
````
