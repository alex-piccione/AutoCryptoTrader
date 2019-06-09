module timeValuesCollector

open System
open System.Collections.Generic

type TimeValuesCollector<'T> () =


    let dates = new List<DateTime>()
    let values = new List<'T>()

    member __.Dates with get() = dates
    member __.Values with get() = values

    member __.AddValue(value:'T) =
        dates.Add DateTime.Now
        values.Add value


///// <summary>
///// Maintains a counter in the last customizable lenght period of time.
///// </summary>
//public class TimedCounter
//{
//    public TimeSpan Period { get; set; }

//    public TimedCounter(TimeSpan period)
//    {
//        Period = period;
//        ticksInPeriod = period.Ticks;
//    }

//    public long Count()
//    {
//        RemovePassed();
//        return collection.Count;
//    }

//    public void Increase() => collection.Enqueue(DateTime.Now.Ticks);

//    #region private 

//    private long ticksInPeriod;
//    private System.Collections.Concurrent.ConcurrentQueue<long> collection = new System.Collections.Concurrent.ConcurrentQueue<long>();
//    private object dequeue_lock = new object();

//    private void RemovePassed()
//    {
//        lock (dequeue_lock)
//        {
//            var go = true;
//            while (go)
//                if (collection.TryPeek(out long ticks) && ticks <= (DateTime.Now.Ticks - ticksInPeriod))
//                    collection.TryDequeue(out long _);
//                else
//                    go = false;
//        }
//    }

//    #endregion
//}