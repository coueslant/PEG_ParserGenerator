using System.Collections.Generic;

namespace ParserGenerator
{
    public class Alternative
    {
        private List<string> _items;
        private string _action;

        public Alternative(List<string> items, string action)
        {
            _items = items;
            _action = action;
        }

        public List<string> GetItems()
        {
            return _items;
        }

        public string GetAction()
        {
            return _action;
        }
    }
}