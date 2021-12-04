
open System
open System.IO

module Puzzle03 =
    let parseInput(lines: list<string>) = 
        lines
        |> List.map(fun line -> line.ToCharArray() |> Array.toList)

    let mapRawDiagnostics(row: list<char>) =
        row
        |> List.map (fun value ->
            match value with
            | '1' -> 1
            | '0' -> -1
            | _ -> 0)

    let getRawDiagnosticsFromChars(rows: list<list<char>>) = rows |> List.map mapRawDiagnostics
    let getRawDiagnostics(rows: list<string>) = rows |> parseInput |> getRawDiagnosticsFromChars
        
    module Part1 =
        let rec sumColumns(acc: list<int>, row: list<int>) =
            match acc with
            | accHead :: accTail -> 
                let rowHead :: rowTail = row
                accHead + rowHead :: sumColumns(accTail, rowTail)
            | [] -> []

        let parseDiagnostics(diagnostics: list<list<char>>) =
            diagnostics
            |> getRawDiagnosticsFromChars
            |> List.reduce (fun acc row -> sumColumns(acc, row))

        let calculatePowerConsumption(lines: list<string>) =
            let diagnostics = lines |> parseInput |> parseDiagnostics

            let gammaRateBinary = 
                diagnostics 
                |> List.map (fun value -> if value > 0 then "1" else "0") 
                |> List.reduce (fun x y -> x + y)

            let epsilonRateBinary = 
                diagnostics
                |> List.map (fun value -> if value < 0 then "1" else "0")
                |> List.reduce (fun x y -> x + y)

            let gammaRate = Convert.ToInt32(gammaRateBinary, 2)
            let epsilonRate = Convert.ToInt32(epsilonRateBinary, 2)

            gammaRate * epsilonRate

    module Part2 =
        let rec calculateRating(index: int, bitCriteria: int -> int, diagnostics: list<list<int>>) =
            let sums = diagnostics |> List.reduce (fun acc row -> Part1.sumColumns(acc, row))

            let keep = sums.Item index |> bitCriteria

            let filteredDiagnostics = diagnostics |> List.filter (fun row -> (row.Item index) = keep)

            match filteredDiagnostics with
            | head :: [] -> head
            | _head :: _tail -> calculateRating(index + 1, bitCriteria, filteredDiagnostics)
            | [] -> []

        let calculateLifeSupportRating(lines: list<string>) =
            let diagnostics = lines |> getRawDiagnostics

            let oxygenBitCriteria = (fun sum -> if sum >= -0 then 1 else -1)
            let co2ScrubberBitCriteria = (fun sum -> if sum < 0 then 1 else -1)

            let getRawRating = (fun (rating: list<int>) -> 
                rating
                |> List.map (fun elem -> if elem = 1 then "1" else "0")
                |> List.reduce (fun acc elem -> acc + elem))

            let oxygenGeneratorRatingRaw = calculateRating(0, oxygenBitCriteria, diagnostics) |> getRawRating
            let co2ScrubberRatingRaw = calculateRating(0, co2ScrubberBitCriteria, diagnostics) |> getRawRating

            let oxygenGeneratorRating = Convert.ToInt32(oxygenGeneratorRatingRaw, 2)
            let co2ScrubberRating = Convert.ToInt32(co2ScrubberRatingRaw, 2)

            oxygenGeneratorRating * co2ScrubberRating


let puzzleInput = File.ReadAllLines("./puzzle-input.txt") |> Array.toList

let consumption = puzzleInput |> Puzzle03.Part1.calculatePowerConsumption

printfn "Consumption: %d" consumption

let lifeSupportRating = puzzleInput |> Puzzle03.Part2.calculateLifeSupportRating

printfn "Life support rating: %d" lifeSupportRating