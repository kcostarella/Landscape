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

        Vector3 frontBottomLeftNormal, frontTopLeftNormal, frontTopRightNormal, frontBottomRightNormal, backBottomLeftNormal, backBottomRightNormal, backTopLeftNormal, backTopRightNormal;
        Vector3 frontBottomLeft, frontTopLeft, frontTopRight, frontBottomRight, backBottomLeft, backBottomRight, backTopLeft, backTopRight;

        public Sun(Game game, Landscape landscape)
        {
            this.landscape = landscape;
            this.terrain = landscape.Terrain;
            float sunsize = terrain.max / 4;

            frontBottomLeftNormal = new Vector3((-0.333f - 10) * sunsize, (-0.333f * sunsize) , (-0.333f - 10) * sunsize);
            frontTopLeftNormal = new Vector3((-0.333f - 10) * sunsize, (0.333f * sunsize), (-0.333f - 10) * sunsize);
            frontTopRightNormal = new Vector3((0.333f - 10) * sunsize, ( 0.333f * sunsize), (-0.333f - 10) * sunsize);
            frontBottomRightNormal = new Vector3((0.333f - 10) * sunsize, ( -0.333f * sunsize), (-0.333f - 10) * sunsize);
            backBottomLeftNormal = new Vector3((-0.333f - 10) * sunsize, ( -0.333f * sunsize), (0.333f - 10) * sunsize);
            backBottomRightNormal = new Vector3((0.333f - 10) * sunsize, ( -0.333f * sunsize), (0.333f - 10) * sunsize);
            backTopLeftNormal = new Vector3((-0.333f - 10) * sunsize, ( 0.333f * sunsize), (0.333f - 10) * sunsize);
            backTopRightNormal = new Vector3((0.333f - 10) * sunsize, ( 0.333f * sunsize), (0.333f - 10) * sunsize);

            frontBottomLeft = new Vector3((-1.0f - 10) * sunsize, ( -1.0f * sunsize), ( -1.0f - 10) * sunsize);
            frontTopLeft = new Vector3((-1.0f - 10) * sunsize, ( 1.0f * sunsize), ( -1.0f - 10) * sunsize);
            frontTopRight = new Vector3((1.0f - 10) * sunsize, ( 1.0f * sunsize), ( -1.0f - 10) * sunsize);
            frontBottomRight = new Vector3((1.0f - 10) * sunsize, ( -1.0f * sunsize), ( -1.0f - 10) * sunsize);
            backBottomLeft = new Vector3((-1.0f - 10) * sunsize, ( -1.0f * sunsize), ( 1.0f - 10) * sunsize);
            backBottomRight = new Vector3((1.0f - 10) * sunsize, ( -1.0f * sunsize), ( 1.0f - 10) * sunsize);
            backTopLeft = new Vector3((-1.0f - 10) * sunsize, ( 1.0f * sunsize), ( 1.0f - 10) * sunsize);
            backTopRight = new Vector3((1.0f - 10) * sunsize, ( 1.0f * sunsize), ( 1.0f - 10) * sunsize);

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
