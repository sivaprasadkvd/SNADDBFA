using System;
using System.Collections.Generic;

namespace SNADDBFA.Models;

public partial class RegistrationTable
{
    public string UserId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int Zipcode { get; set; }

    public string Email { get; set; } = null!;

    public bool Sex { get; set; }

    public bool Language { get; set; }

    public string About { get; set; } = null!;

    public int Sno { get; set; }
}
