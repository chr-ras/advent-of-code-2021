module Tests

open System.IO
open Program
open Xunit

[<Fact>]
let ``parseInput`` () =
    let input = ["0,9 -> 5,9"; "8,0 -> 0,8"]
    let actual = input |> Puzzle05.parseInput
    let expected = [
        ((0, 9), (5, 9));
        ((8, 0), (0, 8))
    ]

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``getNumberOfStraightLineIntersections`` () =
    let input = File.ReadAllLines("./test-input.txt") |> Array.toList
    let actual = input |> Puzzle05.Part1.getNumberOfStraightLineIntersections

    Assert.Equal(5, actual)

[<Fact>]
let ``getLinePoints 1,1 -> 1,3`` () =
    let input = Puzzle05.Line ((1, 1), (1, 3))
    let actual = input |> Puzzle05.getLinePoints
    let expected = [(1, 1); (1, 2); (1, 3) ]

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``getLinePoints 9,7 -> 7,7`` () =
    let input = Puzzle05.Line ((9, 7), (7, 7))
    let actual = input |> Puzzle05.getLinePoints
    let expected = [(9, 7); (8, 7); (7, 7) ]

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``getLinePoints 1,1 -> 3,3`` () =
    let input = Puzzle05.Line ((1, 1), (3, 3))
    let actual = input |> Puzzle05.getLinePoints
    let expected = [(1, 1); (2, 2); (3, 3) ]

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``getLinePoints 9,7 -> 7,9`` () =
    let input = Puzzle05.Line ((9, 7), (7, 9))
    let actual = input |> Puzzle05.getLinePoints
    let expected = [(9, 7); (8, 8); (7, 9) ]

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``getNumberOfAllLineIntersections`` () =
    let input = File.ReadAllLines("./test-input.txt") |> Array.toList
    let actual = input |> Puzzle05.Part2.getNumberOfAllLineIntersections

    Assert.Equal(12, actual)