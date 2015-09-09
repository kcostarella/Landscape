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
        Landscape landscape;
        HeightMap terrain;

        public Sun(Game game, Landscape landscape)
        {
            this.landscape = landscape;
            this.terrain = landscape.Terrain;
            float sunsize = terrain.max / 4;

            Vector3 frontBottomLeftNormal = new Vector3((-0.333f * sunsize) - 10, (-0.333f * sunsize) - 10, (-0.333f * sunsize));
            Vector3 frontTopLeftNormal = new Vector3((-0.333f * sunsize) - 10, (0.333f * sunsize) - 10, (-0.333f * sunsize));
            Vector3 frontTopRightNormal = new Vector3((0.333f * sunsize) - 10, ( 0.333f * sunsize) - 10, (-0.333f * sunsize));
            Vector3 frontBottomRightNormal = new Vector3((0.333f * sunsize) - 10, ( -0.333f * sunsize) - 10, (-0.333f * sunsize));
            Vector3 backBottomLeftNormal = new Vector3((-0.333f * sunsize) - 10, ( -0.333f * sunsize) - 10, (0.333f * sunsize));
            Vector3 backBottomRightNormal = new Vector3((0.333f * sunsize) - 10, ( -0.333f * sunsize) - 10, (0.333f * sunsize));
            Vector3 backTopLeftNormal = new Vector3((-0.333f * sunsize) - 10, ( 0.333f * sunsize) - 10, (0.333f * sunsize));
            Vector3 backTopRightNormal = new Vector3((0.333f * sunsize) - 10, ( 0.333f * sunsize) - 10, (0.333f * sunsize));


            Vector3 frontBottomLeft = new Vector3((-1.0f * sunsize) - 10, ( -1.0f * sunsize) - 10, ( -1.0f * sunsize));
            Vector3 frontTopLeft = new Vector3((-1.0f * sunsize) - 10, ( 1.0f * sunsize) - 10, ( -1.0f * sunsize));
            Vector3 frontTopRight = new Vector3((1.0f * sunsize) - 10, ( 1.0f * sunsize) - 10, ( -1.0f * sunsize));
            Vector3 frontBottomRight = new Vector3((1.0f * sunsize) - 10, ( -1.0f * sunsize) - 10, ( -1.0f * sunsize));
            Vector3 backBottomLeft = new Vector3((-1.0f * sunsize) - 10, ( -1.0f * sunsize) - 10, ( 1.0f * sunsize));
            Vector3 backBottomRight = new Vector3((1.0f * sunsize) - 10, ( -1.0f * sunsize) - 10, ( 1.0f * sunsize));
            Vector3 backTopLeft = new Vector3((-1.0f * sunsize) - 10, ( 1.0f * sunsize) - 10, ( 1.0f * sunsize));
            Vector3 backTopRight = new Vector3((1.0f * sunsize) - 10, ( 1.0f * sunsize) - 10, ( 1.0f * sunsize));

            vertices = Buffer.Vertex.New(
                game.GraphicsDevice,
                new[]
                    {
                    new VertexPositionNormalColor(frontBottomLeft, frontBottomLeftNormal, Color.Yellow), // Front
                    new VertexPositionNormalColor(frontTopLeft, frontTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopRight, frontTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomLeft, frontBottomLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopRight, frontTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomRight, frontBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backBottomLeft, backBottomLeftNormal, Color.Yellow), // BACK
                    new VertexPositionNormalColor(backTopRight, backTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopLeft, backTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(backBottomLeft, backBottomLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(backBottomRight, backBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopRight, backTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopLeft, frontTopLeftNormal, Color.Yellow), // Top
                    new VertexPositionNormalColor(backTopLeft, backTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopRight, backTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopLeft, frontTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopRight, backTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopRight, frontTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomLeft, frontBottomLeftNormal, Color.Yellow), // Bottom
                    new VertexPositionNormalColor(backBottomRight, backBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backBottomLeft, backBottomLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomLeft, frontBottomLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomRight, frontBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backBottomRight, backBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomLeft, frontBottomLeftNormal, Color.Yellow), // Left
                    new VertexPositionNormalColor(backBottomLeft, backBottomLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopLeft, backTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomLeft, frontBottomLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopLeft, backTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopLeft, frontTopLeftNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomRight, frontBottomRightNormal, Color.Yellow), // Right
                    new VertexPositionNormalColor(backTopRight, backTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backBottomRight, backBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontBottomRight, frontBottomRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(frontTopRight, frontTopRightNormal, Color.Yellow),
                    new VertexPositionNormalColor(backTopRight, backTopRightNormal, Color.Yellow),
});

            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                VertexColorEnabled = true,
                View = Matrix.LookAtLH(landscape.currentPosition, landscape.currentTarget, landscape.currentUp),
                Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, -10.0f, (float)terrain.size + 0.0f),
                World = Matrix.Identity,
                LightingEnabled = true
            };

            inputLayout = VertexInputLayout.FromBuffer(0, vertices);
            this.game = game;
        }

       public override void Update(GameTime gameTime)
        {
            var time = (float)gameTime.TotalGameTime.TotalSeconds;

            basicEffect.View = Matrix.LookAtLH(landscape.currentPosition, landscape.currentTarget, landscape.currentUp);
            //basicEffect.World = Matrix.RotationYawPitchRoll();
            basicEffect.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, 0.1f, (float)terrain.size + 10.0f);
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
