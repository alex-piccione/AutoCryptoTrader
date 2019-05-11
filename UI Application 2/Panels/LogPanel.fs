module Panels.logPanel

open System.Windows.Forms
open System

type LogPanel () as panel =
    inherit Panel()

    let textboxContainer = new Panel()
    let textbox = new RichTextBox()
    
    let commandbar = new Panel()
    let cleanButton = new Button()


    let ctx = System.Threading.SynchronizationContext.Current

    do
        
        textboxContainer.Padding <- Padding(10) // TextBox does not have margin or padding
        panel.Controls.Add textboxContainer
        textboxContainer.Dock <- DockStyle.Fill

        panel.Controls.Add commandbar
        commandbar.Dock <- DockStyle.Top

        // textbox
        textboxContainer.Controls.Add textbox
        //textboxContainer.BackColor <- colors.background

        textbox.ScrollBars <- RichTextBoxScrollBars.Vertical
        textbox.Multiline <- true
        textbox.WordWrap <- true
        textbox.ReadOnly <- true
        textbox.BackColor <- colors.background
        textbox.ForeColor <- colors.text
        //textbox.Font <- System.Drawing.Font
        //textbox.Font.S


        textbox.Dock <- DockStyle.Fill
        textbox.BorderStyle <- BorderStyle.None


        // command bar
        commandbar.BackColor <- colors.bar        
        commandbar.Height <- 25

        commandbar.Controls.Add cleanButton

        // cleanbutton
        cleanButton.Text <- "Clear"
        cleanButton.BackColor <- colors.background
        cleanButton.ForeColor <- colors.text //buttonForeColor
        cleanButton.FlatStyle <- FlatStyle.Popup
        
        cleanButton.AutoSizeMode <- AutoSizeMode.GrowAndShrink
        cleanButton.Click.AddHandler (new System.EventHandler( fun sender e -> panel.SetText ""))


    member panel.SetText text = 
        if textbox.InvokeRequired then
            panel.Invoke( new Action( fun() -> textbox.Text <- text) ) |> ignore
        else
            textbox.Text <- text

    member panel.AddText text = 
        if textbox.InvokeRequired then 
            panel.Invoke( new Action(fun() -> textbox.Text <- sprintf "%s\r\n%s" textbox.Text text )) |> ignore
        else
            textbox.Text <- sprintf "%s\r\n%s" textbox.Text text
            
