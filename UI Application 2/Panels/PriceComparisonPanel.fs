module panels.PricesComparisonPanel

open System
open System.Windows.Forms
open Alex75.Cryptocurrencies
open engine
open controls.TextLabel
open controls.ThresholdLabel
open timeValuesCollector
open FSharp.Charting.ChartTypes
open FSharp.Charting
open System.Drawing


type PricesComparisonPanel(engine:Engine) as panel =
    inherit Panel()

    let mutable xrpusd_binance_price = 0m
    let mutable xrpeur_binance_price = 0m
    let mutable xrpbtc_binance_price = 0m

    let mutable xrpusd_bitstamp_price = 0m
    let mutable xrpeur_bitstamp_price = 0m
    let mutable xrpbtc_bitstamp_price = 0m

    let mutable xrpusd_bitfinex_price = 0m
    let mutable xrpeur_bitfinex_price = 0m
    let mutable xrpbtc_bitfinex_price = 0m
    

    let xrpusd_Bitstamp_Binance_diffLabel = new TextLabel("")
    let xrpusd_Bitstamp_Bitfinex_diffLabel = new TextLabel("")

    let xrpeur_Bitstamp_Binance_diffLabel = new TextLabel("")
    let xrpeur_Bitstamp_Bitfinex_diffLabel = new TextLabel("")

    let print_percent (diff:decimal) = if diff = 0m then "=" else sprintf "%+.2f %%" diff
    let xrpbtc_Bitstamp_Binance_diffLabel = new ThresholdLabel(0.2m, true, print_percent)
    let xrpbtc_Bitstamp_Bitfinex_diffLabel = new ThresholdLabel(0.2m, true, print_percent)
    let xrpbtc_Binance_Bitfinex_diffLabel = new ThresholdLabel(0.2m, true, print_percent)

    let table = new TableLayoutPanel()

    //let chart_1 = new FSharp.Charting.Chart.Line([1..3])
    let chart_1 = [for x in 0.0 .. 0.1 .. 6.0 -> sin x + cos (2.0 * x)]
                    |> Chart.Line |> Chart.WithYAxis(Title="Test")
    let chartControl = new ChartControl(chart_1)


    let xrp_btc_bitstamp_collection = new TimeValuesCollector<decimal>()
    let xrp_btc_binance_collection = new TimeValuesCollector<decimal>()
    let xrp_btc_bitfinex_collection = new TimeValuesCollector<decimal>()

    do 
        panel.SuspendLayout()

        panel.Controls.Add table
        table.Dock <- DockStyle.Fill

        // headers
        table.Controls.Add(new TextLabel("XRP/USD"), 1, 0)
        table.Controls.Add(new TextLabel("XRP/EUR"), 2, 0)
        table.Controls.Add(new TextLabel("XRP/BTC"), 3, 0)
        table.AutoSize <- true
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)) |> ignore

        //table.GrowStyle <- TableLayoutPanelGrowStyle.
        table.Controls.Add(new TextLabel("Bitstamp/Binance", true), 0, 1)
        table.Controls.Add(new TextLabel("Bitstamp/Bitfinex", true), 0, 2)
        table.Controls.Add(new TextLabel("Binance/Bitfinex", true), 0, 3)


        // XRP/USD
        table.Controls.Add (xrpusd_Bitstamp_Binance_diffLabel, 1, 1)
        table.Controls.Add (xrpusd_Bitstamp_Bitfinex_diffLabel, 1, 2)
        //table.Controls.Add (xrpusd_Binance_Bitfinex_diffLabel, 1, 3)

        // XRP/EUR
        table.Controls.Add (xrpeur_Bitstamp_Binance_diffLabel, 2, 1)
        table.Controls.Add (xrpeur_Bitstamp_Bitfinex_diffLabel, 2,2)
        //table.Controls.Add (xrpeur_Bitstamp_Bitfinex_diffLabel, 1, 2)

        // XRP/BTC
        table.Controls.Add(xrpbtc_Bitstamp_Binance_diffLabel, 3,1)
        table.Controls.Add(xrpbtc_Bitstamp_Bitfinex_diffLabel, 3,2)
        table.Controls.Add(xrpbtc_Binance_Bitfinex_diffLabel, 3,3)      

        panel.Font <- new Font(new FontFamily("Arial"), 10.0f)
        panel.Controls.Add chartControl
        chartControl.Dock <- DockStyle.Fill
        chartControl.MaximumSize <- new Size(500, 200)
        let a:float32 = 10.0f
        chartControl.Font <- new Font("Arial", 10.0f) //FontFamily.GenericSansSerif, a,  FontStyle.Regular)

        panel.ResumeLayout()

        engine.BitstampTickerChanged.Add (fun ticker -> 
            match ticker.Currencies with
            | p when p = CurrencyPair.XRP_USD -> xrpusd_bitstamp_price <- ticker.Last.Value
            | p when p = CurrencyPair.XRP_EUR -> xrpeur_bitstamp_price <- ticker.Last.Value
            | p when p = CurrencyPair.XRP_BTC -> xrpbtc_bitstamp_price <- ticker.Last.Value
                                                 xrp_btc_bitstamp_collection.AddValue(ticker.Last.Value)
            | _ -> ()
            panel.calculateDifference()

            
        )

        engine.BinanceTickerChanged.Add (fun ticker -> 
            match ticker.Currencies with
            | p when p = CurrencyPair.XRP_USD -> xrpusd_binance_price <- ticker.Last.Value
            | p when p = CurrencyPair.XRP_EUR -> xrpeur_binance_price <- ticker.Last.Value
            | p when p = CurrencyPair.XRP_BTC -> xrpbtc_binance_price <- ticker.Last.Value
                                                 xrp_btc_binance_collection.AddValue(ticker.Last.Value)
            | _ -> ()
            panel.calculateDifference()
        )

        engine.BitfinexTickerChanged.Add (fun ticker -> 
            match ticker.Currencies with
            | p when p = CurrencyPair.XRP_USD -> xrpusd_bitfinex_price <- ticker.Last.Value
            | p when p = CurrencyPair.XRP_EUR -> xrpeur_bitfinex_price <- ticker.Last.Value
            | p when p = CurrencyPair.XRP_BTC -> xrpbtc_bitfinex_price <- ticker.Last.Value
                                                 xrp_btc_bitfinex_collection.AddValue(ticker.Last.Value)
            | _ -> ()
            panel.calculateDifference()
        )




    member __.calculateDifference () =

        //if panel.
        // todo: this is called when window handle is not yet initialized

        let calculateDiffPercentage price_a price_b = 
            if price_a = 0m || price_b = 0m then 0m
            else ( (price_a - price_b) / max price_a price_b ) * 100m            
            

        // XRP/USD       
        // Bitstamp / Binance
        //xrpusd_Bitstamp_Binance_diffLabel.SetText(sprintf "%O %%" (calculateDiffPercentage xrpusd_bitstamp_price xrpusd_binance_price))
        xrpusd_Bitstamp_Bitfinex_diffLabel.SetText( print_percent (calculateDiffPercentage xrpusd_bitstamp_price xrpusd_bitfinex_price)  )

        // XRP/EUR        
        //xrpeur_Bitstamp_Bitfinex_diffLabel.SetText( print_percent (calculateDiffPercentage xrpeur_bitstamp_price xrpeur_bitfinex_price))

        // XRP/BTC      
        xrpbtc_Bitstamp_Binance_diffLabel.SetValue( calculateDiffPercentage xrpbtc_bitstamp_price xrpbtc_binance_price)                         
        xrpbtc_Bitstamp_Bitfinex_diffLabel.SetValue( calculateDiffPercentage xrpbtc_bitstamp_price xrpbtc_bitfinex_price)
        xrpbtc_Binance_Bitfinex_diffLabel.SetValue( calculateDiffPercentage xrpbtc_binance_price xrpbtc_bitfinex_price)


