module readNWrite

open System.IO

let readFile (path: string) : string option =
            let file = File.ReadAllText(path)
            
            if (File.Exists(path)) then
                Some file
            else
                None

let cat (filenames: string list) : string option = 
    let booleans = List.map (fun path -> File.Exists(path)) filenames
    if (List.exists (fun b -> b = false) booleans) then
        None
    else 
        let rec concatter (l: string list) : string = 
            match l with 
            | x :: xs -> x + concatter(xs)
            | _ -> ""
        
        let stringOptions : string option list = List.map (fun path -> readFile(path)) filenames
        let strings : string list = List.map (fun s -> match s with
                                                        | None -> ""
                                                        | Some s -> s) stringOptions 
        
        Some (concatter(strings))

let tac (filenames: string list) : string option = 
    let booleans = List.map (fun path -> File.Exists(path)) filenames
    if (List.exists (fun b -> b = false) booleans) then
        None
    else 
        let rec concatter (l: string list) : string = 
            match l with 
            | x :: xs -> x + concatter(xs)
            | _ -> ""

        let FileToStringList (path: string) : string list =
            let file = File.ReadAllText(path)
            
            let stringList = List.ofArray(file.Split("\\n"))
            stringList
            

        let ReverseFiles = 
            List.map (fun path -> FileToStringList(path)) (List.rev filenames)

        let ReverseContent = 
            List.map (fun s -> (List.rev s)) ReverseFiles

        let rec StringReverser (s: string list) : string list =
            List.map (fun (i : string) -> i  |> Seq.rev |> System.String.Concat) s

        let ReverseStrings =
            List.map (fun s -> (StringReverser s)) ReverseContent

        let concatInnerStrings =
            List.map (fun strings -> (concatter strings)) ReverseStrings

        let concatOuterStrings =
            concatter(concatInnerStrings)

        Some (concatOuterStrings)
        

