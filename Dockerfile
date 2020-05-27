FROM flyway/flyway:6.4-alpine
COPY ./ms-pre-agendamiento/db/migrations/* /flyway/sql/
COPY ./ms-pre-agendamiento.Tests/db/migrations/* /flyway/sql/