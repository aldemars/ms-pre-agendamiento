me-happy:
		dotnet test

ut:
	dotnet test ms-pre-agendamiento.Tests

up:
	docker-compose -f ./ms-pre-agendamiento/docker-compose.yml up --build -d

down:
	docker-compose -f ./ms-pre-agendamiento/docker-compose.yml down