using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن نام الزامی است")]
        [RegularExpression("^[a-zA-Z]+$")]
        public required string Name { get; set; }

        [MinLength(5, ErrorMessage = "{0} باید حداقل دارای {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} باید حداکثر دارای {1} کاراکتر باشد")]
        public required string Family { get; set; }

        //[EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است")]
        //public required string Email { get; set; }


        //[Phone(ErrorMessage = "شماره موبایل نامعتبر است")]
        //public required string Phone { get; set; }

        [Required]
        public string? WebsiteUrl { get; set; }
    }
}
