using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FetchAndDisplayImage : MonoBehaviour
{
    [SerializeField] string imagePath;
    [SerializeField] endpoint server;
    [SerializeField] Renderer planeRenderer;
    enum endpoint 
    {
        NFSA,
        Firebase
    }
    const string NFSA_BASE_URL = "https://australia-southeast1-wfdd-cors.cloudfunctions.net/cors-bypass/";
    const string FIREBASE_BASE_URL = "https://firebasestorage.googleapis.com/v0/b/wfdd-85bb7.appspot.com/o/";
    const float ASPECT_RATIO_SCALAR = 3.0f;

    void Start()
    {
        string imageUrl = "";

        if (server == endpoint.NFSA) 
        {
            imageUrl = NFSA_BASE_URL + "?data=" + UnityWebRequest.EscapeURL(imagePath) + "&type=media";
        }
        else if (server == endpoint.Firebase) 
        {
            imageUrl = FIREBASE_BASE_URL + UnityWebRequest.EscapeURL(imagePath) + "?alt=media";
        }

        StartCoroutine(GetImage(imageUrl));
    }

    IEnumerator GetImage(string imageUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                planeRenderer.material.mainTexture = texture;

                // Get image dimensions
                float imageWidth = texture.width;
                float imageHeight = texture.height;

                // Calculate the aspect ratio
                float aspectRatio = imageWidth / imageHeight;

                // Adjust the plane size based on the aspect ratio
                Vector3 newScale = planeRenderer.gameObject.transform.localScale;
                newScale.x = aspectRatio / ASPECT_RATIO_SCALAR;  
                newScale.z = 1.0f / ASPECT_RATIO_SCALAR;
                planeRenderer.gameObject.transform.localScale = newScale;
            }
        }
    }
}