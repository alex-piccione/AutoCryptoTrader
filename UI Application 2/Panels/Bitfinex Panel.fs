module panels.bitfinexPanel

open System
open System.Windows.Forms
open System.Drawing
open System.Collections.Generic

open Alex75.Cryptocurrencies

open engine
open controls.priceLabel
open controls.borderPanel


type BitfinexPanel(engine:Engine) as panel =
    inherit BorderPanel(colors.Bitfinex)

    let mainPanel = new FlowLayoutPanel()
    let titleLabel = new Label()    

    let pricePanel = new FlowLayoutPanel()
    let xrp_usdLabel = new PriceLabel("XRP/USD")
    let xrp_eurLabel = new PriceLabel("XRP/EUR")
    let xrp_btcLabel = new PriceLabel("XRP/BTC")
    let xrp_ethLabel = new PriceLabel("XRP/ETH")

    //let balancePanel = new FlowLayoutPanel()
    //let xrp_balanceLabel = new Label()
    //let usd_balanceLabel = new Label()
    //let eur_balanceLabel = new Label()

    let priceHistory = Queue<decimal>(100)


    do
        panel.SuspendLayout()        
        panel.InnerPadding <- Padding(10)

        panel.Add mainPanel
        mainPanel.Dock <- DockStyle.Fill
        mainPanel.ForeColor <- colors.text
        mainPanel.FlowDirection <- FlowDirection.LeftToRight


        mainPanel.Controls.Add titleLabel             
        titleLabel.Text <- "Bitfinex"
        titleLabel.BackColor <- colors.Bitfinex
        titleLabel.Dock <- DockStyle.Left

        // prices
        mainPanel.Controls.Add pricePanel
        pricePanel.FlowDirection <- FlowDirection.TopDown
        pricePanel.Controls.Add xrp_usdLabel
        pricePanel.Controls.Add xrp_eurLabel
        pricePanel.Controls.Add xrp_btcLabel
        pricePanel.Controls.Add xrp_ethLabel
        
        xrp_usdLabel.AutoSize <- true
        xrp_eurLabel.AutoSize <- true
        xrp_btcLabel.AutoSize <- true
        xrp_ethLabel.AutoSize <- true
        //xrp_usdLabel.BackColor <- colors.bar
        //xrp_eurLabel.BackColor <- colors.bar
        //pricePanel.Width <- 200
        //xrp_eurLabel.AutoSize <- true
        //xrp_eurLabel.Width <- 200

        //pricePanel.BackColor <- colors.red  //xtest
        //xrp_usdLabel.ForeColor <- colors.text     
        

        //// balance
        //mainPanel.Controls.Add balancePanel
        //balancePanel.FlowDirection <- FlowDirection.TopDown
        //balancePanel.Controls.Add xrp_balanceLabel
        //balancePanel.Controls.Add usd_balanceLabel
        //balancePanel.Controls.Add eur_balanceLabel
        //xrp_balanceLabel.AutoSize <- true

        panel.ResumeLayout()

        engine.BitfinexTickerChanged.Add(fun ticker -> 
            match ticker.Currencies with 
            | p when p = CurrencyPair.XRP_USD -> panel.priceChanged(ticker, xrp_usdLabel)
            | p when p = CurrencyPair.XRP_EUR -> panel.priceChanged(ticker, xrp_eurLabel)
            | p when p = CurrencyPair.XRP_BTC -> panel.priceChanged(ticker, xrp_btcLabel)
            | p when p = CurrencyPair.XRP_ETH -> panel.priceChanged(ticker, xrp_ethLabel)
            //| (:? Currency("xrp") as a), (:? Currency.EUR as b) -> panel.priceChanged(ticker, xrp_eurLabel)
            | _ -> ()
        )

        //engine.Bit????.Add(fun balance ->
        //    xrp_balanceLabel.Invoke( new Action( fun() -> xrp_balanceLabel.Text <- sprintf"XRP %i" balance.XRP)) |> ignore
        //    usd_balanceLabel.Invoke( new Action( fun() -> usd_balanceLabel.Text <- sprintf"USD %i" balance.USD)) |> ignore
        //    eur_balanceLabel.Invoke( new Action( fun() -> eur_balanceLabel.Text <- sprintf"EUR %i" balance.EUR)) |> ignore
        //)



    member __.priceChanged (ticker:Ticker, label:PriceLabel) = 
        //priceHistory.Enqueue priceChange.price
        try
            let newPrice = if ticker.Last.IsSome then ticker.Last.Value else ticker.Ask
            label.Invoke( new Action( fun() -> label.Price <- Some(newPrice) )) |> ignore
        with e -> label.Invoke( new Action( fun() -> label.Text <- "error" )) |> ignore