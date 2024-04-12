using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Advertising : MonoBehaviour
{
    public static UnityAction<int> OnAdVideoClose;
    public static UnityAction<int> OnAdVideoRewarded;
    public static UnityAction OnAdImageClose;

    [DllImport("__Internal")]
    public static extern void AdImage();
    [DllImport("__Internal")]
    public static extern void _AdVideo(int value);

    public static bool isVideo;
    public static bool isImage;

    public void AdImageClose()
    {
        isImage = false;
        AudioController.ResumeAll();
        OnAdImageClose?.Invoke();
    }

    public void AdVideoClose(int value)
    {
        isVideo = false;
        AudioController.ResumeAll();
        OnAdVideoClose?.Invoke(value);
    }

    public void AdVideoRewarded(int value)
    {
        OnAdVideoRewarded?.Invoke(value);
    }
    public static void ShowAdImage()
    {
        isImage = true;
        AudioController.PauseAll();
        AdImage();
    }
    public static void ShowAdVideo(int value)
    {
        isVideo = true;
        AudioController.PauseAll();
        _AdVideo(value);
    }
}
