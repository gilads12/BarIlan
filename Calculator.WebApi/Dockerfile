FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Calculator.sln ./
COPY Calculator.WebApi/Calculator.WebApi.csproj Calculator.WebApi/
COPY Calculator.Core/Calculator.Core.csproj Calculator.Core/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Calculator.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publishhttp://bit.ly/webdev-barilan
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Calculator.WebApi.dll"]
