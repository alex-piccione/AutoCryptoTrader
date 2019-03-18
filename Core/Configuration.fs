//module AutoCryptoTrader.Configuration
module Configuration

open System

//module global


type MongoDB = {
    ConnectionString:string
    }

type Binance = {
    ApiPublicKey:string
    ApiSecretKey:string
}


type Trading = {
    InitialAmount: decimal
    SellPricePercentageTreshold: decimal
    PriceCheckFrequency: TimeSpan
    BuyAmountUsd: decimal
}

type Configuration(mongoDB:MongoDB, binance:Binance, trading:Trading) =
    //member self.MongoDBConnectionString 

    member x.MongoDB = mongoDB
    member x.Binance = binance
    member x.Trading = trading


