FROM microsoft/aspnetcore
WORKDIR /app
COPY src/Calculator.WebApi/bin/Release/netcoreapp2.0/publish/ .
ENTRYPOINT ["dotnet", "Calculator.WebApi.dll"]
