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

## setup
* `git clone https://github.com/linhub15/notifier-api`
* `cd notifier-api`
* `dotnet build`
* `dotnet run`

### Database setup
* `sudo service mongod start` - start the mongodb service
* `cd notifier-api/Database`
* `npm start`

## end points
### AuthController
#### `POST api/auth/phone-number`
* generates a one time code
* text the one time code to a provided phone number

#### `POST api/auth/authenticate`
* given a phone number and a one time code, authenticate the user with a JWT token


### MessageController
#### `GET api/messages`
* reads the subject `sub` claim from JWT 
* returns a list of messages for the subject

#### `GET api/messages/{guid}`
* reads the subject `sub` claim from JWT
* returns the message for the provided message guid

#### `POST api/messages`
* saves a message to the database for given subject `sub`