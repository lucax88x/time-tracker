# time-tracker

Sample timetracking with these technologies:

## Backend

- cqrs
- mediatr
- ES
- DDD

## Infra

- Cassandra for ES
- TBD - some cache for QUERIES
- graphql

## Frontend

- react with typescript
- redux

## Setup

Create a cassandra image

```bash
docker run -p 9042:9042 --name cassandra cassandra
```

Execute some queries on cassandra

```bash
docker exec -it cassandra bash
cqlsh
```

Create a rejson image

```bash
docker run -p 6379:6379 --name rejson redislabs/rejson:latest
```
