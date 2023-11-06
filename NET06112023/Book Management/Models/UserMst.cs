using System;
using System.Collections.Generic;

namespace Book_Management.Models;

public partial class UserMst
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
