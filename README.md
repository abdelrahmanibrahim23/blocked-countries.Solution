Blocked Countries Management API (.NET 8)
This API allows blocking/unblocking countries (permanently or temporarily), verifying IP addresses against those blocks, and logging access attempts using in-memory storage and a third-party geolocation API (e.g., IPGeolocation.io or ipapi.co). Built with .NET 8.
Technologies Used
•	ASP.NET Core Web API (.NET 8)
•	In-Memory Data Store (no database)
•	HttpClient for geolocation
•	BackgroundService for temporal block cleanup
•	AutoMapper
•	Minimal external dependencies
 Endpoints:

 1. Check If IP is Blocked
GET /api/ip/check-block
 Description:
•	Gets the caller’s public IP from HttpContext.
•	Fetches country info via third-party geolocation API.
•	Verifies if the country is:
o	Permanently blocked
o	Temporarily blocked
•	Logs the attempt with timestamp, user agent, IP, and block status.
 Example (Postman):
GET http://localhost:7000/api/ip/check-block
Optional header for IP testing:
Key: X-Forwarded-For
Value: 8.8.8.8
 Response:
{
  "ip": "8.8.8.8",
  "countryCode": "US",
  "isBlocked": true
}
 2. Temporarily Block a Country
POST /api/countries/temporal-block
 Description:
•	Temporarily blocks a country for a specific time (1–1440 minutes).
•	Duplicate temporary blocks are not allowed.
•	Invalid country codes are rejected.
•	A background service unblocks expired entries every 5 minutes.
 Request Body:
{
  "countryCode": "RU",
  "durationMinutes": 120
}
 Conflict Response (409):
{
  "message": "Country RU is already temporarily blocked."
}
 3. Get All Blocked Attempts
GET /api/logs/blocked-attempts?page=1&pageSize=10
 Description:
•	Returns paginated list of all access attempts.
•	Each entry includes:
o	IP
o	Timestamp (UTC)
o	Country code
o	User Agent
o	IsBlocked (true/false)
 Response:
{
  "total": 25,
  "data": [
    {
      "ip": "8.8.8.8",
      "countryCode": "US",
      "timestamp": "2025-05-17T12:34:56Z",
      "isBlocked": true,
      "userAgent": "PostmanRuntime/7.29.2"
    }
  ]
}

 4. Permanently Block a Country
POST /api/countries/block
 Request Body:
{
  "countryCode": "IR",
   "CountryName":"Egypt",
   "IsBlock":false
}
5. Unblock a Country
DELETE /api/countries/block/{code}
Notes
•	Use services like https://ipapi.co/json or https://ipgeolocation.io for geolocation.
•	Background service is implemented with BackgroundService in .NET 8 and runs every 5 minutes.
BackgroundService
•	Removes expired temporary blocks automatically.
•	Runs every 5 minutes using PeriodicTimer.


