FROM microsoft/aspnetcore:2.0
EXPOSE 80

FROM microsoft/aspnetcore-build

COPY src/Calculator.WebApi/Calculator.WebApi.csproj src/Calculator.WebApi/
COPY src/Calculator.Core/Calculator.Core.csproj src/Calculator.Core/
RUN dotnet restore src/Calculator.WebApi/Calculator.WebApi.csproj
COPY . .
WORKDIR /src/src/Calculator.WebApi
RUN dotnet build src/Calculator.WebApi/Calculator.WebApi.csproj -c Release -o /app
RUN dotnet publish src/Calculator.WebApi/Calculator.WebApi.csproj -c Release -o /app
ENTRYPOINT ["dotnet", "Calculator.WebApi.dll"]
