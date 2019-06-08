namespace AutoCryptoTrader.Core

open System.Threading
open Alex75.Cryptocurrencies
open System


type State = 
    | Idle
    | WaitBuyOrder 
    | WaitSellOrder



type FirstTrader(bitstampClient:Alex75.BitstampApiClient.IClient) as trader=

    let logEvent = new Event<string>() (* create the event *)    
    
    let mutable state = State.Idle
    
    let amount = 1000
    //let amount = 100    
    let mainCurrency = Currency.XRP
    let baseCurrency = Currency.EUR
    //let baseCurrency = Currency.USD

    let minProfitPercentage = 01.5m  // fee are 0.25% (for buy and sell become 0.50%)
    let mutable previousBid = 0m

    do        
        let timer = new Timer( fun s -> trader.readTicker() )
        timer.Change(0, 15000 ) |> ignore
        state <- State.Idle

                       

    member __.readTicker() =

        try
            let ticker = bitstampClient.GetTicker( mainCurrency, baseCurrency)
            let spread = ticker.Ask - ticker.Bid
            let spreadPerc = (spread / ticker.Bid)*100m

            // https://www.key-shortcut.com/en/writing-systems/35-symbols/arrows/

            let bidArrow = match (ticker.Bid > previousBid)  with 
                                | true -> char(11105)  // 11201
                                | _ -> char(11107)  // 11206
            logEvent.Trigger (sprintf"%O/%O  Bid:%f Ask:%f Spread:%.5f (%.3f %%) %c" mainCurrency baseCurrency ticker.Bid ticker.Ask spread spreadPerc bidArrow)

            if spreadPerc > minProfitPercentage then
                logEvent.Trigger(char(9989).ToString())

            previousBid <- ticker.Bid

        with e -> ()


    member trader.executeBuy(amount:float, mainCurrency:Currency, baseCurrency:Currency, buyPrice:float) =           

        let doBuy() = 
            let buyOrder = bitstampClient.CreateBuyLimitOrder(float(amount), mainCurrency, baseCurrency, float(buyPrice), false)
            if buyOrder.IsSuccess then
                logEvent.Trigger(sprintf "Buy order created. Id: %i Price:%f " buyOrder.Id buyOrder.Price)
            else 
                logEvent.Trigger(sprintf "ERROR. Buy order failed. %s" buyOrder.ErrorReason)     
            buyOrder

        retryer.execute doBuy 5


    member trader.executeSell(amount:float, mainCurrency:Currency, baseCurrency:Currency, sellPrice:float) =           

        let doSell() = 
            let sellOrder = bitstampClient.CreateSellLimitOrder(float(amount), mainCurrency, baseCurrency, float(sellPrice), false)
            if sellOrder.IsSuccess then
                logEvent.Trigger(sprintf "Sell order created. Id: %i Price:%f " sellOrder.Id sellOrder.Price)
            else 
                logEvent.Trigger(sprintf "ERROR. Sell order failed. %s" sellOrder.ErrorReason)     
            sellOrder

        retryer.execute doSell 5
        

    interface ITrader with
        member this.log = logEvent.Publish (* expose event handler *)
        
        member trader.start () =
            logEvent.Trigger "FirstTrader start"
            
            try         
                let balance = bitstampClient.GetBalance() 
                logEvent.Trigger (sprintf"XRP: %f USD: %f" balance.XRP balance.USD)
            with e -> logEvent.Trigger( "Failed to get balance. " + e.ToString())                           



        member trader.buy() =            
            logEvent.Trigger("Buy command")            

            // when price is increasing
            //let buyPrice = askPrice + 0.00001m;
            //let sellPrice = askPrice * (1 + minProfitPercentage / 100);

            let ticker = bitstampClient.GetTicker(mainCurrency, baseCurrency)

            let priceMargin = 0.0005m

            //let midPrice = ticker.Ask-((ticker.Ask-ticker.Bid)/2m)
            //let buyPrice = midPrice - priceMargin/2m
            //let sellPrice = midPrice + priceMargin/2m

            // todo: check the spreads, must be bigger than the fees.

            let buyPrice = ticker.Bid + 0.0001m
            //let sellPrice = ticker.Ask - 0.0001m  
            let sellPrice = buyPrice * (1m + minProfitPercentage / 100m);



            //let buyOrder = bitstampClient.CreateBuyLimitOrder(float(amount), mainCurrency, baseCurrency, float(buyPrice), false) 
            let buyOrder = trader.executeBuy(float(amount), mainCurrency, baseCurrency, float(buyPrice))
            
            if buyOrder.IsSuccess then
                logEvent.Trigger(sprintf "Buy order created. Id: %i Price:%f " buyOrder.Id buyOrder.Price )

                let sellOrder = trader.executeSell(float(amount), mainCurrency, baseCurrency, float(sellPrice))
                if sellOrder.IsSuccess then
                    logEvent.Trigger(sprintf "Sell order created. Id: %i Price:%f " sellOrder.Id sellOrder.Price )
                else
                    logEvent.Trigger(sprintf "ERROR. Sell order failed. %s" sellOrder.ErrorReason)
            else
                logEvent.Trigger(sprintf "ERROR. Buy order failed. %s" buyOrder.ErrorReason)
