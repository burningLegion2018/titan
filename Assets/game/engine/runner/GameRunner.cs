using System;
using System.Threading;
namespace common.game.engine.runner
{
    /// <summary>
    /// 引擎逻辑线程驱动类
    /// </summary>
    public class GameRunner 
    {
        private AbstractLogReport logReport = LoggerFactory.getInst().getLogger();
        private bool isWorking = false;
        private IRunable run;
        private int period = 20;
        private int nextLoopTime = 0;
        public GameRunner(IRunable run)
        {
            this.run = run;
        }
        Thread exec;
        public void Start()
        {
            isWorking = true;
            exec.Start();
        }
        public void Stop()
        {
            isWorking = false;
            if (exec.IsAlive)
                exec.Join();
            logReport.OnLogReport("Runner stoped ...");
        }
        public void Run()
        {
            int starttime = DateTime.Now.Millisecond;// 时间轴开始时间
            nextLoopTime = starttime + period;
            while (isWorking)
            {
                DateTime runtime = DateTime.Now;
                run.RunInLogic(runtime);
                int now = DateTime.Now.Millisecond;
                long preriod = nextLoopTime - now;
                nextLoopTime += period;
                if (preriod > 0)
                    Thread.Sleep(period);
                else
                    logReport.OnLogReport("Runner proc busy,delay:" + preriod );
            }
        }
    }
}