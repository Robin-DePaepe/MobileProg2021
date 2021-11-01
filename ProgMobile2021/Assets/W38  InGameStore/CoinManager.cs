using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : CurrencyManager
{
    //singleton pattern
    private static CoinManager m_Instance;

    public static CoinManager Instance { get { return m_Instance; } }

    protected override void SaveCurrency()
    {  
    }

    void Awake()
    {
        TextMeshProUGUI CurrencyText = GetComponent<TextMeshProUGUI>();

        if (!CurrencyText) Debug.LogWarning("currency text not found");

      if(m_Instance == null)  m_Instance = this;

        base.Initialize(CurrencyText, 500);
    }
}
