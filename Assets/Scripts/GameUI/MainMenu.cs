using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button btn_Empezar;

    [SerializeField]
    Button btn_Cargar;

    [SerializeField]
    Button btn_salir;

    [SerializeField]
    GameObject newGaemPanel;

    private void Awake()
    {
        btn_Empezar.onClick.AddListener(()=>ShowNewGamePanel());
    }

    void ShowNewGamePanel() { newGaemPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
