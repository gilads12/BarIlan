FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app/src/Calculator.Core
RUN ["dotnet", "restore"]

WORKDIR /app/src/Calculator.WebApi
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]