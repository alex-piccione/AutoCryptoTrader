//module SyncPeriod
open System

type SyncPeriod = {
    Start: DateTime;
    End: DateTime
}

let period = {Start = DateTime.Now; End=DateTime.Now.AddDays(1) }

//console.