module Tests

open System.IO
open Program
open Xunit

let testInput = File.ReadAllLines("./test-input.txt")

[<Fact>]
let ``parseInput`` () =
    let actual = [| "129"; "225"; "123" |] |> Puzzle09.parseInput
    let expected = [| [|9;9;9;9;9|]; [|9;1;2;9;9|]; [|9;2;2;5;9|]; [|9;1;2;3;9|]; [|9;9;9;9;9|] |]

    let result = actual = expected

    Assert.True(result)

[<Fact>]
let ``Part1.getLowPointRiskLevels`` () =
    let actual = testInput |> Puzzle09.Part1.getLowPointRiskLevels

    Assert.Equal(15, actual)

[<Fact>]
let ``Part2.determineThreeLargestBasins`` () =
    let actual = testInput |> Puzzle09.Part2.determineThreeLargestBasins

    Assert.Equal(1134, actual)