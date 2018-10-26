// Learn more about F# at http://fsharp.org


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    Logging.configLogger()

    let logger = log4net.LogManager.GetLogger( System.Reflection.Assembly.GetExecutingAssembly(), "root")
    logger.Info("Start")

    System.Threading.Thread.Sleep(10*1000) // wait 10 seconds for logging finish writing

    0 // return an integer exit code
