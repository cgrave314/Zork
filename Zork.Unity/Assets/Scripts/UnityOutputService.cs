using System;
using UnityEngine;
using Zork.Common;
using TMPro;
using UnityEngine.UIElements;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private TextMeshProUGUI OutputText;

    [SerializeField]
    private GameObject ContentWindow;

    public void Write(string value)
    {
        throw new System.NotImplementedException();
    }

    public void Write(object value)
    {
        Write(value.ToString());
    }

    public void WriteLine(string value)
    {
        OutputText = Instantiate(OutputText);
        OutputText.rectTransform.SetParent(ContentWindow.transform, false);

        OutputText.text = value;
    }

    public void WriteLine(object value)
    {
        WriteLine(value.ToString());
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
