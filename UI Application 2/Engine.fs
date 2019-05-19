module engine

open System.Threading
open System.Threading.Tasks
open System.Linq

open Alex75.BitstampApiClient
open Alex75.Cryptocurrencies


type PriceChange = {pair:CurrencyPair; price:decimal}

type BalanceUpdate = { XRP:int; EUR:int; USD:int; BTC:int }

type Engine(bitstampClient: IClient) =

    let bitstampTickerChanged  = new Event<Ticker>()   
    let bitstampBalanceUpdated  = new Event<BalanceUpdate>()   

    let updatePrices() =           
        let tickers = bitstampClient.GetTickers([|
            CurrencyPair.XRP_USD
            CurrencyPair.XRP_EUR
            CurrencyPair.XRP_BTC
            |]) 

        //tickers.Values.First().

        Parallel.ForEach(tickers.Values, fun ticker -> bitstampTickerChanged.Trigger(ticker) ) |> ignore 

    let updateBalance() =           
        let response = bitstampClient.GetBalance()
        if response.IsSuccess then
            bitstampBalanceUpdated.Trigger( {
                XRP=int(response.XRP)
                EUR=int(response.EUR)
                USD=int(response.USD)
                BTC=int(response.BTC)
                })
    
    member __.startUpdatingUI () =
        let timer = new Timer( fun _ -> updatePrices() )
        timer.Change(0, 5000 ) |> ignore

        let timer_Balance = new Timer( fun _ -> updateBalance())
        timer_Balance.Change(2000, 60*1000) |> ignore
        
        
        
    member __.BitstampTickerChanged = bitstampTickerChanged.Publish
    member __.BitstampBalanceUpdated = bitstampBalanceUpdated.Publish