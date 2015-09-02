using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;

namespace Project1
{
    using SharpDX.Toolkit.Graphics;
    class Landscape : ColoredGameObject
    {

        Vector3 currentPosition, currentTarget, currentUp;
        float prevMouseX, prevMouseY;
        HeightMap Terrain;

        public Landscape(Game game)
        {
            
            Terrain = new HeightMap(5);
            VertexPositionNormalColor[] terrain3D = new VertexPositionNormalColor[Terrain.max * Terrain.max * 6];
            Console.WriteLine(Terrain);
            int index = 0;
            for (int z = 0; z < Terrain.max; z++)
            {
                for (int x = 0; x < Terrain.max; x++)
                {
                    // Front left.
                    terrain3D[index] = new VertexPositionNormalColor(new Vector3(x, Terrain.get(x,z), z), Terrain.getVertexNormal(x,z) , Terrain.getColor(x,z));
                    index++;

                    // Back left.
                    terrain3D[index] = new VertexPositionNormalColor(new Vector3(x, Terrain.get(x, z + 1), z + 1), Terrain.getVertexNormal(x, z + 1), Terrain.getColor(x, z+1));
                    index++;

                    // Back right.
                    terrain3D[index] = new VertexPositionNormalColor(new Vector3(x + 1, Terrain.get(x + 1, z + 1), z + 1), Terrain.getVertexNormal(x + 1, z + 1), Terrain.getColor(x+1, z+1));
                    index++;
                    
                    // Front left.
                    terrain3D[index] = new VertexPositionNormalColor(new Vector3(x, Terrain.get(x, z), z), Terrain.getVertexNormal(x, z), Terrain.getColor(x, z));
                    index++;
                   
                    // Back right.
                    terrain3D[index] = new VertexPositionNormalColor(new Vector3(x + 1, Terrain.get(x + 1, z + 1), z + 1), Terrain.getVertexNormal(x + 1, z + 1), Terrain.getColor(x+1, z+1));
                    index++;

                    // Front right.
                    terrain3D[index] = new VertexPositionNormalColor(new Vector3(x + 1, Terrain.get(x + 1, z), z), Terrain.getVertexNormal(x + 1, z), Terrain.getColor(x+1, z));
                    index++;
                }
            }

            //initialized here because I wanted the terrain details to place the initial position/target.
            currentPosition = new Vector3(0.0f, (Terrain.get(0, 0)+5.0f), 0.0f); //start on corner of map at height of terrain
            currentTarget = new Vector3(Terrain.max, Terrain.get(Terrain.max, Terrain.max), Terrain.max); //looking across to the other corner
            currentUp = Vector3.UnitY;
            /*prevMouseX = ((Project1Game)this.game).mouseState.X;
            prevMouseY = ((Project1Game)this.game).mouseState.Y;*/


            //Create an Arraz of VertexPositionNormalColor objects to draw landscape
            vertices = Buffer.Vertex.New(
                          game.GraphicsDevice, terrain3D);

            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                VertexColorEnabled = true,
                View = Matrix.LookAtLH(new Vector3(0.0f, 80.0f, 0.0f ), new Vector3(0.0f,0.0f,5.0f), Vector3.UnitY),
                Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, -10.0f, Terrain.max + 10.0f),
                World = Matrix.Identity
            };
           
            inputLayout = VertexInputLayout.FromBuffer(0, vertices);
            this.game = game;
        }



        public override void Update(GameTime gameTime)
        {
            var time = (float)gameTime.TotalGameTime.TotalSeconds;

            /*move forward if W is pressed. takes the directional vector of current position and current target, normalize it, and move the currentPosition and Target that amount.
              Note: shifting currentTarget also to make sure we didn't go past the target*/
            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.W)) 
            {
                Vector3 temp = (currentTarget - currentPosition);
                temp.Normalize();
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X - 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z - 1))
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X + 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z + 1)))
                    currentPosition -= temp;
                else
                    currentTarget += temp;
            }

            /*move backward if S is pressed. takes the negative directional vector of current position and current target, normalize it, and move the currentPosition that amount.
              Note: shifting currentTarget also to make sure we didn't go past the target*/
            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.S))
            {
                Vector3 temp = (currentPosition - currentTarget);
                temp.Normalize();
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X - 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z - 1))
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X + 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z + 1)))
                    currentPosition -= temp;
                else
                    currentTarget += temp;
            }

            /*move left if A is pressed. takes the cross product of current position and current target based on right hand rule, normalize it, and move the currentPosition that amount.
              Note: shifting currentTarget to make sure we're facing the same direction*/
            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.A))
            {
                Vector3 temp = Vector3.Cross(currentTarget, currentUp);
                temp.Normalize();
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X - 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z - 1))
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X + 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z + 1)))
                    currentPosition -= temp;
                else
                    currentTarget += temp;
            }

            /*move right if D is pressed. takes the cross product of current position and current target based on right hand rule, normalize it, and move the currentPosition that amount.
              Note: shifting currentTarget to make sure we're facing the same direction*/
            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.D))
            {
                Vector3 temp = Vector3.Cross(currentUp, currentTarget);
                temp.Normalize();
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X - 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z - 1))
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X + 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z + 1)))
                    currentPosition -= temp;
                else
                    currentTarget += temp;
            }

            /*if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.Q))
            {
            }

            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.E))
            {
            }
            
            if (((Project1Game)this.game).mouseState.X != prevMouseX)
            {

            }

            if (((Project1Game)this.game).mouseState.X != prevMouseX)
            {

            }*/

            basicEffect.View = Matrix.LookAtLH(currentPosition, currentTarget, currentUp);
            basicEffect.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, 0.1f, 100.0f);
        }

        public override void Draw(GameTime gameTime)
        {
            // Setup the vertices
            game.GraphicsDevice.SetVertexBuffer(vertices);
            game.GraphicsDevice.SetVertexInputLayout(inputLayout);

            // Apply the basic effect technique and draw the rotating cube
            basicEffect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.Draw(PrimitiveType.TriangleList, vertices.ElementCount);
        }
    }
}
