// MergeSort

open System

let array = [|5;4;1;8;7;2;6;3|]


/// left and right arrays MUST be sorted!
let merge (left:int[], right:int[]): int[] = 
    let output = Array.zeroCreate<int>(left.Length + right.Length)
    let mutable x = 0 // left array cursor
    let mutable y = 0 // right array cursor
    for z in [0..output.Length-1] do 
        if y=right.Length || (x<=left.Length-1 && left.[x] < right.[y]) then 
            output.[z] <- left.[x]
            x <- x+1
        else
            output.[z] <- right.[y]
            y <- y+1                 
    output

//merge([|4;5|],[|1;3|])
//merge_2([|1|],[|2|])
//merge_2([|2|],[|1|])
//merge_2([|1;2|],[|3|])
//merge_2([|1|],[|2;3|])
//merge_2([|3;4|],[|1;2|])


// pass the array and the range so avoid copy values in new arrays
let merge_2 (array:int[], leftSize:int): int[] = 
    let output = Array.zeroCreate<int>(array.Length)
    let rightSize = array.Length-leftSize

    let mutable x = 0 // left array cursor
    let mutable y = leftSize // right array cursor
    for z in [0..output.Length-1] do 
        if x<leftSize && array.[x] < array.[y] then
            output.[z] <- array.[x]
            x <- x+1
        else
            output.[z] <- array.[y]
            if y<=array.Length-1 then
                y <- y+1 
    output



let rec Sort (array:int[]) =
    printf "input: %A\n" array
    if array.Length <= 1 then array
    else
        let len = array.Length/2
        let left = array.[0..len-1]        
        let right = array.[len..array.Length-1]
        printf "left: %A\n" left
        printf "right: %A\n" right

        let sorted_left = Sort(left)
        let sorted_right = Sort(right)
        printf "sorted left: %A\n" sorted_left
        printf "sorted right: %A\n" sorted_right
        
        let res = merge(sorted_left, sorted_right)
        printf "res: %A\n" res
        res

let sorted = Sort(array)
sprintf "%A" sorted

