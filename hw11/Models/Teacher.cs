using System;
using System.Collections.Generic;
using System.Text;

namespace hw11
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
