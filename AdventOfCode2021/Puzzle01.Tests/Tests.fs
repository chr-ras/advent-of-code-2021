module Tests

open Xunit
open Program

[<Fact>]
let ``evaluateMeasurements`` () =
    let actual = [ "199"; "200"; "208"; "210"; "200"; "207"; "240"; "269"; "260"; "263"] |> Puzzle01.evaluateMeasurements
    Assert.Equal(7, actual)

[<Fact>]
let ``evaluateSlidingWindows`` () =
    let actual = [ "199"; "200"; "208"; "210"; "200"; "207"; "240"; "269"; "260"; "263"] |> Puzzle01.evaluateSlidingWindows
    Assert.Equal(5, actual)
