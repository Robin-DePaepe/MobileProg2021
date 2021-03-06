using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CurrencyManager : MonoBehaviour
{
    private uint m_CurrentValue = 0;
    private uint m_MaxValue = uint.MaxValue;

    private TextMeshProUGUI m_CurrencyText;

    public uint CurrentCurrency
    { get { return m_CurrentValue; } }

    bool m_CurrencyChanged = false;

    protected void Initialize(TextMeshProUGUI currencyText, uint startValue = 0, uint maxValue = uint.MaxValue)
    {
        m_CurrentValue = startValue;
        m_MaxValue = maxValue;

        m_CurrencyText = currencyText;

        ClampCurrency();
    }

    public void AddCurrency(uint amount)
    {
        m_CurrentValue += amount;

        ClampCurrency();
    }

    public void RemoveCurrency(uint amount)
    {
        m_CurrentValue -= amount;

        ClampCurrency();
    }
    public void SetCurrency(uint value)
    {
        m_CurrentValue = value;

        ClampCurrency();
    }
    private void ClampCurrency()
    {
        //clamp currency between 0 and maxValue
        if (m_CurrentValue < 0) m_CurrentValue = 0;
        else if (m_CurrentValue > m_MaxValue) m_CurrentValue = m_MaxValue;

        SaveCurrency();


        if (m_CurrencyText) m_CurrencyChanged = true; 
    }

    private void Update()
    {
        if (m_CurrencyChanged) //this is necessary bc the function might be called from an async method and not update the text 
        {
            m_CurrencyText.text = m_CurrentValue.ToString();
            m_CurrencyChanged = false;
        }
    }

    protected abstract void SaveCurrency();
}
