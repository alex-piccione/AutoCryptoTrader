open System
open System.IO
open System.Reflection



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


    System.Threading.Thread.Sleep(10*1000) // wait 10 seconds for logging finish writing

    #if INTERACTIVE  // when is it considered INTERAVCTIVE ?
    System.Threading.Thread.Sleep(10*1000) // wait 10 seconds for logging finish writing
    #endif    

    logger.Logger.Repository.Shutdown()   // flush the log

    0
