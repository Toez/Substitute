using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Substitute
{
    class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">[0] Path to CNC file [1] Path to config file</param>
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    // Set Culture info to English so decimal point is '.'
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    
                    // Create new FileHandler to read and write the CNC file
                    FileHandler fh = new FileHandler(args[0]);
                    // Default config path
                    string cfg = @".\Config.xml";

                    // If command line arguments is more than 1, change config file path
                    if (args.Length > 1)
                        cfg = args[1];

                    // Create new XmlHandler to read the config file
                    XmlHandler xh = new XmlHandler(cfg);

                    try
                    {
                        // Get path of log file from config file
                        ErrorHandler.FileName = xh.Read("PATHS").SelectSingleNode("LOG").InnerText;
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.AddMessage(e);
                    }

                    // Create new Reference from config file
                    Reference refs = new Reference(xh.Read("REFS"));
                    // Create new TextHandler from CNC file and Reference
                    TextHandler th = new TextHandler(fh.Read(), refs);
                    // Create new MachineHandler from TextHandler and Reference
                    MachineHandler mh = new MachineHandler(th, refs);
                    
                    // Get all tools from config file
                    mh.GetToolsFromXml(cfg);

                    // Create new array to save replaced lines
                    string[] lines = new string[] { };

                    // Replace lines
                    lines = th.Replace(mh);
                    // Write output file
                    fh.Write(lines);
                }
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }
            finally
            {
                // Write logfile
                ErrorHandler.WriteOutputFile();
            }
        }
    }
}
