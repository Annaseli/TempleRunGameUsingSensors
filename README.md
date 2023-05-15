# TempleRunGameUsingSensors
A Temple-Run-like game in which The transitions of the avatar between lanes in the game are made 
based on the data streamed from FSR sensors

Authors: Anna Seliverstov, Nitzan Nahshon



Demo video:
https://www.youtube.com/watch?v=nUPt6BKYqKY&t=6s




A final project in the HCI course at the Hebrew University of Jerusalem.
The project was made as a donation for the ALYN Hospital in Jerusalem, which specializes in rehabilitating 
children with physical disabilities.

The project aims to help children walk in a balanced manner. A balanced manner means to step with equal 
intensity on both legs while walking. 
The project consists of a physical and a virtual part. 

The physical part consists of 2 touch sensors (FSR) attached to the child's feet that are connected to an 
Arduino hardware component. The Arduion is placed in 3D printed container that we designed using
the Rhino program and printed using PrusaSlicer program and a 3D printer. 
The container is attached to the child's pants and is connected to the computer.

The virtual part is a Temple Run-like game. 
The child plays a Temple Run-like game on a computer, where the avatar's movements in the game depend on 
the child's gait and speed. When the child walks in a balanced manner, the avatar runs in the middle lane 
and collects coins, and if the child walks in an unbalanced way, the avatar runs in the right or left lane
depending on the child's inclination. Speedwise, the faster the child walks, the faster the avatar runs. 
If the child walks long enough in a balanced way, he rises to the next level in the game. 

The sensors are used to identify the child's gait and speed, which are processed by the Arduino 
and the game code to control the avatar's movements. The Arduino streams the sensor's data to the game 
in real time. The data undergoes processing, engineering, and analysis to achieve a highly precise and 
rapid determination of the avatar's movements.

We programmed a software for the Arduino in the INO file. 
The INO file is a software program created for use with Arduino. It contains source code written in the 
Arduino programming language (which is a variant of C++). 

The program combines the Arduino software that reads data from the FSR sensors and streames it to the game. 
The data is processed and outputted through a Serial object in a specific format.
The game code analyzes it in real-time to determine whether the child has started walking. 

To create the game, we adapted a basic open-source Temple-Run game by adjusting the avatar's lanes to have
three options to run and changing the position of the coins to appear in the middle lane. We also extended
the game time so that the child can transition to the next level only after walking in a balanced manner
for enough time. We altered the triggers for transitioning between lanes so that the game responds to the
real-time analysis of data streamed from the sensors attached to the child's feet. In contrast
to the original game, in which transitions are made by pressing the arrow keys on the keyboard, our game
uses the sensor data to determine when to transition between lanes.

The game is built on Unity Engine and is written in C#. 

In the PlayerMovement file.cs we wrote the following functions:
1.	Start – starts the game animation and initializes the C# SerialPort object. Using this object, the port 
    from which the real-time information will come is initialized. The port we initialized is the same as 
    the port we defined in the INO file.
3.	readPort – converts the information received from the port to an integer.
4.	readArduino – the logic we use on the parsed information from the port to decide which route the avatar 
    should run and and at what speed he should run.
6.	RUN – runs the avatar in every frame of the game on a certain lane and at a certain speed. The walk 
    and speed are determined based on the output of readArduino.


