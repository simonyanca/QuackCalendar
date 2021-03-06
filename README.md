# QuackCalendar
ASP.NET Core web app using Razor pages interfacing with a SQL backend.

The SQL functionality is performed via a service using the domain-facade pattern. It is dependency-injected into the web application via ASP.NET. The only current SQL gateway is via MySQL, but because a service locator pattern was used for determining which gateway to use, adding additional SQL support just requires a new service locator to new-up the new gateway.

This is the main calendar view, where the month is drawn dynamically based on where the days of that month fall:
![alt text](https://github.com/jefft22/QuackCalendar/blob/master/Images/qcal-1.gif)

When you click an event on the calendar, you can view the details of it. You can then edit, delete, or return to the month view:
![alt text](https://github.com/jefft22/QuackCalendar/blob/master/Images/qcal-2.gif)

If you chose to edit an event, you can change the name, description, start time, and end time. Clicking save will update the event in the database, or cancel will return to the month view:
![alt text](https://github.com/jefft22/QuackCalendar/blob/master/Images/qcal-3.gif)
