namespace AutoCryptoTrader.DesktopApplication.Forms

open System
open System.Windows.Forms
open System.Drawing

open Alex75.BitstampApiClient
open AutoCryptoTrader.Core
open Panels.logPanel
open engine
open Panels.bitstampPanel
open System.Drawing.Text


// ref: https://www.codeproject.com/Articles/30414/Getting-Started-in-F-A-Windows-Forms-Application
// https://blogs.msdn.microsoft.com/mcsuksoldev/2011/05/27/f-windows-application-template-for-winforms/
// https://github.com/Acadian-Ambulance/vinyl-ui

type MainForm (config:settings.Settings, engine:Engine) as this =
    inherit Form()

    let mainPanel = new Panel()
    let logPanel = new LogPanel()
    let toolBar = new FlowLayoutPanel()
    let buyButton = new Button()
    let bitstampPanel = new BitstampPanel(engine)

    let initializeTrader() = 

        let bitstampConfig = { 
            TickerCacheDuration=TimeSpan.FromSeconds(float(config.Bitstamp.``ticker cache``));
            CustomerId=config.Bitstamp.``account number``
            PublicKey=config.Bitstamp.``public key``;
            SecretKey=config.Bitstamp.``secret key``;
            }
        let bitstampClient = new Alex75.BitstampApiClient.Client(bitstampConfig) :> IClient
        
        
        let trader = new FirstTrader(bitstampClient) :> ITrader
        trader.log.Add (fun log -> logPanel.AddText log )        
        trader

    let setFont () = 

        let fonts = new PrivateFontCollection()
        fonts.AddFontFile("fonts/consola.ttf")
        this.Font <- new Font(fonts.Families.[0], float32(10.), Drawing.FontStyle.Regular)    
        ()

    do 
        this.SuspendLayout();
                     
        this.Text <- "Auto Crypto Trader"
        this.MinimumSize <- Size(1024, 800)
        this.Size <- this.MinimumSize
        
        this.BackColor <- colors.background
        this.ForeColor <- colors.text
        setFont()        

        //var exe = System.Reflection.Assembly.GetExecutingAssembly()
        //var iconStream = exe.GetManifestResourceStream("Namespace.IconName.ico")
        //if (iconStream != null) Icon = new Icon(iconStream)
        this.Icon <- new Icon("icon.ico")


        // main panel 
        this.Controls.Add mainPanel
        mainPanel.Dock <- DockStyle.Fill        


        // LogPanel     
        mainPanel.Controls.Add logPanel
        logPanel.Dock <- DockStyle.Fill  // "Dock=Fill" must be always the first one
        logPanel.DockPadding.Top <- 10
        logPanel.SetText "Start"


        // Bitstamp panel
        mainPanel.Controls.Add bitstampPanel
        bitstampPanel.Dock <- DockStyle.Top     


        // test
        //let p1 = new controls.borderPanel.BorderPanel(colors.red)
        //mainPanel.Controls.Add p1
        //p1.Dock <- DockStyle.Top
        //p1.BackColor <- colors.red


        // toolbar
        this.Controls.Add toolBar
        toolBar.FlowDirection <- FlowDirection.LeftToRight
        toolBar.Dock <- DockStyle.Top
        toolBar.BackColor <- colors.bar         
        toolBar.Height <- 25

        //toolBar.Anchor <- AnchorStyles.Top
        toolBar.Controls.Add buyButton
        buyButton.Text <- "BUY"
        buyButton.BackColor <- colors.background
        buyButton.ForeColor <- colors.text //buttonForeColor
        buyButton.FlatStyle <- FlatStyle.Popup        

        this.AddCurrencies()


        let trader = initializeTrader()
        trader.start()
        buyButton.Click.Add ( fun _ -> trader.buy() )

        this.ResumeLayout(true)
        

    member this.AddCurrencies() =

        let xrp_usd = new CheckBox()
        xrp_usd.Checked <- true
        xrp_usd.Text <- "XRP/USD"
        toolBar.Controls.Add (xrp_usd)
        xrp_usd.Click.Add(fun _ ->  logPanel.AddText(xrp_usd.Text))

        let xrp_eur = new CheckBox()
        xrp_eur.Checked <- true
        xrp_eur.Text <- "XRP/USD"
        toolBar.Controls.Add (xrp_eur)
        xrp_eur.Click.Add(fun _ ->  logPanel.AddText(xrp_eur.Text))