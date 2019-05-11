namespace AutoCryptoTrader.Core



type FirstTrader() =

    let logEvent = new Event<string>() (* create the event *)

    interface ITrader with
        member this.log = logEvent.Publish (* expose event handler *)
        member trader.start () = logEvent.Trigger "FirstTrader start"

