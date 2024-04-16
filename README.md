# LoginApp

This application includes a console client capable of logging into a microservice backend with additional backend notification after login.

### Installation and Running

Clone the repository to your local machine:
git clone https://github.com/AnMukha/LoginApp

#### Navigate to the directory of the project:

cd LoginApp

#### Restore the necessary packages

dotnet restore

#### Build the application:

dotnet build

#### Install PostgreSQL database 

It should be MySQL but I didn't have time to change

#### Edit appsettings.json for LoginApp.Services.Identity project

1. DB connection string:
"DefaultConnection": "Host=localhost:5432;Database=***;Username=***;Password=***"
  
2. SigningKey:
"SigningKey": "...................some long enough string to be a signing key........................"

#### Edit appsettings.json for LoginApp.Services.GeteWay project

Copy SigningKey:
"SigningKey": "...................some long enough string to be a signing key........................"

#### Run database seader

It is console application LoginApp.DBSeeder. It should be started with appropriate DB connection string in first command line argument.
It adds one user to the users table.

#### Run all services in application

The most simple way to do this - start services in VS
LoginApp.Services.Identity, LoginApp.Services.PostLogin, LoginApp.Gateway.

#### Run console application

In console enter:
User name: user1
Password: user1

In console of this application and in consoles of services you will see information about authentication in Identity service throw GeteWay service and sending message to PostLogin service.

### Explanations

The backend microservice architecture contains GateWay (Ocelot) with two API methods - for authentication and sending of post-login message.
The client application is authorized and sends a post-login message with the received token.
All communications, including the client-GateWay, work via http (didn’t have time to check if https works).
User authentication is verified on GateWay, the network behind GateWay is considered secure and service communications are not protected.
Error handling in services is organized by adding a new item to midleware.

The LoginApp.RestServicesCore project was also created to create a common basis for describing Rest services, but this project just implements the creation of a service without the capabilities of a flexible builder. There was not enough time to create a full-fledged framework supporting the concern list.
