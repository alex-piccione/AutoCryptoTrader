module AutoCryptoTrader.DesktopApplication.Program

open System
open System.Windows.Forms
open AutoCryptoTrader.DesktopApplication.Forms

[<EntryPoint>]
[<STAThread>]
let main argv =

    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault false

    use form = new MainForm()

    Application.Run(form)

    0 // return an integer exit code
