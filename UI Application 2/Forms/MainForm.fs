namespace AutoCryptoTrader.DesktopApplication.Forms

open System
open System.Windows.Forms
open System.Drawing
open Panels.logPanel

// ref: https://www.codeproject.com/Articles/30414/Getting-Started-in-F-A-Windows-Forms-Application
// https://blogs.msdn.microsoft.com/mcsuksoldev/2011/05/27/f-windows-application-template-for-winforms/
// https://github.com/Acadian-Ambulance/vinyl-ui

type MainForm () as this =
    inherit Form()

    let logPanel = new LogPanel()

    do 
        this.SuspendLayout();

        this.BackColor <- colors.background
        this.Text <- "Auto Crypto Trader"
        this.MinimumSize <- Size(600, 400)
        
        //var exe = System.Reflection.Assembly.GetExecutingAssembly();
        //var iconStream = exe.GetManifestResourceStream("Namespace.IconName.ico");
        //if (iconStream != null) Icon = new Icon(iconStream);
        this.Icon <- new Icon("icon.ico")


        // add LogPanel     
        this.Controls.Add logPanel
        logPanel.Dock <- DockStyle.Fill
        logPanel.SetText "Start"

        this.ResumeLayout(false)
        this.PerformLayout()
        

    






