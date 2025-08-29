using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dbfirst.Models;

public partial class Department
{
    public int Departmentid { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
