module Tests

open Program
open Xunit

[<Fact>]
let ``Part1.getPositionWithMinimalFuelConsumption`` () =
    let actual = "16,1,2,0,4,2,7,1,2,14" |> Puzzle07.Part1.getPositionWithMinimalFuelConsumption
    Assert.StrictEqual((2, 37), actual)

[<Fact>]
let ``Part2.getPositionWithMinimalAlternateFuelConsumption`` () =
    let actual = "16,1,2,0,4,2,7,1,2,14" |> Puzzle07.Part2.getPositionWithMinimalAlternateFuelConsumption
    Assert.StrictEqual((5, 168), actual)
