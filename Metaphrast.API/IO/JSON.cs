using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace Metaphrast.IO
{
    internal class Json<T>
    {
        public static T Load(string path)
        {
            var json = JsonNode.Parse(File.ReadAllText(path));
            if (json == null)
            {
                throw new Exception();
            }

            var jsonObject = JsonConvert.DeserializeObject<T>(json.ToJsonString());
            if (jsonObject == null)
            {
                throw new Exception();
            }

            return jsonObject;
        }

        public static void Save(T entity, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(entity));
        }

    }
}
