
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace go_back_n
{
    class MessageList
    {
        public MessageList()
        {
            if (File.Exists("in.txt"))
            {
                Messages = File.ReadAllLines("in.txt").ToList();
            }
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    Messages.Add(i.ToString());
                }
                File.WriteAllLines("in.txt", Messages);
            }
            int id = 0;
            for (int i = 0; i < Messages.Count; i++)
            {
                IdList.Add(id);
                id++;
                if (id == 16)
                {
                    id = 0;
                }

                IsSendList.Add(false);
            }
            Console.WriteLine();
        }

        public List<string> Messages { get; set; } = new List<string>();
        public List<bool> IsSendList { get; set; } = new List<bool>();
        public List<int> IdList { get; set; } = new List<int>();

        public string GetMessageById(int id)
        {
            for (int i = 0; i < Messages.Count; i++)
            {
                if (!IsSendList[i] && IdList[i] == id)
                {
                    return Messages[i];
                }
            }
            throw new KeyNotFoundException();
        }

        public void SetAlreadySend(int id)
        {
            int n = 0;
            for (int i = 0; i < Messages.Count; i++)
            {
                if (IdList[i] == id && IsSendList[i] == false)
                {

                    break;
                }
                if (!IsSendList[i])
                {
                    n++;
                }

            }
            if (n >= 5)
            {
                Console.WriteLine($"reject {id}");
                Console.WriteLine(n);
                return;
            }
            Console.WriteLine($"process {id}");
            for (int i = 0; i < Messages.Count; i++)
            {
                if (IdList[i] == id && IsSendList[i] == false)
                {
                    //Console.WriteLine($"set {i}");
                    //IsSendList[i] = true;
                    break;
                }
                if (!IsSendList[i])
                {
                    Console.WriteLine($"set {i}");
                    IsSendList[i] = true;

                }

            }
        }
    }
}
