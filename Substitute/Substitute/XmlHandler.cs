using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Substitute
{
    class XmlHandler
    {
        private string _FileName;
        /// <summary>
        /// Path to xml file
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
        /// Xml doc to save file in
        /// </summary>
        private XmlDocument doc;
        /// <summary>
        /// Reader to read the xml file
        /// </summary>
        private XmlReader reader;
        /// <summary>
        /// Root node
        /// </summary>
        private XmlNode root;
        /// <summary>
        /// List of nodes that the file contains
        /// </summary>
        private List<XmlNode> nodes;

        /// <summary>
        /// Default constructor
        /// FileName is default
        /// </summary>
        public XmlHandler()
            : this(@".\Config.xml")
        {

        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="fileName">Path to xml file</param>
        public XmlHandler(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Reads a node from the xml file
        /// </summary>
        /// <param name="node">Name of node you want to get</param>
        /// <returns>Node with all child nodes</returns>
        public XmlNode Read(string node)
        {
            XmlNode result = null;

            try
            {
                // Create new doc
                doc = new XmlDocument();
                // Create new reader
                reader = XmlReader.Create(FileName);
                // Initialize list of nodes
                nodes = new List<XmlNode>();

                // Load xml file
                doc.Load(reader);
                // Set root node
                root = doc.LastChild;
                // Add root to node list
                nodes.Add(root);
                // Check for childnodes
                ChildNodes(root);

                // Find node in list
                result = nodes.Find(x => x.Name == node);
            }
            catch (Exception e)
            {
                ErrorHandler.AddMessage(e);
            }

            return result;
        }

        /// <summary>
        /// Checks if current node has a childnode
        /// </summary>
        /// <param name="parent">Parent node</param>
        private void ChildNodes(XmlNode parent)
        {
            foreach (XmlNode child in parent)
            {
                // Add node to node list
                nodes.Add(child);

                // Check for childnodes
                if (child.HasChildNodes)
                    ChildNodes(child);
            }
        }
    }
}
