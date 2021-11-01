using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsTest : MonoBehaviour
{
    public void OpenStore()
    {
        AnalyticsResult result = Analytics.CustomEvent("StoreOpened");
        Debug.Log("analytics for opening store result: " + result);


        Dictionary<string, object> infoTest = new Dictionary<string, object>
        {
             {"info1", "This is the result of info 1" },
             { "info2", "This is the result of info  2" }
         };

         result = Analytics.CustomEvent("StoreOpened", infoTest);
        Debug.Log("analytics dict for opening store result: " + result);
    }
}
 