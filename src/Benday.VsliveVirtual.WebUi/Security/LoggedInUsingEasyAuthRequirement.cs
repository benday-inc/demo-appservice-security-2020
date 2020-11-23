using Microsoft.AspNetCore.Authorization;
using System;
using System.IO;

namespace Benday.VsliveVirtual.WebUi.Security
{
    public class LoggedInUsingEasyAuthRequirement : IAuthorizationRequirement
    {
    }

}