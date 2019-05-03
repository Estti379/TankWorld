using Pong.src.ressources.Panels;
using System;
using static SDL2.SDL;

namespace Pong.src.ressources.Items
{
    class Ball : DrawnEntity, IUpdateEntity
    {
        static private Random random = new Random(); //TODO: check for parameter

        private double posX;
        private double posY;
        private double speedX;
        private double speedY;
        private double speedFactor = 2;
        

        //Constructors
        public Ball(string key, IntPtr render) : base() //TODO: key not needed. Remove it
        {
            SpriteEntity ballSprite;
            if (!SpriteEntity.TextureExists("ball"))
            {
                ballSprite = new SpriteEntity("ball", render, "assets/images/ball.bmp", 0, 255, 0);
            }
            else
            {
                ballSprite = new SpriteEntity("ball");
            }
            ballSprite.Pos.h = 25;
            ballSprite.Pos.w = 25;
            ballSprite.SubRect.h = 100;
            ballSprite.SubRect.w = 100;
            AddSprite("ball", ballSprite);
            ResetBall();
        }

        //Accessors


        //Methods
        override public void EntityRender(IntPtr render)
        {
            foreach (SpriteEntity current in AllSprites.Values)
            {
                SDL_RenderCopy(render, current.Texture, ref current.SubRect, ref current.Pos);
            }
        }

        private void ResetBall()
        {
            posX = GameConstants.PONG_TABLE_X / 2 - AllSprites["ball"].Pos.x/2;
            AllSprites["ball"].Pos.x = (int)posX;
            posY = GameConstants.PONG_TABLE_Y / 2 - AllSprites["ball"].Pos.y/2;
            AllSprites["ball"].Pos.y = (int)posY;

            double radian = 0;
            do
            {
                radian = random.NextDouble() * 2 * Math.PI;
                speedX = Math.Cos(radian);
            } while ( speedX <= 0.25 && speedX >= -0.25); // Refuse all values between 0.25 and -0.25. Angle is too sharp.
            speedX *= speedFactor;
            speedY = Math.Sin(radian) * speedFactor;
        }

        public void EntityUpdate(Panel panel)
        {
            if(panel is PongPanel table)
            CheckforCollision(table);
            Move();
        }

        private void CheckforCollision(PongPanel table)
        {
            if ( posY <= 0)
            {
                speedY = -speedY;
            }
            else if ( (posY + AllSprites["ball"].Pos.h) >= GameConstants.PONG_TABLE_Y)
            {
                speedY = -speedY;
            }
            if ( posX<= 0)
            {
                //TODO: Point for player 2
                Console.Write("Point for player 2\n");
                ResetBall();
            }
            else if ( (posX + AllSprites["ball"].Pos.w) >= GameConstants.PONG_TABLE_X)
            {
                //TODO: Point for player 1
                Console.Write("Point for player 1\n");
                ResetBall();
            }

            if (posX+2 <= GameConstants.PADDLES_OFFSET)
            {
                int posYPaddle = table.Player1.AllSprites["paddle"].Pos.y;
                if (posY >= posYPaddle && posY <= posYPaddle + table.Player1.AllSprites["paddle"].Pos.h)
                {
                    speedX = -speedX;
                }
            }
            else if ((posX + AllSprites["ball"].Pos.w) >= GameConstants.PONG_TABLE_X - GameConstants.PADDLES_OFFSET)
            {
                int posYPaddle = table.Player2.AllSprites["paddle"].Pos.y;
                if (posY >= posYPaddle && posY <= posYPaddle + table.Player2.AllSprites["paddle"].Pos.h)
                {
                    speedX = -speedX;
                }
            }

        }

        private void Move()
        {
            posX += speedX;
            posY += speedY;
            AllSprites["ball"].Pos.x = (int)posX;
            AllSprites["ball"].Pos.y = (int)posY;
        }
    }
}

