# DDD Sample
Install before getting started:
1) Docker (https://www.docker.com/get-started/)

Get and start running RabbitMQ 
This is a good alternative to Azure Service Bus on your local machine.
Get RabbitMQ Container from Docker but running in terminal: docker pull rabbitmq

Run in terminal: docker run -d --hostname my-rabbit --name some-rabbit -p 8181:15672 rabbitmq:3-management

Open Browser and go to: http://localhost:8181/
Login with: guest/guest

Further Help; https://hub.docker.com/_/rabbitmq

Getting started with the web sample:
1. Go to the 'container-demo' folder
2. Open a terminal
3. Enter: docker build -t container-demo .
4. Run Demo: docker run -p 3000:3000 container-demo
5. Open brawser and goto: http://localhost:3000

Getting started with the api sample:
1. Go to the 'demo-api' folder
2. Open a terminal
3. Enter: docker build -t demo-api .
4. Run Demo: docker run -p 8080:8080 demo-api
5. Open brawser and goto: http://localhost:8080/swagger/index.html

Set the dashboard API URL to: http://localhost:8080

Set the people service url to: http://localhost:8181

Start the people service by:
1. Go to the 'people-service' folder
2. Open a terminal
3. Enter: docker build -t people-service .
4. Run Demo: docker run -p people-service

You should be able to create people, get a list back from the data source. If you turn the service off by: 
Stop the service by running: docker stop people-service
Create a new person and you will be able to see a message in the 'people' queue when you go to: http://localhost:8181/
If you restart the people-service by: docker start people-service
The message should be picked up and processed. 