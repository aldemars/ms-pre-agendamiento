me-happy:
		dotnet test
test:
	docker-compose up --build -d db migrations
	dotnet test
	docker-compose down
up:
	docker-compose -f ./ms-pre-agendamiento/docker-compose.yml up --build -d
down:
	docker-compose -f ./ms-pre-agendamiento/docker-compose.yml down