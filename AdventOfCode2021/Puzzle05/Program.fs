
open System.IO

module Puzzle05 =
    type Point = (int * int)
    type Line = (Point * Point)

    let parseInput(lines: list<string>) =
        lines
        |> List.map (fun line -> 
            let rawPoints = line.Split(" -> ")

            let getPoint(rawPoint: string) =
                let rawCoords = rawPoint.Split(",")
                Point (int(rawCoords[0]), int(rawCoords[1]))

            Line (rawPoints[0] |> getPoint, rawPoints[1] |> getPoint))

    let getTotalIntersections(map: int[][]) =
        map 
        |> Array.map (fun row -> (row |> Array.filter (fun elem -> elem >= 2)).Length)
        |> Array.sum

    let getLinePoints(line: Line) = 
        let ((x1, y1), (x2, y2)) = line

        let xDiff = x2 - x1
        let yDiff = y2 - y1
        let xDiffAbs = abs xDiff
        let yDiffAbs = abs yDiff

        let lineLength = (max xDiffAbs yDiffAbs)

        [0..lineLength]
        |> List.map (fun index -> Point (x1 + (xDiff / lineLength) * index, y1 + (yDiff / lineLength) * index))

    let getNumberOfLineIntersections(input: list<string>, filter: Line -> bool) =
        let lines = 
            input 
            |> parseInput 
            |> List.filter filter

        let map = [| for _ in 1..1_000 -> [|for _ in 1..1_000-> 0|]|]

        lines
        |> List.iter (fun line -> 
            line 
            |> getLinePoints
            |> List.iter (fun (x, y) -> map[y][x] <- map[y][x] + 1 ))

        map |> getTotalIntersections

    module Part1 =
        let getNumberOfStraightLineIntersections(input: list<string>) =
            (input, (fun ((x1, y1), (x2, y2)) -> x1 = x2 || y1 = y2)) |> getNumberOfLineIntersections

    module Part2 = 
        let getNumberOfAllLineIntersections(input: list<string>) =
            (input, (fun _ -> true)) |> getNumberOfLineIntersections

let input = File.ReadAllLines("./puzzle-input.txt") |> Array.toList

let straightIntersections = input |> Puzzle05.Part1.getNumberOfStraightLineIntersections
printfn "Straight intersections: %d" straightIntersections

let allIntersections = input |> Puzzle05.Part2.getNumberOfAllLineIntersections
printfn "All intersections: %d" allIntersections