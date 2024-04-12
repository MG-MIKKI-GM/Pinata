using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private TMP_Text textCoin;

    private void Start()
    {
        textCoin.text = CoinController.Coin.ToString();
        CoinController.onChangeCoin += UpdateTexCoin;
    }

    private void UpdateTexCoin(int value)
    {
        textCoin.text = value.ToString();
    }

    private void OnDestroy()
    {
        CoinController.onChangeCoin -= UpdateTexCoin;
    }
}
