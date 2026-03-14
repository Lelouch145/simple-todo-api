# Simple Todo API

A simple REST API built with ASP.NET Core Minimal API to practice backend development fundamentals.

This project was created to learn how APIs work, how endpoints are created, and how data can be sent and retrieved using HTTP methods.

## Features

- Get all tasks
- Get a task by ID
- Add a new task
- Delete a task
- Test the API using Swagger UI

## Endpoints

GET /tasks  
Returns all tasks.

GET /tasks/{id}  
Returns a specific task by id.

POST /tasks  
Adds a new task.

Example body:

{
  "id": 4,
  "title": "Study ASP.NET Core"
}

DELETE /tasks/{id}  
Deletes a task by id.

## Technologies Used

- C#
- .NET
- ASP.NET Core Minimal API
- Swagger / OpenAPI

## Run the project

Run the API with:

dotnet run

Then open Swagger in your browser to test the endpoints.

## Notes

Tasks are stored in memory, meaning they reset when the server restarts.
