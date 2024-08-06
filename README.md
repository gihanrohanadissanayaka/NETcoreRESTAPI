# Academic Modules & Study Sessions API

This ASP.NET Core Web API provides CRUD operations for managing academic modules and study sessions. It is designed to help track study activities and organize academic resources.

## Features

- Modules Management
  - Create, Read, Update, and Delete academic modules.
  - Module code and name validation to prevent duplicates.

- Study Sessions Management
  - Create, Read, Update, and Delete study sessions.
  - Filter study sessions by date range, module, and session type.

## Technologies Used
- Back End: ASP.NET Core
- Database: (Add your database here, e.g., SQL Server, PostgreSQL, etc.)
- Other Tools: Swagger for API documentation

## API Endpoints
### Modules
- Get All Modules: GET /api/modules
- Get Module by ID: GET /api/modules/{id}
- Create Module: POST /api/modules
- Update Module: PUT /api/modules/{id}
- Delete Module: DELETE /api/modules/{id}

### Study Sessions
- Get All Study Sessions: GET /api/studySessions
- Get Study Session by ID: GET /api/studySessions/{id}
- Create Study Session: POST /api/studySessions
- Update Study Session: PUT /api/studySessions/{id}
- Delete Study Session: DELETE /api/studySessions/{id}
- Filter Study Sessions: GET /api/studySessions/filter?startDate={startDate}&endDate={endDate}&moduleId={moduleId}&sessionType={sessionType}
