using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PenataController : MonoBehaviour
{
    public static event UnityAction<PenataController> OnClick;

    [SerializeField] private int price;
    public int id;

    public int Price => price;

    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }
}
