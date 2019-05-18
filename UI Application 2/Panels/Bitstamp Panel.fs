module Panels.bitstampPanel

open System
open System.Windows.Forms
open AutoCryptoTrader.Core

open engine
open System.Drawing
open System.Collections.Generic
open controls.priceLabel
open controls.borderPanel


type UpDown =
    | Up  
    | Down


let unchanged = char(9679).ToString() 
let up = char(9651).ToString() 
let UP = char(9650).ToString() 
let down = char(9661).ToString() 
let DOWN = char(9660).ToString() 


type BitstampPanel(engine:Engine) as panel =
    inherit BorderPanel(colors.Bitstamp_bg)

    let mainPanel = new FlowLayoutPanel()

    let titleLabel = new Label()    
    let priceLabel = new PriceLabel()

    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/how-to-choose-between-stackpanel-and-dockpanel
    //let a = new System.Windows.Controls. ()

    let priceHistory = Queue<decimal>(100)


    do
        panel.SuspendLayout()        
        panel.InnerPadding <- Padding(10)

        panel.Add mainPanel
        mainPanel.Dock <- DockStyle.Fill
        mainPanel.ForeColor <- colors.text
        mainPanel.FlowDirection <- FlowDirection.LeftToRight


        mainPanel.Controls.Add titleLabel             
        titleLabel.Text <- "Bitstamp"
        titleLabel.Dock <- DockStyle.Left
        //let font = new Font(FontFamily.Families.)
        //titleLabel.Font = 

        mainPanel.Controls.Add priceLabel
        priceLabel.ForeColor <- colors.text
        //priceLabel.Dock <- DockStyle.Left

        // https://www.alt-codes.net/arrow_alt_codes.php
        // https://www.rapidtables.com/code/text/unicode-characters.html
               

        priceLabel.ForeColor <- colors.Bitstamp_fg

        engine.BitstampPriceChanged.Add( fun p -> panel.priceChanged(p) )


        panel.ResumeLayout()


    member __.priceChanged (priceChange:PriceChange) = 
        //priceLabel.Text <- priceChange.price.ToString()

        let currentPrice = if priceHistory.Count > 0 then Some(priceHistory.Peek() ) else None
        priceHistory.Enqueue priceChange.price


        let newPrice = priceChange.price

        if priceLabel.InvokeRequired then
            panel.Invoke( new Action( fun() -> priceLabel.Price <- Some(newPrice) )) |> ignore
        else
            priceLabel.Price <- Some(newPrice)