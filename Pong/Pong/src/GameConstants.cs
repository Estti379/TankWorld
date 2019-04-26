namespace Pong.src
{
    static public class GameConstants
    {
        public const int WINDOWS_X = 1280;
        public const int WINDOWS_Y = 720;

        public const int SCORE_OFFSET = 100;

        public const int PONG_TABLE_X = WINDOWS_X;
        public const int PONG_TABLE_Y = WINDOWS_Y - SCORE_OFFSET;

        public const int PADDLES_OFFSET = 50;

        public const double MS_PER_UPDATE = 5; //Milliseconds between each game Update

    }
}
