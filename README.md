# MSGraphMentalAware


[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)


## The problem we're solving
These days is very common to see people strugling with depression and mental health. The _WHO (World Health Organzacion)_ said we [need to transform mental health](https://www.who.int/news/item/17-06-2022-who-highlights-urgent-need-to-transform-mental-health-and-mental-health-care)


## The solution?
Create a WebAPI to access the user's calendar and manage the comming events and reschedule if needed so he/she can take a small or long break.

## Features

- Ask the user how is feeling right now.
- Make a quick 20min meeting in theuser calendar, so he/she can take a break.
- Block the calendar with a all-day appoitment when the user say is not feelig well. 

## Presentation and demo
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/TkHRsVpxN8I/0.jpg)](https://www.youtube.com/watch?v=TkHRsVpxN8IE)


## Solution Diagram

![SolutionDiagram](./SolutionDiagram.png)


## ScreenShots


**Swagger view**  
![Swagger view](./Swagger.png)


**Office Outlook add-in panel**  
![Office Outlook add-in panel](./awareaddin.png)


**Crete short 20min event to take a break**  
![Crete short 20min event to take a break](./createEvent.png)  
  

**Out of the Office created with Graph**  
![OOO created with the graph](./OOO.png)


## TODO
- Reschedule meetings if you have an appointment collision
- Add the add-in project to work as the fronted of the project

## Ideas for version 0.2
* Track daily moods and prenset as a chart to the user so he can visualize how is 
* NOtify the manager of the user who is struglinig with his daily tasks
* Create a MS Teams bot to interact with the API
