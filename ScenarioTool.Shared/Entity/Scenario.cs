using System.Xml.Serialization;

namespace ScenarioTool.Shared.Entity
{
    [XmlRoot("Scenario")]
    public class Scenario
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlArray]
        public ScenarioStep[] Steps { get; set; }

        public override string ToString() => Name;
    }
}
