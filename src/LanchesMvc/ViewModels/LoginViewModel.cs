using System.ComponentModel.DataAnnotations;

namespace LanchesMvc.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o nome")]
    [Display(Name = "Usuário")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}