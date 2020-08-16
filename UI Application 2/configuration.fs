module configuration

open FSharp.Configuration

type Configuration = YamlConfig<"configuration.yaml", true, "", false>


