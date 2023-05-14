# TempleRunGameUsingSensors
A Temple-Run-like game in which the player's transitions between tracks in the game are made by FSR sensors.

Authors: Anna Seliverstov, Nitzan Nahshon

Demo video:
https://www.youtube.com/watch?v=nUPt6BKYqKY&t=6s

A final project in the HCI course at the university, which was done as a donation for the ALYN Hospital in Jerusalem. 
ALYN Hospital is a rehabilitation center for children and youth. They specializes in diagnosing and rehabilitating 
children suffering from a wide range of congenital and acquired physical disabilities.

The goal of the project is to get the child to walk in a balanced way that is, to step with equal intensity on both 
legs while walking. 
The project consists of a physical and virtual part. 
The virtual part is a Temple Run-like game. 
The physical part consists of 2 touch sensors attached to the child's legs that are connected to a hardware component - 
Arduino that the child attaches to his pants. The Arduino is connected to the computer.

When the child walks in a balanced manner on his two legs, the avatar running in the game runs in the middle lane and 
manages to collect the coins. However, when the child walks in an unbalanced shape, i.e., inclined to the right or left, 
the avatar in the game runs in the right or left lane depending on the child's inclination and misses the coins in the 
middle lane. 
If the child walks long enough in a balanced way, he rises to the next level in the game. In addition, there is 
identification of child's walking speed, meaning that the faster the child walks, the faster the avatar runs. 

The child's walk and speed are identified using FSR sensors, which are touch sensors attached to the child's legs – 
a sensor at each heel. The sensors are connected to the Arduino hardware component, which processes the information 
received from the sensors and transmits it to the computer in real time. 

In the code of the game, we process the information from the sensors in order to analyze the type of gait and speed of 
the child in his last steps. This requires processing and analyzing the information from the sensors in order to control 
the avatar in the game in the most accurate way and with the shortest response time. 

We programmed a software for the Arduino in the INO file. 
The INO file is a software program created for use with Arduino. It contains source code written in the Arduino programming 
language (which is a variant of C++). INO files are used to control Arduino circuit boards. 

The program reads the information from the FSR sensors in real time and checks if the child has started walking based on 
analyzing and processing the incoming information. It outputs the information received from the sensors in a specific format 
through a port defined by a Serial object.
We used a basic open-source Temple-Run game and adapted it according to our needs. We adjusted the path where the avatar runs 
so that the avatar has 3 lanes to run. We changed the position of the coins so that they appear in the middle lane. We have 
extended the play time so that there is a transition to the next stage only after enough time has passed in which the child 
walks in a balanced manner. We changed the triggers for transitions between the avatar's lanes. 
In the original game, the transitions are made by pressing the arrow keys on the keyboard, but in our game the transitions 
are made as a result of real-time analysis of the information transmitted from the sensors attached to the child's feet.

The game is built on Unity Engine and is written in C#. 

In the PlayerMovement file.cs we wrote the following functions:
1.	Start – starts the game animation and initializes the C# SerialPort object. Using this object, the port from which the 
    real-time information will come is initialized. The port we initialized is the same as the port we defined in the INO file.
3.	readPort – deploys the information received from the port to an integer.
4.	readArduino – the logic we use on the parsed information from the port to decide which route the avatar should run and 
    at what speed.
6.	RUN – runs the avatar in every frame of the game on a certain lane and at a certain speed. The walk and speed are 
    determined based on the output of readArduino.


