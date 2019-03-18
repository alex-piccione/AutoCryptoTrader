module lists =

    let triple numbers = List.map (fun x -> x * 3) numbers
    let odds numbers = List.filter (fun x -> (x % 2) = 0) numbers


    let numbers = [1;2;3]

    let triple_and_odds = triple >> odds

   
    //printf "numbers: %A" (triple_and_odds numbers)

    numbers
    |> triple_and_odds
    |> printf "triple and odds: %A"

    printf "\npress any key to close..."
    
    //System.Console.Read() |> ignore 


