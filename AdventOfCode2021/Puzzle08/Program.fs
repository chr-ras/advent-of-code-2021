open System.IO

module Puzzle08 =
    type Segments = list<string>
    type Outputs = list<string>
    type DisplayDebug = list<(Segments * Outputs)>

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

    module Part2 = 
        let withoutSolvedLetters(digit: list<char>, solution: string[]) =
            digit |> List.filter (fun letter -> not (solution |> Array.contains(string(letter))))
        
        let solveDisplay(patterns: Segments) =
            let solution = Array.create 7 ""
            
            let sortedPatterns = 
                patterns 
                |> List.map (fun pattern -> pattern |> Seq.toList |> List.sort)
                |> List.sortBy (fun pattern -> pattern.Length)
                |> List.toArray

            let mutable one = sortedPatterns[0]
            let mutable four = sortedPatterns[2]
            let mutable seven = sortedPatterns[1]
            let mutable eight = sortedPatterns[6]

            let mutable lengthFive = sortedPatterns |> Array.filter (fun pattern -> pattern.Length = 5)
            let mutable lengthSix = sortedPatterns |> Array.filter (fun pattern -> pattern.Length = 6)

            let mutable nine = lengthSix |> Array.find (fun pattern -> (four |> List.except(pattern)).Length = 0 )
            lengthSix <- lengthSix |> Array.except([nine])

            solution[0] <- string((seven |> List.except(one)).Head)
            nine <- withoutSolvedLetters(nine, solution)

            solution[6] <- string((nine |> List.except(four)).Head)
            lengthFive <- lengthFive |> Array.map (fun pattern -> withoutSolvedLetters(pattern, solution))

            let mutable two = lengthFive |> Array.find (fun pattern -> (pattern |> List.except(four)).Length = 1 )
            lengthFive <- lengthFive |> Array.except([two])

            solution[4] <- string((two |> List.except(four)).Head)

            two <- withoutSolvedLetters(two, solution)
            lengthFive <- lengthFive |> Array.map (fun pattern -> withoutSolvedLetters(pattern, solution))

            let mutable three = lengthFive |> Array.find (fun pattern -> (pattern |> List.except(two)).Length = 1)
            let mutable five = lengthFive |> Array.find (fun pattern -> (pattern |> List.except(two)).Length = 2)

            solution[5] <- string((three |> List.except(two)).Head)
            
            solution[2] <- string((withoutSolvedLetters(one, solution)).Head)

            lengthSix <- lengthSix |> Array.map (fun pattern -> withoutSolvedLetters(pattern, solution))

            solution[1] <- string((lengthSix |> Array.find (fun pattern -> pattern.Length = 1)).Head)

            solution[3] <- string(withoutSolvedLetters(two, solution).Head)

            solution

        let solveDisplays(debug: DisplayDebug) = debug |> List.map (fun (displays, _outputs) -> displays |> solveDisplay)

        let buildNumbersFromSolution(solution: string[]) =
            [
                // 0-9
                [solution[0]; solution[1]; solution[2]; solution[4]; solution[5]; solution[6];] |> List.sort;
                [solution[2]; solution[5];] |> List.sort;
                [solution[0]; solution[2]; solution[3]; solution[4]; solution[6];] |> List.sort;
                [solution[0]; solution[2]; solution[3]; solution[5]; solution[6];] |> List.sort;
                [solution[1]; solution[3]; solution[2]; solution[5];] |> List.sort;
                [solution[0]; solution[1]; solution[3]; solution[5]; solution[6];] |> List.sort;
                [solution[0]; solution[1]; solution[3]; solution[4]; solution[5]; solution[6];] |> List.sort;
                [solution[0]; solution[2]; solution[5];] |> List.sort;
                solution |> Array.toList |> List.sort;
                [solution[0]; solution[1]; solution[2]; solution[3]; solution[5]; solution[6];] |> List.sort;
            ]

        let getOutputSum(lines: list<string>) =
            let debug = lines |> parseInput

            debug
            |> List.map (fun (segments, outputs) -> 
                let solution = segments |> solveDisplay
                let digitMapping = solution |> buildNumbersFromSolution
                
                let digits =
                    outputs 
                    |> List.map (fun output ->
                        digitMapping 
                        |> List.findIndex (fun digit -> digit = (output |> Seq.toList |> List.map (fun x -> string(x)) |> List.sort)))
                
                let stringNumber = digits |> List.fold (fun acc v -> acc + string(v)) ""

                int(stringNumber))
            |> List.sum

let puzzleInput = "./puzzle-input.txt" |> File.ReadAllLines |> Array.toList

let uniqueSegments = puzzleInput |> Puzzle08.Part1.getUniqueSegmentCount
printfn "Unique segments: %d" uniqueSegments

let outputSum = puzzleInput |> Puzzle08.Part2.getOutputSum
printfn "Output sum: %d" outputSum
