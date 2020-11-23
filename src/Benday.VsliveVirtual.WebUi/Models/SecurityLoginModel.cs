using System.Collections.Generic;

namespace Benday.VsliveVirtual.WebUi.Models
{
    public class SecurityLoginModel
    {
        public SecurityLoginModel()
        {
            LoginTypes = new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> LoginTypes { get; set; }
    }
}
