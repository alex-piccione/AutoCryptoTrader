﻿open System
open System.IO
open System.Reflection
open Trading
open System.Threading



[<EntryPoint>]
let main argv =
    let version = 0.1

    printfn "CryptoAutotrader v %f" version

    let logFile = sprintf @"logs/Autotrader (%s).log" (DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"))
    Logging.configLogger(Path.Combine( __SOURCE_DIRECTORY__, logFile))

    let logger = log4net.LogManager.GetLogger( Assembly.GetExecutingAssembly(), "root")
    logger.Info (sprintf "Start CryptoAutotrader v %f" version)

    // do the job here

    logger.Info "End"
    let tradingSessionLength = TimeSpan.FromMinutes(5.0)

    let sessionEnd = DateTime.Now + tradingSessionLength

    let trader = DummyTrader( {InitialAmount=100m; StopLoss=25m; SessionLength=tradingSessionLength}) :> ITrader
    trader.StartTrading()
    while trader.CanTrade() && DateTime.Now < sessionEnd do
        Thread.Sleep(10*1000) // check every 10 seconds

    if DateTime.Now > sessionEnd then 
        trader.StopTrading() 
        Thread.Sleep(5*1000)

    while trader.IsInvesting do 
        Thread.Sleep(2*1000)

    let report = trader.CreateSessionReport
    printfn "Session Report for Trader %s n\%s" trader.Name (report.ToString())
           
    System.Threading.Thread.Sleep(10*1000) // wait 10 seconds for logging finish writing
  

    logger.Logger.Repository.Shutdown()   // flush the log

    0
