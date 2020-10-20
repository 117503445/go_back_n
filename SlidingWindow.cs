using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace go_back_n
{
    public class SlidingWindow
    {
        private UniformGrid UniformGrid;

        private int WindowsSize;

        private List<TextBox> textBoxs = new List<TextBox>();

        public SlidingWindow(UniformGrid uniformGrid, int windowsSize)
        {
            UniformGrid = uniformGrid ?? throw new ArgumentNullException(nameof(uniformGrid));
            WindowsSize = windowsSize;


            for (int i = 0; i < 16; i++)
            {
                TextBox textBox = new TextBox()
                {
                    Text = i.ToString(),
                    Background = new SolidColorBrush(Colors.White),
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    IsReadOnly = true
                };
                textBoxs.Add(textBox);
                uniformGrid.Children.Add(textBox);
            }

            Draw(0);
        }

        public void Draw(int startIndex)
        {
            for (int i = 0; i < 16; i++)
            {
                int index = (startIndex + i) % 16;

                if (i < WindowsSize)
                {
                    textBoxs[index].Background = new SolidColorBrush(Colors.Cyan);
                }
                else
                {
                    textBoxs[index].Background = new SolidColorBrush(Colors.White);
                }

            }
        }
    }
}
