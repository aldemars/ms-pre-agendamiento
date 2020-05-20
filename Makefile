me-happy:
		dotnet test
up:
	docker-compose -f ./ms-pre-agendamiento/docker-compose.yml up --build -d
down:
	docker-compose -f ./ms-pre-agendamiento/docker-compose.yml down