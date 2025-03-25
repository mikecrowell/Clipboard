using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    [Serializable()]
    public class BuildingDetails
    {
        [XmlArray("HeatingSystemList", IsNullable = true)]
        [XmlArrayItem("HeatingSystem", IsNullable = true)]
        public List<string> HeatingSystem { get; set; }

        [XmlArray("HeatFuelList", IsNullable = true)]
        [XmlArrayItem("HeatFuel", IsNullable = true)]
        public List<string> HeatFuel { get; set; }

        [XmlArray("AirConditioningList", IsNullable = true)]
        [XmlArrayItem("AirConditioning", IsNullable = true)]
        public List<string> AirConditioning { get; set; }

        [XmlArray("DHWList", IsNullable = true)]
        [XmlArrayItem("DHW", IsNullable = true)]
        public List<string> DHW { get; set; }

        [XmlArray("DHWFuelList", IsNullable = true)]
        [XmlArrayItem("DHWFuel", IsNullable = true)]
        public List<string> DHWFuel { get; set; }

        [XmlArray("ParkingList", IsNullable = true)]
        [XmlArrayItem("Parking", IsNullable = true)]
        public List<string> Parking { get; set; }

        [XmlArray("AtticTypeList", IsNullable = true)]
        [XmlArrayItem("AtticType", IsNullable = true)]
        public List<string> AtticType { get; set; }

        [XmlArray("WallTypeList", IsNullable = true)]
        [XmlArrayItem("WallType", IsNullable = true)]
        public List<string> WallType { get; set; }

        [XmlArray("FoundationList", IsNullable = true)]
        [XmlArrayItem("Foundation", IsNullable = true)]
        public List<string> Foundation { get; set; }

        [XmlArray("WindowsList", IsNullable = true)]
        [XmlArrayItem("Windows", IsNullable = true)]
        public List<string> Windows { get; set; }
    }
}