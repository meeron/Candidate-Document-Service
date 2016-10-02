# Candidate Document Service

## Installation
1. Download and install .NET Core from https://www.microsoft.com/net/core
2. Clone repository
3. Enter command line and navigate to directory containing cloned project
4. Run `dotnet restore`
5. Enter to `CDS.WebApi/` directory
6. Run `dotnet run`
7. Application is avaible on address: `http://localhost:5000/`

## Api
1. GET `api/CandidateDocuments/{cid}?apikey={apikey}` - Lists candidate's all documents where `{cid}` is candidate id. `{apikey}` defined an api key used for authentication.
2. POST `api/CandidateDocuments/{cid}?apikey={apikey}&dt={dt}&eid={eid}` - upload document for candidate `{cid}` with document type `{dt}` and optional employer id `{eid}` using `{apikey}` for authentication.

## Why this technologies
1. Asp.Net Core
  * Can run on Linux
  * Module configuration allow choose only those components which are be used. Perfect for building micro services
  * Can run on Linux
  * Source codes are available
2. MongoDb
  * No schema needed
  * Great solution for file storage along with their metadata
  * Can run on Linux
