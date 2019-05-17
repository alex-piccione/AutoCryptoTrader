namespace AutoCryptoTrader.Core

type ITrader =    
    
    abstract member log : IEvent<string>
    abstract member start : unit -> unit
    abstract member buy: unit -> unit
    

