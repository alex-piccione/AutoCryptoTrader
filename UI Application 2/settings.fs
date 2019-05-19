module settings

open FSharp.Configuration


#if DEBUG
type AppSettings = YamlConfig<"settings.yaml", true, "", false>
#else
type AppSettings = YamlConfig<"settings.production.yaml", true, "", false>
#endif


