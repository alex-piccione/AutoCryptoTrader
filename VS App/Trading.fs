module Trading

open System.Diagnostics
open System


type TradeSessionReport = {
    NumberOfOperations:int
    ProfitOrLoss:decimal
    }
    with
        override this.ToString() = sprintf "Operations: %i, Profit or Loss: %f" this.NumberOfOperations this.ProfitOrLoss
    


// interface
type ITrader = 
    
    abstract member Name:string with get
    abstract member InitialBudget: decimal
    
    abstract StartTrading: unit -> unit
    abstract StopTrading: unit -> unit
    abstract CanTrade: unit -> bool
    // indicates if there is actually a purchase of the cryptocurrency or it sold everything (waiting to buy)
    abstract IsInvesting:bool
    abstract CurrentBudget:decimal

    abstract CreateSessionReport:unit ->TradeSessionReport
        
type TraderConfiguration = {
    InitialAmount:decimal
    StopLoss: decimal  // max loose; when reached, stops trading
    SessionLength:TimeSpan
}

// dummy trader
type DummyTrader(configuration) = 


    let StartTrading = printfn "start trading"
    let StopTrading = printfn "stop trading"
    let CanTrade = true
    let IsInvesting = false
    let CurrentBudget = 0m

    let CreateSessionReport = { NumberOfOperations=0; ProfitOrLoss=0m}
    
    interface ITrader with
        
        member __.Name = "Dummy"
        member __.InitialBudget = configuration.InitialAmount
        member __.StartTrading() = StartTrading
        member __.StopTrading() = StopTrading
        member __.CanTrade() = CanTrade
        member __.IsInvesting = IsInvesting
        member __.CurrentBudget = CurrentBudget
        member __.CreateSessionReport() = CreateSessionReport
