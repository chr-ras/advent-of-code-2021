module Tests

open Program
open Xunit
open System.IO

let simpleInput = ["acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"]
let testInput = "./test-input.txt" |> File.ReadAllLines |> Array.toList

[<Fact>]
let ``Puzzle08.parseInput`` () =
    let expected = [(
        ["acedgfb"; "cdfbe"; "gcdfa"; "fbcad"; "dab"; "cefabd"; "cdfgeb"; "eafb"; "cagedb"; "ab"],
        ["cdfeb"; "fcadb"; "cdfeb"; "cdbaf"]
    )]

    let actual = simpleInput |> Puzzle08.parseInput
    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``Puzzle08.Part1.getUniqueSegmentCount`` () =
    let actual = testInput |> Puzzle08.Part1.getUniqueSegmentCount
    
    Assert.Equal(26, actual)

[<Fact>]
let ``Puzzle08.Part2.solveDisplays`` () =
    let actual = simpleInput |> Puzzle08.parseInput |> Puzzle08.Part2.solveDisplays

    Assert.StrictEqual([ [|"d"; "e"; "a"; "f"; "g"; "b"; "c"|] ], actual)

[<Fact>]
let ``Puzzle08.Part2.buildNumbersFromSolution`` () =
    let actual = simpleInput |> Puzzle08.parseInput |> Puzzle08.Part2.solveDisplays |> List.map (fun solution -> solution |> Puzzle08.Part2.buildNumbersFromSolution)

    let expected = 
        [
            [
                "cagedb" |> Seq.toList |> List.sort;
                "ab" |> Seq.toList;
                "gcdfa" |> Seq.toList |> List.sort;
                "fbcad" |> Seq.toList |> List.sort;
                "eafb" |> Seq.toList |> List.sort;
                "cdfbe" |> Seq.toList |> List.sort;
                "cdfgeb" |> Seq.toList |> List.sort;
                "abd" |> Seq.toList;
                "acedgfb" |> Seq.toList |> List.sort;
                "cefabd" |> Seq.toList |> List.sort;
            ] |> List.map (fun digit -> digit |> List.map (fun segment -> string(segment)))
        ]

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``Puzzle08.Part2.getOutputSum_simpleInput`` () =
    let actual = simpleInput |> Puzzle08.Part2.getOutputSum

    Assert.Equal(5353, actual)

[<Fact>]
let ``Puzzle08.Part2.getOutputSum_testInput`` () =
    let actual = testInput |> Puzzle08.Part2.getOutputSum

    Assert.Equal(61229, actual)