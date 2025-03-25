namespace FieldTool.ClipboardLookup.Models
{
    public class Link
    {
        public string Relation { get; set; }
        public string Href { get; set; }

        public Link(string relation, string href)
        {
            Relation = relation;
            Href = href;
        }
    }
}