
# Vehicle Tracking

## Description

This is the RESTful API to track ​vehicles ​​location.

The driver can start/stop location tracking and update thier location for each 30 miliseconds.

The admin can see the current location and journey of specific vehicle in certain time on a map.

## Workflow


## Technical applied
- ASP .Net Core web API
- ASP .NET Core Identity
- Entity framework core
- CQRS pattern
- Redis cache/Application Insight
- Swagger
- 
## Database Structure
There are thee databases:
1. ApplicationUser database - it uses for managing identity user
2. Vehicle database - it uses for managing data of system. 
3. Tracking database - it uses for tracking location and manage the session of all vehicles.

## Before you start

### Prepare the database
I have prepared a create script that you can find it on Scripts folder. You can run it to create the database.

Also you can create the migration and apply the migration to the database to create the schema by command. 

*Note:* Because there are three dbcontext here, so you need to create three migration for each dbcontext. 

### Prepare Redis cache server
Because the app uses distributed cache (Redis) so you need to prepare the cache server. You can download Redis cache for windows at 
[https://github.com/microsoftarchive/redis](https://github.com/microsoftarchive/redis)


## API list


## Testing user
You can sign up a new account or use the available account below for testing:
- Admin: admin@mail.com/1234x@X
- User: user@mail.com/1234x@X

## Future plane to improve

- Apply validation - Not completed yet
- Apply Event Sourcing pattern
- Writing UT