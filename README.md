# BarIlan
Bar ilan advance web 


todo
1. write readme 
2. code review 
3. build stable image as latest tag


(the current image is calculatorwebapi:v1.0.4)

#notes

we decided in case of failer in the request we initilayze the calcuator to "0" and return display="0"

#overview

This project was written in C# using .core 2.0
we use webapi as Asp.core (with Kestrel)
for check our api we use "Swagger"
we use MSTS framework as out testing framework.

#PreInstall
1) go to https://www.microsoft.com/net/download/windows and install dotnet core 2

#Test

For running our test:
run cmd and go to our Test folder "\src\Calculator.Test" and run "dotnet test"


