using System;
using System.Collections.Generic;
using System.Text;

namespace hw11
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
