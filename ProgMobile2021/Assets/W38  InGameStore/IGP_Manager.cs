using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IGP_Manager : MonoBehaviour
{
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}"));
    }

    public void OnPurchaseCompleted(Product product)
    {
        CurrencyManager currencyManager = FindObjectOfType<CoinManager>();
        if (!currencyManager) currencyManager = FindObjectOfType<CoinManagerSavedLocally>();
        if (!currencyManager) currencyManager = FindObjectOfType<CoinManagerSavedOnline>();

        switch (product.definition.id)
        {
            case "com.Robin.MobileProg2021.Purchase1":
                if (currencyManager.CurrentCurrency >= 1000) currencyManager.RemoveCurrency(1000);
                else Debug.Log("Not enough coins!");
                break;
            case "com.Robin.MobileProg2021.Purchase2":
                if (currencyManager.CurrentCurrency >= 6000) currencyManager.RemoveCurrency(6000);
                else Debug.Log("Not enough coins!");
                break;
            case "com.Robin.MobileProg2021.Purchase3":
                if (currencyManager.CurrentCurrency >= 15000) currencyManager.RemoveCurrency(15000);
                else Debug.Log("Not enough coins!");
                break;
            default:
                break;
        }
    }
    }
