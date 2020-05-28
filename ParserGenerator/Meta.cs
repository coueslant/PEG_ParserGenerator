namespace ParserGenerator
{
    public class Meta
    {
        private string _name;
        private string _string;

        public string @String
        {
            get
            {
                return _string;
            }
            set
            {
                _string = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Meta(string name, string @string)
        {
            _name = name;
            _string = @string;
        }
    }
}