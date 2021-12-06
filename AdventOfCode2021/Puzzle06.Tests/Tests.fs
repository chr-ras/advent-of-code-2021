module Tests

open Program
open Xunit

let testInput = "3,4,3,1,2"

[<Fact>]
let ``Part1.getSchoolSizeAfter_18days`` () =
    let actual = Puzzle06.Part1.getSchoolSizeAfter(18, testInput |> Puzzle06.parseInput)
    Assert.Equal(26, actual)

[<Fact>]
let ``Part1.getSchoolSizeAfter_80days`` () =
    let actual = Puzzle06.Part1.getSchoolSizeAfter(80, testInput |> Puzzle06.parseInput)
    Assert.Equal(5934, actual)

[<Fact>]
let ``sortByCycle`` () =
    let actual = testInput |> Puzzle06.parseInput |> Puzzle06.Part2.sortByCycle
    Assert.StrictEqual([0; 1; 1; 2; 1; 0; 0; 0], actual)

[<Fact>]
let ``Part2.getSchoolSizeAfter_18days`` () =
    let actual = Puzzle06.Part2.getSchoolSizeAfter(18, testInput |> Puzzle06.parseInput)
    Assert.Equal(26, actual)

[<Fact>]
let ``Part2.getSchoolSizeAfter_80days`` () =
    let actual = Puzzle06.Part2.getSchoolSizeAfter(80, testInput |> Puzzle06.parseInput)
    Assert.Equal(5934, actual)