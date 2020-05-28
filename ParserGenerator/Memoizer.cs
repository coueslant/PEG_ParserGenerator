// using System;
// using System.Collections.Generic;

// namespace ParserGenerator
// {
//     public static class Memoizer
//     {
//         // code unapologetically borrowed from https://stackoverflow.com/questions/2852161/c-sharp-memoization-of-functions-with-arbitrary-number-of-arguments
//         static Func<A, R> Memoize<A, R>(this Func<A, R> f)
//         {
//             var _dict = new Dictionary<A, R>();
//             return a =>
//             {
//                 R r;
//                 if (!_dict.TryGetValue(a, out r))
//                 {
//                     r = f(a);
//                     _dict.Add(a, r);
//                 }
//                 return r;
//             };
//         }

//         static Func<Tuple<A, B>, R> TuplifyTwo<A, B, R>(this Func<A, B, R> f)
//         {
//             return t => f(t.Item1, t.Item2);
//         }

//         static Func<A, B, R> DetuplifyTwo<A, B, R>(this Func<Tuple<A, B>, R> f)
//         {
//             return (a, b) => f(Tuple.Create(a, b));
//         }

//         static Func<A, B, R> Memoize<A, B, R>(this Func<A, B, R> f)
//         {
//             return f.TuplifyTwo().Memoize().DetuplifyTwo();
//         }

//         static Func<Tuple<A, B, C>, R> TuplifyThree<A, B, C, R>(this Func<A, B, C, R> f)
//         {
//             return t => f(t.Item1, t.Item2, t.Item3);
//         }

//         static Func<A, B, C, R> DetuplifyThree<A, B, C, R>(this Func<Tuple<A, B, C>, R> f)
//         {
//             return (a, b, c) => f(Tuple.Create(a, b, c));
//         }

//         static Func<A, B, C, R> Memoize<A, B, C, R>(this Func<A, B, C, R> f)
//         {
//             return f.TuplifyThree().Memoize().DetuplifyThree();
//         }

//         static Func<Tuple<A, B, C, D>, R> TuplifyFour<A, B, C, D, R>(this Func<A, B, C, D, R> f)
//         {
//             return t => f(t.Item1, t.Item2, t.Item3, t.Item4);
//         }

//         static Func<A, B, C, D, R> DetuplifyFour<A, B, C, D, R>(this Func<Tuple<A, B, C, D>, R> f)
//         {
//             return (a, b, c, d) => f(Tuple.Create(a, b, c, d));
//         }

//         static Func<A, B, C, D, R> Memoize<A, B, C, D, R>(this Func<A, B, C, D, R> f)
//         {
//             return f.TuplifyFour().Memoize().DetuplifyFour();
//         }
//     }

// }