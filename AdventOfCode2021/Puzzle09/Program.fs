
open System.IO

module Puzzle09 =
    type LowPoint = ((int * int) * int)

    let parseInput(lines: string[]) =
        let borderLine = [| for _ in 1..lines[0].Length + 2 -> 9 |]

        let parsedLocations = 
            lines
            |> Array.toList
            |> List.map Seq.toList
            |> List.map (fun line -> (['9'] @ line @ ['9']) |> List.toArray)
            |> List.map (fun line -> line |> Array.map (fun loc -> int(string(loc))))
        
        ([borderLine] @ parsedLocations @ [borderLine]) |> List.toArray

    module Part1 =
        let findLowPoints(input: int[][]): LowPoint list = 
            let xs = [for x in 1..input[0].Length - 2 -> x]
            let ys = [for y in 1..input.Length - 2 -> y]

            let coordsToCheck = List.allPairs xs ys

            coordsToCheck
            |> List.filter (fun (x, y) ->
                let height = input[y][x]
                
                let neighborsHigherThanCurrent = 
                    [ input[y-1][x]; input[y][x-1]; input[y+1][x]; input[y][x+1] ]
                    |> List.filter (fun otherHeight -> otherHeight > height)

                neighborsHigherThanCurrent.Length = 4)
            |> List.map (fun (x, y) -> ((x, y), input[y][x]))

        let getLowPointRiskLevels(lines: string[]) =
            lines
            |> parseInput
            |> findLowPoints
            |> List.map (fun (_, height) -> 1 + height)
            |> List.sum

    module Part2 =
        let rec traverse(x: int, y: int, input: int[][]) =
            let currentHeight = input[y][x]
            if currentHeight = 9 || currentHeight = -1
            then 0
            else
                input[y][x] <- (-1)

                1 
                + traverse(x, y-1, input) 
                + traverse(x-1, y, input) 
                + traverse(x, y+1, input)
                + traverse(x+1, y, input)

        let determineBasinSize(lowpoint: LowPoint, input: int[][]) =
            let ((x, y), _) = lowpoint
            traverse(x, y, input)

        let threeLargestBasins(lowPoints: LowPoint list, input: int[][]) =
            lowPoints
            |> List.map (fun lowpoint -> determineBasinSize(lowpoint, input))
            |> List.sortDescending
            |> List.take 3
            |> List.reduce (fun acc size -> acc * size)

        let determineThreeLargestBasins(lines: string[]) =
            let input = lines |> parseInput

            threeLargestBasins(input |> Part1.findLowPoints, input)

let puzzleInput = "./puzzle-input.txt" |> File.ReadAllLines

let riskLevel = puzzleInput |> Puzzle09.Part1.getLowPointRiskLevels
printfn "Risk level: %d" riskLevel

let basinSize = puzzleInput |> Puzzle09.Part2.determineThreeLargestBasins
printfn "Three largest basins: %d" basinSize