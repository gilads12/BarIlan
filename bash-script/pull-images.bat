
docker pull "gilads123/calculatorwebapi:latest"
docker pull "webdevtoolsandtech/currency-backend"
docker pull "webdevtoolsandtech/currency-frontend"
docker pull "webdevtoolsandtech/user-service"



docker tag gilads123/calculatorwebapi:latest gilads123/currency-calculator
docker rmi gilads123/calculatorwebapi:latest

echo "Done!"
pause