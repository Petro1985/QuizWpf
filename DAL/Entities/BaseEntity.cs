using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class BaseEntity<T>
{
    [Key]
    public T Id { get; set; }
}