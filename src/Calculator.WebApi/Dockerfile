FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

RUN dotnet restore ./Calculator.WebApi.csproj

RUN dotnet build ./Calculator.WebApi.csproj

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]