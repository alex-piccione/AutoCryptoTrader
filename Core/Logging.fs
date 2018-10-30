module Logging

open log4net
open log4net.Appender
open log4net.Config

// http://blog.ctaggart.com/2016/11/google-logging-from-log4net.html
// the example is obviously not working

let configLogger(logFile) = 

    // set errors visible
    //Util.LogLog.InternalDebugging <- true;  //
    let hierarchy = LogManager.GetRepository(System.Reflection.Assembly.GetCallingAssembly()) :?> Repository.Hierarchy.Hierarchy
    hierarchy.Root.Level <- Core.Level.Info

    //let traceAppender = Appender.TraceAppender()
    //traceAppender.Layout <- Layout.PatternLayout("%date %-5level [%2thread] %message%newline")
    //traceAppender.ActivateOptions()
    //hierarchy.Root.AddAppender traceAppender

    let resultAppender = Appender.RollingFileAppender()
    resultAppender.File <- logFile    
    resultAppender.AppendToFile <- true
    resultAppender.Layout <- Layout.PatternLayout("%date %-5level [%2thread] %message%newline")
    resultAppender.ImmediateFlush <- false
    resultAppender.RollingStyle <- RollingFileAppender.RollingMode.Date;
    resultAppender.ActivateOptions()
    hierarchy.Root.AddAppender resultAppender


    hierarchy.Configured <- true

    BasicConfigurator.Configure hierarchy |> ignore

    