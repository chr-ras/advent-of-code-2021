module Tests

open Program
open Xunit

[<Fact>]
let ``part1_evaluatePosition`` () =
    let lines = [ "forward 5"; "down 5"; "forward 8"; "up 3"; "down 8"; "forward 2" ]
    Assert.Equal(150, Puzzle02.Part1.evaluatePosition(lines))

[<Fact>]
let ``part2_evaluatePosition`` () =
    let lines = [ "forward 5"; "down 5"; "forward 8"; "up 3"; "down 8"; "forward 2" ]
    Assert.Equal(900, Puzzle02.Part2.evaluatePosition(lines))
