# 2021Fall-SQLScaffolding

## Problem Statment
This application is designed to help make learning SQL (specifically, SQLite) easier, and to also provide a more convienent way to edit questions.

## Navigation
- [2021Fall-SQLScaffolding Development and Usage](ScaffoldingSQLProject-master/README.md)
- [File Format](ScaffoldingSQLProject-master/FILEFORMAT.md)

## Installation
- Refer to [2021Fall-SQLScaffolding Development and Usage](ScaffoldingSQLProject-master/README.md) for instructions on compiling. 
- Once you have compiled the application, run the executable file (Such as ScaffoldSQLProject.exe) and run it. No intstall required.

## Outstanding Issues
- Lack of gamification 
    * Refer to [File Format](ScaffoldingSQLProject-master/FILEFORMAT.md) for ideas on a story based system.
- Firebase server security
    * Currently, all users who have signed in anonymously have read and write access. This could be a security issue.
- Database page does not report errors on files that do not follow SQLite encoding on upload and does not report errors from the C# layer. This should be rectafied.
- There is not unit testing for the FileController ASP.net API (there is some for the C# layer, though).
- There is not unit testing for the QuestionMaker razor page.
- There is not a banner notifying the user to enable JavaScript.
- Documentation needs major improvement.
