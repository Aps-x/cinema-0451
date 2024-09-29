using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;

public class VideoPlayerHelper : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] bool shouldLoad = false;
    [SerializeField] float loadDelay = 4;
    const string FIREBASE_BASE_URL = "https://firebasestorage.googleapis.com/v0/b/wfdd-85bb7.appspot.com/o/";
    GameObject player;
    VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (shouldLoad)
        {
            StartCoroutine(LoadVideo());
        }
    }
    IEnumerator LoadVideo()
    {
        // This is redundant now but whatever
        yield return new WaitForSeconds(loadDelay);
        videoPlayer.url = FIREBASE_BASE_URL + UnityWebRequest.EscapeURL(fileName) + "?alt=media";
    }
    // 3D audio works in editor but not in the WEBGL build in browser
    // This workaround means that the video only plays when the player is within the box collider
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger zone.");
        if (other.gameObject == player)
        {
            videoPlayer.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exited the trigger zone.");
        if (other.gameObject == player)
        {
            videoPlayer.Stop();
        }
    }
}
