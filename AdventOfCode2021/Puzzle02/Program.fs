open System.IO

module Puzzle02 =

    let parseInput(lines: list<string>) = 
        lines
        |> List.map(fun (line) -> line.Split(" ") |> Array.toList)
        |> List.map(fun ([direction; speed]) -> (direction, int(speed)))
    
    module Part1 =
        let calculatePosition(commands: list<(string * int)>) = 
            commands
            |> List.map (fun ((direction, speed)) -> 
                match direction with
                | "forward" -> (speed, 0)
                | "up" -> (0, -speed)
                | "down" -> (0, speed)
                | _ -> (0, 0))
            |> List.fold (fun (x, y) (horizontalSpeed, verticalSpeed) -> (x + horizontalSpeed, y + verticalSpeed)) (0, 0)

        let evaluatePosition(lines: list<string>) =
            let (x, y) = 
                lines
                |> parseInput
                |> calculatePosition

            x * y

    module Part2 =
        let rec handleCommand(aim: int, position: (int * int), commands: list<(string * int)>) =
            let (x, y) = position

            match commands with
            | command :: tail -> 
                match command with
                | ("forward", speed) -> handleCommand(aim, (x + speed, y + speed * aim), tail)
                | ("up", speed) -> handleCommand(aim - speed, position, tail)
                | ("down", speed) -> handleCommand(aim + speed, position, tail)
                | _ -> handleCommand(aim, position, tail)
            | [] -> position

        let calculatePosition(commands: list<(string * int)>) =
            handleCommand(0, (0, 0), commands)

        let evaluatePosition(lines: list<string>) = 
            let (x, y) =
                lines
                |> parseInput
                |> calculatePosition

            x * y
        
let puzzleInput = File.ReadAllLines("./puzzle-input.txt") |> Array.toList

let result = puzzleInput |> Puzzle02.Part1.evaluatePosition
printfn "Position mulitple: %d" result

let correctedResult = puzzleInput |> Puzzle02.Part2.evaluatePosition
printfn "Position multiple: %d" correctedResult