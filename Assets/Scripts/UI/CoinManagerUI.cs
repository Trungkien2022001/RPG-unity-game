using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinValue;

    private void Update()
    {
        coinValue.text = DeliveryManager.Instance.GetCoin().ToString();
    }

}