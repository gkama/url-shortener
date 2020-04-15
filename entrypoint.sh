#!/bin/bash
set -e

run_cmd="dotnet run"

until dotnet ef database update -p /app/url.shortener.data; do
>&2 echo "PostgreSQL server is starting up"
sleep 1
done

>&2 echo "PostgreSQL Server is up - executing command"
exec $run_cmd