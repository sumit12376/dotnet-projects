using System;
using System.Collections.Generic;

namespace dbfirst.Models;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Departmentid { get; set; }


    public virtual Department? Department { get; set; }

}
