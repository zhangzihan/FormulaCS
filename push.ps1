# Login to https://www.nuget.org/account/apikeys and generate a new api key
.\pack.p1
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey [insert your api key here]]