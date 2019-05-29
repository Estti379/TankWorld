namespace TankWorld.Engine
{
    public class Timer
    {
        public enum Type
        {
            ASCENDING,
            DESCENDING,
            PAUSE_AT_ZERO //it's a descending timer that pauses automatically when timer steps over 0

        }
        
        //times are in milliseconds
        private double time;
        private double defaultTime;
        private double executeTime;
        private double oldTime;

        private Command command;
        private Type timerType;
        private bool paused;

        //Constructors
        public Timer()
        {
            time = 0;
            defaultTime = 0;
            command = null;
            timerType = Type.ASCENDING;
            executeTime = -100;
            oldTime = 0;
            paused = false;
        }

        public Timer(Type type)
        {
            time = 0;
            defaultTime = 0;
            oldTime = 0;
            command = null;
            timerType = type;
            //To avoid accidental execution, set execution time
            if (timerType == Type.ASCENDING)
            {
                executeTime = -100;
            }
            else if ( (timerType == Type.DESCENDING) || (timerType == Type.PAUSE_AT_ZERO))
            {
                executeTime = 100;
            }
        }


        //Accessors
        public double Time
        {
            get { return time; }
            set
            {
                time = value;
                oldTime = value;
            }
        }
        public double DefaultTime { get => defaultTime; set => defaultTime = value; }
        public double ExecuteTime { get => executeTime; set => executeTime = value; }
        public Command Command { get => command; set => command = value; }
        public Type TimerType { get => timerType; set => timerType = value; }


        //Methods
        public void Reset()
        {
            Time = defaultTime;
        }
        public void ResetTo(double resetTime)
        {
            Time = resetTime;
        }
        public void SwitchPause()
        {
            paused = !paused;
        }
        public void Pause()
        {
            paused = true;
        }
        public void UnPause()
        {
            paused = false;
        }

        public void Execute()
        {
            if (command != null)
            {
                command.Execute();
            }
        }

        public void Update()
        {//Note: execute only if time has "stepped over" execute time. time bigger/smaller than execute time is not enough
            if (!paused)
            {
                if(timerType == Type.ASCENDING)
                {
                    time += GameConstants.MS_PER_UPDATE;
                    if ( (time >= executeTime) && (oldTime <= executeTime)) { Execute(); }
                }
                else if ( (timerType == Type.DESCENDING) || (timerType == Type.PAUSE_AT_ZERO) )
                {
                    time -= GameConstants.MS_PER_UPDATE;
                    if ((time <= executeTime) && (oldTime >= executeTime)) { Execute(); }
                    //Pause when stepping over 0, if timer is a PAUSE_AT_ZERO
                    if ( (timerType == Type.PAUSE_AT_ZERO) && (time <= 0) && (oldTime > 0)) { Pause(); }
                }
                oldTime = time;
            }
            

        }

    }
}
