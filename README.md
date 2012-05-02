byteblog
========

A blog CMS written in C#, Backbone.js, ASP.NET MVC 3, and RavenDB.

The frontend is managed largely with Backbone.js, though non-REST abuses have been made when talking to the API (Due to its non REST-ful nature. I need to correct that.)

The C# app is written in ASP.NET MVC 3, using MvcContrib portable areas (embedded views and encapsulated functionality in DLLs; in this case 'rendering' and 'editorial' are totally seperate and hang off the application core.)

The backing data store is RavenDB.

At some point my goal is to switch this to ASP.NET MVC 4, turn that into a RESTful API, and just have Backbone.js deal with that. 

@benlakey


