# Bowling Simulation

The game is developed using Unity 2020.3.0f1.

# About the game
The game is meant to be a simplified physic simulation of a bowling round.

# How to play
Once Unity is open go to ***Assets/Scenes/GameScene*** and play.

The player will have 2 launches to down all the ten pins.

Ball can be:

* **Moved Horizontally on launch position**: with A/D keys or left/right arrow
* **Aim**: Moving mouse pointer around the screen
* **Launched**: Clicking and holding the left mouse button to charge the force

On the top-right corner of the screen there is a **Restart** button to start a fresh game
whenever you want.

# About the Physics

To develop this game a set of API and components of the Unity Physics Engine have been
used, like:

* **Rigidbodies**;
* **Colliders**;
* **Physic Materials**;
* **Rigidbody API AddForce**

The Physic of the game does not 100% respect the Physics applied during a Bowling match.
In fact the physics behind the ball movement and the lane interaction with the ball is simplified to 
a manageable way.