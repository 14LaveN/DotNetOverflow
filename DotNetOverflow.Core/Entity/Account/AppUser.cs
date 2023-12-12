using System.Collections;
using DotNetOverflow.Core.Entity.Question;
using Microsoft.AspNetCore.Identity;

namespace DotNetOverflow.Core.Entity.Account;

public class AppUser : IdentityUser<long>
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public new string? Email { get; set; }
    public string? Patronymic { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? RefreshToken { get; set; }
    public ICollection<QuestionEntity>? Questions { get; set; }
}