
open System.IO

module Puzzle10 =
    type CorruptionResult = {
        isCorrupted: bool
        expected: char
        found: char
        remainingStack: char list
    }

    module Part1 =
        let getCorrespondingClosingSymbol(symbol: char): char =
            match symbol with
            | '(' -> ')'
            | '[' -> ']'
            | '{' -> '}'
            | '<' -> '>'
            | _ -> 'x'

        let rec findCorruption(line: list<char>, stack: list<char>): CorruptionResult =
            match line with
            | head::tail -> 
                let closingSymbol = head |> getCorrespondingClosingSymbol
                if closingSymbol = 'x'
                then
                    match stack with
                    | stackHead::stackTail -> 
                        if stackHead = head 
                        then findCorruption(tail, stackTail) 
                        else { isCorrupted = true; expected = stackHead; found = head; remainingStack = []}
                    | _ -> { isCorrupted = false; expected = ' '; found = ' '; remainingStack = []}
                else
                    findCorruption(tail, closingSymbol::stack)
            | [] -> { isCorrupted = false; expected = ' '; found = ' '; remainingStack = stack}

        let getIllegalSymbolPoints(symbol: char) =
            match symbol with
            | ')' -> 3
            | ']' -> 57
            | '}' -> 1197
            | '>' -> 25137
            | _ -> 0

        let getCorruptionScore(lines: string[]) =
            lines
            |> Array.map (fun line -> findCorruption(line |> Seq.toList, []))
            |> Array.filter (fun result -> result.isCorrupted)
            |> Array.map (fun result -> result.found |> getIllegalSymbolPoints)
            |> Array.sum


    module Part2 = 
        let getIncompleteSymbolScore(symbol: char) =
            match symbol with
            | ')' -> 1
            | ']' -> 2
            | '}' -> 3
            | '>' -> 4
            | _ -> 0
        
        let getIncompleteScore(lines: string[]) =
            let sortedScores = 
                lines
                |> Array.map (fun line -> Part1.findCorruption(line |> Seq.toList, []))
                |> Array.filter (fun result -> not result.isCorrupted)
                |> Array.map (fun result -> result.remainingStack |> List.map (fun stackElem -> stackElem |> getIncompleteSymbolScore))
                |> Array.map (fun scores -> scores |> List.fold (fun acc score -> acc * 5I + bigint(score)) 0I)
                |> Array.sort

            sortedScores[sortedScores.Length / 2]

let input = "./puzzle-input.txt" |> File.ReadAllLines

let corruptedScore = input |> Puzzle10.Part1.getCorruptionScore
printfn "Syntax error score: %d" corruptedScore

let incompleteScore = input |> Puzzle10.Part2.getIncompleteScore
printfn "Incomplete score: %A" incompleteScore

