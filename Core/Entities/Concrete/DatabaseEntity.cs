using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete;

public class DatabaseEntity : IDatabaseEntity<int>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdate { get; set; }
}