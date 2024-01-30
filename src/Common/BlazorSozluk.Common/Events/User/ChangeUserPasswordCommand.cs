using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Events.User;

public class ChangeUserPasswordCommand : IRequest<bool>
{
    public ChangeUserPasswordCommand(Guid userId, string oldpassword, string newPassword)
    {
        UserId = userId;
        Oldpassword = oldpassword;
        NewPassword = newPassword;
    }

    public Guid? UserId{ get; set; }

    public string Oldpassword{ get; set; }

    public string NewPassword{ get; set; }
}
