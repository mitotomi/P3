# P3
P3 task
I do not know where this should be published, so I am writing instructions the way it should work.

1. Right click on project P3 (not solution) and choose Publish.
2. Then choose folder
3. Publish location could be changed but I prefer not to change it, so just keep "bin\Release\net6.0\publish\" and click Finish
4. Change option Delete existing files to true (click on pen next to that option, expand File publish options and check checkbox and then click Save
5. Copy all files and folders from "bin\Release\net6.0\publish\" to server

Instructions do not include setup for domain
Database could be setup via migrations or just download create.sql script and run it on sql server. After it is done, change connection string to fit Your database.
