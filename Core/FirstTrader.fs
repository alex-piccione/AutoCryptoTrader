namespace AutoCryptoTrader.Core

type FirstTrader(bitstampClient:Alex75.BitstampApiClient.IClient) =

    let logEvent = new Event<string>() (* create the event *)
                             


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


