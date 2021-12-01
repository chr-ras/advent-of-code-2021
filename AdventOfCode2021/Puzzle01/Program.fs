open System.IO

module Puzzle01 =
    let countIncreases(measurements: list<int>) =
        measurements
        |> List.pairwise
        |> List.map(fun (pre, next) -> if pre < next then 1 else 0)
        |> List.sum

    let countRisingSlidingWindows(measurements: list<int>) =
        measurements
        |> List.windowed 3
        |> List.map(fun (window) -> List.sum(window))
        |> countIncreases

    let parseInput(lines: list<string>) =
        lines |> List.map(fun line -> int(line))

    let evaluateMeasurements(lines: list<string>) =
        lines 
        |> parseInput
        |> countIncreases

    let evaluateSlidingWindows(lines: list<string>) =
        lines
        |> parseInput
        |> countRisingSlidingWindows

let puzzleInput = File.ReadAllLines("./puzzle-input.txt") |> Array.toList

let result = puzzleInput |> Puzzle01.evaluateMeasurements
printfn "Total increases: %d" result

let windowedResult = puzzleInput |> Puzzle01.evaluateSlidingWindows
printfn "Total windowed increases: %d" windowedResult
