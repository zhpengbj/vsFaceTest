using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQ
{
    public class ShareList
    {
        private static ShareList one;
        public static ShareList GetShareList()
        {
            if (one==null)
            {
                one = new ShareList();
            }
            return one;

        }
        private Queue<string> ListQueue = new Queue<string>();
        public int  AddQueue(string str) //入列
        {
            ListQueue.Enqueue(str);
            return ListQueue.Count;
        }
        public string GetQueue() //出列
        {
            if (ListQueue.Count > 0)
            {
                return ListQueue.Dequeue();
            }
            else
            {
                return "";
            }
        }

    }
}
