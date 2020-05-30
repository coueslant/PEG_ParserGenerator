**Date:** 05/30/2020

**Entry:** Figured I should probably keep some form of record as to what I've been doing on this project. It seems like it's going to grow into something fairly large so I am keen to begin documenting well.

I've been working on the code for about a week now. Inspired by a reddit post that I can no longer remember I decided to take a deep dive into PEGs. That led me to Guido Van Rossum's talk "Writing a PEG parser for fun and profit", in which he explores the specifics of rewriting PGen (the python parser generator) as a PEG parser generator. He also produced an excellent blog series on the topic which has formed the foundation for this work so far.

The last few days I have written a lot of code. So far I've written an tokenizer and a parser for the meta-grammar describing the generic form of a PEG. I am currently in the process of writing a code generator to emit a parser for any given grammar. In doing so I will be able to make the project self hosting, as the "meta-parser" will be able to generate a grammar for the meta-grammar. In turn this bootstrapping process will make it easier to add more and more features simply by enhancing the grammar.

There are many interesting areas I could go with this project, and it encompasses a lot of what I am interested in. PEGs do not typically allow for left recursion, and as such parsers don't tend to implement it, but with some interesting graph theory and oracle programming it is possible to implement it. This is an avenue that will nicely meet some theoretical urges of mine in both math and computer science. Atop that, there are a lot of formal language niceties which can be explored in a parsing project like this. Questions like how large a grammar can I generate a parser for? What sorts of grammars and expressions can I parse? and may others. Lastly, the packrat parsing algorithm has only been around 15 years or so and is not widely used, but memory is cheap now and having a parser which can run in linear time is a tantilising prospect. Exploring the limits and limitations of packrat parsing and its place in the world as opposed to other methods is very exciting.

**Date:** 05/31/2020
**Entry:** This morning's work consisted of figuring out an infinite recursion bug in the generated code, along with some cleanup of the code generator.

The infinite recursion problem is interesting. In my handrolled pattern for the grammar parser I have a mechanism in place which prevents falling into infinite recursion when a right recursive alternate matches, but I haven't yet embedded that into the code generator. Working on that now.

Worked on the code generator for a while. Handled right recursive rules fairly well, but there are some edge cases that I certainly haven't covered yet and I think that it'll take a while to catch them all. I think that once I've completed doing that the next big necessary change is a better building and testing system for the generated parser. Right now the testing code is intertwined a little with the generator, which isn't great. I'll spend some time thinking about builds after I finish writing the first version of the code generator.

Right now the special case I am considering is a right recursive rule alternative with multiple items between the first and last items. In the metagrammar this doesn't occur, but it is possible that it could occur in other grammars, and so I think it's probably a good idea to build support for that into this version of the code generator.

**Date:** 06/01/2020
**Entry:** Well, the sun has indeed risen on a new day, and a new month. I think today I might step away from code for the most part and think about building and documentation. Can't decide whether I want some internal, auto generated documentation or a more separated solution. Advantages and disadvantages to both I suppose.

As for building and testing, I have no idea where to start. MSBuild is the most obvious candidate given that I have already got some of that infrastructure in place, otherwise I am not sure. The other problem is testing the generated parsers from the code generator. At the moment what I am doing is repaetedly commenting and uncommmenting some code in the parser generator main function which calls on the class created by the generator. This way causes issues if the generator hasn't been run or if the generated code contains errors, so I think I need to find a smoother way of doing it. There are a couple of candidates:

- Writing a main function in the generated code so that the class can be called directly with whatever string I'd like to parse
- Making duplicate copies of the generated code in the tests directory and writing a new class to call upon code in there

Again, advantages an disadvantages. I'll have a think.

Also going to work today on writing up a proposal to turn this into my Honor's Thesis. There are a whole lot of super interesting ideas and fields which come together here (some of which I alluded to in my initial entry), so I think it'll allow me to work nicely amongst my interests.

Didn't spend all that much time on anything today, it was too nice a day outside for that. I did manage to write the proposal and send it off in an email to Dr. Labouseur to see if he was interested. It's a good first step but I do feel like the project still requires a little bit more direction and depth than just "I'm gonna write a parser generator for an interesting form of grammar".
