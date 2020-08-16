module Entities

open System
open Alex75.Cryptocurrencies



type Exchange =
    | Bitstamp
    | Bitfinex

/// Represents the price of currency AAA on currency BBB (AAA/BBB)
type Price = { 
    MainCurrency:Currency
    BaseCurrency:Currency
    /// the value of MainCurrency / BaseCurrency
    Value: decimal
}


type OpenOrder = {
    Exchange:Exchange
    Date:DateTime
    BuyCurrency: Currency
    BuyAmount: decimal
    PayCurrency: Currency
    PayAmount: decimal
    Price: decimal
}