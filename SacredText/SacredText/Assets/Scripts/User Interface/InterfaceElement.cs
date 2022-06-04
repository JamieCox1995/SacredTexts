using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceElement : MonoBehaviour
{
    public TMP_Text Text;
    public TMP_InputField EditText;
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void Initialize()
    {
        Text = GetComponentInChildren<TMP_Text>(); ;
        EditText = GetComponentInChildren<TMP_InputField>();
        Image = GetComponentInChildren<Image>();
    }
}
