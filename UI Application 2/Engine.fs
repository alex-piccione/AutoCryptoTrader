module engine

open System.Threading
open System.Threading.Tasks
open System.Linq

open Alex75.BitstampApiClient
open Alex75.Cryptocurrencies


type PriceChange = {pair:CurrencyPair; price:decimal}

type Engine(bitstampClient: IClient) =

    let bitstampTickerChanged  = new Event<Ticker>()   

    let updatePrices() =           
        let tickers = bitstampClient.GetTickers([|
            CurrencyPair.XRP_USD
            CurrencyPair.XRP_EUR
            CurrencyPair.XRP_BTC
            |]) 

        Parallel.ForEach(tickers.Values, fun ticker -> bitstampTickerChanged.Trigger(ticker) ) |> ignore 

    
    member __.startUpdatingUI () =
        let timer = new Timer( fun _ -> updatePrices() )
        timer.Change(0, 5000 ) |> ignore
        
        
        
    member __.BitstampTickerChanged = bitstampTickerChanged.Publish