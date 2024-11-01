# CIT24_ProjectPortfolio
PRIORITIZED LIST OF THINGS WE NEED TO DO. 
KEEP IT UPDATED.


#NeedToHave
Use CallSQL.cs to implement structured_string_search from DB - Are done!
Use CallSQL.cs to implement find_similar_movies from DB - Are done! - But need to be revisited later + fix the function in DB to take care of the DISTINC!
Implement linkgenerator and pagination for Title
Impelment tests for Datalayer and WebAPI
Authentication



#NiceToHave
All names for methods in Controllers should be same convention AKA "Post()/Get()/Delete()/Put()". Meaning no "GetAllUserRatings()".  
In Datalayer ensure if equating between strings use .Equals() and if equating between ints use ==
In WebAPI>Controllers for all Create/update methods return relevant object (has to be displayed with link).
Research if all parts of CRUD operations in Datalayer is required to use await and toAsync for the operation to be wholly async. 
If yes: All CRUD operations in Datalayer should be made into the async version of the operation.
SpawnDTO needs to overloaded to take single entities and lists of entities
Function find_similar_movies() from database can return doublicates titles, need to be fixed later!


#Database:
Add Rating.customer_ID as FK to customer 
Rating.rating needs to be limited to 10 max, currently 99.9 is possible rating.
Add triggers for all tables containing Updated_at field. 
Ensure all FK's cascade on delete as appropriate.
