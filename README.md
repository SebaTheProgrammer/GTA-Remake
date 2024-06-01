# GTA-Remake in 6 weeks
This is my own remake of GTA named Street Hustle: Survival Struggle. You can look though all the code, or read here some of my biggest problems and how I solved them!
If you want to play the game, please click here: 
https://s-tiergames.itch.io/street-hustle-survival-struggle
(You can not reuse any of this code in any sort of way.)

# A Simple, Reusable and Expendable Quest-System:
Due to a lot of time shortage, I needed to come up with a fast, simple, reusable and expendable quest system.

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

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/472ad381-0df4-4e62-8680-77b2519659bd)

''' //Behavior
                                if (!QuestsInstance.Instance.HasActiveQuest()&& !m_NPCQuest.HasDoneQuest())
                                {
                                    QuestsInstance.Instance.StartQuest();

                                    m_NPCQuest?.StartQuest();
                                    m_NPCSHp?.SetNoRespawn();

                                    if (m_NPCQuest.IsAngry())
                                    {
                                        m_NPCSHp?.GetQuestAngry();
                                        m_NPCBehaviour?.ChangeAttackOrRun(0);
                                    }
                                    else if (m_NPCQuest.IsScared())
                                    {
                                        m_NPCSHp?.GetQuestAngry();
                                        m_NPCBehaviour?.ChangeAttackOrRun(1);
                                    }
                                    else
                                    {
                                        //neutral
                                    }'''

But how do I do the dialogues? After some time thinking, I came on the idea of using instances. It's fast and expendable, the only downside is, it can quickly become a spaghetti...

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/cf69adfc-a262-46ae-9297-531ed339e58b)

![image](https://github.com/SebaTheProgrammer/GTA-Remake/assets/119673781/8a88a421-b914-4874-a6ba-eaefc4f7a805)

 I used some folder structer, but still, it's not that great. Definitly something to research later on!

 # Traffic
 


