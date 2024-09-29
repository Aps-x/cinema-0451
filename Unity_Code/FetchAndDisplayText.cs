using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchAndDisplayText : MonoBehaviour
{
    // Note: NFSA requires an ID number ||| Firebase requires an ID number and '.txt'
    [SerializeField] string itemID; 
    [SerializeField] endpoint server;
    [SerializeField] TextMeshProUGUI textUI;
    enum endpoint
    {
        NFSA,
        Firebase
    }
    const string NFSA_BASE_URL = "https://australia-southeast1-wfdd-cors.cloudfunctions.net/cors-bypass/";
    const string FIREBASE_BASE_URL = "https://firebasestorage.googleapis.com/v0/b/wfdd-85bb7.appspot.com/o/";

    void Start()
    {
        string textUrl = "";

        if (server == endpoint.NFSA)
        {
            textUrl = NFSA_BASE_URL + "?data=" + UnityWebRequest.EscapeURL(itemID) + "&type=text";
        }
        else if (server == endpoint.Firebase)
        {
            textUrl = FIREBASE_BASE_URL + UnityWebRequest.EscapeURL(itemID) + "?alt=media";
        }

        StartCoroutine(GetText(textUrl));
    }
    IEnumerator GetText(string textUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(textUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error encountered = early return
                Debug.LogError(uwr.error);
                yield break;
            }

            if (server == endpoint.NFSA)
            {
                // Get JSON response as a string
                string jsonResponse = uwr.downloadHandler.text;

                // Parse the JSON into a C# object
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);

                // Access fields: 'name' and 'parentVersion.title'
                string name = response.name;
                string parentTitle = response.parentVersion.title;

                // Add to exhibit text
                textUI.text = parentTitle + " " + name;
            }
            else if (server == endpoint.Firebase)
            {
                // Get the downloaded text
                string downloadedText = uwr.downloadHandler.text;

                // Add to exhibit text
                textUI.text = downloadedText;
            }
        }
    }
}

[System.Serializable]
public class ParentVersion
{
    public string title;
}

[System.Serializable]
public class ApiResponse
{
    public string name;
    public ParentVersion parentVersion;
}