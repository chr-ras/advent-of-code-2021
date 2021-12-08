module Tests

open Program
open Xunit
open System.IO

let simpleInput = ["acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"]

[<Fact>]
let ``Puzzle08.parseInput`` () =
    let expected = [(
        ["acedgfb"; "cdfbe"; "gcdfa"; "fbcad"; "dab"; "cefabd"; "cdfgeb"; "eafb"; "cagedb"; "ab"],
        ["cdfeb"; "fcadb"; "cdfeb"; "cdbaf"]
    )]

    let actual = simpleInput |> Puzzle08.parseInput
    Assert.StrictEqual(expected, actual)

let testInput = "./test-input.txt" |> File.ReadAllLines |> Array.toList

[<Fact>]
let ``Puzzle08.Part1.getUniqueSegmentCount`` () =
    let actual = testInput |> Puzzle08.Part1.getUniqueSegmentCount
    
    Assert.Equal(26, actual)
