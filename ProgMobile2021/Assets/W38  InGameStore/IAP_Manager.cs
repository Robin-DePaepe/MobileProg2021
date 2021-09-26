using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP_Manager : MonoBehaviour
{
    [SerializeField] private GameObject m_RestoreButton;

    private void Awake()
    {
        //the restore button is only for apple users
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
                    Application.platform == RuntimePlatform.OSXPlayer)
        {
            m_RestoreButton.SetActive(false);
        }
    }

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.Robin.MobileProg2021.1500Coins":
                CoinManager.Instance.AddCurrency(1500);
                break;
            case "com.Robin.MobileProg2021.5000Coins":
                CoinManager.Instance.AddCurrency(5000);
                break;
            case "com.Robin.MobileProg2021.12500Coins":
                CoinManager.Instance.AddCurrency(12500);
                break;
            case "com.Robin.MobileProg2021.20000Coins":
                CoinManager.Instance.AddCurrency(20000);
                break;
            default:
                break;
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}"));
    }
}
