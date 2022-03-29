using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web;

public class UserLoginDto
{
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class UserRegisterDto
{
    [Display(Name = "First Name")]
    [Required]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    [Required]
    public string LastName { get; set; }
    [Display(Name = "Email")]
    [Required]
    public string Email { get; set; }
    [Display(Name = "Password")]
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    [Compare(nameof(ConfirmPassword))]
    public string Password { get; set; }
    [Display(Name = "Confirm Password")]
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; } = -1;
    [Display(Name = "Category Name")]
    public string Name { get; set; }
}

public class ProductCreateDto
{
    public IFormFile Image { get; set; }
    [Required]
    public string Name { get; set; }
    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
}

public class ProductUpdateDto
{
    public int Id { get; set; }
    public IFormFile Image { get; set; }
    [Required]
    public string Name { get; set; }
    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
}

public class ShoppingListAddDto
{
    [MinLength(1)]
    public string Name { get; set; }
}

public class UserShoppingListShareDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public class ShoppingListProductAddDto
{
    public int ProductId { get; set; }
    public int ShoppingListId { get; set; }
    public string Description { get; set; }
}