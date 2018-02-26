using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Substitute
{
    class Reference
    {
        private string[] _SubReferences;
        /// <summary>
        /// All commands to make a new sub
        /// </summary>
        public string[] SubReferences
        {
            get
            {
                return _SubReferences;
            }
            private set
            {
                _SubReferences = value;
            }
        }

        private string[] _EndSubReferences;
        /// <summary>
        /// All commands to end the sub
        /// </summary>
        public string[] EndSubReferences
        {
            get
            {
                return _EndSubReferences;
            }
            private set
            {
                _EndSubReferences = value;
            }
        }

        private string[] _LineReferences;
        /// <summary>
        /// All commands to make a new line
        /// </summary>
        public string[] LineReferences
        {
            get
            {
                return _LineReferences;
            }
            private set
            {
                _LineReferences = value;
            }
        }

        private string[] _ArcReferences;
        /// <summary>
        /// All commands to make a new arc
        /// </summary>
        public string[] ArcReferences
        {
            get
            {
                return _ArcReferences;
            }
            private set
            {
                _ArcReferences = value;
            }
        }

        private string[] _SubCallReferences;
        /// <summary>
        /// All commands to call a sub
        /// </summary>
        public string[] SubCallReferences
        {
            get
            {
                return _SubCallReferences;
            }
            private set
            {
                _SubCallReferences = value;
            }
        }

        private string[] _PenDownReferences;
        /// <summary>
        /// All commands to set pen to down
        /// </summary>
        public string[] PenDownReferences
        {
            get
            {
                return _PenDownReferences;
            }
            private set
            {
                _PenDownReferences = value;
            }
        }

        private string[] _PenUpReferences;
        /// <summary>
        /// All commands to set pen to up
        /// </summary>
        public string[] PenUpReferences
        {
            get
            {
                return _PenUpReferences;
            }
            private set
            {
                _PenUpReferences = value;
            }
        }

        private string[] _PenReference;
        /// <summary>
        /// All commands to set pen color
        /// </summary>
        public string[] PenReference
        {
            get
            {
                return _PenReference;
            }
            set
            {
                _PenReference = value;
            }
        }

        /// <summary>
        /// Skips all these lines in program
        /// </summary>
        private string[] _Skip;
        public string[] Skip
        {
            get
            {
                return _Skip;
            }
            set
            {
                _Skip = value;
            }
        }

        private string _LineFormula;
        /// <summary>
        /// Formula for line speed
        /// </summary>
        public string LineFormula
        {
            get
            {
                return _LineFormula;
            }
            private set
            {
                _LineFormula = value;
            }
        }

        private string _ArcFormula;
        /// <summary>
        /// Formula for arc speed
        /// </summary>
        public string ArcFormula
        {
            get
            {
                return _ArcFormula;
            }
            private set
            {
                _ArcFormula = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// Hardcoded references from xml file
        /// </summary>
        public Reference()
            : this(new string[] { "DFS,P", "PROGIN", "%", "O", "P" }, new string[] { "M30" }, new string[] { "G00", "G1", "G01" }, 
            new string[] { "G2", "G02", "G3", "G03" }, new string[] { "Q", "P" }, new string[] { "PD" }, new string[] { "PU" }, new string[] { "M98P" },
            new string[] { "M98P8010" }, "F#xT#y", "F#xT#y")
        {
            
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="xml">Xml node with all information (Default node is "REFS")</param>
        public Reference(XmlNode xml)
        {
            try
            {
                SubReferences = SplitReferences(xml.SelectSingleNode("SUBS").InnerText);
                EndSubReferences = SplitReferences(xml.SelectSingleNode("SUBE").InnerText);
                LineReferences = SplitReferences(xml.SelectSingleNode("LINE").InnerText);
                ArcReferences = SplitReferences(xml.SelectSingleNode("ARC").InnerText);
                SubCallReferences = SplitReferences(xml.SelectSingleNode("SUBC").InnerText);
                PenDownReferences = SplitReferences(xml.SelectSingleNode("PD").InnerText);
                PenUpReferences = SplitReferences(xml.SelectSingleNode("PU").InnerText);
                PenReference = SplitReferences(xml.SelectSingleNode("PEN").InnerText);
                Skip = SplitReferences(xml.SelectSingleNode("SKIP").InnerText);
                LineFormula = xml.SelectSingleNode("ANGLE").InnerText;
                ArcFormula = xml.SelectSingleNode("ANGLE").InnerText;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="subref">All commands to make a new sub</param>
        /// <param name="endsubref">All commands to detect ending sub</param>
        /// <param name="lineref">All commands to create a new line</param>
        /// <param name="arcref">All commands to create a new arc</param>
        /// <param name="subcallref">All commands to call a sub</param>
        /// <param name="pendownref">All commands to set pen to down</param>
        /// <param name="penupref">All commands to set pen to up</param>
        /// <param name="lineformula">Formula for line speed</param>
        /// <param name="arcformula">Formula for arc speed</param>
        public Reference(string[] subref, string[] endsubref, string[] lineref, string[] arcref, string[] subcallref, string[] pendownref, string[] penupref, 
            string[] penref, string[] skip, string lineformula, string arcformula)
        {
            SubReferences = subref;
            EndSubReferences = endsubref;
            LineReferences = lineref;
            ArcReferences = arcref;
            SubCallReferences = subcallref;
            PenDownReferences = pendownref;
            PenUpReferences = penupref;
            PenReference = penref;
            Skip = skip;
            LineFormula = lineformula;
            ArcFormula = arcformula;
        }

        /// <summary>
        /// Creates the last formula of line to write in the output file
        /// </summary>
        /// <param name="angle">Line angle</param>
        /// <param name="tool">Current tool</param>
        /// <returns>Line formula</returns>
        public string GetFullLineFormula(double angle, Tool tool)
        {
            string result = "";

            try
            {
                // If angle is 0, use X positive on both
                if (angle == 0)
                {
                    result = LineFormula;
                    result = result.Replace("x", tool.XPF.ToString());
                    result = result.Replace("y", tool.XPT.ToString());
                }
                // If angle is 90, use Y positive on both
                else if (angle == 90)
                {
                    result = LineFormula;
                    result = result.Replace("x", tool.YPF.ToString());
                    result = result.Replace("y", tool.YPT.ToString());
                }
                // If angle is 180, use X negative on both
                else if (angle == 180)
                {
                    result = LineFormula;
                    result = result.Replace("x", tool.XNF.ToString());
                    result = result.Replace("y", tool.XNT.ToString());
                }
                // If angle is 270, use Y negative on both
                else if (angle == 270)
                {
                    result = LineFormula;
                    result = result.Replace("x", tool.YNF.ToString());
                    result = result.Replace("y", tool.YNT.ToString());
                }
                // If angle falls in between these angles, make a new formula
                else
                {
                    result = CalculateFormula(angle, tool);
                }
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Creates the last formula of arc to write in the output file
        /// </summary>
        /// <param name="tool">Current tool</param>
        /// <returns>Arc formula</returns>
        public string GetFullArcFormula(Tool tool)
        {
            string result = "";

            try
            {
                result = ArcFormula;
                result = result.Replace("x", tool.ArcF.ToString());
                result = result.Replace("y", tool.ArcT.ToString());
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Get replace text of select
        /// </summary>
        /// <param name="tool">Current tool</param>
        /// <returns>Replace text of tool select</returns>
        public string GetFullSubcallFormula(Tool tool)
        {
            string result = "";

            try
            {
                result = tool.Select;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Get replace text of pendown
        /// </summary>
        /// <param name="tool">Current tool</param>
        /// <returns>Replace text of pendown</returns>
        public string GetFullPenDownFormula(Tool tool)
        {
            string result = "";

            try
            {
                result = tool.On;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Get replace text of penup
        /// </summary>
        /// <param name="tool">Current tool</param>
        /// <returns>Replace text of penup</returns>
        public string GetFullPenUpFormula(Tool tool)
        {
            string result = "";

            try
            {
                result = tool.Off;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Creates a formula for angle that are not 0, 90, 180 or 270 degrees
        /// </summary>
        /// <param name="angle">Angle of line</param>
        /// <param name="tool">Current tool</param>
        /// <returns>Formula of line speed</returns>
        private string CalculateFormula(double angle, Tool tool)
        {
            string result = "";

            try
            {
                double[] numbers = new double[]{ };

                if (angle > 0 && angle < 90)
                {
                    numbers = new double[] { tool.XPF, tool.YPF, tool.XPT, tool.YPT };
                }
                else if (angle > 90 && angle < 180)
                {
                    angle -= 90;
                    numbers = new double[] { tool.YPF, tool.XNF, tool.YPT, tool.XNT };
                }
                else if (angle > 180 && angle < 270)
                {
                    angle -= 180;
                    numbers = new double[] { tool.XNF, tool.YNF, tool.XNT, tool.YNT };
                }
                else if (angle > 270 && angle < 360)
                {
                    angle -= 270;
                    numbers = new double[] { tool.YNF, tool.XPF, tool.YNT, tool.XPT };
                }

                // Create new formula
                string f = "F[#" + numbers[0] + "+[[[#" + numbers[1] + "-#" + numbers[0] + "]/90]*" + angle + "]]";
                string t = "T[#" + numbers[2] + "+[[[#" + numbers[3] + "-#" + numbers[2] + "]/90]*" + angle + "]]";
                result = f + t;
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Split innertext of xml nodes
        /// </summary>
        /// <param name="refs">Xml node inner text</param>
        /// <returns>All commands cut from node</returns>
        public string[] SplitReferences(string refs)
        {
            string[] result = new string[] { };

            try
            {
                // Split line at all ','
                result = refs.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }
    }
}
