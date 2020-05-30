using NUnit.Framework;

namespace ParserGenerator.Tests {
    public class ParserGenerator_GrammarReaderShould {
        private ParserGenerator _parserGenerator;

        [SetUp]
        public void Setup () {
            _parserGenerator = new ParserGenerator ();
        }

        [Test]
        public void ReadCorrectNumberOfRules () {
            Assert.Fail ();
        }
    }
}