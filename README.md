# GTA-Remake in 6 weeks
This is my own remake of GTA named Street Hustle: Survival Struggle. You can look though all the code, or read here some of my biggest problems and how I solved them!
If you want to play the game, please click here: 
https://s-tiergames.itch.io/street-hustle-survival-struggle
(You can not reuse any of this code in any sort of way.)

# A Simple, Reusable and Expendable Quest-System:
Due to a lot of time shortage, I needed to come up with a fast, simple, reusable and expendable quest system.

https://github.com/SebaTheProgrammer/GTA-Remake/tree/main/Project/Assets/GTA/Behaviours/NPCS/DialogueSystems

For now there are just 3 different Quest-types
- Fighting
- Searching (For example a phone, dirty money, packeges, ect)
- Or just talking

But combined with some behaviours like scared or angry, you can make a lot of quests. 
It needed to be reusable, so I made it that you can change every parameter that you want in Unity itself:

![Screenshot 2024-05-26 151830](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/7f66d694-47ad-407f-80ab-cee3dad07713)

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/5f795d80-67ac-41d2-bc3a-c4e9388dd5d1)

![Screenshot 2024-05-26 131646](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/82b93054-56e6-4901-8abb-fa28709174b7)

![Screenshot 2024-05-26 131659](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/4e4a8d89-a1ed-4f61-bb76-2b80a6047eef)

https://github.com/SebaTheProgrammer/GTA-Remake/blob/3041300a85d7bb22b1dc27185d1fc7fb8cc60c1d/Project/Assets/GTA/Behaviours/NPCS/DialogueSystems/Quest.cs#L229C5-L290C6

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/472ad381-0df4-4e62-8680-77b2519659bd)

https://github.com/SebaTheProgrammer/GTA-Remake/blob/07c90394e038ee61165423ebdaeb193f09130c74/Project/Assets/GTA/Behaviours/NPCS/DialogueSystems/DialogueManger.cs#L62C33-L83C38

But how do I do the dialogues? After some time thinking, I came on the idea of using instances. It's fast and expendable, the only downside is, it can quickly become a spaghetti...

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/cf69adfc-a262-46ae-9297-531ed339e58b)

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/8a88a421-b914-4874-a6ba-eaefc4f7a805)

 I used some folder structer, but still, it's not that great. Definitly something to research later on!

 # Traffic
 <img width="1280" alt="Car" src="https://github.com/user-attachments/assets/f57abf12-a088-4245-b4c8-2e3ce390a770">
 
 There was no chance that I would have any time to make a smart traffic system. But what could I do?

 I first thought about how we even drive a car. What are the main rules?
 - Don't hit anyone. (Obviously)
 - Give cars comming from the right priority (In my country)
 - And stop at traffic lights (Marked as extra, if I had more time)

So, why couldn't I just make those simple behaviours? Would that work?
Well yes and no. Driving is such a complex mechanic, but the only possibility to make this work in a small time frame is to simplify it.

Every car in the real world is different, of course I need to implement that. 
In my game, every car has a different speed, acceleration/slowing speed and mass.
This was a subtle nice touch!

For now it works:

They don't hit pedastrians, other cars, or you.
They each have a different driving path.
The downside is, they turn directly 90 degrees for making a turn.
So if a car is driving there, they crash..
It's funny to see, or see them fly away because of that. It's a cartoony game so why not.

<img width="1052" alt="City" src="https://github.com/user-attachments/assets/78ccffc6-b5a1-4474-bccc-4e5031a46a62">



