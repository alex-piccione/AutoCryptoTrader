module AutoCryptoTrader.DesktopApplication.Program

open System
open System.Windows.Forms

open engine
open Alex75.BitstampApiClient
open Alex75.BinanceApiClient
open AutoCryptoTrader.DesktopApplication.Forms


[<EntryPoint>]
[<STAThread>]
let main argv =

    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault false

    
    let configuration = configuration.Configuration()
    let tickerCache = TimeSpan.Parse(configuration.Exchanges.``ticker cache``)

    let bitstampConfig = { 
        TickerCacheDuration=tickerCache
        CustomerId=""
        PublicKey=""
        SecretKey=""
        }
    let bitstampClient = new Alex75.BitstampApiClient.Client(bitstampConfig) :> Alex75.BitstampApiClient.IClient

    let binanceSettings = {
        TickerCacheDuration=tickerCache
        PublicKey=""
        SecretKey=""}
    let binanceClient = new Alex75.BinanceApiClient.Client(binanceSettings) :> Alex75.BinanceApiClient.IClient

    let bitfinexConfiguration = Alex75.BitfinexApiClient.Configuration("", "")
    let bitfinexClient = new Alex75.BitfinexApiClient.Client( bitfinexConfiguration)

    let engine = Engine(bitstampClient, binanceClient, bitfinexClient)


    use form = new MainForm(configuration, engine, bitstampClient)
    // to avoid the error "Invoke or BeginInvoke cannot be called on a control until the window handle has been created"
    // when try to update labels
    form.HandleCreated.Add(fun _ -> engine.startUpdatingUI() )
         

    Application.Run(form)

    0 // return an integer exit code
