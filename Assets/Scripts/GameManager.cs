using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    private Weapon weapon;
    private PenataController pinata;

    private void Start()
    {
        pinata = Instantiate(BuyPinataController.Select.Pinata.gameObject).GetComponent<PenataController>();
        weapon = BuyController.Select?.Weapon;
        PenataController.OnClick += ClickPenata;
        BuyController.onClickWeapon += BuyWeapon;
        BuyPinataController.onClickPinata += BuyPinata;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && CoinController.Coin == 7)
            CoinController.Coin = 85000;
    }

    private void ClickPenata(PenataController penata)
    {
        if (weapon == null) return;

        Vector3 pos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        pos = hit.point;

        Weapon w = CreatWeapon(pos);

        WeaponAttack(w, pos);

        CoinController.Coin += w.Damage;
    }

    private Weapon CreatWeapon(Vector3 pos)
    {
        GameObject newWeaponObj = Instantiate(weapon.gameObject, pos + Vector3.down * 0.5f, Quaternion.identity);
        newWeaponObj.name = weapon.gameObject.name;

        return newWeaponObj.GetComponent<Weapon>();
    }

    private void WeaponAttack(Weapon weapon, Vector3 pos)
    {
        weapon.PlayParticle(pos);

        Vector3[] axies = new Vector3[]
        {
            new Vector3(0,-0.5f,0.5f),
            new Vector3(0,0.5f,-0.5f),
        };

        weapon.Rotate(90, axies[Random.Range(0, axies.Length)], 5);
    }

    private void BuyPinata(BuyPinataController buy)
    {
        if (buy.StatusPinata == StatusBuy.NoBuy)
        {
            if (CoinController.Coin < buy.Pinata.Price) return;
            CoinController.Coin -= buy.Pinata.Price;
        }

        Destroy(pinata.gameObject);
        BuyPinataController.Select.StatusPinata = StatusBuy.Buy;

        buy.StatusPinata = StatusBuy.Selected;

        pinata = Instantiate(buy.Pinata.gameObject).GetComponent<PenataController>();
    }

    private void BuyWeapon(BuyController buy)
    {
        if (buy.StatusWeapon == StatusBuy.NoBuy)
        {
            if (CoinController.Coin < buy.Weapon.Price) return;
            CoinController.Coin -= buy.Weapon.Price;
        }

        BuyController.Select.StatusWeapon = StatusBuy.Buy;

        buy.StatusWeapon = StatusBuy.Selected;
        weapon = buy.Weapon;
    }

    private void OnDestroy()
    {
        PenataController.OnClick -= ClickPenata;
        BuyPinataController.onClickPinata -= BuyPinata;
        BuyController.onClickWeapon -= BuyWeapon;
    }
}
