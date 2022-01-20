﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Collections
{
    public class DataGridColumns
    {
        public string name;
        public List<DataGridColumns> innertCol;
        public string binding;
        int sizeCol = 0;
        public int SizeCol
        {
            get 
            {
                if (innertCol == null)
                {
                    return sizeCol;
                }
                else
                {
                    foreach (var elem in innertCol) 
                    {
                        sizeCol += elem.sizeCol;
                    }
                    return sizeCol;
                }
            
            }
            set 
            {
                if (sizeCol != value) 
                {
                    sizeCol = value;
                } 
            }
        }

        public int Level
        {
            get
            {
                if(innertCol==null)
                {
                    return 1;
                }
                var tmp = innertCol.FirstOrDefault();
                if(tmp==null)
                {
                    return 1;
                }
                else
                {
                    return tmp.Level + 1;
                }
            }
        }
    }
}