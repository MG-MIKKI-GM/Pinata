using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum StatusBuy
{
    Buy, Selected, NoBuy
}

public class BuyController : MonoBehaviour
{
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Sprite iconeBuy;
    [SerializeField] private Sprite iconeNoBuy;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Weapon weapon;
    private StatusBuy statusWeapon;

    public static BuyController Select
    {
        get
        {
            BuyController free = null;
            foreach (BuyController buy in FindObjectsOfType<BuyController>())
            {
                if (buy.StatusWeapon == StatusBuy.Selected)
                    return buy;

                if (buy.Weapon.Price == 0)
                    free = buy;
            }
            free.StatusWeapon = StatusBuy.Selected;
            return free;
        }
    }

    public static UnityAction<BuyController> onClickWeapon;

    public Weapon Weapon => weapon;

    public StatusBuy StatusWeapon 
    {
        get
        {
            return statusWeapon;
        }
        set
        {
            PlayerPrefs.SetString("Buy" + weapon.id, value.ToString());
            statusWeapon = value;
            if (value == StatusBuy.NoBuy)
            {
                buttonBuy.GetComponent<Image>().sprite = iconeNoBuy;
                text.text = weapon.Price.ToString();
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
        string status = PlayerPrefs.GetString("Buy" + weapon.id, StatusBuy.NoBuy.ToString());

        if (status == StatusBuy.NoBuy.ToString())
        {
            StatusWeapon = StatusBuy.NoBuy;
        }
        else if (status == StatusBuy.Buy.ToString())
        {
            StatusWeapon = StatusBuy.Buy;
        }
        else
        {
            StatusWeapon = StatusBuy.Selected;
        }

        buttonBuy.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        onClickWeapon?.Invoke(this);
    }

    private void OnDestroy()
    {
        buttonBuy.onClick.RemoveAllListeners();
    }
}
