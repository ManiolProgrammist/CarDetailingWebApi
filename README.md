# CarDetailingWebApi
backend for CarDetailing web application.

Example of backend for web application.
Features

Functional account creation with full encryption of password, First created user will have Admin privileges. 

CRUD for orders, ordersTemplates and users.

Comments/notes for orders, 

Authorization for api routes by Roles (Admin/Employee/User/TemporaryUser).

PayU support for EU transactions example

Sending email with informations abaut new accounts, sending email for orders
(change default example login and password in UtilityService.cs).






Entire architecture is divided by layers for optimal testing and possible fast changes of modules, all needed modules 
are injected by NINJECT lib. 

Entire install instruction for Frontend and backend (PL) in "Instrukcja obsługi aplikacji.md" file.


Example of frontend for this api:
https://github.com/ManiolProgrammist/CarDetailingAngular
