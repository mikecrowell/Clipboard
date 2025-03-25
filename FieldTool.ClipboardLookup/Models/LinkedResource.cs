using System.Collections.Generic;

namespace FieldTool.ClipboardLookup.Models
{
    public abstract class LinkedResource
    {
        public ICollection<Link> Links { get; set; }

        public LinkedResource()
        {
            Links = new List<Link>();
        }

        public void AddSelfLink(string href)
        {
            AddLink("self", href);
        }

        public void AddLink(string relation, string href)
        {
            Links.Add(new Link(relation, href));
        }
    }
}