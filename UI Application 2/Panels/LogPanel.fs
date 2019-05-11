module Panels.logPanel

open System.Windows.Forms

type LogPanel () as panel =
    inherit Panel()

    let textboxContainer = new Panel()
    let textbox = new RichTextBox()
    
    let commandbar = new Panel()
    let cleanButton = new Button()


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


    member panel.SetText text =  textbox.Text <- text
    member panel.AddText text = textbox.Text <- sprintf "%s\r\n%s" textbox.Text text
        

    
