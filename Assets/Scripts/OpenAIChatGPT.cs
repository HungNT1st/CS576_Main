using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; 

public class OpenAIChatGPT : MonoBehaviour
{
    private string apiKey = "YOUR_API_KEY";
    private string apiUrl = "https://api.openai.com/v1/chat/completions";

    private void Start()
    {
        
    }

    public IEnumerator GetChatGPTResponse(string prompt, System.Action<string> callback)
    {
        string path = System.IO.Path.Combine(Application.dataPath, "Scripts", "configK_e_y.txt");
        apiKey = System.IO.File.ReadAllText(path).Trim().ToString();
        var jsonData = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "You must only respond with a valid JSON schema. Do not include any plain text or explanations outside of the JSON schema." },
                new { role = "user", content = prompt }
            },
            max_tokens = 300
        };

        string jsonString = JsonConvert.SerializeObject(jsonData);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            var responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);

            var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseText);
            callback(response.choices[0].message.content.Trim());
        }
    }

    public class OpenAIResponse
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}