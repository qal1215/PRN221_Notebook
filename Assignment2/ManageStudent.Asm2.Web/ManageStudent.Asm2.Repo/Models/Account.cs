﻿using System;
using System.Collections.Generic;

namespace ManageStudent.Asm2.Repo.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Type { get; set; }
}
