using System.Xml;
using System.Xml.Serialization;

namespace BackEndTest.Models
{
    [XmlRoot("Root")]
    public class XmlModelList
    {
        [XmlElement("row")]
        public List<XmlModel> XmlModels { get; set; }
    }

    public class XmlModel
    {
        [XmlElement("TrainNumber")]
        public int TrainNumber { get; set; }
        [XmlElement("TrainIndexCombined")]
        public string TrainIndexCombined { get; set; }
        [XmlElement("FromStationName")]
        public string FromStationName { get; set; }
        [XmlElement("ToStationName")]
        public string ToStationName { get; set; }
        [XmlElement("LastStationName")]
        public string LastStationName { get; set; }

        [XmlElement("WhenLastOperation")]
        public string strWhenLastOperation { get; set; }
        [XmlIgnore]
        public DateTime WhenLastOperation { get { return Convert.ToDateTime(strWhenLastOperation).ToUniversalTime(); } set { WhenLastOperation = value.ToUniversalTime(); } }

        [XmlElement("LastOperationName")]
        public string LastOperationName { get; set; }
        [XmlElement("InvoiceNum")]
        public string InvoiceNum { get; set; }
        [XmlElement("PositionInTrain")]
        public int PositionInTrain { get; set; }
        [XmlElement("CarNumber")]
        public int CarNumber { get; set; }
        [XmlElement("FreightEtsngName")]
        public string FreightEtsngName { get; set; }
        [XmlElement("FreightTotalWeightKg")]
        public int FreightTotalWeightKg { get; set; }
    }
}
