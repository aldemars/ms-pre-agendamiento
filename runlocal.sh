export ConnectionStrings__database=$(sh az keyvault secret show --name 'ConnectionStrings' --vault-name 'dev-pre-agendamiento' --query value  | tr -d \")
export ConnectionStrings__AppConfig=$(sh az keyvault secret show --name 'appConfiguration' --vault-name 'dev-pre-agendamiento' --query value  | tr -d \")
dotnet run --project ms-pre-agendamiento/ms-pre-agendamiento.csproj