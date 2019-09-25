# notifier-api

Communities can setup text messages to be sent out to their community members.

Given a phone number and a community id (#fmdc), a community member can
subscribe to text-message notifications by texting the community id (#fmdc) to
the provided phone number.

When the community needs to send out notifications, all subscribers will
recieve a text.

## features
* Community members (subscribers) don't need to install an app
* Community organizers (creators) can use a simple interface for creating and scheduling texts

## Setup
* `git clone https://github.com/linhub15/notifier-api`
* `cd notifier-api`

### API
* `dotnet build web-service`
* `dotnet run -p web-service`
* https://localhost:5001/swagger/index.html

### Database
* `sudo service mongod start` - start the mongodb service
* `cd notifier-api/Database`
* `npm start`

