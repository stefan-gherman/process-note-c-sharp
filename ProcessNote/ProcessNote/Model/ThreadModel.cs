using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote.Model
{
    class ThreadModel
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public int Started { get; set; }

        private List<ProcessThread> processThreadsList;

        public ThreadModel()
        {
            processThreadsList = new List<ProcessThread>();
        }

        private void GetAllProcessThreads(Process process)
        {
            ProcessThreadCollection threads = process.Threads;

            foreach(ProcessThread thread in threads)
            {
                processThreadsList.Add(thread);
            }
        }

        public List<ProcessThread> GetThreadsList()
        {
            return processThreadsList;
        }

    }
}
