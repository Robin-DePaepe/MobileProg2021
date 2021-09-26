using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : CurrencyManager
{
    //singleton pattern
    private static CoinManager m_Instance;

    public static CoinManager Instance { get { return m_Instance; } }

    void Awake()
    {
        m_CurrencyText = GetComponent<TextMeshProUGUI>();

        if (!m_CurrencyText) Debug.LogWarning("currency text not found");

      if(m_Instance == null)  m_Instance = this;

        base.Initialize(m_CurrencyText,500);
    }

    //vars
    private TextMeshProUGUI m_CurrencyText = null;

}
