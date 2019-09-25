

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