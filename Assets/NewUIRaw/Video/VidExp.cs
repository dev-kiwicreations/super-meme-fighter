using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidExp : MonoBehaviour
{
    public string videoFileName;
    public GameObject staticImage;
    private const string VideoLoadedKey = "isVideoLoaded";

    bool isVideoLoaded = false;

    private void Start()
    {
        // Initialize isVideoLoaded with PlayerPref
        isVideoLoaded = PlayerPrefs.GetInt(VideoLoadedKey, 0) == 1;
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer player = GetComponent<VideoPlayer>();
        if (player)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            player.url = videoPath;
            player.prepareCompleted += OnVideoPrepared;
            player.Prepare(); // Start preparing the video
        }
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        player.prepareCompleted -= OnVideoPrepared; // Unsubscribe from the event
        if (!isVideoLoaded) //If video is not loaded then delay the video after being prepared for 0.5f
        {
            Invoke(nameof(DisableStaticImage), 0.5f);
            isVideoLoaded = true;

            // Save the state to PlayerPrefs
            PlayerPrefs.SetInt(VideoLoadedKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            DisableStaticImage();
        }
        player.Play(); // Play the video
    }

    public void DisableStaticImage()
    {
        staticImage.SetActive(false); // Hide the static image
    }
}
