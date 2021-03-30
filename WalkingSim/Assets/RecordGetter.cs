using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecordGetter : MonoBehaviour
{
    public string ifNoRecords;
    [Space(3)]
    public string []recordNames;
    public float[] recordTimes;
    [Space(5)]
    public TMP_Text[] namesText;
    public TMP_Text[] timesText;
    private bool doneCheck;
    private int index;
    private void Start()
    {
        GetRecordas();
    }
    public void GetRecordas()
    {
        foreach (string name in recordNames)
        {
            recordTimes[index]=PlayerPrefs.GetFloat(recordNames[index]);
            namesText[index].text = recordNames[index];
            if (recordTimes[index] > 0f)
            {
                timesText[index].text = recordTimes[index].ToString();
            }
            else if (recordTimes[index] == 0f)
            {
                timesText[index].text = ifNoRecords;
            }
                index++;
        }
    }
}
