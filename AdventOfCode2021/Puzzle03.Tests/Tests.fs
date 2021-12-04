module Tests

open Program
open Xunit

[<Fact>]
let ``calculatePowerConsumption`` () =
    let input = ["00100"; "11110"; "10110"; "10111"; "10101"; "01111"; "00111"; "11100"; "10000"; "11001"; "00010"; "01010"]
    Assert.Equal(198, Puzzle03.Part1.calculatePowerConsumption(input))

[<Fact>]
let ``calculateLifeSupportRating`` () =
    let input = ["00100"; "11110"; "10110"; "10111"; "10101"; "01111"; "00111"; "11100"; "10000"; "11001"; "00010"; "01010"]
    Assert.Equal(230, Puzzle03.Part2.calculateLifeSupportRating(input))
