# Components

## DualPantoSync
The main component. This handles all the direct communication to the firmware. You should not use any functions in this component directly. All the functionality is accessible through the following components.

## PantoHandle
There are two instances of this component attached to the Panto Prefab, the UpperHandle and the LowerHandle. These represent the two handles of the DualPanto.
Position and rotation of the handles can either be controlled by the player or by the game.
You can access each handle using `GameObject.Find("Panto").GetComponent<UpperHandle>()`. 

* `HandlePosition(Vector3 currentPosition)`  
Returns the position of the respective handle, taking obstacles into account, if in debug mode.
If you want to have a game object follow the handle you should use this in your `Update()` method:  
`transform.position = <Reference to a handle>.HandlePosition(transform.position)`

* `GetPosition()`  
Returns the current position of the respective handle, if in debug mode this will ignore obstacles, see `HandlePosition` instead.
If in debug mode, there are two options: if the handle is attached to an object, it will return that game object positions, if the handle is user controlled it will return the mouse position instead.

* `GetRotation()`  
Returns the current rotation of the respective handle.  
If in debug mode, you can control the rotation with `a` and `d`.

* `async SwitchTo(GameObject gameObject, float speed)`  
Moves the handle to the specified game object. The handle will then follow the game object until it is switched to another game object.  
:warning: Please make sure to not move the object too fast, as the Panto handle will try to follow it.

* `async MoveToPosition(Vector3 position, float speed, bool shouldFree = true)`  
Moves the handle to the specified position. The handle will be freed by default, unless otherwise specified with `shouldFree`.

* `Free()`  
Frees the handle, it can now be controlled by the player.

* `Freeze()`  
Freezes the handle in its current position.

* `ApplyForce()`
Applies a force to the handle. This will only work if the handle is not attached to an object.

## Level & ObjectOfInterest
The Level component is attached to the Panto Prefab. You can add the ObjectOfInterest component to game objects in your scene.

### ObjectOfInterest
* `int priority `**:**  Defines in which order the objects should be introduced.
* `string description `**:**  A text that will be read aloud when the object is introduced.
* `bool isOnUpper `**:**  Which handle the object should be introduced on.
* `bool traceShape `**:** If this is true, the handle will not move to the game object, but instead to all its children.

### Level
* `async PlayIntroduction()`   
Finds all ObjectOfInterest in the scene, sorts them by their priority and moves the handle to each one, reading its description.


## PantoCollider
Represents an obstacle. You can choose which handle an obstacle will be registered on, by default obstacles will be registered for both the upper and the lower handle.

* `CreateObstacle()`  
Creates the obstacles in the shape as defined by the subclass, see below. Don't forget to enable the obstacle as well.

* `CreateFromCorners(Vector2[] corners)`  
Creates an obstacle from an array of corner points. You can define more complex obstacles using this function. Don't forget to enable the obstacle as well.

* `Enable()`  
Enables the obstacle. Only when this function is called, will you be able to feel it using the DualPanto.

* `Disable()`  
Disables the obstacle.

Instead of enabling the colliders immediately, you can make use of the `ObstacleSphere`. Attach it to your player components, and it will Create and Enable all obstacles when the player moves near them. This can be useful if your scene has many obstacles, that cannot be stored on the Panto at the same time. See the `ManyObstacles.unity` scene for an example.

### PantoBoxCollider
Attach this to a cube and `CreateObstacle()` will create a box obstacle using the dimensions of the BoxCollider.

### PantoCircularCollider
Attach this to a sphere and `CreateObstacle()` will create a circular obstacle using the position and radius of the SphereCollider. The obstacle, however, will only be an approximation of a sphere using the number of corners defined, 8 by default.

### PantoPolygonCollider
Use it with a Polygon2D to create more complex shapes.

### PantoLineCollider
A simple line from point A to point B. You need to set start and end, before using using `CreateObstacle()`.