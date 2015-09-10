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

        public Vector3 currentPosition, currentTarget, currentUp;
        float prevMouseX, prevMouseY;
        public HeightMap Terrain;
        VertexPositionNormalColor[] terrain3D;
        float waterHeight;
        public Matrix lightRotation;
        public float lightRotationOffset;
        public Landscape(Game game)
        {            
            Terrain = new HeightMap(10);
           
            terrain3D = new VertexPositionNormalColor[Terrain.max * Terrain.max * 6  + 12];
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


            Color blue = Color.MidnightBlue;
            blue.A = 0xC0;

            waterHeight = 0.695f * Terrain.maxHeight;

            //Set Water Polygons
            // Front left.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(0.0f, waterHeight, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Back left.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(0.0f, waterHeight, Terrain.max), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Back right.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(Terrain.max, waterHeight, Terrain.max), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Front left.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(0.0f, waterHeight, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Back right.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(Terrain.max, waterHeight, Terrain.max), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Front right.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(Terrain.max, waterHeight, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            //water from the bottom
            // Front left.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(0.0f, waterHeight, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Back right.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(Terrain.max, waterHeight, Terrain.max), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Back left.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(0.0f, waterHeight, Terrain.max), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Front left.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(0.0f, waterHeight, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Front right.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(Terrain.max, waterHeight, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            // Back right.
            terrain3D[index] = new VertexPositionNormalColor(new Vector3(Terrain.max, waterHeight, Terrain.max), new Vector3(0.0f, 1.0f, 0.0f), blue);
            index++;

            //initialized here because I wanted the terrain details to place the initial position/target.
            currentPosition = new Vector3(0.0f, Terrain.maxHeight, 0.0f); //start on corner of map at highest point of terrain
            currentTarget = new Vector3(Terrain.max, Terrain.maxHeight, Terrain.max); //looking across to other corner (same height)
            currentUp = Vector3.UnitY;
            prevMouseX = 0.5f;
            prevMouseY = 0.5f;
            

            vertices = Buffer.Vertex.New(game.GraphicsDevice, terrain3D);

            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                VertexColorEnabled = true,
                View = Matrix.LookAtLH(currentPosition, currentTarget, currentUp),
                Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, -10.0f, (float)Terrain.size * 2),
                World = Matrix.Identity,
                LightingEnabled = true
            };

           
            inputLayout = VertexInputLayout.FromBuffer(0, vertices);
            basicEffect.EnableDefaultLighting();
            
            basicEffect.AmbientLightColor = new Vector3(.01f * 255/255, .01f * 244/255, .01f * 229/255);
          
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
                if (currentPosition.Y > waterHeight)
                    temp *= 2; //move slower if underwater (cuz Mat said so)

                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0 || currentPosition.X < 0
                    || currentPosition.X > Terrain.max || currentPosition.Z > Terrain.max
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
                if (currentPosition.Y > waterHeight)
                    temp *= 2; //move slower if underwater (cuz Mat said so)
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.X > Terrain.max || currentPosition.Z > Terrain.max
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
                if (currentPosition.Y > waterHeight)
                    temp *= 2; //move slower if underwater (cuz Mat said so)
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.X > Terrain.max || currentPosition.Z > Terrain.max
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
                if (currentPosition.Y > waterHeight)
                    temp *= 2; //move slower if underwater (cuz Mat said so)
                currentPosition += temp;
                if (currentPosition.X < 0 || currentPosition.Y < Terrain.get((int)currentPosition.X, (int)currentPosition.Z) || currentPosition.Z < 0
                    || currentPosition.X > Terrain.max || currentPosition.Z > Terrain.max
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X - 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z - 1))
                    || currentPosition.Y < Terrain.get((int)(currentPosition.X + 1), (int)(currentPosition.Z)) || currentPosition.Y < Terrain.get((int)(currentPosition.X), (int)(currentPosition.Z + 1)))
                    currentPosition -= temp;
                else
                    currentTarget += temp;
            }

            float Yaw = ((float)Math.PI * 2 * (((Project1Game)this.game).mouseState.X - prevMouseX));
            prevMouseX = ((Project1Game)this.game).mouseState.X;

            float Pitch = (float)Math.PI * (((Project1Game)this.game).mouseState.Y - prevMouseY);
            prevMouseY = ((Project1Game)this.game).mouseState.Y;

            float Roll = 0.0f;
            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.Q))
                Roll += 0.1f;
            if (((Project1Game)this.game).keyboardState.IsKeyDown(SharpDX.Toolkit.Input.Keys.E))
                Roll -= 0.1f;

            Matrix translation = Matrix.RotationYawPitchRoll(Yaw, Pitch, 0);
            currentTarget = Vector3.TransformCoordinate(currentTarget, translation);

            translation = Matrix.RotationYawPitchRoll(0, 0, Roll);
            currentUp = Vector3.TransformCoordinate(currentUp, translation);

            basicEffect.View = Matrix.LookAtLH(currentPosition, currentTarget, currentUp);
            basicEffect.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, 0.1f, (float)Terrain.size * 2);
            lightRotationOffset = .01f;
            lightRotation = Matrix.RotationZ(lightRotationOffset);
            basicEffect.DirectionalLight0.Direction = Vector3.TransformCoordinate(basicEffect.DirectionalLight0.Direction, lightRotation);
            basicEffect.DirectionalLight1.Direction = Vector3.TransformCoordinate(basicEffect.DirectionalLight1.Direction, lightRotation);
            basicEffect.DirectionalLight2.Direction = Vector3.TransformCoordinate(basicEffect.DirectionalLight2.Direction, lightRotation);

            //basicEffect.DirectionalLight0.SpecularColor += new Vector3(-offset, -offset, -offset);
            //basicEffect.DirectionalLight0.SpecularColor += new Vector3(-offset, -offset, -offset);
            //basicEffect.DirectionalLight0.SpecularColor += new Vector3(-offset, -offset, -offset);
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
