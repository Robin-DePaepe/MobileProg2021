using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private uint m_CurrentValue = 0;
    private uint m_MaxValue = uint.MaxValue;

    private Sprite m_Icon= null;

    public Sprite Icon
    {
        get { return m_Icon; }
    }

    protected void Initialize(Sprite icon,uint startValue = 0, uint maxValue = uint.MaxValue)
    {
        m_CurrentValue = startValue;
        m_MaxValue = maxValue;

        m_Icon = icon;
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
    }
}
