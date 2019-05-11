namespace AutoCryptoTrader.Core

open System.Threading
open Alex75.Cryptocurrencies




type FirstTrader(bitstampClient:Alex75.BitstampApiClient.IClient) as trader=

    let logEvent = new Event<string>() (* create the event *)           


    do        
        let timer = new Timer( fun s -> trader.readTicker() )
        timer.Change(0, 15000 ) |> ignore

    member __.readTicker() =

        let mainCurrency = Currency.XRP
        let baseCurrency = Currency.USD

        let ticker = bitstampClient.GetTicker( mainCurrency, baseCurrency)
        logEvent.Trigger (sprintf"%O/%O  Bid:%f Ask:%f" mainCurrency baseCurrency ticker.Bid ticker.Ask)


    interface ITrader with
        member this.log = logEvent.Publish (* expose event handler *)
        
        member trader.start () =
            logEvent.Trigger "FirstTrader start"
            
            try         
                let balance = bitstampClient.GetBalance() 
                logEvent.Trigger (sprintf"XRP: %f USD: %f" balance.XRP balance.USD)
            with
                | _Exception as ex -> logEvent.Trigger(ex.ToString())                           

            ()


