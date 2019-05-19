module AutoCryptoTrader.DesktopApplication.Program

open System
open System.Windows.Forms

open Alex75.BitstampApiClient
open AutoCryptoTrader.DesktopApplication.Forms
open engine


[<EntryPoint>]
[<STAThread>]
let main argv =

    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault false

    
    let configuration = settings.Settings()

    let bitstampConfig = { 
        TickerCacheDuration=TimeSpan.FromSeconds(float(configuration.Bitstamp.``ticker cache``));
        CustomerId=configuration.Bitstamp.``account number``
        PublicKey=configuration.Bitstamp.``public key``;
        SecretKey=configuration.Bitstamp.``secret key``;
        }
    let bitstampClient = new Alex75.BitstampApiClient.Client(bitstampConfig) :> IClient

    let engine = Engine(bitstampClient)


    use form = new MainForm(configuration, engine)
    // to avoid the error "Invoke or BeginInvoke cannot be called on a control until the window handle has been created"
    // when try to update labels
    form.HandleCreated.Add(fun _ -> engine.startUpdatingUI() )
         

    Application.Run(form)

    0 // return an integer exit code
