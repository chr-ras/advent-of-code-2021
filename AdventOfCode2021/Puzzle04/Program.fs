
open System.IO
open System

module Puzzle04 = 
    type Board = list<(bool * int)>

    let parseInput(text: string) = 
        let blocks = text.Split(Environment.NewLine + Environment.NewLine) |> Array.toList
        let rawRandomNumbers = blocks.Head
        let rawBlocks = blocks.Tail

        let randomNumbers = rawRandomNumbers.Split(",") |> Array.toList |> List.map (fun num -> int(num))

        let blocks =
            rawBlocks
            |> List.map (fun rawBlock -> 
                rawBlock.Split(Environment.NewLine) 
                |> Array.toList 
                |> List.map (fun line -> 
                    line.Split(" ") 
                    |> Array.toList
                    |> List.filter (fun elem -> elem.Length > 0))
                |> List.reduce (fun acc row -> acc @ row)
                |> List.map (fun num -> (false, int(num))))

        randomNumbers, blocks

    let checkBoard(randomNumber: int, board: Board): Board = 
        board |> List.map (fun (mark, entry) -> if entry = randomNumber then (true, entry) else (mark, entry))

    let hasWon(board: Board) =
        let rows = 
            [0; 5; 10; 15; 20] 
            |> List.map (fun startIndex -> board |> List.skip startIndex |> List.take 5)

        let columns = 
            [0; 1; 2; 3; 4]
            |> List.map (fun colIndex -> [0; 1; 2; 3; 4] |> List.map (fun value -> board.Item(colIndex + value * 5)))

        let allElementsMarked(values: list<(bool * int)>) = 
            let markedValues = values |> List.filter (fun (marked, _) -> marked)
            markedValues.Length  = 5

        let rowsWon = rows |> List.map(allElementsMarked) |> List.reduce (fun acc marked -> acc || marked)
        let columnsWon = columns |> List.map allElementsMarked |> List.reduce (fun acc marked -> acc || marked)

        rowsWon || columnsWon

    let calculateScore(winningNumber: int, board: Board) = 
        (board |> List.filter (fun (marked, _) -> not marked) |> List.sumBy (fun (_, value) -> value)) * winningNumber

    let findWinners(boards: list<Board>) =
        boards |> List.filter hasWon
    
    module Part1 =
        let findWinner(boards: list<Board>) =
            let winningBoards = boards |> findWinners

            match winningBoards with
            | board::_ -> (true, board)
            | _ -> (false, [])

        let rec checkBoards(randomNumbers: list<int>, boards: list<Board>) =
            let (currentNumber, remainingNumbers) = 
                match randomNumbers with
                | head::tail -> (head, tail)
                | [] -> (0, [])

            let updatedBoards = boards |> List.map (fun board -> checkBoard(currentNumber, board))

            match updatedBoards |> findWinner with
            | (true, winningBoard) -> (winningBoard, currentNumber)
            | (false, _) -> checkBoards(remainingNumbers, updatedBoards)

        let getWinningBoardScore(input: string) = 
            let (winningBoard, winningNumber) = 
                input 
                |> parseInput
                |> checkBoards

            calculateScore(winningNumber, winningBoard)

    module Part2 =
        let rec findLastWinningBoard(randomNumbers: list<int>, boards: list<Board>) =
            let (currentNumber, remainingNumbers) = 
                match randomNumbers with
                | head::tail -> (head, tail)
                | [] -> (0, [])

            let updatedBoards = boards |> List.map (fun board -> checkBoard(currentNumber, board))

            let boardsWithoutWinners = updatedBoards |> List.except(updatedBoards |> findWinners)

            match boardsWithoutWinners with
            | [] -> 
                match updatedBoards with
                | lastBoard::[] -> (lastBoard, currentNumber)
                | _ -> ([], currentNumber)
            | remainingBoards -> findLastWinningBoard(remainingNumbers, remainingBoards)

        let getLastWinningBoardScore(input: string) =
            let (lastWinningBoard, winningNumber) = 
                input 
                |> parseInput
                |> findLastWinningBoard

            calculateScore(winningNumber, lastWinningBoard)
    

let puzzleInput = File.ReadAllText("./puzzle-input.txt")

let winningScore = puzzleInput |> Puzzle04.Part1.getWinningBoardScore
printfn "Winning board score: %d" winningScore

let lastWinningScore = puzzleInput |> Puzzle04.Part2.getLastWinningBoardScore
printfn "Last winning board score: %d" lastWinningScore
        