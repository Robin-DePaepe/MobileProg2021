using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class CoinManagerSavedOnline : CurrencyManager
{
    //singleton pattern
    private static CoinManagerSavedOnline m_Instance;
    const string m_CoinKey = "CurrentCoins";
    DatabaseReference m_DataBaseCoinsRef;

    public static CoinManagerSavedOnline Instance { get { return m_Instance; } }

    void Awake()
    {
        int currentCoins = 200;
        TextMeshProUGUI CurrencyText = GetComponent<TextMeshProUGUI>();

        if (!CurrencyText) Debug.LogWarning("currency text not found");
        if (m_Instance == null) m_Instance = this;


        //database
        m_DataBaseCoinsRef = FirebaseDatabase.DefaultInstance.GetReference(m_CoinKey);

        //get value 
        m_DataBaseCoinsRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.Log("Loading failed");
            else if (task.IsCompleted)
            {
                Debug.Log("Loading succes");
                DataSnapshot snapshot = task.Result;

                currentCoins = int.Parse(snapshot.Value.ToString());
                Debug.Log(currentCoins);
                base.Initialize(CurrencyText, (uint)currentCoins);
            }
        });
    }

    protected override void SaveCurrency()
    {
        m_DataBaseCoinsRef.SetValueAsync(CurrentCurrency).ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.Log("saving failed");
            else if (task.IsCompleted) Debug.Log("saving succes");
            Debug.Log(CurrentCurrency);
        });
    }

}
