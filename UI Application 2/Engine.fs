module engine

open System.Threading
open System.Threading.Tasks
open System.Linq

open Alex75.Cryptocurrencies
open Alex75.BitstampApiClient
open Alex75.BinanceApiClient
open Alex75.BitfinexApiClient
open System.Collections.Generic



type PriceChange = {pair:CurrencyPair; price:decimal}

type BalanceUpdate = { XRP:int; EUR:int; USD:int; BTC:int }

type Engine(bitstampClient: Alex75.BitstampApiClient.IClient, 
            binanceClient: Alex75.BinanceApiClient.IClient,
            bitfinexClient: Alex75.BitfinexApiClient.IClient
            ) =
            
    let error = new Event<string>()
    let bitstampTickerChanged  = new Event<Ticker>()   
    let bitstampBalanceChanged  = new Event<BalanceUpdate>()   
    let binanceTickerChanged  = new Event<Ticker>()   
    let bitfinexTickerChanged  = new Event<Ticker>()   
    let bitstampOrdersChanged = new Event<IEnumerable<OpenOrder>>()
    

    let updatePrices() =  
    
        // Bitstamp
        try  
            let tickers = bitstampClient.GetTickers([|
                CurrencyPair.XRP_USD
                CurrencyPair.XRP_EUR
                CurrencyPair.XRP_BTC
                |]) 

            Parallel.ForEach(tickers.Values, fun ticker -> bitstampTickerChanged.Trigger(ticker) ) |> ignore 

        with e -> error.Trigger(sprintf "Failed to update Bitstamp prices. %s" e.Message)

        // Binance
        try
            let response = binanceClient.GetTicker(CurrencyPair.XRP_BTC)
            if response.IsSuccess then binanceTickerChanged.Trigger(response.Ticker.Value) 

            match binanceClient.GetTicker(CurrencyPair("XRP", "ETH")) with
            | r when r.IsSuccess -> binanceTickerChanged.Trigger(r.Ticker.Value) 
            | _ -> ()

        with e -> error.Trigger(sprintf "Failed to update Binance prices. %s" e.Message)     

        // Bitfinex
        try
            let bitfinexPairs = [|
                CurrencyPair.XRP_USD
                //CurrencyPair.XRP_EUR            
                CurrencyPair.XRP_BTC
                CurrencyPair.XRP_ETH
                |]

            let bitfinexGetTicker pair = 
                let response = bitfinexClient.GetTicker(pair) 
                if response.IsSuccess then bitfinexTickerChanged.Trigger response.Ticker.Value
                else error.Trigger response.Error

            Parallel.ForEach (bitfinexPairs, bitfinexGetTicker) |> ignore
        
        with e -> error.Trigger(sprintf "Failed to update Bitfinex prices. %s" e.Message)    


    let updateBalance() =  
    
    // why client does not manage 403 ???
        try
            let response = bitstampClient.GetBalance()
            if response.IsSuccess then
                bitstampBalanceChanged.Trigger( {
                    XRP=int(response.XRP)
                    EUR=int(response.EUR)
                    USD=int(response.USD)
                    BTC=int(response.BTC)
                    })
        with e -> error.Trigger(sprintf "Failed to update Bitstamp balance. %s" e.Message)  


    
    let updateOpenOrders() =

        // Bitstamp
        try
            let response = bitstampClient.ListOpenOrders(1, 100, SortDirection.Desc)
            if response.IsSuccess then
                bitstampOrdersChanged.Trigger(response.Orders) 
            else failwith response.ErrorReason
        with e -> error.Trigger(sprintf "Failed to load Bitstamp Open Orders. %s" e.Message)


    
    member __.startUpdatingUI () =
        let timer = new Timer( fun _ -> updatePrices() )
        timer.Change(0, 5000 ) |> ignore

        let timer_Balance = new Timer( fun _ -> updateBalance() )
        timer_Balance.Change(2000, 30*1000) |> ignore

        let timer_orders = new Timer( fun _ -> updateOpenOrders() )
        timer_orders.Change(3000, 30*1000) |> ignore
                       
        
    member __.Error = error.Publish
    member __.BitstampTickerChanged = bitstampTickerChanged.Publish
    member __.BitstampBalanceChanged = bitstampBalanceChanged.Publish
    member __.BinanceTickerChanged = binanceTickerChanged.Publish
    member __.BitfinexTickerChanged =  bitfinexTickerChanged.Publish
    member __.BitstampOrdersChanged = bitstampOrdersChanged.Publish