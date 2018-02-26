using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Substitute
{
    class MachineHandler
    {
        private List<Tool> _Tools;
        /// <summary>
        /// List of tools from xml file
        /// </summary>
        public List<Tool> Tools
        {
            get
            {
                return _Tools;
            }
            set
            {
                _Tools = value;
            }
        }

        private List<SteelTool> _SteelTools;
        /// <summary>
        /// List of steel tools from xml file
        /// </summary>
        public List<SteelTool> SteelTools
        {
            get
            {
                return _SteelTools;
            }
            set
            {
                _SteelTools = value;
            }
        }

        private int _CurrentSteelTool;
        /// <summary>
        /// Id of current steel tool in list
        /// </summary>
        public int CurrentSteelTool
        {
            get
            {
                return _CurrentSteelTool;
            }
            set
            {
                _CurrentSteelTool = value;
            }
        }

        /// <summary>
        /// Instance of TextHandler to get file lines from
        /// </summary>
        private TextHandler TextHandler;
        /// <summary>
        /// Instance of Reference to detect commands
        /// </summary>
        private Reference Reference;

        /// <summary>
        /// Default constructor
        /// Default TextHandler and Reference 
        /// </summary>
        public MachineHandler()
            : this (new TextHandler())
        {
            
        }

        /// <summary>
        /// Custom constructor
        /// Default Reference
        /// </summary>
        /// <param name="th">TextHandler that contains a CNC file</param>
        public MachineHandler(TextHandler th)
            : this(th, new Reference())
        {
            
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="th">TextHandler that contains a CNC file</param>
        /// <param name="refc">Reference to detect commands</param>
        public MachineHandler(TextHandler th, Reference refc)
        {
            Tools = new List<Tool>();
            SteelTools = new List<SteelTool>();
            TextHandler = th;
            Reference = refc;
        }

        /// <summary>
        /// Reads xml file, creates tools and adds them to the tool list
        /// </summary>
        /// <param name="fileName">Path to xml file</param>
        public void GetToolsFromXml(string fileName)
        {
            try
            {
                // Create new instance of XmlHandler to read config file
                XmlHandler xml = new XmlHandler(fileName);
                // Read all tools from config file
                XmlNode tools = xml.Read("TOOLS");

                if (tools != null)
                {
                    // Create new instance for all tools in config file
                    foreach (XmlNode tool in tools)
                    {
                        // If element name is tool
                        if (tool.Name == "TOOL")
                        {
                            int id = int.Parse(tool.SelectSingleNode("ID").InnerText);
                            string select = tool.SelectSingleNode("SELECT").InnerText;
                            string on = tool.SelectSingleNode("ON").InnerText;
                            string off = tool.SelectSingleNode("OFF").InnerText;

                            int xpf = int.Parse(tool.SelectSingleNode("SPEEDXP1").InnerText);
                            int xpt = int.Parse(tool.SelectSingleNode("SPEEDXP2").InnerText);
                            int xnf = int.Parse(tool.SelectSingleNode("SPEEDXN1").InnerText);
                            int xnt = int.Parse(tool.SelectSingleNode("SPEEDXN2").InnerText);
                            int ypf = int.Parse(tool.SelectSingleNode("SPEEDYP1").InnerText);
                            int ypt = int.Parse(tool.SelectSingleNode("SPEEDYP2").InnerText);
                            int ynf = int.Parse(tool.SelectSingleNode("SPEEDYN1").InnerText);
                            int ynt = int.Parse(tool.SelectSingleNode("SPEEDYN2").InnerText);
                            int arcf = int.Parse(tool.SelectSingleNode("ARCF").InnerText);
                            int arct = int.Parse(tool.SelectSingleNode("ARCT").InnerText);

                            // Create new instance of tool
                            Tool tl = new Tool(id, select, on, off, arcf, arct, xpt, xpf, xnt, xnf, ypt, ypf, ynt, ynf);
                            // Add new tool to tool list
                            Tools.Add(tl);
                        }
                    }
                }
                else
                    Tools.Add(new Tool());

                XmlNode steeltools = xml.Read("STEELTOOLS");

                if (steeltools != null)
                {
                    foreach (XmlNode tool in steeltools)
                    {
                        SteelTools.Add(new SteelTool(tool));
                    }

                    if (SteelTools.Count >= 1)
                        CurrentSteelTool = SteelTools[0].Id;
                }
                else
                {
                    SteelTools.Add(new SteelTool());
                    CurrentSteelTool = SteelTools[0].Id;
                }
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);

                if (Tools.Count <= 0)
                    Tools.Add(new Tool());

                if (SteelTools.Count <= 0)
                {
                    SteelTools.Add(new SteelTool());
                    CurrentSteelTool = SteelTools[0].Id;
                }
            }
        }

        /// <summary>
        /// Gets tool from list by id
        /// </summary>
        /// <param name="id">Id of tool</param>
        /// <returns>Tool by given id</returns>
        public Tool GetTool(int id)
        {
            Tool result = null;

            try
            {
                // Go through all tools
                for (int i = 0; i < Tools.Count; i++)
                {
                    // Check if tool matches id
                    if (Tools[i].Id == id)
                    {
                        result = Tools[i];
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Gets the next steel tool in list and changes current tool
        /// </summary>
        /// <returns>Next steel tool in list</returns>
        public SteelTool GetNextSteelTool()
        {
            SteelTool result = new SteelTool();

            try
            {
                result = SteelTools[CurrentSteelTool];
                CurrentSteelTool = result.Id;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Gets steel tool in list by id and changes current tool
        /// </summary>
        /// <param name="id">Id of tool</param>
        /// <returns>Steel tool by id</returns>
        public SteelTool GetSteelTool(int id)
        {
            SteelTool result = new SteelTool();

            try
            {
                result = SteelTools[id - 1];
                CurrentSteelTool = result.Id;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Gets the current steel tool
        /// </summary>
        /// <returns>Current steel tool</returns>
        public SteelTool GetCurrentSteelTool()
        {
            SteelTool result = new SteelTool();

            try
            {
                result = SteelTools[CurrentSteelTool - 1];
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }
    }
}
