module controls.ThresholdLabel

open System
open System.Windows.Forms


type ThresholdLabel(threshold, absolute, getText:decimal -> string) as label =
    
    inherit Label()

    //let threshold =

    //do 
    //    label.Text <- text
    //    label.AutoSize <- autosize


    //new(text) = new TextLabel(text, false)
            
        

    //member __.SetTreshold(text) =
    //    try
    //        label.Invoke(new Action(fun _ -> label.Text <- text)) |> ignore
    //    with e -> ()


    member __.SetValue(value:decimal) =
        try
            label.Invoke(new Action(fun _ -> label.Text <- getText(value))) |> ignore
            let overThreshold = if absolute then value > abs threshold else value > threshold
            label.ForeColor <- if overThreshold then colors.yellow else colors.text
        with e -> ()

