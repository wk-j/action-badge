open System

type Options = {
    Organization: string
    Project: string
    Action: string
}

let rec parseOptions options argv =
    match argv with
    | "--project" :: xs ->
        match xs with
        | value :: xss ->
            parseOptions { options with Project = value } xss
        | _ ->
            parseOptions options xs
    | "--organization" :: xs ->
        match xs with
        | value :: xss ->
            parseOptions { options with Organization = value } xss
        | _ ->
            parseOptions options xs
    | "--action" :: xs ->
        match xs with
        | value :: xss ->
            parseOptions { options with Action = value } xss
        | _ ->
            parseOptions options xs
    | _ ->
        options

[<EntryPoint>]
let main argv =
    let options =
        parseOptions
            { Organization = "wk-j"; Project= ""; Action = "" }
            (argv |> List.ofArray)

    let md =
        """
        [![Actions](https://github.com/{organization}/{project}/workflows/{action}/badge.svg)](https://github.com/{organization}/{project}/actions)
        """
            .Replace("{project}", options.Project)
            .Replace("{organization}", options.Organization)
            .Replace("{action}", options.Action)
            .Trim()

    printfn "%s" md
    0
