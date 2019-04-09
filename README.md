Presentation : https://speakerdeck.com/suadev/microservice-architecture-and-implementation-with-asp-dot-net-core

The aim of the demo is showing ***event-driven and eventual consistent communication*** between the microservices. 

<img src="https://speakerd.s3.amazonaws.com/presentations/d74133b1f0d1409ab6093806e005f64e/preview_slide_17.jpg" />


## Prerequities

* DotNet Core SDK 2.2

* Docker

* pgAdmin or Azure Data Studio (Needs PostgreSQL extension - https://github.com/Microsoft/azuredatastudio-postgresql/ )


## Running in Debug Mode

* Run 'docker-compose up'

* Wait all services to up and running. ( rabbitmq, postgres, elasticsearch and kibana )

* Select 'All' debug option and start debuging.

* Wait until all microservices are up and running.

P.S. You can use ***.postman_project/Dotnet_Istanbul.postman_collection.json*** file for a quick test from Postman.


## Tool Set

* Asp.Net Core 2.2 
* Entity Framework Core 2.2
* PostgreSQL - Npgsql
* Serilog - Elasticsearch - Kibana
* RabbitMQ - RawRabbit
* Docker Containers ( PostgreSQL, RabbitMQ, Elasticsearch and Kibana )
* pgAdmin or Azure Data Studio
* VS Code
