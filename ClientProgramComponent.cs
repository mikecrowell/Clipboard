using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FieldTool.Integration.Test
{
    [Serializable()]
    public class ClientProgramComponent
    {
        [XmlIgnore]
        public ProgramComponent programcomponent { get; set; }

        [Serializable()]
        public class ProgramComponentCollection
        {
            [XmlElement("ClientProgramComponents")]
            public List<ClientProgramComponent> obj = new List<ClientProgramComponent>();
        }

        public Add add { get; set; }
        public List<Add> programcomponents = new List<Add>();

        [Serializable()]
        public class ProgramComponent
        {
            public Add add { get; set; }
        }

        [Serializable()]
        public class Add
        {
            public string add { get; set; }

            [XmlAttribute("clientprogramcomponentId")]
            public string clientprogramcomponentId { get; set; }

            [XmlAttribute("clientprogramId")]
            public string clientprogramId { get; set; }

            [XmlAttribute("existingcomponentid")]
            public string existingcomponentid { get; set; }

            [XmlAttribute("newcomponentid")]
            public string newcomponentid { get; set; }

            [XmlAttribute("bensightid")]
            public string bensightid { get; set; }

            [XmlAttribute("effectivestartdate")]
            public string effectivestartdate { get; set; }

            [XmlAttribute("effectiveenddate")]
            public string effectiveenddate { get; set; }

            [XmlAttribute("rebateamount")]
            public string rebateamount { get; set; }

            [XmlAttribute("picturepath")]
            public string picturepath { get; set; }

            [XmlAttribute("clientprogramcomponentdescription")]
            public string clientprogramcomponentdescription { get; set; }

            [XmlAttribute("clientprogramcomponentname")]
            public string clientprogramcomponentname { get; set; }

            [XmlAttribute("energysavingskwh")]
            public string energysavingskwh { get; set; }

            [XmlAttribute("energysavingsthrms")]
            public string energysavingsthrms { get; set; }

            [XmlAttribute("systemequipmenttypeid")]
            public string systemequipmenttypeid { get; set; }

            [XmlAttribute("unitsize")]
            public string unitsize { get; set; }

            [XmlAttribute("unitefficiency")]
            public string unitefficiency { get; set; }

            [XmlAttribute("hasrebateunitcapture")]
            public string hasrebateunitcapture { get; set; }

            [XmlAttribute("rebateunitcapturetext")]
            public string rebateunitcapturetext { get; set; }

            [XmlAttribute("rebateformula")]
            public string rebateformula { get; set; }

            [XmlAttribute("systemtypeid")]
            public string systemtypeid { get; set; }

            [XmlAttribute("typicalsavingsformulatherms")]
            public string typicalsavingsformulatherms { get; set; }

            [XmlAttribute("typicalsavingsformulakwh")]
            public string typicalsavingsformulakwh { get; set; }

            [XmlAttribute("clientprogramcomponentreferenceid")]
            public string clientprogramcomponentreferenceid { get; set; }

            [XmlAttribute("unittext")]
            public string unittext { get; set; }
        }
    }
}