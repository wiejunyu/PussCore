using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Workflow.User
{
    public class OpenUserAction
    {
        public string Key { get; set; }

        public string Prompt { get; set; }

        public string AssignedPrincipal { get; set; }

        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
    }
}
