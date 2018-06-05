# BarIlan

1. Gilad segal 318162252
2. Shmuel feled ________
3. Shani _____ _________

todo
1. write readme
2. code review 


#notes

we decided in case of failer in the request we initilayze the calcuator to "0" and return display="0" 
in our e2e test we didn't test the authentication, and only register our user (we didn't test if the user has added)

#overview

This project was written in C# using .core 2.0
we use webapi as Asp.core (with Kestrel)
for check our api we use "Swagger"
we use MSTS framework as our testing framework
we use selenium framework as our e2e framework.

# Preinstall:
1) go to https://www.microsoft.com/net/download/windows and install dotnet core 2
2) install chorme (for the selenium framework)
3) go to \bash-script and run "pull-images" for downloading our image
4) for our e2e test we use 5002 port!

#Test

For running our test:
run cmd and go to our Test folder "\src\Calculator.Test" and run "dotnet test"

#Run 

For run all the microservices goto cmd\Terminal and run "docker-compose -f \docker-compose.yml up"