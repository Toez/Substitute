using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Substitute
{
    class SteelTool
    {
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private string _PenDownRef;
        public string PenDownRef
        {
            get
            {
                return _PenDownRef;
            }
            set
            {
                _PenDownRef = value;
            }
        }

        private string _PenUpRef;
        public string PenUpRef
        {
            get
            {
                return _PenUpRef;
            }
            set
            {
                _PenUpRef = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// Id is 1
        /// References are empty
        /// </summary>
        public SteelTool()
            : this(1)
        {
            
        }

        /// <summary>
        /// Custom constructor
        /// References are empty
        /// </summary>
        /// <param name="id">Id of steel tool</param>
        public SteelTool(int id)
            : this(id, "11", "10")
        {
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="id">Id of steel tool</param>
        /// <param name="pendownref">Replace text of pen down command</param>
        /// <param name="penupref">Replace text of pen up command</param>
        public SteelTool(int id, string pendownref, string penupref)
        {
            Id = id;
            PenDownRef = pendownref;
            PenUpRef = penupref;
        }

        /// <summary>
        /// Gets all data of steel tool from xml node
        /// </summary>
        /// <param name="xml">"TOOL" in "STEELTOOLS" from xml file</param>
        public SteelTool(XmlNode xml)
        {
            Id = int.Parse(xml.SelectSingleNode("ID").InnerText);
            PenDownRef = xml.SelectSingleNode("PD").InnerText;
            PenUpRef = xml.SelectSingleNode("PU").InnerText;
        }
    }
}
