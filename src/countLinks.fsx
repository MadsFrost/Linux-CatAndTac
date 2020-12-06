open System.IO

let urlStream (url:string) : Stream option = 
    try
        let uri = System.Uri url
        let request = System.Net.WebRequest.Create uri
        let response = request.GetResponse ()
        Some (response.GetResponseStream())
    with
    | _ -> None
        

let urlHtml (url:string) : string option =
    let stream = urlStream url
    match stream with 
        | Some string -> 
            let reader = new System.IO.StreamReader(string)
            Some (reader.ReadToEnd())
        | None -> None

let countLinks (url:string) : int =
       
    let HTML = (urlHtml url)
    match HTML with
    | None -> 0
    | Some data -> 
        let split = data.Split("</a>")
        split.Length - 1
        
let url = "http://example.com/"

printfn "%A" (countLinks(url))




