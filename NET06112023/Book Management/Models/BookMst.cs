using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Management.Models;

public partial class BookMst
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public int CategoryId { get; set; }

    public int SubcategoryId { get; set; }

    public string AuthorName { get; set; } = null!;

    public int BookPages { get; set; }

    public string Publisher { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public string Edition { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string CoverImagePath { get; set; } = null!;

    public string PdfPath { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    [NotMapped]
    public string CategoryName { get; set; }
    [NotMapped]
    public string SubcategoryName { get; set; } = null!;
}
