using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substitute
{
    class FileHandler
    {
        private string _FileName;
        /// <summary>
        /// Path to file
        /// </summary>
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// Path to file is "./Config.txt"
        /// </summary>
        public FileHandler()
            : this(@"./Config.txt")
        {
            
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="fileName">Path to file</param>
        public FileHandler(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Reads all lines from file
        /// </summary>
        /// <returns>String array that contains all file lines</returns>
        public string[] Read()
        {
            string[] result = null;

            try
            {
                // Read all lines
                result = File.ReadAllLines(FileName);
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Writes lines to the file
        /// </summary>
        /// <param name="lines">Array of lines you want to write</param>
        public void Write(string[] lines)
        {
            try
            {
                // Create new instance of StreamWriter
                StreamWriter sw = new StreamWriter(FileName);

                // Write all lines in stream
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }

                // Save and close file
                sw.Close();
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }
        }
    }
}
