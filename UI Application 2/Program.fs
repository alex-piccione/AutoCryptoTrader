module AutoCryptoTrader.DesktopApplication.Program

open System
open System.Windows.Forms

open AutoCryptoTrader.DesktopApplication.Forms


[<EntryPoint>]
[<STAThread>]
let main argv =

    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault false

    
    let configuration = settings.Settings()

    use form = new MainForm(configuration)

         

    Application.Run(form)

    0 // return an integer exit code
