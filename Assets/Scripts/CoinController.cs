using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CoinController
{
    public static event UnityAction<int> onChangeCoin;

    public static int Coin
    {
        get 
        {
            return PlayerPrefs.GetInt("Coin", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Coin", value);
            onChangeCoin?.Invoke(value);
        }
    }
        
}
