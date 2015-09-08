using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;

namespace Project1
{
    using SharpDX.Toolkit.Graphics;
    class Sun : ColoredGameObject
    {
        VertexPositionNormalColor[] sun;
        Landscape landscape;
        HeightMap terrain;
        Vector3 currentPosition, currentTarget, currentUp;
        public Sun(Game game, Landscape landscape)
        {
            this.landscape = landscape;
            this.terrain = landscape.Terrain;
            sun = new VertexPositionNormalColor[12];
            float sunX = this.terrain.size / 2;
            float sunY = this.terrain.maxHeight + 10f;
            float sunZ = this.terrain.size / 2;
            float sunOffset = 10000.0f;
            int index = 0;
            // Front left
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX, sunY, sunZ), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++;

            // Back left.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX, sunY, sunZ + sunOffset), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);           
            index++;

            // Back right.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX + sunOffset, sunY, sunZ + sunOffset), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++;

            // Front left.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX, sunY, sunZ), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++;

            // Back right.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX + sunOffset, sunY, sunZ + sunOffset), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            // Front right.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX + sunOffset, sunY, sunZ), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            //sun from the bottom
            // Front left.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX, sunY, sunZ), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            // Back right.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX + sunOffset, sunY, sunZ + sunOffset), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            // Back left.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX, sunY, sunZ + sunOffset), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            // Front left.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX, sunY, sunZ), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            // Front right.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX + sunOffset, sunY, sunZ), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++; 

            // Back right.
            sun[index] = new VertexPositionNormalColor(new Vector3(sunX + sunOffset, sunY, sunZ + sunOffset), new Vector3(0.0f, 1.0f, 0.0f), Color.Yellow);
            index++;

            vertices = Buffer.Vertex.New(game.GraphicsDevice, sun);
            currentPosition = new Vector3(0.0f, this.terrain.maxHeight, 0.0f); //start on corner of map at highest point of terrain
            currentTarget = new Vector3(this.terrain.max, (this.terrain.get(this.terrain.max, this.terrain.max)), this.terrain.max); //looking across to other corner (same height)
            currentUp = Vector3.UnitY;
            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                VertexColorEnabled = true,
                World = Matrix.Identity,
                LightingEnabled = true
            };
            inputLayout = VertexInputLayout.FromBuffer(0, vertices);
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            var time = (float)gameTime.TotalGameTime.TotalSeconds;
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
