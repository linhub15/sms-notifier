# notifier-api

Communities can setup text messages to be sent out to their community members.

Given a phone number and a community id (#fmdc), a community member can
subscribe to text-message notifications by texting the community id (#fmdc) to
the provided phone number.

When the community needs to send out notifications, all subscribers will
recieve a text.

* [Local Swagger](https://localhost:5001/swagger/index.html)
* [Local HangFire](https://localhost:5001/HangFire)

## features
* Community members (subscribers) don't need to install an app
* Community organizers (creators) can use a simple interface for creating and scheduling texts

## Setup
* `git clone https://github.com/linhub15/notifier-api`
* `cd notifier-api`

### API
* `dotnet build web-service`
* `dotnet run -p web-service`
* `dotnet watch -p web-service run`
* `dotnet run --urls https://0.0.0.0:5001 -p web-service`


### Database
* `sudo service mongod start` - start the mongodb service
* `cd notifier-api/Database`
* `npm start`

# Roadmap

## API

### Creator
- [x] GET /messages
- [x] POST /message - sends the message to all subscribers
- [ ] Cancel a scheduled message
- [ ] Modify a scheduled message time
- [x] Modify a scheduled message content

### Subscriber
- [x] Subscribe to a community tag
- [x] Unsubscribe to a community tag

## Service Layer
- [x] Store data with MongoDb.Driver
- [x] Implement scheduling with `HangFire` & `HangFire.Mongo`
- [x] Implement sending sms with Twilio Api
- [ ] Implement authentication with Auth0

## UI
- [ ] Build web app for Creator to manage messages

