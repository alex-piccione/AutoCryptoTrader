module controls.priceLabel

open System
open System.Windows.Forms


let unchanged = char(9726).ToString() // 9675 // 9670
let up = char(9651).ToString()   // 9652, 9653
let UP = char(9650).ToString() 
let down = char(9661).ToString() // 9662, 9663
let DOWN = char(9660).ToString() 

type PriceLabel(currency) as label=
    inherit Label()

    let mutable price:decimal option = None

    do 
        label.Text <- currency + " ... "
        label.AutoSize <- true

    member label.Price 
        with get() = price
        and set (new_value:decimal option) =        
            
            let mutable text = "---"            

            if new_value.IsNone then text <- ""
            else 
                if price.IsSome then                     
                    match new_value.Value.CompareTo(price.Value) with
                    |  1 -> text <- sprintf "%s %.8f %s" currency new_value.Value UP
                    | -1 -> text <- sprintf "%s %.8f %s" currency new_value.Value DOWN
                    |  _ -> text <- sprintf "%s %.8f %s" currency new_value.Value unchanged            
            
            label.Invoke( new Action( fun() -> label.Text <- text; label.ResumeLayout(true) )) |> ignore
            price <- new_value