using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tail
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string filename = "";
                int nlines = 0;

                if (args.Length > 0)
                {
                    filename = args[0];
                }

                if (args.Length > 1)
                {
                    if (!int.TryParse(args[1], out nlines))
                    {
                        // clear filename so usage displays
                        filename = "";
                    }
                }

                if (filename == "")
                {
                    Console.WriteLine("");
                    Console.WriteLine("USAGE: Tail <filename> <last n lines (optional)>");
                    Console.WriteLine("EXAMPLE: To display last 500 lines in somefile => ");
                    Console.WriteLine("         Tail somefile.txt 500");
                    Console.WriteLine("<cntrl-C> to exit");
                    Console.WriteLine("");
                    return;
                }

                var wh = new AutoResetEvent(false);
                var fsw = new FileSystemWatcher(".");
                fsw.Filter = filename;
                fsw.EnableRaisingEvents = true;
                fsw.Changed += (s, e) => wh.Set();



                var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (var sr = new StreamReader(fs))
                {
                    var s = "";
                    var q = new FixedSizedQueue<string>();
                    if (nlines > 0) q.Limit = nlines;

                    while (true)
                    {
                        s = sr.ReadLine();
                        if (s != null)
                        {
                            if (nlines > 0)
                            {
                                // queue up!
                                q.Enqueue(s);
                            }
                            else
                            {
                                Console.WriteLine(s);
                            }
                        }
                        else
                        {
                            if (nlines > 0)
                            {
                                // dequeue last lines
                                while (q.TryDequeue(out s))
                                {
                                    Console.WriteLine(s);
                                }
                                nlines = 0;
                            }
                            wh.WaitOne(1000);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public class FixedSizedQueue<T>
        {
            ConcurrentQueue<T> q = new ConcurrentQueue<T>();

            public int Limit { get; set; }
            public void Enqueue(T obj)
            {
                q.Enqueue(obj);
                lock (this)
                {
                    T overflow;
                    while (q.Count > Limit && q.TryDequeue(out overflow)) ;
                }
            }

            public bool TryDequeue(out T result)
            {
                return q.TryDequeue(out result);
            }
        }
    }
}

