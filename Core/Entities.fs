module Entities


type Currency = {
    Symbol:string
    Name:string
}

/// Represents the price of currency AAA on currency BBB (AAA/BBB)
type Price = { 
    // the AAA of AAA/BBB
    MainCurrency:Currency
    // the BBB in AAA/BBB
    BaseCurrency:Currency

    /// the value of MainCurrency / BaseCurrency
    Value: decimal
}