#!/bin/bash

rm -rf report/allure-results

projects=("money" "syntax")

for project in "${projects[@]}"
do
    cd "${project}" || exit
    pwd
    rm -rf bin/ obj/
    dotnet clean
    dotnet restore
    dotnet build
    dotnet test
    cd ..
done

allure serve report/allure-results/
