module settings

open FSharp.Configuration


#if DEBUG
type Settings = YamlConfig<"settings.yaml", true, "", false>
#else
type Settings = YamlConfig<"settings.production.yaml", true, "", false>
#endif


