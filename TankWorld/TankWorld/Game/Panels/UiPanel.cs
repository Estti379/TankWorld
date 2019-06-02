

using TankWorld.Engine;
using TankWorld.Game.Commands;
using TankWorld.Game.Events;
using TankWorld.Game.Models;
using static SDL2.SDL;

namespace TankWorld.Game.Panels
{
    public class UiPanel : Panel
    {
        private ClockModel clock;
        private Timer time;

        private Sprite title;
        private Sprite score1;
        private Sprite score2;

        private bool timeUp;

        //Constructors
        public UiPanel()
        {
            timeUp = false;
            clock = new ClockModel(100);
            this.time = new Timer(Timer.Type.DESCENDING);
            this.time.Time = 180 * 1000;
            this.time.ExecuteTime = 0;
            this.time.Command = new ThrowEventCommand(new SceneStateEvent(SceneStateEvent.Type.TIME_UP));
        }
        //Accessors

        //Methods

        public override void Render(RenderLayer layer)
        {
            clock.Render(layer);
            if (timeUp)
            {
                title.RenderAtPosition(title.Pos.x, title.Pos.y);
                score1.RenderAtPosition(score1.Pos.x, score1.Pos.y);
                score2.RenderAtPosition(score2.Pos.x, score2.Pos.y);
            }
        }

        public override void Update()
        {
            string newTime;
            if (time.Time <= 0)
            {
                time.Time = 0;
                time.Pause();
            }
            int seconds = Helper.TimerToSeconds(time);
            string secondsString = ""+seconds;
            int minutes = Helper.TimerToMinutes(time);
            string minutesString = ""+minutes;

            if (seconds < 10) { secondsString = "0" + secondsString;  }
            if (minutes < 10) { minutesString = "0" + minutesString; }

            newTime = minutesString + " : " + secondsString;

            clock.UpdateClock(newTime);
            time.Update();
        }

        public void TimeIsUp(int ennemyHit, int timesHit)
        {
            this.timeUp = !timeUp;
            SDL_Color color = new SDL_Color()
            {
                r = 0,
                g = 0,
                b = 0,
                a = 0
            };

            title = new Sprite("TimeUp", TextGenerator.pixel_millenium_big, "Time is UP!", color);
            score1 = new Sprite("Score1", TextGenerator.pixel_millenium_medium, "You put "+ ennemyHit + " holes in your ennemy!", color);
            score2 = new Sprite("Score2", TextGenerator.pixel_millenium_medium, "Your enemy got you " + timesHit + " times.", color);

            title.Pos.x = GameConstants.WINDOWS_X / 2 - title.Pos.w/2;
            title.Pos.y = GameConstants.WINDOWS_Y / 2 - title.Pos.h/2;

            score1.Pos.x = GameConstants.WINDOWS_X / 2 - score1.Pos.w/2;
            score1.Pos.y = title.Pos.y + title.Pos.h + 100;

            score2.Pos.x = GameConstants.WINDOWS_X / 2 - score2.Pos.w/2;
            score2.Pos.y = score1.Pos.y + score1.Pos.h + 50;

        }
    }
}
