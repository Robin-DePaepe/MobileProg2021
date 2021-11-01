using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManagerSavedLocally : CurrencyManager
{
    //singleton pattern
    private static CoinManagerSavedLocally m_Instance;
    const string m_CoinKey = "CurrentCoins";

    public static CoinManagerSavedLocally Instance { get { return m_Instance; } }

    void Awake()
    {
        int currentCoins = 500;

        if(PlayerPrefs.HasKey(m_CoinKey))
        {
            currentCoins = PlayerPrefs.GetInt(m_CoinKey);
        }
        TextMeshProUGUI CurrencyText = GetComponent<TextMeshProUGUI>();

        if (!CurrencyText) Debug.LogWarning("currency text not found");

      if(m_Instance == null)  m_Instance = this;

        base.Initialize(CurrencyText, (uint)currentCoins);
    }

    protected override void SaveCurrency()
    {
        PlayerPrefs.SetInt(m_CoinKey, (int)CurrentCurrency);
        PlayerPrefs.Save();
    }
}
