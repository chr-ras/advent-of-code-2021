
open System.IO

module Puzzle06 =
    let parseInput(raw: string) =
        raw.Split(",")
        |> Array.map (fun v -> int(v)) 
        |> Array.toList

    module Part1 = 
        let rec simulateSchool(school: list<int>, day: int, until: int) =
            if day = until
            then 
                school
            else
                let reproducingFishCount = (school |> List.filter (fun f -> f = 0)).Length
                let newSchool = (school |> List.map (fun f -> if f = 0 then 6 else f - 1)) @ ([0..reproducingFishCount-1] |> List.map (fun _ -> 8))

                simulateSchool(newSchool, day + 1, until)

        let getSchoolSizeAfter(days: int, school: list<int>) =
            let school = simulateSchool(school, 0, days)
            school.Length

    module Part2 =
        let sortByCycle(school: list<int>) =
            [for _ in 0..8 -> 0]
            |> List.mapi (fun cycle _ -> bigint((school |> List.filter (fun f -> f = cycle)).Length))

        let simulateDay(school: list<bigint>) =
            match school with
            | head::tail -> (tail |> List.mapi (fun i current -> if i = 6 then current + head else current)) @ [head]
            | _ -> []

        let getSchoolSizeAfter(days: int, school: list<int>) =
            let schoolByCycle = school |> sortByCycle
            
            [1..days] |> List.fold (fun acc _ -> acc |> simulateDay) schoolByCycle |> List.sum

let puzzleInput = File.ReadAllText("./puzzle-input.txt") |> Puzzle06.parseInput

let schoolSizeAfter80Days = Puzzle06.Part1.getSchoolSizeAfter(80, puzzleInput)
printfn "School size after 80 days: %d" schoolSizeAfter80Days

let schoolSizeAfter256Days = Puzzle06.Part2.getSchoolSizeAfter(256, puzzleInput)
printfn "School size after 256 days: %A" schoolSizeAfter256Days
