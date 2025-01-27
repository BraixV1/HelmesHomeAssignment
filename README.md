# HelmesHomeAssignment
This is a simple Todo monorepo with .NET backend and Next.js frontend



## Why I chose this teck stack

* I chose .NET backend because I am candidating for a .NET junior developer position :D. Also I am more knowledgable in .NET than Java/Spring altough doable would take me considerably longer. Also I like little neat things .NET has to offer that make it really pleasing language to use.
* For the frontEnd I chose Next.js(react) because in the interview I was told you use React.js more than Vue.js but I am capable of using both.
* I chose TailwindCss because I think html is more readable with Tailwind and it providess great responsive css with standards but also has customization features. Also Tailwind is pretty fast so I don't see it as performance loss by using it.


# How to run this whole project with just one command?

if your computer has docker then with the following command creates container and runs it

note: Make sure you are in the root directory (HelmesHomeAssignment) when executing the command in terminal

```bash
docker-compose up --build
```

after successful build and launch front-end should be accessable at 

http://localhost:3000


swagger should be at

http://locahost:5001/swagger


## Migration generating

if for some reason in back-end/App.Dal.EF.Migrations aren't any files or c# project can't access correct schemas use the following command:

Note: Make sure you are in the Back-end folder when executing this command

```Bash
dotnet ef migrations --project App.DaL.EF --startup-project WebApp add Initial
```


