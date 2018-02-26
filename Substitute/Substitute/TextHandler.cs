using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substitute
{
    class TextHandler
    {
        private string[] _Lines;
        /// <summary>
        /// All lines from CNC file
        /// </summary>
        public string[] Lines
        {
            get
            {
                return _Lines;
            }
            private set
            {
                _Lines = value;
            }
        }
        
        /// <summary>
        /// Instance of Reference to detect commands
        /// </summary>
        private Reference Reference;

        /// <summary>
        /// Default constructor
        /// Lines are empty
        /// Reference is default
        /// </summary>
        public TextHandler()
            : this(new string[] { })
        {

        }

        /// <summary>
        /// Custom constructor
        /// Reference is default
        /// </summary>
        /// <param name="lines">CNC file lines</param>
        public TextHandler(string[] lines)
            : this (lines, new Reference())
        {
            
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="lines">CNC file lines</param>
        /// <param name="refc">Instance of reference to detect commands</param>
        public TextHandler(string[] lines, Reference refc)
        {
            Lines = lines;
            Reference = refc;
        }

        /// <summary>
        /// Detects commands in lines and replaces them with new lines and commands
        /// </summary>
        /// <param name="mh">MachineHandler to get tools from</param>
        /// <returns>All CNC file lines with replaced lines</returns>
        public string[] Replace(MachineHandler mh)
        {
            string[] result = Lines;

            try
            {
                // Begin on first tool
                Tool tool = mh.GetTool(1);

                // Go through all lines
                for (int i = 0; i < result.Length; i++)
                {
                    bool skip = false;

                    for (int k = 0; k < Reference.Skip.Length; k++)
                    {
                        if (result[i].Contains(Reference.Skip[k]))
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                        continue;

                    // Check for each type of command if it exists in line
                    for (int l = 0; l < Reference.LineReferences.Length; l++)
                    {
                        if (result[i].Contains(Reference.LineReferences[l]) && result[i].Contains('&'))
                        {
                            // Create new line formula
                            double angle = double.Parse(result[i].Substring(result[i].IndexOf('&') + 1));
                            string formula = Reference.GetFullLineFormula(angle, tool);
                            result[i] = result[i].Substring(0, result[i].IndexOf('&') - 1) + formula;
                            break;
                        }
                    }
                    
                    for (int a = 0; a < Reference.ArcReferences.Length; a++)
                    {
                        if (result[i].Contains(Reference.ArcReferences[a]) && result[i].Contains('&'))
                        {
                            // Create new arc formula
                            string formula = Reference.GetFullArcFormula(tool);
                            result[i] = result[i].Substring(0, result[i].IndexOf('&') - 1) + formula;
                            break;
                        }
                    }

                    for (int s = 0; s < Reference.SubCallReferences.Length; s++)
                    {
                        if (result[i].Contains(Reference.SubCallReferences[s]) && result[i].Contains('&'))
                        {
                            // Get id of next tool
                            int id = int.Parse(result[i].Substring(result[i].IndexOf('&') + 1));
                            // Switch tool
                            tool = mh.GetTool(id);
                            // Replace with new text
                            string formula = Reference.GetFullSubcallFormula(tool);
                            result[i] = formula;
                            break;
                        }
                    }
                    
                    for (int d = 0; d < Reference.PenDownReferences.Length; d++)
                    {
                        if (result[i].Contains(Reference.PenDownReferences[d]) && result[i].Contains('&'))
                        {
                            // Get replace text of pendown
                            string formula = Reference.GetFullPenDownFormula(tool);
                            result[i] = formula;
                            break;
                        }
                    }

                    for (int u = 0; u < Reference.PenUpReferences.Length; u++)
                    {
                        if (result[i].Contains(Reference.PenUpReferences[u]) && result[i].Contains('&'))
                        {
                            // Get replace text of penup
                            string formula = Reference.GetFullPenUpFormula(tool);
                            result[i] = formula;
                            break;
                        }
                    }

                    for (int p = 0; p < Reference.PenReference.Length; p++)
                    {
                        if (result[i].Contains(Reference.PenReference[p]) && result[i].Contains('&'))
                        {
                            // Get current tool
                            SteelTool stool = mh.GetCurrentSteelTool();
                            // Get id
                            int id = int.Parse(result[i].Substring(result[i].IndexOf('&') + 1));

                            // If id is 0 use pen up
                            if (id <= 0)
                                result[i] = stool.PenUpRef;
                            // Else use new tool and pen down
                            else
                            {
                                stool = mh.GetSteelTool(id);
                                result[i] = stool.PenDownRef;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }
    }
}
