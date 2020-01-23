using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core
{
    public class UserTable
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
    }
}
