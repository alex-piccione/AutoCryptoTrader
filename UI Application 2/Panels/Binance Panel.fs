module panels.binancePanel

open System
open System.Windows.Forms
open Alex75.Cryptocurrencies

open engine
open controls.borderPanel
open controls.priceLabel


type BinancePanel(engine:Engine) as panel =
    inherit BorderPanel(colors.Binance)

    let mainPanel = new FlowLayoutPanel()
    let titleLabel = new Label()    

    let pricePanel = new FlowLayoutPanel()
    
    let xrp_btcLabel = new PriceLabel("XRP/BTC")
    let xrp_ethLabel = new PriceLabel("XRP/ETH")

    let balancePanel = new FlowLayoutPanel()
    let xrp_balanceLabel = new Label()
    let usd_balanceLabel = new Label()
    let eur_balanceLabel = new Label()


    do
        panel.SuspendLayout()        
        panel.InnerPadding <- Padding(10)
        
        panel.Add mainPanel
        mainPanel.Dock <- DockStyle.Fill
        mainPanel.ForeColor <- colors.text
        mainPanel.FlowDirection <- FlowDirection.LeftToRight
        
        
        mainPanel.Controls.Add titleLabel             
        titleLabel.Text <- "Binance"
        titleLabel.BackColor <- colors.Binance
        titleLabel.Dock <- DockStyle.Left
        
        // prices
        mainPanel.Controls.Add pricePanel
        pricePanel.FlowDirection <- FlowDirection.TopDown
        pricePanel.Controls.Add xrp_btcLabel
        pricePanel.Controls.Add xrp_ethLabel
        
        xrp_btcLabel.AutoSize <- true
        xrp_ethLabel.AutoSize <- true
        //xrp_usdLabel.BackColor <- colors.bar
        //xrp_eurLabel.BackColor <- colors.bar
        //pricePanel.Width <- 200
        //xrp_eurLabel.AutoSize <- true
        //xrp_eurLabel.Width <- 200
        
        //pricePanel.BackColor <- colors.red  //xtest
        //xrp_usdLabel.ForeColor <- colors.text     
        
        
        // balance
        mainPanel.Controls.Add balancePanel
        balancePanel.FlowDirection <- FlowDirection.TopDown
        balancePanel.Controls.Add xrp_balanceLabel
        balancePanel.Controls.Add usd_balanceLabel
        balancePanel.Controls.Add eur_balanceLabel
        xrp_balanceLabel.AutoSize <- true
        
        panel.ResumeLayout()

        engine.BinanceTickerChanged.Add(fun ticker -> 
            match ticker.Currencies with 
            | p when p = CurrencyPair.XRP_BTC -> panel.priceChanged(ticker, xrp_btcLabel)
            | p when p = CurrencyPair("XRP", "ETH") -> panel.priceChanged(ticker, xrp_ethLabel)
            
            //| (:? Currency("xrp") as a), (:? Currency.EUR as b) -> panel.priceChanged(ticker, xrp_eurLabel)
            | _ -> ()
        )

        //engine.BinanceBalanceUpdated.Add(fun balance ->
        //      xrp_balanceLabel.Invoke( new Action( fun() -> xrp_balanceLabel.Text <- sprintf"XRP %i" balance.XRP)) |> ignore
        //      usd_balanceLabel.Invoke( new Action( fun() -> usd_balanceLabel.Text <- sprintf"USD %i" balance.USD)) |> ignore
        //      eur_balanceLabel.Invoke( new Action( fun() -> eur_balanceLabel.Text <- sprintf"EUR %i" balance.EUR)) |> ignore
        //)


    member __.priceChanged (ticker:Ticker, label:PriceLabel) = 
        try
            let newPrice = if ticker.Last.IsSome then ticker.Last.Value else ticker.Ask
            label.Invoke( new Action( fun() -> label.Price <- Some(newPrice) )) |> ignore
        with e -> label.Invoke( new Action( fun() -> label.Text <- "error" )) |> ignore