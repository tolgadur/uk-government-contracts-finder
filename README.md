# UK Government Contracts Finder
This is a website/api that lets search for contracts published by the UK government. The tool offers basic capability of searching for keywords that are contained by the description of the contract

## Website Wrapper
The website is a very simple wrapper for the api. You can type in the keywords that you want to search for and a list of all contracts that match are displayed. Unfortunately, I did not have time for pagination so the website will be quite slow.

![Screenshot](Screenshot.PNG?raw=true)

## API User Guide
The gateway works has two simple endpoints. The first fetches all contracts published by the UK government and saves them in a simple SQLite database:
```
REQUEST:
curl --location --request GET 'https://localhost:44382/contracts/save/year'

RESPONSE:
Contracts saved
```


The second and more important one lets the user search for keywords that are mentioned in the description of the published contract. Unfortunately, I did not have time for things such as elasticsearch, pagination etc. that would have improved the API tremendously. 
```
REQUEST:
curl --location --request POST 'https://localhost:44382/contracts/search' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Description": "fire"
}'

RESPONSE:
{
    {
    "matchingContracts": [
        {
            "id": "69fbaf85-a546-47e2-a537-e88e04b6cbc4",
            "title": "GB-Poole: DH952 Bridport Fire Detection Works",
            "publishedDate": "2020-11-26T08:05:04+00:00",
            "organisationName": "BIP SOLUTIONS LIMITED",
            "deadlineDate": "2020-11-24T00:00:00+00:00",
            "awardedDate": "2020-11-24T00:00:00+00:00",
            "description": "Fire Alarm replacement at Bridport Community Hospital"
        },
        {
            "id": "018e96ac-d4d5-499a-9b89-7e592358862e",
            "title": "Camden Property Works Framework",
            "publishedDate": "2020-11-25T15:00:41+00:00",
            "organisationName": "EU SUPPLY LIMITED",
            "deadlineDate": "2020-03-27T12:00:00+00:00",
            "awardedDate": "2019-11-19T00:00:00+00:00",
            "description": "A framework agreement for the delivery of construction and property works (including \r\nconstruction and and mechanical and electrical works) for   
            the Property Management and Development Divisions of the Supporting Communities Directorate, but also for use by the Council&#039;s other Directorates and Divisions.
            An option for London Borough of Islington to participate in this framework is retained. \r\n\r\nIn total there are 7 seven lots. The lots are:\r\nLot 1 - General
            building improvement works\r\nLot 2 - Cyclical decorations and repairs\r\nLot 3 - Fire safety improvement and upgrade works\r\nLot 4 - Life replacement and major
            upgrades\r\nLot 5 - Electrical system replacement and major upgrades\r\nLot 6 - Heating and water management system replacement and major upgrades\r\nLot 7 - New
            build and regeneration"
        },
        [...]
}
```
