using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    [Serializable()]
    public class RetrofitFilters
    {
        [XmlArray("WeatherZoneList", IsNullable = true)]
        [XmlArrayItem("WeatherZone", IsNullable = true)]
        public List<string> WeatherZone { get; set; }

        [XmlArray("BuildingTypeList", IsNullable = true)]
        [XmlArrayItem("BuildingType", IsNullable = true)]
        public List<string> BuildingType { get; set; }

        [XmlArray("BuildingVintageList", IsNullable = true)]
        [XmlArrayItem("BuildingVintage", IsNullable = true)]
        public List<string> BuildingVintage { get; set; }

        [XmlArray("HeatingCoolingSystemList", IsNullable = true)]
        [XmlArrayItem("HeatingCoolingSystem", IsNullable = true)]
        public List<string> HeatingCoolingSystem { get; set; }
    }
}