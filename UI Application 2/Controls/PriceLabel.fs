module controls.priceLabel

open System.Windows.Forms


let unchanged = char(9552).ToString() 
let up = char(9651).ToString() 
let UP = char(9650).ToString() 
let down = char(9661).ToString() 
let DOWN = char(9660).ToString() 

type PriceLabel() as label =

    inherit Label()

    let mutable price:decimal option = None

    member label.Price 
        with get() = price
        and set (new_value:decimal option) = 
            if new_value.IsNone then label.Text <- "--.--"
            else 
                 if price.IsSome then                     
                        match new_value.Value.CompareTo(price.Value) with
                            | 1 -> label.Text <- sprintf "%f %s" new_value.Value UP
                            | -1 -> label.Text <- sprintf "%f %s" new_value.Value DOWN
                            | _ -> label.Text <- sprintf "%f %s" new_value.Value unchanged
                

            price <- new_value