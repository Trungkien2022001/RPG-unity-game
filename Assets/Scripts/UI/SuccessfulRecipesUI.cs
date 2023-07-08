using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuccessfulRecipesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesValue;

    private void Update()
    {
        recipesValue.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
    }
}
