using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace go_back_n
{
    public class SendingFrameList
    {
        private List<Frame> frames = new List<Frame>();
        private bool isCouldSend = true;
        private Timer timer = new Timer()
        {
            Interval = 1000
        };

        public SendingFrameList()
        {
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (frames.Count == 0)
            {
                isCouldSend = true;
                timer.Stop();
            }
            else
            {
                frames[0].ShowAndMove();
                Console.WriteLine($"Go {frames[0].Content} {GetTimeStamp()}");
                frames.RemoveAt(0);
            }
        }

        public void Add(Frame frame)
        {
            Console.WriteLine($"Add {frame.Content} {GetTimeStamp()}");
            if (frames.Count == 0 && isCouldSend)
            {
                frame.ShowAndMove();
                Console.WriteLine($"Go Go {frame.Content} {GetTimeStamp()}");

                isCouldSend = false;
                timer.Start();

            }
            else
            {
                frames.Add(frame);
            }
        }
        public void Clear()
        {
            frames.Clear();
        }
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
    }

}
