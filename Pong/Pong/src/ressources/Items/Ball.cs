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
        

        //Constructors
        public Ball(string key, IntPtr render) : base()
        {
            SpriteEntity ballSprite;
            if (!SpriteEntity.TextureExists("ball"))
            {
                ballSprite = new SpriteEntity("ball", render, "images/ball.bmp", 0, 255, 0);
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
            posX = GameConstants.PONG_TABLE_X / 2;
            AllSprites["ball"].Pos.x = (int)posX;
            posY = GameConstants.PONG_TABLE_Y / 2;
            AllSprites["ball"].Pos.y = (int)posY;

            //TODO: take care of negative values
            while ( (speedX = random.NextDouble() * 2) < 0.5)
            {
                /* empty */
            }
            speedY = 2 - speedX;
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
            else if ( (posY + AllSprites["ball"].Pos.h/2) >= GameConstants.PONG_TABLE_Y)
            {
                speedY = -speedY;
            }
            if ( posX <= 0)
            {
                //TODO: Point for player 2
                Console.Write("Point for player 2\n");
                ResetBall();
            }
            else if ( (posX + AllSprites["ball"].Pos.h) >= GameConstants.PONG_TABLE_X)
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
            else if ((posX + AllSprites["ball"].Pos.h) >= GameConstants.PONG_TABLE_X - GameConstants.PADDLES_OFFSET)
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

