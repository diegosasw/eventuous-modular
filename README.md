# eventuous-modular
Modular monolith with ES and CQRS using Eventuous

`WebApi` is the main project.

### Issues and How to Reproduce

#### Eventuous 0.14.1-alpha.0.7
Using generated commands, the errors are not properly handled.

To reproduce try out the following scenarios.

Scenario 1:
1. Ensure you have EventStoreDb listening on localhost 2113 or adjust `appsettings.json` accordingly.
2. Run the main project `WebApi` and access http://localhost:5001/swagger
3. Create a client with a POST on /clients/create and payload
   ```
   {
    "id": "valid",
    "displayName": "foo",
    "adminEmail": "bar"
   } 
   ```
4. Create another client with the exact same Id to force a concurrency error.

The error will be a 500 internal server error with 
``` 
{
  "type": "NullReferenceException",
  "title": "Object reference not set to an instance of an object.",
  "status": 500,
  "detail": "System.NullReferenceException: Object reference not set to an instance of an object.\n   at Eventuous.OptimisticConcurrencyException..ctor(Type aggregateType, StreamName streamName, Exception inner)\n   at Eventuous.OptimisticConcurrencyException`1..ctor(StreamName streamName, Exception inner)\n   at Eventuous.StoreFunctions.Store[T](IEventWriter eventWriter, StreamName streamName, T aggregate, Func`2 amendEvent, CancellationToken cancellationToken)\n   at Eventuous.CommandService`3.Handle[TCommand](TCommand command, CancellationToken cancellationToken)"
}
```
The expected result was an `OptimisticConcurrencyException` with status Conflict as per https://eventuous.dev/docs/application/command-api/#controller-base

Scenario 2:
1. Ensure you have EventStoreDb listening on localhost 2113 or adjust `appsettings.json` accordingly.
2. Run the main project `WebApi` and access http://localhost:5001/swagger
3. Create a client with a POST on /clients/create and payload with invalid id due to spaces not allowed
   ```
   {
    "id": "not valid",
    "displayName": "foo",
    "adminEmail": "bar"
   }
   ```
   
The error will be a 500 internal server error with
``` 
{
  "type": "InvalidClientIdException",
  "title": "Invalid client Id not valid",
  "status": 500,
  "detail": "ClientModule.ClientExceptions+InvalidClientIdException: Invalid client Id not valid\n   at ClientModule.Client.CreateClient(String id, String displayName, String adminEmail) in /media/diegosasw/data/src/sandbox/eventuous-modular/src/ClientModule/Client.cs:line 17\n   at ClientModule.ClientCommandService.<>c.<.ctor>b__0_1(Client client, CreateClient cmd) in /media/diegosasw/data/src/sandbox/eventuous-modular/src/ClientModule/ClientCommandService.cs:line 18\n   at Eventuous.HandlersMap`1.<>c__DisplayClass3_0`1.<AddHandler>b__0(TAggregate aggregate, Object cmd, CancellationToken _)\n   at Eventuous.CommandService`3.Handle[TCommand](TCommand command, CancellationToken cancellationToken)"
}
```
The expected result was an `OptimisticConcurrencyException` with status Bad Request as per https://eventuous.dev/docs/application/command-api/#controller-base

### Event Store DB
The application needs to access an instance (single or multiple node) of Event Store DB through gRPC.

For development purposes, an instance of Event Store DB with in-memory persistence can be launched as a docker container
```
docker run \
    --name esdb \
    -p 2113:2113 \
    -p 1113:1113 \
    eventstore/eventstore:22.10.0-buster-slim \
    --insecure \
    --enable-atom-pub-over-http \
    --mem-db=True
```

The connection string would be `esdb://localhost:2113?tls=false`.

Additionally, a web UI would be available at [http://localhost:2113](http://localhost:2113/web/index.html)

After first time creation, the container could be stopped with `docker stop esdb` and started again with `docker start esdb`.