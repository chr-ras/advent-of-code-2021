module Tests

open System.IO
open Xunit
open Program

[<Fact>]
let ``parseInput`` () =
    let input = File.ReadAllText("./parseInput-test-input.txt")

    let (actualNumbers, actualBoards) = input |> Puzzle04.parseInput
    let expectedNumbers = [7; 4; 9]
    let expectedBoards = [
        [(false, 22);(false, 13);(false, 17);(false, 8);(false, 2);(false, 23)];
        [(false, 3);(false, 15);(false, 0);(false, 9);(false, 18);(false, 13)]
    ]

    Assert.StrictEqual(expectedNumbers, actualNumbers)
    Assert.StrictEqual(expectedBoards, actualBoards)

[<Fact>]
let ``getWinningBoardScore`` () =
    let input = File.ReadAllText("./test-input.txt")
    let actual = input |> Puzzle04.Part1.getWinningBoardScore
    Assert.Equal(4512, actual)

[<Fact>]
let ``getLastWinningBoardScore`` () =
    let input = File.ReadAllText("./test-input.txt")
    let actual = input |> Puzzle04.Part2.getLastWinningBoardScore
    Assert.Equal(1924, actual)
