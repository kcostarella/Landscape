There were multiple components on camera rotation, and we made sure to apply each part separately. All the camera navigation can be done by manipulating the lookat method, and so we created class variables for the position, target, and up as parameters, all named with the prefix current, and are manipulated every time the mouse is moved or keyboard is pressed. First, we implemented movement for moving forward, backward, and sideways. While moving forward and backwards only require moving the position of the lookat function, moving the target with it can ensure that we will not move past the target and end up looking backwards. Moving left and right will require moving the position and target in order to look at the same direction. We therefore applied a normalized directional vector change on both the position and target for all four movements. The forward directional vector is determined by taking the difference of the target and the position; the opposite will yield the backward directional vector. The left and right directional vector can be evaluated by taking the cross product of the forward directional vector and the up vector or the cross product of up vector and forward directional vector, respectively. The collision is also done within the "arrow" movements. If the current movements causes it to collide with any terrains or goes out of bound, we reverse the movement. We also "slowed down" movement in water by applying a faster speed when we're above water height.

Yaw, pitch, and roll are done with RotationYawPitchRoll(), which inputs angles and returns a 4x4 matrix, which can then be passed into a function called TransformCoordinate() with the vector we need to transform and return a transformed vector. Yaw and pitch is affected by the X and Y mouse movements and, since we're not moving, only affects the target. We stored the previous X and Y state and is moved based on the difference from the previous mouse state. We scaled the yaw and pitch so it'll only be 2*pi yaw and pi pitch; looking backwards will then only possible from turning left and right. The roll is affected by Q and E, and only requires a transformation on the Up vector.

The light is generated in the Update method of Landscape with basicEffect. BasicEffect DirectionalLight has three vectors. Since landscape constructor is called every update to redraw the landscape, we used that to our advantage and applied a rotation around z every time the constructor is called. We modify the light vectors every draw, thus only require a constant rotation every update. Lighting doesn't require a translation since lighting is applied all throughout one direction. We then worked on drawing the sun. The sun is a yellow cube and is transformed and translated during update.  Since we wanted the rotation of the sun to be in sync with the lighting, we created a class variable named numberUpdates to track updates, and used it to apply rotations instead of the total time. 