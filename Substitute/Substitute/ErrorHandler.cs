using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substitute
{
    static class ErrorHandler
    {
        private static string _FileName;
        /// <summary>
        /// Path to logfile
        /// </summary>
        public static string FileName
        {
            get
            {
                // Use default folder for logfile
                return _FileName ?? (_FileName = @".\Logfile.txt");
            }
            set
            {
                _FileName = value;
            }
        }

        private static string[] _Messages;
        /// <summary>
        /// Array of messages received from errors thrown
        /// </summary>
        public static string[] Messages
        {
            get
            {
                // Use current date and time as first line of logfile
                return _Messages ?? (_Messages = new string[] { DateTime.Now.ToString() });
            }
            private set
            {
                _Messages = value;
            }
        }

        /// <summary>
        /// Adds a new message to errorlist
        /// </summary>
        /// <param name="e">Exception with message and stack trace</param>
        public static void AddMessage(Exception e)
        {
            try
            {
                // Construct message for the array
                string message = "Message: " + e.Message + Environment.NewLine + "Stack Trace: " + e.StackTrace;
                // Clone array
                string[] temp = (string[])Messages.Clone();
                // Make it bigger
                Array.Resize(ref temp, Messages.Length + 1);
                // Write last entry
                temp[temp.Length - 1] = message;
                // Clone again
                Messages = (string[])temp.Clone();
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// Writes and saves the logfile
        /// </summary>
        public static void WriteOutputFile()
        {
            try
            {
                FileHandler fh = new FileHandler(FileName);

                // Write all messages in logfile
                fh.Write(Messages);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
