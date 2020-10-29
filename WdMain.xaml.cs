using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace go_back_n
{
    /// <summary>
    /// WdMain.xaml 的交互逻辑
    /// </summary>
    public partial class WdMain : Window
    {

        private SlidingWindow senderSlidingWindow;
        private SlidingWindow receiverSlidingWindow;
        private SendingFrameList sendingFrameList;
        public WdMain()
        {
            InitializeComponent();
            Frame.ReceiverReceived += Frame_ReceiverReceived;
            Frame.SenderReceived += Frame_SenderReceived;
            TimerSenderTimeOut.Tick += TimerSenderTimeOut_Tick;
            senderSlidingWindow = new SlidingWindow(UfgSender, App.SenderWindowSize);
            receiverSlidingWindow = new SlidingWindow(UfgReceiver, 1);
            sendingFrameList = new SendingFrameList();
            Frame.Canvas = CanvasMain;

            for (int i = 0; i < App.SenderWindowSize; i++)
            {
                sendingFrameList.Add(new Frame(i.ToString(), true));
            }
        }

        private void TimerSenderTimeOut_Tick(object sender, EventArgs e)
        {
            sendingFrameList.Clear();
            for (int i = 0; i < App.SenderWindowSize; i++)
            {
                sendingFrameList.Add(new Frame(((sender_sending_frame + i) % 16).ToString(), true));
            }
            //sendingFrameList.Add(new Frame(sender_sending_frame.ToString(), true));
        }

        int sender_sending_frame = 0;
        int receiver_receiving_frame = 0;
        DispatcherTimer TimerSenderTimeOut = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(5),
            IsEnabled = true
        };
        int lastACK = -1;
        private void Frame_SenderReceived(object sender, string e)
        {
            int id = ParseIDFromAck(e);
            if (id == lastACK)
            {

            }
            else
            {
                if (id - 1 >= sender_sending_frame || id >= (sender_sending_frame + App.SenderWindowSize) % 16)
                {
                    // sender_sending_frame = (sender_sending_frame + 1) % 16;
                    sender_sending_frame = id;

                    TimerSenderTimeOut.Stop();
                    TimerSenderTimeOut.Start();

                    senderSlidingWindow.Draw(sender_sending_frame);
                }

                //sendingFrameList.Clear();
                //for (int i = 0; i < App.SenderWindowSize; i++)
                //{
                //    sendingFrameList.Add(new Frame(((sender_sending_frame + i) % 16).ToString(), true));
                //}
                sendingFrameList.Add(new Frame(((sender_sending_frame + App.SenderWindowSize - 1) % 16).ToString(), true));
                lastACK = id;
            }

        }

        private void Frame_ReceiverReceived(object sender, string e)
        {
            if (isBufferFull)
            {
                return;
            }
            int currentIndex = int.Parse(e);
            if (receiver_receiving_frame == currentIndex)
            {
                receiver_receiving_frame = (receiver_receiving_frame + 1) % 16;

            }
            receiverSlidingWindow.Draw(receiver_receiving_frame);
            Frame frame = new Frame($"ACK {receiver_receiving_frame}", false);
            frame.ShowAndMove();
        }

        private void BtnDebug1_Click(object sender, RoutedEventArgs e)
        {
            // Frame frame = new Frame("1", true);
            sendingFrameList.Add(new Frame("0", true));
            sendingFrameList.Add(new Frame("1", true));
            sendingFrameList.Add(new Frame("2", true));
            sendingFrameList.Add(new Frame("3", true));
        }

        private void BtnDebug2_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame("1", false);

        }
        private int ParseIDFromAck(string ack)
        {
            return int.Parse(ack.Split(' ')[1]);
        }

        private bool isBufferFull = false;
        private void BtnBufferFull_Click(object sender, RoutedEventArgs e)
        {
            if (isBufferFull)
            {
                BtnBufferFull.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnBufferFull.Background = new SolidColorBrush(Colors.Red);

            }
            isBufferFull = !isBufferFull;
        }
    }
}
