using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pa.Api;

public class Book
{
    [Required]
    [DisplayName("Book id")]
    public int Id { get; set; }
        
        
    [Required]
    [DisplayName("Book name")]
    public string? Name { get; set; }
        
        
    [Required]
    [DisplayName("Book author info")]
    public string? Author { get; set; }
        
        
    [Required]
    [DisplayName("Book page count")]
    public int PageCount { get; set; }
        
        
    [Required]
    [DisplayName("Book year")]
    public int Year { get; set; }

}