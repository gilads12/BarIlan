# BarIlan

1. Gilad Segal 318162252
2. Shmuel feled 305469801
3. Shani Shliselberg 313288839



# notes:

1. We decided in case of failer in the request we initilayze the calcuator to "0" and return display="0".
2. In our e2e test we didn't test the authentication, and only register our user (we didn't test if the user has added).

# overview:

* This project was written in C# using .core 2.0
* We use webapi as Asp.core (with Kestrel)
* For check our api we use "Swagger"
* We use MSTS framework as our testing framework
* We use selenium framework as our e2e framework
* We use circleCi as our ci for automate test and push to docker-hub (each time we commit to git circleci start automatically)

# Preinstall:
1) go to https://www.microsoft.com/net/download/windows and install dotnet core 2
2) install chorme (for the selenium framework)
3) go to ./script and run "pull-images" for downloading our image
4) In src/Calculator.Test there "test_config.json" to config our tests

# Test:
	-	configuration at "\src\Calculator.Test" => test_config (there defualt configuration)
	-	run cmd and go to our Test folder "\src\Calculator.Test" and run "dotnet test"
	
# Run:

For run all the microservices goto cmd\Terminal and run "docker-compose -f \docker-compose.yml up"

[![CircleCI](https://circleci.com/gh/gilads12/BarIlan/tree/master.svg?style=svg)](https://circleci.com/gh/gilads12/BarIlan/tree/master)