using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Family { get; set; }
        public string? WebsiteUrl { get; set; }
    }
}
