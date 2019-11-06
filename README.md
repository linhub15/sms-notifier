# Sms Notifier

[![Build Status](https://github.com/linhub15/notifier-api/workflows/ci/badge.svg?branch=master&event=push)](https://github.com/linhub15/notifier-api/actions)

Follows Robert C. Martin's clean architecture pattern.

Built with .NET Core, Hangfire and MongoDb.

Uses Twilio API.

## Summary

Communities can schedule text messages to a list of subscriber phone numbers.

A community member can subscribe to text-messages by texting `follow`
to the community phone number. Text `unfollow` to stop receiving notifications.

## Dev Setup

### Dependencies
* .NET Core SDK 2.2
* MongoDb v4.2.1

### Secrets

```
echo {} > ~/.microsoft/usersecrets/dfb1a7e1-bd3d-41ff-9d34-f0d983500092/secrets.json
dotnet user-secrets set "Twilio:AccountId" "the-actual-account-id"
dotnet user-secrets set "Twilio:AuthToken" "auth-token"
```

### Database

```
mongo
use notifier
db.communities.insertOne({
    "_id":"5dc24344d0d0b9503730ea5e",
    "Name":"Freestyle Movement",
    "Tag":"fmdc",
    "Subscribers":[]
})
```

## Debug
- [Local Swagger](https://localhost:5001/swagger/index.html)
- [Local HangFire](https://localhost:5001/HangFire)

## Cli References

- `dotnet build WebApi`
- `dotnet run -p WebApi`
- `dotnet watch -p WebApi run`
- `dotnet run --urls https://0.0.0.0:5001 -p WebApi` - serves it to public (when port forwarded)
- `sudo service mongod start` - start the mongodb service

