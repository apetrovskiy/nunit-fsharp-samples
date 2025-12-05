[<NUnit.Framework.TestFixture>]
[<NUnit.Allure.Core.AllureNUnit>]

[<NUnit.Allure.Attributes.AllureEpic("Unit testing in F#")>]
[<NUnit.Allure.Attributes.AllureFeature([| "Syntax tests" |])>]
[<NUnit.Allure.Attributes.AllureStory([| "Syntax" |])>]
[<NUnit.Allure.Attributes.AllureSuite("Syntax")>]
[<NUnit.Allure.Attributes.AllureTag([| "Syntax" |])>]
module NUnit.Samples.AssertSyntaxTests

open System
open System.Collections
open NUnit.Allure.Attributes
open NUnit.Allure.Core
open NUnit.Framework
open NUnit.Framework.Constraints
open NUnit.Framework.Legacy

/// <summary>
/// This test fixture attempts to exercise all the syntactic
/// variations of Assert without getting into failures, errors 
/// or corner cases. Thus, some of the tests may be duplicated 
/// in other fixtures.
/// 
/// Each test performs the same operations using the classic
/// syntax (if available) and the constraint syntax. The
/// inherited syntax is not used in this example, since it
/// would require using a class to hold the tests, which
/// seems to make it less useful in F#.
/// </summary>

[<Test>]
[<AllureName("ClassicAssert.IsNull")>]
let IsNull() =
    let nada : obj = null
    ClassicAssert.IsNull(nada)
    ClassicAssert.That(nada, Is.Null)

[<Test>]
[<AllureName("ClassicAssert.IsNotNull")>]
let IsNotNull() =
    ClassicAssert.IsNotNull(42)
    ClassicAssert.That(42, Is.Not.Null)

[<Test>]
[<AllureName("ClassicAssert.IsTrue")>]
let IsTrue() =
    ClassicAssert.IsTrue(2+2=4)
    ClassicAssert.True(2+2=4)
    ClassicAssert.That(2+2=4, Is.True)
    ClassicAssert.That(2+2=4)

[<Test>]
[<AllureName("ClassicAssert.IsFalse")>]
let IsFalse() =
    ClassicAssert.IsFalse(2+2=5)
    ClassicAssert.That(2+2=5, Is.False)

[<Test>]
[<AllureName("ClassicAssert.IsNan")>]
let IsNaN() =
    let d : double = Double.NaN
    let f : float = Double.NaN
    ClassicAssert.IsNaN(d)
    ClassicAssert.IsNaN(f)
    ClassicAssert.That(d, Is.NaN)
    ClassicAssert.That(f, Is.NaN)

[<Test>]
[<AllureName("ClassicAssert.IsEmpty, ClassicAssert.IsNotEmpty, ClassicAssert.That")>]
let EmptyStringTests() =
    ClassicAssert.IsEmpty("")
    ClassicAssert.IsNotEmpty("Hello!")
    ClassicAssert.That("", Is.Empty)
    ClassicAssert.That("Hello!", Is.Not.Empty)

[<Test>]
let EmptyCollectionTests() =
    // Lists
    ClassicAssert.IsEmpty([])
    ClassicAssert.IsNotEmpty([ 1; 2; 3 ])
    ClassicAssert.That([], Is.Empty)
    ClassicAssert.That([ 1; 2; 3 ], Is.Not.Empty)
    //Arrays
    ClassicAssert.IsEmpty([||])
    ClassicAssert.IsNotEmpty([| 1; 2; 3 |])
    ClassicAssert.That([||], Is.Empty)
    ClassicAssert.That([| 1; 2; 3 |], Is.Not.Empty)

[<Test>]
let ExactTypeTests() =
    ClassicAssert.AreEqual(typeof<string>, "Hello".GetType())
    ClassicAssert.AreEqual("System.String", "Hello".GetType().FullName)
    ClassicAssert.AreNotEqual(typeof<int>, "Hello".GetType())
    ClassicAssert.AreNotEqual("System.Int32", "Hello".GetType().FullName)
    ClassicAssert.That("Hello", Is.TypeOf<string>())
    ClassicAssert.That("Hello", Is.Not.TypeOf<int>())

[<Test>]
let InstanceOfTypeTests() =
    ClassicAssert.IsInstanceOf(typeof<string>, "Hello")
    ClassicAssert.IsNotInstanceOf(typeof<string>, 5)
    ClassicAssert.That("Hello", Is.InstanceOf(typeof<string>))
    ClassicAssert.That(5, Is.Not.InstanceOf(typeof<string>))

[<Test>]
let AssignableFromTypeTests() =
    ClassicAssert.IsAssignableFrom(typeof<string>, "Hello")
    ClassicAssert.IsNotAssignableFrom(typeof<string>, 5)
    ClassicAssert.That( "Hello", Is.AssignableFrom(typeof<string>))
    ClassicAssert.That( 5, Is.Not.AssignableFrom(typeof<string>))

[<Test>]
let SubstringTests() =
    let phrase = "Hello World!"
    let array = [| "abc"; "bad"; "dba" |]
    StringClassicAssert.Contains("World", phrase)
    ClassicAssert.That(phrase, Contains.Substring("World"))
    ClassicAssert.That(phrase, Does.Not.Contain("goodbye"))
    ClassicAssert.That(phrase, Contains.Substring("WORLD").IgnoreCase)
    ClassicAssert.That(phrase, Does.Not.Contain("BYE").IgnoreCase)
    ClassicAssert.That(array, Has.All.Contains( "b" ) )

[<Test>]
let StartsWithTests() =
    let phrase = "Hello World!"
    let greetings = [| "Hello!"; "Hi!"; "Hola!" |]
    StringClassicAssert.StartsWith("Hello", phrase);
    ClassicAssert.That(phrase, Does.StartWith("Hello"))
    ClassicAssert.That(phrase, Does.Not.StartWith("Hi!"))
    ClassicAssert.That(phrase, Does.StartWith("HeLLo").IgnoreCase)
    ClassicAssert.That(phrase, Does.Not.StartWith("HI").IgnoreCase)
    ClassicAssert.That(greetings, Is.All.StartsWith("h").IgnoreCase)

[<Test>]
let EndsWithTests() =
    let phrase = "Hello World!"
    let greetings = [| "Hello!"; "Hi!"; "Hola!" |];
    StringClassicAssert.EndsWith("!", phrase)
    ClassicAssert.That(phrase, Does.EndWith("!"))
    ClassicAssert.That(phrase, Does.Not.EndWith("?"))
    ClassicAssert.That(phrase, Does.EndWith("WORLD!").IgnoreCase)
    ClassicAssert.That(greetings, Is.All.EndsWith("!"))

[<Test>]
let EqualIgnoringCaseTests() =
    let phrase = "Hello World!"
    StringClassicAssert.AreEqualIgnoringCase("hello world!",phrase)
    ClassicAssert.That(phrase, Is.EqualTo("hello world!").IgnoreCase)
    ClassicAssert.That(phrase, Is.Not.EqualTo("goodbye world!").IgnoreCase)
    ClassicAssert.That( [| "Hello"; "World" |], 
        Is.EqualTo( [| "HELLO"; "WORLD" |] ).IgnoreCase)
    ClassicAssert.That( [| "HELLO"; "Hello"; "hello" |],
        Is.All.EqualTo( "hello" ).IgnoreCase)
          
[<Test>]
let RegularExpressionTests() =
    let phrase = "Now is the time for all good men to come to the aid of their country."
    let quotes = [| "Never say never"; "It's never too late"; "Nevermore!" |]
    StringClassicAssert.IsMatch( "all good men", phrase )
    StringClassicAssert.IsMatch( "Now.*come", phrase )
    ClassicAssert.That( phrase, Does.Match( "all good men" ) )
    ClassicAssert.That( phrase, Does.Match( "Now.*come" ) )
    ClassicAssert.That( phrase, Does.Not.Match("all.*men.*good") )
    ClassicAssert.That( phrase, Does.Match("ALL").IgnoreCase )
    ClassicAssert.That( quotes, Is.All.Matches("never").IgnoreCase )

[<Test>]
let EqualityTests() =
    let i3 = [| 1; 2; 3 |]
    let d3 = [| 1.0; 2.0; 3.0 |]
    let iunequal = [| 1; 3; 2 |]
    ClassicAssert.AreEqual(4, 2 + 2)
    ClassicAssert.AreEqual(i3, d3)
    ClassicAssert.AreNotEqual(5, 2 + 2)
    ClassicAssert.AreNotEqual(i3, iunequal)
    ClassicAssert.That(2 + 2, Is.EqualTo(4))
    ClassicAssert.That(2 + 2 = 4)
    ClassicAssert.That(i3, Is.EqualTo(d3))
    ClassicAssert.That(2 + 2, Is.Not.EqualTo(5))
    ClassicAssert.That(i3, Is.Not.EqualTo(iunequal))

[<Test>]
let EqualityTestsWithTolerance() =
    ClassicAssert.AreEqual(5.0, 4.99, 0.05)
    ClassicAssert.That(4.99, Is.EqualTo(5.0).Within(0.05))
    ClassicAssert.That(4.0, Is.Not.EqualTo(5.0).Within(0.5))
    ClassicAssert.That(4.99f, Is.EqualTo(5.0f).Within(0.05f))
    ClassicAssert.That(4.99m, Is.EqualTo(5.0m).Within(0.05m))
    ClassicAssert.That(3999999999u, Is.EqualTo(4000000000u).Within(5u))
    ClassicAssert.That(499, Is.EqualTo(500).Within(5))
    ClassicAssert.That(4999999999L, Is.EqualTo(5000000000L).Within(5L))
    ClassicAssert.That(5999999999UL, Is.EqualTo(6000000000UL).Within(5UL))

[<Test>]
let EqualityTestsWithTolerance_MixedFloatAndDouble() =
    // Bug Fix 1743844
    ClassicAssert.That(2.20492, Is.EqualTo(2.2).Within(0.01f),
        "Double actual, Double expected, Single tolerance")
    ClassicAssert.That(2.20492, Is.EqualTo(2.2f).Within(0.01),
        "Double actual, Single expected, Double tolerance" )
    ClassicAssert.That(2.20492, Is.EqualTo(2.2f).Within(0.01f),
        "Double actual, Single expected, Single tolerance" )
    ClassicAssert.That(2.20492f, Is.EqualTo(2.2f).Within(0.01),
        "Single actual, Single expected, Double tolerance")
    ClassicAssert.That(2.20492f, Is.EqualTo(2.2).Within(0.01),
        "Single actual, Double expected, Double tolerance")
    ClassicAssert.That(2.20492f, Is.EqualTo(2.2).Within(0.01f),
        "Single actual, Double expected, Single tolerance")

[<Test>]
let EqualityTestsWithTolerance_MixingTypesGenerally() =
    ClassicAssert.That(202.0, Is.EqualTo(200.0).Within(2),
        "Double actual, Double expected, int tolerance")
    ClassicAssert.That( 4.87m, Is.EqualTo(5).Within(0.25),
        "Decimal actual, int expected, Double tolerance" )
    ClassicAssert.That( 4.87m, Is.EqualTo(5ul).Within(1),
        "Decimal actual, ulong expected, int tolerance" )
    ClassicAssert.That( 487, Is.EqualTo(500).Within(25),
        "int actual, int expected, int tolerance" )
    ClassicAssert.That( 487u, Is.EqualTo(500).Within(25),
        "uint actual, int expected, int tolerance" )
    ClassicAssert.That( 487L, Is.EqualTo(500).Within(25),
        "long actual, int expected, int tolerance" )
    ClassicAssert.That( 487ul, Is.EqualTo(500).Within(25),
        "ulong actual, int expected, int tolerance" )

[<Test>]
let ComparisonTests() =
    ClassicAssert.Greater(7, 3)
    ClassicAssert.GreaterOrEqual(7, 3)
    ClassicAssert.GreaterOrEqual(7, 7)
    ClassicAssert.That(7, Is.GreaterThan(3))
    ClassicAssert.That(7, Is.GreaterThanOrEqualTo(3))
    ClassicAssert.That(7, Is.AtLeast(3))
    ClassicAssert.That(7, Is.GreaterThanOrEqualTo(7))
    ClassicAssert.That(7, Is.AtLeast(7))

    ClassicAssert.Less(3, 7)
    ClassicAssert.LessOrEqual(3, 7)
    ClassicAssert.LessOrEqual(3, 3)
    ClassicAssert.That(3, Is.LessThan(7))
    ClassicAssert.That(3, Is.LessThanOrEqualTo(7))
    ClassicAssert.That(3, Is.AtMost(7))
    ClassicAssert.That(3, Is.LessThanOrEqualTo(3))
    ClassicAssert.That(3, Is.AtMost(3))

[<Test>]
let AllItemsTests() =
    let ints = [| 1; 2; 3; 4 |]
    let doubles = [| 0.99; 2.1; 3.0; 4.05 |]
    let strings = [| "abc"; "bad"; "cab"; "bad"; "dad" |]
    CollectionClassicAssert.AllItemsAreNotNull(ints)
    CollectionClassicAssert.AllItemsAreInstancesOfType(ints, typeof<int>)
    CollectionClassicAssert.AllItemsAreInstancesOfType(strings, typeof<string>)
    CollectionClassicAssert.AllItemsAreUnique(ints)
    ClassicAssert.That(ints, Is.All.Not.Null)
    ClassicAssert.That(ints, Has.None.Null)
    ClassicAssert.That(ints, Is.All.InstanceOf(typeof<int>))
    ClassicAssert.That(ints, Has.All.InstanceOf(typeof<int>))
    ClassicAssert.That(strings, Is.All.InstanceOf(typeof<string>))
    ClassicAssert.That(strings, Has.All.InstanceOf(typeof<string>))
    ClassicAssert.That(ints, Is.Unique)
    ClassicAssert.That(strings, Is.Not.Unique)
    ClassicAssert.That(ints, Is.All.GreaterThan(0))
    ClassicAssert.That(ints, Has.All.GreaterThan(0));
    ClassicAssert.That(ints, Has.None.LessThanOrEqualTo(0))
    ClassicAssert.That(strings, Is.All.Contains( "a" ) )
    ClassicAssert.That(strings, Has.All.Contains( "a" ) )
    ClassicAssert.That(strings, Has.Some.StartsWith( "ba" ) )
    ClassicAssert.That( strings, Has.Some.Property( "Length" ).EqualTo( 3 ) )
    ClassicAssert.That( strings, Has.Some.StartsWith( "BA" ).IgnoreCase )
    ClassicAssert.That( doubles, Has.Some.EqualTo( 1.0 ).Within( 0.05 ) )

[<Test>]
let SomeItemTests() =
    let mixed = [| 1; 2; "3"; null; "four"; 100 |]: obj array
    let strings = [| "abc"; "bad"; "cab"; "bad"; "dad" |]
    ClassicAssert.That(mixed, Has.Some.Null)
    ClassicAssert.That(mixed, Has.Some.InstanceOf<int>())
    ClassicAssert.That(mixed, Has.Some.InstanceOf<string>())
    ClassicAssert.That(strings, Has.Some.StartsWith( "ba" ) )
    ClassicAssert.That(strings, Has.Some.Not.StartsWith( "ba" ) )

[<Test>]
let NoItemTests() =
    let ints = [| 1; 2; 3; 4; 5 |]
    let strings = [| "abc"; "bad"; "cab"; "bad"; "dad" |]
    ClassicAssert.That(ints, Has.None.Null)
    ClassicAssert.That(ints, Has.None.InstanceOf<string>());
    ClassicAssert.That(ints, Has.None.GreaterThan(99));
    ClassicAssert.That(strings, Has.None.StartsWith( "qu" ) );

[<Test>]
let CollectionContainsTests() =
    let iarray = [| 1; 2; 3 |]
    let sarray = [| "a"; "b"; "c" |]

    ClassicAssert.Contains(3, iarray)
    ClassicAssert.Contains("b", sarray)
    CollectionClassicAssert.Contains(iarray, 3)
    CollectionClassicAssert.Contains(sarray, "b")
    CollectionClassicAssert.DoesNotContain(sarray, "x")
    // Showing that Contains uses NUnit equality
    CollectionClassicAssert.Contains( iarray, 1.0 )

    ClassicAssert.That(iarray, Has.Member(3))
    ClassicAssert.That(sarray, Has.Member("b"))
    ClassicAssert.That(sarray, Has.No.Member("x"))
    // Showing that Contains uses NUnit equality
    ClassicAssert.That(iarray, Has.Member( 1.0 ))

    // Only available using the new syntax
    // Note that EqualTo and SameAs do NOT give
    // identical results to Contains because 
    // Contains uses Object.Equals()
    ClassicAssert.That(iarray, Has.Some.EqualTo(3))
    ClassicAssert.That(iarray, Has.Member(3))
    ClassicAssert.That(sarray, Has.Some.EqualTo("b"))
    ClassicAssert.That(sarray, Has.None.EqualTo("x"))
    ClassicAssert.That(iarray, Has.None.SameAs( 1.0 ))
    ClassicAssert.That(iarray, Has.All.LessThan(10))
    ClassicAssert.That(sarray, Has.All.Length.EqualTo(1))
    ClassicAssert.That(sarray, Has.None.Property("Length").GreaterThan(3))

[<Test>]
let CollectionEquivalenceTests() =
    let ints1to5 = [| 1; 2; 3; 4; 5 |]
    let twothrees = [| 1; 2; 3; 3; 4; 5 |]
    let twofours = [| 1; 2; 3; 4; 4; 5 |]

    CollectionClassicAssert.AreEquivalent( [| 2; 1; 4; 3; 5 |], ints1to5)
    CollectionClassicAssert.AreNotEquivalent( [| 2; 2; 4; 3; 5 |], ints1to5)
    CollectionClassicAssert.AreNotEquivalent( [| 2; 4; 3; 5 |], ints1to5)
    CollectionClassicAssert.AreNotEquivalent( [| 2; 2; 1; 1; 4; 3; 5 |], ints1to5)
    CollectionClassicAssert.AreNotEquivalent(twothrees, twofours)

    ClassicAssert.That( [| 2; 1; 4; 3; 5 |], Is.EquivalentTo(ints1to5))
    ClassicAssert.That( [| 2; 2; 4; 3; 5 |], Is.Not.EquivalentTo(ints1to5))
    ClassicAssert.That( [| 2; 4; 3; 5 |], Is.Not.EquivalentTo(ints1to5))
    ClassicAssert.That( [| 2; 2; 1; 1; 4; 3; 5 |], Is.Not.EquivalentTo(ints1to5))

[<Test>]
let SubsetTests() =
    let ints1to5 = [| 1; 2; 3; 4; 5 |]

    CollectionClassicAssert.IsSubsetOf( [| 1; 3; 5 |], ints1to5)
    CollectionClassicAssert.IsSubsetOf( [| 1; 2; 3; 4; 5 |], ints1to5)
    CollectionClassicAssert.IsNotSubsetOf( [| 2; 4; 6 |], ints1to5)
    CollectionClassicAssert.IsNotSubsetOf( [| 1; 2; 2; 2; 5 |], ints1to5)

    ClassicAssert.That( [| 1; 3; 5 |], Is.SubsetOf(ints1to5))
    ClassicAssert.That( [| 1; 2; 3; 4; 5 |], Is.SubsetOf(ints1to5))
    ClassicAssert.That( [| 2; 4; 6 |], Is.Not.SubsetOf(ints1to5))

[<Test>]
let PropertyTests() =
    let array = [| "abc"; "bca"; "xyz"; "qrs" |]
    let array2 = [| "a"; "ab"; "abc" |]
    let list = new System.Collections.ArrayList( array )

    ClassicAssert.That( list, Has.Property( "Count" ) )
    ClassicAssert.That( list, Has.No.Property( "Length" ) )

    ClassicAssert.That( "Hello", Has.Length.EqualTo( 5 ) )
    ClassicAssert.That( "Hello", Has.Length.LessThan( 10 ) )
    ClassicAssert.That( "Hello", Has.Property("Length").EqualTo(5) )
    ClassicAssert.That( "Hello", Has.Property("Length").GreaterThan(3) )

    ClassicAssert.That( array, Has.Property( "Length" ).EqualTo( 4 ) )
    ClassicAssert.That( array, Has.Length.EqualTo( 4 ) )
    ClassicAssert.That( array, Has.Property( "Length" ).LessThan( 10 ) )

    ClassicAssert.That( array, Has.All.Property("Length").EqualTo(3) )
    ClassicAssert.That( array, Has.All.Length.EqualTo( 3 ) )
    ClassicAssert.That( array, Is.All.Length.EqualTo( 3 ) )
    ClassicAssert.That( array, Has.All.Property("Length").EqualTo(3) )
    ClassicAssert.That( array, Is.All.Property("Length").EqualTo(3) )

    ClassicAssert.That( array2, Has.Some.Property("Length").EqualTo(2) )
    ClassicAssert.That( array2, Has.Some.Length.EqualTo(2) )
    ClassicAssert.That( array2, Has.Some.Property("Length").GreaterThan(2) )

    ClassicAssert.That( array2, Is.Not.Property("Length").EqualTo(4) )
    ClassicAssert.That( array2, Is.Not.Length.EqualTo( 4 ) )
    ClassicAssert.That( array2, Has.No.Property("Length").GreaterThan(3) )

    ClassicAssert.That( List.Map( array2 ).Property("Length"), Is.EqualTo( [| 1; 2; 3 |] ) )
    ClassicAssert.That( List.Map( array2 ).Property("Length"), Is.EquivalentTo( [| 3; 2; 1 |] ) )
    ClassicAssert.That( List.Map( array2 ).Property("Length"), Is.SubsetOf( [| 1; 2; 3; 4; 5 |] ) )
    ClassicAssert.That( List.Map( array2 ).Property("Length"), Is.Unique )

    ClassicAssert.That( list, Has.Count.EqualTo( 4 ) )

[<Test>]
let NotTests() =
    ClassicAssert.That(42, Is.Not.Null)
    ClassicAssert.That(42, Is.Not.True)
    ClassicAssert.That(42, Is.Not.False)
    ClassicAssert.That(2.5, Is.Not.NaN)
    ClassicAssert.That(2 + 2, Is.Not.EqualTo(3))
    ClassicAssert.That(2 + 2, Is.Not.Not.EqualTo(4))
    ClassicAssert.That(2 + 2, Is.Not.Not.Not.EqualTo(5))
