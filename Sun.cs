using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;

/** Ko Costarella and Ellie Yeung */
namespace Project1
{
    using SharpDX.Toolkit.Graphics;
    class Sun : ColoredGameObject
    {
        Landscape landscape;
        HeightMap terrain;

        Vector3 frontBottomLeftNormal, frontTopLeftNormal, frontTopRightNormal, frontBottomRightNormal, backBottomLeftNormal, backBottomRightNormal, backTopLeftNormal, backTopRightNormal;
        Vector3 frontBottomLeft, frontTopLeft, frontTopRight, frontBottomRight, backBottomLeft, backBottomRight, backTopLeft, backTopRight;
        float numberUpdates;

        public Sun(Game game, Landscape landscape)
        {
            this.landscape = landscape;
            this.terrain = landscape.Terrain;
            float sunsize = terrain.max / 4;
            numberUpdates = 80f;
            frontBottomLeftNormal = new Vector3(-0.333f, -0.333f,-0.333f);
            frontTopLeftNormal = new Vector3(-0.333f, 0.333f, -0.333f);
            frontTopRightNormal = new Vector3(0.333f, 0.333f, -0.333f);
            frontBottomRightNormal = new Vector3(0.333f,-0.333f, -0.333f);
            backBottomLeftNormal = new Vector3(-0.333f, -0.333f, 0.333f);
            backBottomRightNormal = new Vector3(0.333f, -0.333f, 0.333f);
            backTopLeftNormal = new Vector3(-0.333f,0.333f,0.333f);
            backTopRightNormal = new Vector3(0.333f, 0.333f , 0.333f);

            float sunX, sunY, sunZ, sunOffset;
            sunX = terrain.size; sunY = terrain.maxHeight + 10f; sunZ = terrain.size/2; sunOffset = 40f;
            frontBottomLeft = new Vector3(sunX-sunOffset, sunY - sunOffset, sunZ-sunOffset);
            frontTopLeft = new Vector3(sunX-sunOffset, sunY + sunOffset, sunZ-sunOffset);
            frontTopRight = new Vector3(sunX + sunOffset, sunY + sunOffset, sunZ-sunOffset);
            frontBottomRight = new Vector3(sunX + sunOffset, sunY - sunOffset, sunZ-sunOffset);
            backBottomLeft = new Vector3(sunX-sunOffset, sunY - sunOffset, sunZ + sunOffset);
            backBottomRight = new Vector3(sunX + sunOffset, sunY - sunOffset, sunZ + sunOffset);
            backTopLeft = new Vector3(sunX-sunOffset, sunY + sunOffset, sunZ + sunOffset);
            backTopRight = new Vector3(sunX + sunOffset, sunY + sunOffset, sunZ + sunOffset);



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
                Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, -10.0f, (float)terrain.size * 2),
                World = Matrix.Identity,
            };



            inputLayout = VertexInputLayout.FromBuffer(0, vertices);
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            var time = (float)gameTime.TotalGameTime.TotalSeconds;
            numberUpdates++;
            basicEffect.View = Matrix.LookAtLH(landscape.currentPosition, landscape.currentTarget, landscape.currentUp);
            Matrix lightTranslation = Matrix.Translation(new Vector3(terrain.size / 2, 0.0f, 0.0f));
            Matrix lightRotation = Matrix.RotationZ(landscape.lightRotationOffset * numberUpdates);
            basicEffect.World = lightRotation * lightTranslation;
            basicEffect.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, (float)game.GraphicsDevice.BackBuffer.Width / game.GraphicsDevice.BackBuffer.Height, 0.1f, (float)terrain.size * 2);
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
