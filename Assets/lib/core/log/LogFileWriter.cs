using common.lib.core.collections.concurrent;
using System;
using System.IO;
using System.Threading;
using UnityEngine;
public class LogFileWriter
{
    private string dir;
    private LinkedBlockingQueue<Log> logs = new LinkedBlockingQueue<Log>();
    private int logLv = Log.LOG_LV_LOG;
    private Thread wr;
    private volatile bool isWorking = false;
    public int LogLv{ set { logLv = value; } }
    public LogFileWriter(string dir) 
    {
        this.dir = dir;
    }
    public void Start()
    {
        if (Application.isMobilePlatform)
            return;
        isWorking = true;
        wr = new Thread(WriteFile);
        wr.Start(this);
    }
    public void Stop()
    {
        isWorking = false;
        if (wr.IsAlive)
            wr.Join();
    }
    public void WriteLog(string msg,int lv) 
    {
        if (Application.isMobilePlatform)
            return;
        Log log = new Log(msg,lv);
        logs.Put(log);
    }
    public void WriteFile(System.Object param)
    {
        LogFileWriter lfw = (LogFileWriter)param;
        while(lfw.isWorking)
        {
            while (logs.GetCount() != 0)
            {
                Log log = logs.Poll(1000);
                if (log != null)
                    WriteFile(log);
            }
        }
    }
    public void WriteFile(Log log)
    {
        string msg = log.Msg;
        int lv = log.Lv;
        DateTime time = log.Time;
        string fileName = time.ToString("yyyy.MM.dd") + ".log";
        FileStream fs = null;
        StreamWriter sw = null;
        try
        {
            fs = getFileStream(dir, fileName);
            sw = new StreamWriter(fs);
            if (lv >= logLv)
                sw.WriteLine(log.Info);
                
        }
        finally
        {
            if (sw != null)
                sw.Close();
            if (fs != null)
                fs.Close();
        }
        
    }
    private FileStream getFileStream(String dir,string fileName)
    {
        FileStream fs;
        string fullFileName = dir + "/" + fileName;
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        if (File.Exists(fullFileName))
            fs = new FileStream(fullFileName, FileMode.Append, FileAccess.Write);
        else
            fs = new FileStream(fullFileName, FileMode.CreateNew, FileAccess.Write); 
        return fs;
    }
}
