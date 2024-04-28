**Run Commands**

https://localhost:7037/CreatePerson
{
  "personName": "Jonas",
  "age": 26,
  "phoneNumber": 234567890
}

https://localhost:7037/CreateInterest
{
  "personId": 1,
  "interestName": "gaming",
  "interestDescription": "gaming late nights"
}

https://localhost:7037/CreateLink
{
  "interestId": 1,
  "webbsite": "https://store.steampowered.com/"
}

https://localhost:7037/GetLinks/1

https://localhost:7037/GetInterests/1

https://localhost:7037/GetAllPersons
