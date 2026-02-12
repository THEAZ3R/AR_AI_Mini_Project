using UnityEngine;
using UnityEngine.Video;

public class AnimalUIController : MonoBehaviour
{
    public AudioSource audioSource;
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;

    public void PlaySound()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    public void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPanel.SetActive(true);
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        videoPanel.SetActive(false);
    }
}