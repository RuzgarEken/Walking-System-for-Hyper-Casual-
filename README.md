# Walking System for Hyper Casual games
This repository is a showcase for different walking mechanics in hyper casual games.

  As we all know in the hyper casual industry, the prototyping process should be fast as possible to be able to catch new trends and also not losing discovered 
revenue chances to others (sadly this is the natural situation for all competitive industries). For this purpose, we should ask ourselves at the beginning of 
each project "what assets and codes can be useful for later projects", so we can make them generic and reusable to improve our development process.

  I want to share with you simple but very effective system that used vastly in hyper casual games. Runners, decision making games, idle arcades, simulators 
and many other hyper casual subgenres use walking system commonly. So this is an excellent fit to "make it once and use for later". Lets dive in.

  Making reusable systems sounds great but it is also a challenging process but no worries, this was also challenge for developers before us and they gave us very
well-defined guide to follow,  so following these guides helps us a lot, and after spending some time, designing generic systems becomes natural way of thinking, 
and it gets easier, fun and also rewarding.

  In this demonstration and always, we will be getting the benefits of Object Oriented Programming, SOLID principles, and Component Design Pattern (as Unity relies 
on it). On the internet, there are great resources to learn these subjects properly so I will rather choose to mention them as topics for your later search. Still,
we can mention Component Design Pattern briefly for groundwork.

  Component design pattern tells us to separate all behaviors that make one entity work. So as we follow this mentality properly development process turns like 
making pieces of the puzzle to connect them later. It also gets easier over time because we already have many useful pieces from previous ones to make new ones.

  In action, I am using a class(ComponentBase) to be the base for my all components and it helps communicate between them to extending the base logic. So take 
a look at what I actually meant by extending;

![Walking](https://user-images.githubusercontent.com/38860395/178273246-62411359-ae2f-46bf-9791-a9af3862c481.gif)

  ComponentBase fires an event when it is enabled or disabled. So other components can listen this event to align their states with it. In this 
scenario, WalkingBehaviour is in the center of other components and JoystickInput component processes input to enable and pass input to WalkingBehaviour when there is active input. As you see here, there are different components for each logic, movement, rotation, animation, and input that works together. They just mind their own business without causing a mess together. In the end, this allows me to sync many components to work together, and also provide me easily replace some components to vary behaviour for other use cases.

  In the other example below, i changed the final mechanic by only changing WalkingMoveBehaviour variants. Our runner has teleport movement variant and it is 
following the cursor which is following the path. The enemy characters also have speed-based movement variant and they follow our character if it is in their 
range.

![WalkingRunner(1)](https://user-images.githubusercontent.com/38860395/178307896-3841b5c1-c4aa-480b-b68c-77c7bfb6c3d8.gif)

And another one for NavMeshAgent

![WalkingNavMeshAgent](https://user-images.githubusercontent.com/38860395/178422768-82545345-3289-434c-b949-f71c6404f4ff.gif)

It is easy to use same approach for any further requirements. In this repository you can find 5 different movement behaviour.

Extra assets used in this project: 
- BetterWaitForSeconds by yasirkula - https://gist.github.com/yasirkula/5cd2681d0cfdbf6ed4a369e1664cfb98
- MyBox by Deadcows - https://github.com/Deadcows/MyBox
- Unity Standard Assets - https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351
