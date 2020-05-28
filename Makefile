me-happy:
		dotnet test

ut:
	dotnet test ms-pre-agendamiento.Tests

it:
	docker-compose up --build -d db migrations
	dotnet test ms-pre-agendamiento.IntegrationTests
	docker-compose down

up:
	docker-compose up --build -d

down:
	docker-compose down