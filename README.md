# Inimco Exercise — Person Analyzer (Job Application)

This repository contains a small full‑stack application implemented as a job application assignment for the company **Inimco**.  
The app demonstrates a simple workflow: a frontend form collects person data, sends it to a backend API which performs a name analysis (vowel/consonant counts, reversed full name), and persists the submitted person to a JSON file.

## High level
- Backend: ASP.NET Core (.NET 8) Web API that exposes an analyze endpoint and persists submitted persons.
  - Main entry: [backend/Program.cs](backend/Program.cs)  
  - Analyze controller: [`backend.Controllers.PersonController`](backend/Controllers/PersonController.cs) — POST /api/person/analyze
  - Business logic: [`backend.Services.PersonService`](backend/Services/PersonService.cs)
  - Persistence: [`backend.Repository.FileRepository`](backend/Repository/FileRepository.cs) (writes to [backend/people.json](backend/people.json))
  - Models: [`backend.Models.Person`](backend/Models/Person.cs), [`backend.Models.PersonAnalysis`](backend/Models/PersonAnalysis.cs), [`backend.Models.SocialAccount`](backend/Models/SocialAccount.cs)
  - Dev helpers: Swagger is enabled in development (see [backend/Properties/launchSettings.json](backend/Properties/launchSettings.json)).

- Frontend: Angular 18 standalone component application that posts person data to the API and displays results.
  - App bootstrap: [frontend/src/main.ts](frontend/src/main.ts)
  - Main component: [frontend/src/app/app.component.ts](frontend/src/app/app.component.ts)
  - Person form: [`PersonFormComponent`](frontend/src/app/component/person-form/person-form.component.ts)
  - Frontend service calling API: [frontend/src/app/service/person.service.ts](frontend/src/app/service/person.service.ts)
  - Shared types: [frontend/types.ts](frontend/types.ts)
  - Dev proxy (routes /api → backend): [frontend/proxy.conf.json](frontend/proxy.conf.json)
  - Scripts: see [frontend/package.json](frontend/package.json)

## What the app does
1. User fills a person form (first name, last name, social skills, social accounts) in the frontend.
2. Frontend posts the person JSON to POST /api/person/analyze.
3. Backend validates the input, performs analysis:
   - computes full name and reversed full name,
   - counts vowels and consonants in the name (see [`backend.Services.PersonService`](backend/Services/PersonService.cs)).
4. Backend returns the analysis and appends the person to [backend/people.json](backend/people.json) via [`backend.Repository.FileRepository`](backend/Repository/FileRepository.cs).

## Run (development)
Backend
1. Open a terminal in the `backend` folder.
2. Run:
   dotnet run
3. In development profile Swagger UI will be available (see [backend/Properties/launchSettings.json](backend/Properties/launchSettings.json)).

Frontend
1. Open a terminal in the `frontend` folder.
2. Install dependencies if needed:
   npm install
3. Start the dev server:
   npm start
   - The frontend uses [frontend/proxy.conf.json](frontend/proxy.conf.json) to forward /api requests to the backend (configured for the backend dev HTTPS port). Adjust if your backend runs on a different URL.

## Tests
- Frontend unit tests (Karma/Jasmine): run `npm test` in `frontend` (see [frontend/package.json](frontend/package.json)).
- Backend: no tests included in this repository.

## Notes and caveats
- The backend repository persists persons by appending to a JSON file using [`backend.Repository.FileRepository`](backend/Repository/FileRepository.cs). The repository tries to gracefully handle empty or malformed JSON.
- CORS is enabled permissively in [backend/Program.cs](backend/Program.cs) for development convenience.
- This is an example application implemented as a solicitation assignment for Inimco.
