module controls.TextLabel

open System.Windows.Forms
open System

type TextLabel(text, autosize) as label =
    inherit Label()

    do 
        label.Text <- text
        label.AutoSize <- autosize


    new(text) = new TextLabel(text, false)
            
        

    member __.SetText(text) =
        try
            label.Invoke(new Action(fun _ -> label.Text <- text)) |> ignore
        with e -> ()


    //member __.SetValue(value:decimal, threshold, postfix) =
    //    try
    //        label.Invoke(new Action(fun _ -> label.Text <- value.ToString() + postfix)) |> ignore
    //        label.ForeColor <- if abs(value) > threshold then colors.yellow else colors.text
    //    with e -> ()

