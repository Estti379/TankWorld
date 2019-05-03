using Pong.src.ressources.Items;
using System;

namespace Pong.src.ressources.Panels
{
    public class PongPanel : Panel
    {
        private Paddle player1;
        private Paddle player2;
        private Ball ball;

        //Constructors
        public PongPanel(IntPtr render)
        {
            player1 = new Paddle("player1", render, 0 + GameConstants.PADDLES_OFFSET - 2);
            player2 = new Paddle("player2", render, GameConstants.PONG_TABLE_X - GameConstants.PADDLES_OFFSET);

            ball = new Ball("ball1", render);
        }

        //Accessors
        public Paddle Player1
        {
            get
            {
                return player1;
            }
        }
        public Paddle Player2
        {
            get
            {
                return player2;
            }
        }



        //Methods
        override public void RenderAll(IntPtr render)
        {
            player1.EntityRender(render);
            player2.EntityRender(render);
            ball.EntityRender(render);
        }

        override public void UpdateAll()
        {
            IUpdateEntity toUpdate;
            if (ball is IUpdateEntity)
            {
                toUpdate = (IUpdateEntity)ball;
                toUpdate.EntityUpdate(this);
            }
            if (player1 is IUpdateEntity)
            {
                toUpdate = (IUpdateEntity)player1;
                toUpdate.EntityUpdate(this);
            }
            if (player2 is IUpdateEntity)
            {
                toUpdate = (IUpdateEntity)player2;
                toUpdate.EntityUpdate(this);
            }
        }

        public void DownButtonDown()
        {
            player2.StartMoving(PaddleState.DOWN);
        }
        public void UpButtonDown()
        {
            player2.StartMoving(PaddleState.UP);
        }
        public void DownButtonUP()
        {
            player2.Stop(PaddleState.DOWN);
        }
        public void UpButtonUP()
        {
            player2.Stop(PaddleState.UP);
        }
        public void SButtonDown()
        {
            player1.StartMoving(PaddleState.DOWN);
        }
        public void WButtonDown()
        {
            player1.StartMoving(PaddleState.UP);
        }
        public void SButtonUP()
        {
            player1.Stop(PaddleState.DOWN);
        }
        public void WButtonUP()
        {
            player1.Stop(PaddleState.UP);
        }
    }
}
