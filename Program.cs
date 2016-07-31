using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Emrys.Desktop
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var task = Task.Factory.StartNew(() =>
              {
                  if (!Desktop.SetDesktop())
                  {
                      Thread.Sleep(1000 * 60 * 10);
                      while (!Desktop.SetDesktop())
                      {
                          // 设置失败后没10分钟重试一次
                          Thread.Sleep(1000 * 60 * 10);
                      }
                  }
              }); 

            Task.WaitAll(task);
        }


    }
}
