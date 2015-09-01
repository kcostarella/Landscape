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

        public Landscape(Game game)
        {

            HeightMap Terrain = new HeightMap(5);
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
            // Rotate the cube.
            var time = (float)gameTime.TotalGameTime.TotalSeconds;
            //basicEffect.World = Matrix.RotationX(time) * Matrix.RotationY(time * 2.0f) * Matrix.RotationZ(time * .7f);
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
