using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyPinataController : MonoBehaviour
{
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Sprite iconeBuy;
    [SerializeField] private Sprite iconeNoBuy;
    [SerializeField] private TMP_Text text;
    [SerializeField] private PenataController pinata;
    private StatusBuy statusPinata;

    public static BuyPinataController Select
    {
        get
        {
            BuyPinataController free = null;
            foreach (BuyPinataController buy in FindObjectsOfType<BuyPinataController>())
            {
                if (buy.StatusPinata == StatusBuy.Selected)
                    return buy;

                if (buy.Pinata.Price == 0)
                    free = buy;
            }
            free.StatusPinata = StatusBuy.Selected;
            return free;
        }
    }

    public static UnityAction<BuyPinataController> onClickPinata;

    public PenataController Pinata => pinata;

    public StatusBuy StatusPinata
    {
        get
        {
            return statusPinata;
        }
        set
        {
            PlayerPrefs.SetString("BuyPinata" + Pinata.id, value.ToString());
            statusPinata = value;
            if (value == StatusBuy.NoBuy)
            {
                buttonBuy.GetComponent<Image>().sprite = iconeNoBuy;
                text.text = Pinata.Price.ToString();
            }
            else if (value == StatusBuy.Buy || value == StatusBuy.Selected)
            {
                buttonBuy.GetComponent<Image>().sprite = iconeBuy;
                if (text != null)
                    Destroy(text.gameObject);
            }
        }
    }

    private void Start()
    {
        string status = PlayerPrefs.GetString("BuyPinata" + Pinata.id, StatusBuy.NoBuy.ToString());

        if (status == StatusBuy.NoBuy.ToString())
        {
            StatusPinata = StatusBuy.NoBuy;
        }
        else if (status == StatusBuy.Buy.ToString())
        {
            StatusPinata = StatusBuy.Buy;
        }
        else
        {
            StatusPinata = StatusBuy.Selected;
        }

        buttonBuy.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        onClickPinata?.Invoke(this);
    }

    private void OnDestroy()
    {
        buttonBuy.onClick.RemoveAllListeners();
    }
}
