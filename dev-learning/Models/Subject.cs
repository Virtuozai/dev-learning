﻿using System.Collections.Generic;

namespace dev_learning.Models
{
    public class Subject
    {

    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }

    }
 }
