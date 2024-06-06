// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Arfolyamok));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Arfolyamok)serializer.Deserialize(reader);
// }

using System.Xml.Serialization;

[XmlRoot(ElementName = "item")]
public class Item
{
    [XmlElement(ElementName = "bank")]
    public string Bank;

    [XmlElement(ElementName = "datum")]
    public string Datum;

    [XmlElement(ElementName = "penznem")]
    public string Penznem;

    [XmlElement(ElementName = "kozep")]
    public List<decimal> Kozep;
}

[XmlRoot(ElementName = "deviza")]
public class Deviza
{

    [XmlElement(ElementName = "item")]
    public Item Item;
}

[XmlRoot(ElementName = "arfolyamok")]
public class Arfolyamok
{

    [XmlElement(ElementName = "valuta")]
    public object Valuta;

    [XmlElement(ElementName = "deviza")]
    public Deviza Deviza;
}