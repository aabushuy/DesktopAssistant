using System.Xml.Serialization;

namespace ScenarioTool.Shared.Entity
{
    public class StepParam
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
