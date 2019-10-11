

# Vehicle Tracking

## Description

This is the RESTful API to track ​vehicles ​​location. We have a device that is boarded in a vehicle, it will communicate with your API to register the vehicle and update its position.

I assume that the device boarded in a vehicle is an interactive device (tablet, smartphone,...). So drivers can interact with the device proactively. It's almost like a taxi-mounted device. It will support more useful features in the future. Such as direction, traffic jams, SOS, broken down vehicle location report,...

The driver can start/stop location tracking proactively. After start tracking, the device will update its location every 30 seconds.

The admin can retrieve the current location and journey of a specific vehicle in certain time on a map.

## Workflow

### Driver User
The driver has to sign in to the system first. The system will authenticate the driver. If authentication is successful then the system will response token - the token will be used for authentication purposes.

When the driver clicks the button to start tracking. The system will create a new tracking session for this driver and store this session to the cache server.

Every 30 seconds, the device will send a request to update the vehicle location until the end of the journey.

### Admin user
The admin has to sign in to the back office side. After sign-in successful, the admin can retrieve the current location and journey of a specific vehicle at a certain time on a map.

## Future plane to improve

- Apply validation - Not completed yet
- Integrate Google Maps API to retrieve the address
- Apply Event Sourcing pattern
- Apply Message Queue
- Writing UT


## Technical applied
- ASP .Net Core web API
- ASP .NET Core Identity
- Entity framework core
- CQRS pattern
- Redis cache/Application Insight
- Swagger

## Database Structure
There are thee databases:
1. ApplicationUser database - it uses for managing identity user
2. Vehicle database - it uses for managing data of the system. 
3. Tracking database - it uses for tracking location and manage the session of all vehicles.

## Before you start

### Prepare the database
I have prepared a create script that you can find it in the Scripts folder. You can run it to create the database.

Also, you can create the migration and apply the migration to the database to create the schema by command. 

*Note:* Because there are three dbcontext here, so you need to create three migration for each dbcontext. 

### Prepare Redis cache server
Because the app uses a distributed cache (Redis) so you need to prepare the cache server. You can download Redis cache for windows at 
[https://github.com/microsoftarchive/redis](https://github.com/microsoftarchive/redis)


## API list
Since I applied Swagger for API document, so you can take a look at Swagger documents for more information.

### Tracking API
| Method | URL | Role | Description |
| --- | --- | --- | --- |
| GET | /api​/tracking​/start | User | Start tracking sesssion |
| GET | /api​/tracking​/stop |  User | Stop tracking session |
| GET | ​/api​/tracking​/update-location | User | Update vehicle loction |
| GET | /api​/tracking​/get-current-location | Admin | Retrieve current location of specific vehicle |
| POST | /api​/tracking​/get-journey | Admin | Retreive journey of sepecific vehicle |

### User API
| Method | URL | Role | Description |
| --- | --- | --- | --- |
| POST | ​/api​/user​/sign-in |   | Sign in |
| POST | ​/api​/user​/sign-up |   | Sign up |

### Vehicle API
| Method | URL | Role | Description |
| --- | --- | --- | --- |
| GET | ​/api​/vehicle | User  | Retrieve vehicle infomation |
| POST | /api​/vehicle​/register | User  | Regester a new vehicle |
| POST | /api​/vehicle​/resign | Uer  | Resign a vehicle |


## Testing user
You can sign up a new account or use the available account below for testing:
- Admin: admin@mail.com/1234x@X
- User: user@mail.com/1234x@X

