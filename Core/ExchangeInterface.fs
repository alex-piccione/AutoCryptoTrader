module Exchange

open System
open Entities 


type MarketLimitOrder = {
    Amount:decimal
    PriceLimit:Price
}

type Transaction = {
    When:DateTime
}

// example from
// interface
type IExchangeInterface = {
    GetBuyPrice: unit -> Price
    CreateBuyOrder: MarketLimitOrder -> unit
    CreateSellOrder : MarketLimitOrder -> unit
    GetTransactions: DateTime -> DateTime -> seq<Transaction>
}


