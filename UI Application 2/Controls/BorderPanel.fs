module controls.borderPanel

open System.Drawing
open System.Windows.Forms




type BorderPanel(color:Color) as panel =
    inherit Panel()

    let innerPanel = new Panel()

    do 
        panel.SuspendLayout()

        panel.Padding <- Padding(2)
        panel.BackColor <- color
        panel.Dock <- DockStyle.Fill
        panel.Controls.Add innerPanel

        innerPanel.Dock <- DockStyle.Fill
        //innerPanel.Padding <- Padding(3)
        //innerPanel.Padding <- Padding(2)
        innerPanel.BackColor <- colors.background

        panel.ResumeLayout(false)


    member panel.Add control =
        innerPanel.Controls.Add control

    member panel.InnerPadding 
        with set value = innerPanel.Padding <- value