using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substitute
{
    class Tool
    {
        private int _Id;
        /// <summary>
        /// Tool id
        /// </summary>
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

        private int _XPT;
        /// <summary>
        /// Speed of T on positive X axis
        /// </summary>
        public int XPT
        {
            get
            {
                return _XPT;
            }
            set
            {
                _XPT = value;
            }
        }

        private int _XPF;
        /// <summary>
        /// Speed of F on positive X axis
        /// </summary>
        public int XPF
        {
            get
            {
                return _XPF;
            }
            set
            {
                _XPF = value;
            }
        }

        private int _XNT;
        /// <summary>
        /// Speed of T on negative X axis
        /// </summary>
        public int XNT
        {
            get
            {
                return _XNT;
            }
            set
            {
                _XNT = value;
            }
        }

        private int _XNF;
        /// <summary>
        /// Speed of F on negative X axis
        /// </summary>
        public int XNF
        {
            get
            {
                return _XNF;
            }
            set
            {
                _XNF = value;
            }
        }

        private int _YPT;
        /// <summary>
        /// Speed of T on positive Y axis
        /// </summary>
        public int YPT
        {
            get
            {
                return _YPT;
            }
            set
            {
                _YPT = value;
            }
        }

        private int _YPF;
        /// <summary>
        /// Speed of F on positive Y axis
        /// </summary>
        public int YPF
        {
            get
            {
                return _YPF;
            }
            set
            {
                _YPF = value;
            }
        }

        private int _YNT;
        /// <summary>
        /// Speed of T on negative Y axis
        /// </summary>
        public int YNT
        {
            get
            {
                return _YNT;
            }
            set
            {
                _YNT = value;
            }
        }

        private int _YNF;
        /// <summary>
        /// Speed of F on negative Y axis
        /// </summary>
        public int YNF
        {
            get
            {
                return _YNF;
            }
            set
            {
                _YNF = value;
            }
        }

        private string _Select;
        /// <summary>
        /// Replace string of tool select
        /// </summary>
        public string Select
        {
            get
            {
                return _Select;
            }
            set
            {
                _Select = value;
            }
        }

        private string _On;
        /// <summary>
        /// Replace string of pendown
        /// </summary>
        public string On
        {
            get
            {
                return _On;
            }
            set
            {
                _On = value;
            }
        }

        private string _Off;
        /// <summary>
        /// Replace string of penup
        /// </summary>
        public string Off
        {
            get
            {
                return _Off;
            }
            set
            {
                _Off = value;
            }
        }

        private int _ArcF;
        /// <summary>
        /// Speed F of arc
        /// </summary>
        public int ArcF
        {
            get
            {
                return _ArcF;
            }
            set
            {
                _ArcF = value;
            }
        }

        private int _ArcT;
        /// <summary>
        /// Speed T of arc
        /// </summary>
        public int ArcT
        {
            get
            {
                return _ArcT;
            }
            set
            {
                _ArcT = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// Id is 1
        /// Replace texts are empty
        /// Speed is default
        /// </summary>
        public Tool()
            : this(1)
        {

        }

        /// <summary>
        /// Custom constructor
        /// Replace texts are empty
        /// Speed is default
        /// </summary>
        /// <param name="id">Tool id</param>
        public Tool(int id)
            : this(id, "", "", "")
        {

        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="id">Tool id</param>
        /// <param name="select">Tool select replace text</param>
        /// <param name="on">Pendown replace text</param>
        /// <param name="off">Penup replace text</param>
        public Tool(int id, string select, string on, string off)
            : this(id, select, on, off, 505, 605, 601, 501, 602, 502, 603, 503, 604, 504)
        {

        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="id">Tool id</param>
        /// <param name="select">Tool select replace text</param>
        /// <param name="on">Pendown replace text</param>
        /// <param name="off">Penup replacd text</param>
        /// <param name="arcf">Speed F of arc</param>
        /// <param name="arct">Speed T of arc</param>
        /// <param name="xpt">Speed of T on positive X axis</param>
        /// <param name="xpf">Speed of F on positive X axis</param>
        /// <param name="xnt">Speed of T on negative X axis</param>
        /// <param name="xnf">Speed of F on negative X axis</param>
        /// <param name="ypt">Speed of T on positive Y axis</param>
        /// <param name="ypf">Speed of F on positive Y axis</param>
        /// <param name="ynt">Speed of T on negative Y axis</param>
        /// <param name="ynf">Speed of F on negative Y axis</param>
        public Tool(int id, string select, string on, string off, int arcf, int arct, int xpt, int xpf, int xnt, int xnf, int ypt, int ypf, int ynt, int ynf)
        {
            Id = id;
            Select = select;
            On = on;
            Off = off;
            ArcF = arcf;
            ArcT = arct;
            XPT = xpt;
            XPF = xpf;
            XNT = xnt;
            XNF = xnf;
            YPT = ypt;
            YPF = ypf;
            YNT = ynt;
            YNF = ynf;
        }
    }
}
