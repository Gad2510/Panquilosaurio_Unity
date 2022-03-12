using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloUI : MonoBehaviour
{
    [SerializeField]
    Button btnUiElement;

    [SerializeField]
    GameObject messagePanel;


    // Start is called before the first frame update
    void Start()
    {
        btnUiElement.onClick.AddListener(() => { print("HELLO WORLD!");
            messagePanel.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
