using Newtonsoft.Json;

namespace MetaphrastSDK.DeepL.Response;

/**
* JSON DeepL response from usage API.
* https://www.deepl.com/de/docs-api/accessing-the-api/authentication/
*
* Usage json response:
* {
*	"character_count": 633586,
*	"character_limit": 1000000000000
* }
*/
internal class UsageResponse
{
    [JsonProperty(PropertyName = "character_count")]
    public long CharacterCount { get; set; }
    [JsonProperty(PropertyName = "character_limit")]
    public long CharacterLimit { get; set; }
}

