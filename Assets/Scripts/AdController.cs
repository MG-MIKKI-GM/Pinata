using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdController : MonoBehaviour
{
    [SerializeField] private int CountCoin;
    [SerializeField] private Button buttonAd;

    private void Start()
    {
        buttonAd.onClick.AddListener(ShowAdVideo);
        Advertising.OnAdVideoRewarded += AddCoin;
    }

    private void ShowAdVideo()
    {
        Advertising.ShowAdVideo(CountCoin);
    }

    private void AddCoin(int value)
    {
        CoinController.Coin += value;
    }

    private void OnDestroy()
    {
        buttonAd.onClick.RemoveAllListeners();
        Advertising.OnAdVideoRewarded -= AddCoin;
    }
}
