using System.Xml.Serialization;

namespace ScenarioTool.Shared.Entity
{
    public class ScenarioStep
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public ScenarioActionType ActionType { get; set; }

        [XmlArray]
        public StepParam[] Params { get; set; }

        public override string ToString() => Name;
    }
}
