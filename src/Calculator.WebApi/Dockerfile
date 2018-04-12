FROM microsoft/dotnet:latest

WORKDIR /src
COPY . /.
RUN dotnet restore Calculator.Core.csproj -nowarn:msb3202,nu1503

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]