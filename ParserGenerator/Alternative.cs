using System.Collections.Generic;

namespace ParserGenerator
{
    public class Alternative
    {
        private List<string> _items;
        private string _action;

        public string Action { get { return _action; } }

        public List<string> Items { get { return _items; } }
        public Alternative(List<string> items, string action)
        {
            _items = items;
            _action = action;
        }
    }
}