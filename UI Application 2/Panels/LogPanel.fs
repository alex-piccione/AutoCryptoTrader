module Panels.logPanel

open System.Windows.Forms
open System

type LogPanel () as panel =
    inherit Panel()

    let textboxContainer = new Panel()
    let textbox = new RichTextBox()
    
    let toolBar = new Panel()
    let cleanButton = new Button()


    let ctx = System.Threading.SynchronizationContext.Current

    do
       
        panel.Controls.Add textboxContainer
        textboxContainer.Dock <- DockStyle.Fill    // "Dock=Fill" must be always the first one
        
        panel.Controls.Add toolBar
        toolBar.Dock <- DockStyle.Top

        // textbox
        textboxContainer.Padding <- Padding(10) // TextBox does not have margin or padding
        textboxContainer.Controls.Add textbox
        textbox.Dock <- DockStyle.Fill
        //textboxContainer.BackColor <- colors.background

        textbox.ScrollBars <- RichTextBoxScrollBars.Vertical
        textbox.Multiline <- true
        textbox.WordWrap <- true
        textbox.ReadOnly <- true
        textbox.BackColor <- colors.background
        textbox.ForeColor <- colors.text
        textbox.Font <- new System.Drawing.Font(Drawing.FontFamily.GenericMonospace, float32(11.), Drawing.FontStyle.Regular); 
        //textbox.Font.S        
        textbox.BorderStyle <- BorderStyle.None


        // command bar
        toolBar.BackColor <- colors.bar   
        toolBar.Height <- 25

        toolBar.Controls.Add cleanButton

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
            
