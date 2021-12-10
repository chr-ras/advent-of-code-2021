module Tests

open Program
open Xunit
open System.IO

let testInput = File.ReadAllLines("./test-input.txt")

[<Theory>]
[<InlineData("{([(<{}[<>[]}>{[]{[(<()>", true, ']', '}', "")>]
[<InlineData("[[<[([]))<([[{}[[()]]]", true, ']', ')', "")>]
[<InlineData("[{[{({}]{}}([{[{{{}}([]", true, ')', ']', "")>]
[<InlineData("[<(<(<(<{}))><([]([]()", true, '>', ')', "")>]
[<InlineData("<{([([[(<>()){}]>(<<{{", true, ']', '>', "")>]

[<InlineData("[({(<(())[]>[[{[]{<()<>>", false, ' ', ' ', "}}]])})]")>]
[<InlineData("[(()[<>])]({[<{<<[]>>(", false, ' ', ' ', ")}>]})")>]
[<InlineData("(((({<>}<{<{<>}{[]{[]{}", false, ' ', ' ', "}}>}>))))")>]
[<InlineData("{<[[]]>}<{[{[{[]{()[[[]", false, ' ', ' ', "]]}}]}]}>")>]
[<InlineData("<{([{{}}[<[[[<>{}]]]>[]]", false, ' ', ' ', "])}>")>]
let ``Part1.findCorruption`` (input: string, isCorrupted: bool, expected: char, found: char, remainingStack: string) =
    let actual = Puzzle10.Part1.findCorruption(input |> Seq.toList, [])
    let expected: Puzzle10.CorruptionResult = { isCorrupted = isCorrupted; expected = expected; found = found; remainingStack = remainingStack |> Seq.toList }

    Assert.StrictEqual(expected, actual)

[<Fact>]
let ``Part1.getCorruptionScore`` () =
    let actual = testInput |> Puzzle10.Part1.getCorruptionScore

    Assert.Equal(26397, actual)

[<Fact>]
let ``Part2.getIncompleteScore`` () =
    let actual = testInput |> Puzzle10.Part2.getIncompleteScore

    Assert.Equal(288957I, actual)