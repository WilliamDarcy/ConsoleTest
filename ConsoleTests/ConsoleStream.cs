using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public class ConsoleStream
    {
        MemoryStream MsIn;
        MemoryStream MsOut;

        /// <summary>
        /// The function SetConsoleOut creates a memory stream and a stream writer. The streamwriter fetches all the display on the console sent by the functionTest .
        /// </summary>
        /// <param name="functionTest">The function to test</param>
        public void SetConsoleOut(Action functionTest)
        {

            MsOut = new MemoryStream();

            using (var sw = new StreamWriter(MsOut, Encoding.UTF8, 512, true))
            {
                Console.SetOut(sw);
                functionTest();
            }

            MsOut.Seek(0, SeekOrigin.Begin);



        }
        /// <summary>
        /// the function creates a memory stream  and write in the stream all the parameters we need to take the test.
        /// </summary>
        /// <param name="args">the ordered list of params that the console.ReadLine uses to take a test.</param>
        public void SetConsoleIn(params string[] args)
        {
            MsIn = new MemoryStream();

            using (StreamWriter sw = new StreamWriter(MsIn, Encoding.UTF8, 512, true))
            {

                foreach (var item in args)
                {
                    sw.WriteLine(item);
                }

            }
            MsIn.Seek(0, SeekOrigin.Begin);

            StreamReader sr = new StreamReader(MsIn);

            Console.SetIn(sr);


        }
        /// <summary>
        /// Get the last line displayed on the console during the test. 
        /// It removes the blank space before and after the string and lowers all the characters.
        /// </summary>
        /// <returns>the last line</returns>
        public string GetLastLine()
        {
            List<string> ConsoleStrings = GetAllLines();
            return ConsoleStrings[ConsoleStrings.Count - 1].ToLower().Trim();
        }


        /// <summary>
        /// Get all the lines displayed by the console during the test.
        /// </summary>
        /// <returns>the list of lines</returns>
        public List<String> GetAllLines()
        {
            if (MsOut == null) throw new NullReferenceException("Stream is null, Call SetConsoleOut");

            List<string> ConsoleOutList = new List<string>();
            using (var sr = new StreamReader(MsOut))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    ConsoleOutList.Add(line);
                    line = sr.ReadLine();
                }
            }
            return ConsoleOutList;

        }
        /// <summary>
        /// CleanText extracts a sentence from the line. This is due to the Console.Write that doesn't generate an end of line. 
        /// </summary>
        /// <param name="line">the line</param>
        /// <param name="sentence">the sentence to find in the line</param>
        /// <returns>the sentence</returns>
        public string CleanText(string line, string sentence)
        {
            string result = line;
            int no = line.IndexOf(sentence);
            if (no >= 0)
            {
                result = line.Substring(no);
            }

            return result;

        }

        /// <summary>
        /// Close all the streams.
        /// </summary>
        public void EndTest()
        {
            if (MsIn != null) MsIn.Close();
            
            if (MsOut != null) MsOut.Close();
        }



    }
}
