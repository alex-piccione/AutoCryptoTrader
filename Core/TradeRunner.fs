module AutoCryptoTrader.TradeRunner

open Configuration
open System.Threading
open Trading

type TradeStep = {
    state:string
    buyPrice: decimal
}


let tradingOrder = 0
//let tradingConfiguration = {InitialAmount: 100m; StopLoss: 10m }
//let trader = Trading.DummyTrader(tradingConfiguration)

let tradeStep state = 

    // buy from the market
    //trader.

    // place a sell order 
    ()


let Run (trading:Trading, configuration:Configuration) =

    let period = (int)configuration.Trading.PriceCheckFrequency.TotalMilliseconds
    let timer = new System.Threading.Timer(tradeStep, null, 0, period)
    ()
