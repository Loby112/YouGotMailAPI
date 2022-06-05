# YouGotMailAPI
 
 
3.Semester Mail notification system

We've built an application that detects when you get physical post in your mailbox. When you've received mail it automatically sends you an email with a notification.
In the app which is built for mobile usage you can see the latest delivery with time stamp. The message shown changes depending on how many times you've gotten post.
The app automatically updates when you empty your mailbox as well, saying there's no new mail. 

We're using a Raspberry Pi with python and an ultrasound detector to gather the data needed. We broadcast the data and gather it with an UDP proxy which forwards it to our web API as a relevant object. If the reading is considered new information in our system it's added as a new object which can be used to show there's been delivered post. 
