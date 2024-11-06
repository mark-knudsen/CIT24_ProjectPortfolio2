# CIT24_ProjectPortfolio
PRIORITIZED LIST OF THINGS WE NEED TO DO. 
KEEP IT UPDATED.

#NeedToHave Code:
Seperate DTO's into folders

#NeedToHave Database:

#NeedToHave Report
TALK ABOUT URI, PAGING STUFF - Hassan
Nice rapport setup
Read testing chapter - Jeff 


#NiceToHave
Authentication for personBookmark, User Rating, and Search History!
Remove userId parameter from endpoints
Instead of userId to CRUD specific data, get the id from the token
Controller + Repository for Search History
Make search method in TitleController return a URL
All names for methods in Controllers should be same convention AKA "Post()/Get()/Delete()/Put()". Meaning no "GetAllUserRatings()".  
In Datalayer ensure if equating between strings use .Equals() and if equating between ints use ==
In WebAPI>Controllers for all Create/update methods return relevant object (has to be displayed with link).
Research if all parts of CRUD operations in Datalayer is required to use await and toAsync for the operation to be wholly async. 
If yes: All CRUD operations in Datalayer should be made into the async version of the operation.
SpawnDTO needs to overloaded to take single entities and lists of entities
Getting titles by genre title/genre have lost it's generater url
Bookmark should link to respective title/person, same for ratings. Need URL in DTO

