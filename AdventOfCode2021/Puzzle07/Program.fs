
open System.IO

module Puzzle07 =
    let parseInput(input: string) =
        input.Split(",") |> Array.map (fun crab -> int(crab)) |> Array.toList

    let calculateTotalFuelForPosition(position: int, crabs: list<int>, determineFuel: int -> int) =
        crabs |> List.fold (fun acc crab -> acc + (abs(crab - position) |> determineFuel)) 0

    let calculateMinFuelConsumption(rawCrabs: string, determineFuel: int -> int) =
        let crabs = rawCrabs |> parseInput
        let minPos = crabs |> List.min
        let maxPos = crabs |> List.max

        [minPos..maxPos]
        |> List.map (fun pos -> (pos, calculateTotalFuelForPosition(pos, crabs, determineFuel)))
        |> List.minBy (fun (_, fuel) -> fuel)

    module Part1 =
        let getPositionWithMinimalFuelConsumption(rawCrabs: string) =
            calculateMinFuelConsumption(rawCrabs, fun distance -> distance)

    module Part2 =
        let calculateAlternateFuelForDistance(distance: int) =
            [1..distance] |> List.sum

        let getPositionWithMinimalAlternateFuelConsumption(rawCrabs: string) =
            calculateMinFuelConsumption(rawCrabs, calculateAlternateFuelForDistance)

let input = File.ReadAllText("./puzzle-input.txt")

let (pos, fuel) = input |> Puzzle07.Part1.getPositionWithMinimalFuelConsumption
printfn "Position %d requires the least amount of fuel: %d" pos fuel

let (alternatePos, alternateFuel) = input |> Puzzle07.Part2.getPositionWithMinimalAlternateFuelConsumption
printfn "Position %d requires the least amount of fuel (alternate calculation): %d" alternatePos alternateFuel