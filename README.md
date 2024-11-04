# CIT24_ProjectPortfolio
PRIORITIZED LIST OF THINGS WE NEED TO DO. 
KEEP IT UPDATED.


#NeedToHave
Read Domain models and finish up - Hassan
Write about authentication - Jacob
Finish section A.3 JSON - Jeff
TALK ABOUT URI, PAGING STUFF - Hassan
Finish section Testing - Mark
Ensure all diagram are updated to current state of project
Nice rapport setup
Read endpoints chapter through


#NiceToHave
Authentication for User Rating and Search History!
Controller for Search History
All names for methods in Controllers should be same convention AKA "Post()/Get()/Delete()/Put()". Meaning no "GetAllUserRatings()".  
In Datalayer ensure if equating between strings use .Equals() and if equating between ints use ==
In WebAPI>Controllers for all Create/update methods return relevant object (has to be displayed with link).
Research if all parts of CRUD operations in Datalayer is required to use await and toAsync for the operation to be wholly async. 
If yes: All CRUD operations in Datalayer should be made into the async version of the operation.
SpawnDTO needs to overloaded to take single entities and lists of entities
Function find_similar_movies() from database can return doublicates titles - Are done!


#Database:
Add Rating.customer_ID as FK to customer 
Rating.rating needs to be limited to 10 max, currently 99.9 is possible rating.
Add triggers for all tables containing Updated_at field. 
Ensure all FK's cascade on delete as appropriate.
