module engine

open System.Threading
open Alex75.BitstampApiClient
open Alex75.Cryptocurrencies


type PriceChange = {pair:CurrencyPair; price:decimal}

type Engine(bitstampClient: IClient) =

    let bitstampPriceChanged  = new Event<PriceChange>()
    
    
    let pair = new CurrencyPair("xrp", "eur")

    let updatePrices() = 
        try
            let bitstampTicker = bitstampClient.GetTicker(pair.Main, pair.Other)
            bitstampPriceChanged.Trigger({pair=pair; price=bitstampTicker.Ask})
        
        with e -> ()
        

    
    do

        let timer = new Timer( fun _ -> updatePrices() )
        timer.Change(0, 5000 ) |> ignore
        //state <- State.Idle


    member __.BitstampPriceChanged = bitstampPriceChanged.Publish