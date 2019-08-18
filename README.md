# AppSheetChallenge
The frontend can be found here: http://appsheet-dev.us-east-1.elasticbeanstalk.com/
The primary API endpoint is http://appsheet-dev.us-east-1.elasticbeanstalk.com/api/Person
The list method is at http://appsheet-dev.us-east-1.elasticbeanstalk.com/api/Person/list
The detail method is at http://appsheet-dev.us-east-1.elasticbeanstalk.com/api/Person/detail/1

Extra Credit: 
-To automatically test the app I would set up deployment pipelines to run tests when code is committed to make sure all of the tests are passing and that a certain level of code coverage is achieved by the tests (~80%). Then and only then can the code be deployed to production.
-If the data set was 3 OEMs larger, the data source should be in a persistent medium like a SQL database instead of a file. When the data needs to be accessed queries would be made to access specific portions of the data, instead of loading it all into memory at once and searching through it that way.
