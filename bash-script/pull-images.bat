
docker pull "gilads123/calculatorwebapi:v1.0.3"
docker pull "webdevtoolsandtech/currency-backend"
docker pull "webdevtoolsandtech/currency-frontend"
docker pull "webdevtoolsandtech/user-service"



docker tag gilads123/calculatorwebapi:v1.0.4 gilads123/currency-calculator
docker rmi gilads123/calculatorwebapi:v1.0.4

echo "Done!"
pause