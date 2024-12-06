dotnet test --filter TestCategory=api --settings .runsettings
dotnet test --filter TestCategory=ui --settings .runsettings -- TestRunParameters.Parameter(name=\"baseUrl\", value=\"http://automationexercise.com\")
