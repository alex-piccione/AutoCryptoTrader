module retryer



let rec execute work attempts = 
    
    match attempts with
    | 0 -> failwith "no more attempts"
    | _ -> 
        try
            work() //|> ignore
        with
        | _ -> 
            System.Threading.Thread.Sleep(250)
            execute work (attempts-1)
    
   