open System.IO

module Puzzle08 =
    type Segments = list<string>
    type Outputs = list<string>
    type DisplayDebug = list<(list<string> * list<string>)>

    let parseInput(lines: list<string>): DisplayDebug =
        lines
        |> List.map (fun line -> 
            let parts = line.Split(" | ")
            (parts[0].Split(" ") |> Array.toList, parts[1].Split(" ") |> Array.toList))

    module Part1 =
        let consistsOfUniqueSegments(digit: string) =
            List.contains digit.Length [2;3;4;7]

        let countUniqueSegments(displays: DisplayDebug) =
            displays
            |> List.map (fun (_, outputs) -> outputs)
            |> List.map (fun outputs -> (outputs |> List.filter consistsOfUniqueSegments).Length)
            |> List.sum

        let getUniqueSegmentCount(lines: list<string>) =
            lines |> parseInput |> countUniqueSegments

let uniqueSegments = "./puzzle-input.txt" |> File.ReadAllLines |> Array.toList |> Puzzle08.Part1.getUniqueSegmentCount
printfn "Unique segments: %d" uniqueSegments
