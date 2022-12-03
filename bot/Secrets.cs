using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot
{
    struct Secrets
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("apitoken")]
        public string ApiToken { get; private set; }
    }
}
