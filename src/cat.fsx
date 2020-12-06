// Cat
open readNWrite 

try
    printfn "%s" "Skriv ind nogen .txt filer med mellemrom:"
    let FileNames = System.Console.ReadLine().Split(" ")
    printfn "%A" (cat (List.ofArray(FileNames)))
    0
with
    | _ -> 1