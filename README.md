# CIT24_ProjectPortfolio
PRIORITIZED LIST OF THINGS WE NEED TO DO. 
KEEP IT UPDATED.

#NeedToHave Code:
Fix Url for individual element like link to title or person. - DONE
Authentication: Check if Validate is even required or if Authentication tag in Controller is sufficient - DONE
Seperate DTO's into folders
Delete TitleWriterDTO.cs - DONE
Refactoring Domain Models class names to TitleModel.cs, PersonModel.cs ect. - DONE


#NeedToHave Database:
Add a search function that does not require user_id - DONE


#NeedToHave Report
Write about authentication - Jacob
TALK ABOUT URI, PAGING STUFF - Hassan
Finish section Testing - Mark
Ensure all diagram are updated to current state of project
Nice rapport setup
Read endpoints chapter through - Mark
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

