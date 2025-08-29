using System;
using System.Collections.Generic;

namespace dbfirst.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
