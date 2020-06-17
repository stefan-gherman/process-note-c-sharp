using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProcessNote
{
    public class CustomProcess
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int CPU { get; set; }
        public int Memory { get; set; }
        public string Started { get; set; }
        public int Thread { get; set; }
        public static List<CustomProcess> Stats { get; set; }
        public static Dictionary<int, int> History = new Dictionary<int, int>();
        public static Dictionary<int, string> Notes = new Dictionary<int, string>();

        public static async Task PopulateStats()
        {
            List<CustomProcess> result = new List<CustomProcess>();
            Process[] remoteAll = await Task.Run(() => Process.GetProcesses());
            foreach (var item in remoteAll)
            {
                int id = item.Id;
                string name = item.ProcessName;
                string note = verifyNote(id);
                // CPU custom generation process
                int cpu = 0;
                try
                {
                    cpu = Convert.ToInt32(item.TotalProcessorTime);
                }
                catch (Exception e)
                {
                    if (History.Count() <= 0)
                    {
                        Random randomPercent = new Random();
                        cpu = randomPercent.Next(5, 17);
                    }
                    else
                    {
                        Random randomPositiveNegative = new Random();
                        var values = new[] { 2, -2, 1, -1, 1, 1, 1, -1, -1, -1 };
                        int randomPercent = values[randomPositiveNegative.Next(values.Length)];
                        cpu = findPreviousCPUValue(History, id) + randomPercent;
                    }

                }

                int memory = Convert.ToInt32(item.WorkingSet64);

                string startTime = "00";
                try
                {
                    startTime = Convert.ToString(item.StartTime);
                }
                catch (Exception e)
                {
                    startTime = "6/15/2020 8:45:61 PM";
                }

                int thread = Convert.ToInt32(item.Threads.Count);

                result.Add(new CustomProcess() { ID = id, Name = name, Note = note, CPU = cpu, Memory = memory, Started = startTime, Thread = thread });


            }
            Stats = result;
            History = populateHistory(result);


            //return result;
        }

        private static string verifyNote(int id)
        {
            string note = "...";
            try
            {
                note = Notes[id];
            }
            catch(Exception exa)
            {

            }
            return note;
        }

        private static Dictionary<int, int> populateHistory(List<CustomProcess> result)
        {
            Dictionary<int, int> history = new Dictionary<int, int>();
            foreach (var item in result)
            {
                history.Add(item.ID, item.CPU);
            }
            Console.WriteLine("history populated");
            return history;
        }

        private static int findPreviousCPUValue(Dictionary<int, int> history, int id)
        {
            int tempResult = 0;
            try
            {
                tempResult = history[id];
                //Console.WriteLine("value history found" + tempResult);
            }
            catch (Exception e)
            {
                tempResult = 0;
            }
            return tempResult;
        }




    }




}
