using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace go_back_n
{
    public class Frame
    {
        private readonly Button button = new Button();
        public static Canvas Canvas;
        private readonly bool isFromSender;

        public static event EventHandler<string> SenderReceived;
        public static event EventHandler<string> ReceiverReceived;
        public bool IsBroken { get; set; } = false;
        public bool IsLost { get; set; } = false;

        public string Content { get; }

        private void Show()
        {
            Canvas.Dispatcher.Invoke(() =>
            {
                if (isFromSender)
                {
                    Canvas.SetLeft(button, 300);
                    Canvas.SetTop(button, 50);
                }
                else
                {
                    Canvas.SetLeft(button, 1000);
                    Canvas.SetTop(button, 200);
                }

                button.Width = 100;
                button.Height = 100;
                button.Content = Content;
                button.FontSize = 22;
                button.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.Button_MouseLeftButtonDown), true);
                button.MouseRightButtonDown += Button_MouseRightButtonDown;
            }
            );

        }


        private void Button_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Canvas.Children.Remove(button);
            IsLost = true;
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            button.Background = new SolidColorBrush(Colors.Red);
            IsBroken = true;
        }

        public Frame(string value, bool isFromSender)
        {
            this.isFromSender = isFromSender;
            this.Content = value;

            Canvas.Children.Add(button);
        }
        public void ShowAndMove()
        {
            Show();
            Move();
        }
        public void Move()
        {
            Canvas.Dispatcher.Invoke(() =>
            {

                DoubleAnimation leftAnimation;
                if (isFromSender)
                {
                    leftAnimation = new DoubleAnimation()
                    {
                        From = 300,
                        To = 1000,
                        Duration = TimeSpan.FromSeconds(2),
                    };
                }
                else
                {
                    leftAnimation = new DoubleAnimation()
                    {
                        From = 1000,
                        To = 300,
                        Duration = TimeSpan.FromSeconds(2),
                    };
                }
                leftAnimation.Completed += LeftAnimation_Completed;
                button.BeginAnimation(Canvas.LeftProperty, leftAnimation);

            });

        }

        private void LeftAnimation_Completed(object sender, EventArgs e)
        {
            Canvas.Children.Remove(button);
            if (!IsLost && !IsBroken)
            {
                if (isFromSender)
                {
                    ReceiverReceived?.Invoke(sender, Content);
                }
                else
                {
                    SenderReceived?.Invoke(sender, Content);
                }
            }
        }
    }
}
