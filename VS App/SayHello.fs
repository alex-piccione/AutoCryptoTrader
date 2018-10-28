module Learning

let getName = printfn "What is your name? \n" |> System.Console.ReadLine

//let sayHello name = printfn "Hello %s!" name 
let sayHello name =
    match name with
    | "" -> printfn "You don't have a name?"
    | _ -> printfn "Hello %s!" name 

let close = printfn "\nPress any key to close..." |> System.Console.Read |> ignore



getName
|> sayHello

close



