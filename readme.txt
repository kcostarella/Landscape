TERRAIN:
A standard recursive version of the Diamond Square algorithm is used in
HeightMap.cs to generate the terrain. an object of type HeightMap has several
useful fields and methods for retrieving information that is used to draw the
landscape. It can get heights at a given x,z coordinate, and can also return
vertex normals for a vertex at position x,z as well. When a HeightMap object
is instantiated, it precolors each vertex using a slightly non-deterministic
algorithm (areas between color changes are colored 'randomly' between two 
colors).


MOVEMENT:
All the camera navigation can be done by manipulating the lookat method, and so
we created class variables for the position, target, and up as parameters,
all named with the prefix "current", and are manipulated every time the mouse 
is moved or keyboard is pressed. 

First, we implemented movement for moving forward, backward, and sideways. 
While moving forward and backwards only require moving the position of the
lookat function, moving the target with it can ensure that we will not move
past the target and end up looking backwards. 

Moving left and right requires moving the position and target in order to look
at the same direction. We therefore apply a normalized directional vector
change on both the position and target for all four movements. 

The collision is also done within the "arrow" movements. If the current
movements causes it to collide with any polygon on the terrain
or goes out of bound, we reverse the movement. We also "slowed down" movement
in water.

Yaw, pitch, and roll are done with RotationYawPitchRoll(). Yaw and pitch are
affected by the X and Y mouse movements and, since we're not moving, only
affects the target. We store the previous X and Y state and move based on
the difference from the previous mouse state. We scale the yaw and pitch so
it'll only be 2*pi yaw and pi pitch; looking backwards will then only be
possible from turning left and right. The roll is affected by Q and E, and only
requires a transformation on the Up vector.

The light is generated in the Update method of Landscape with basicEffect.
BasicEffect DirectionalLight has three vectors. Since landscape constructor
is called every update to redraw the landscape, we apply a rotation around z
every time the constructor is called. We modify the light vectors every draw,
thus only require a constant rotation every update. Lighting doesn't require a
translation since lighting is applied all throughout one direction.

We then worked on drawing the sun. The sun is a yellow cube and is transformed
and translated during update.  Since we wanted the rotation of the sun to be in
sync with the lighting, we created a class variable named numberUpdates to
track updates, and used it to apply rotations instead of the total time.

The Ambient light is a a dimmed candlelight. Default specular lighting is used.
