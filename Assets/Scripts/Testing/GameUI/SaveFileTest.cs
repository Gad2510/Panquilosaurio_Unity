using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveFileTest : MonoBehaviour
{
    [SerializeField]
    TMP_InputField gameplayName;

    [SerializeField]
    Button btnCreate;

    [SerializeField]
    Button btnCancel;

    [SerializeField]
    Button btnCargar;

    string gameNew;

    private void Awake()
    {
        gameplayName.onEndEdit.AddListener(value=> {
            ChangeValueEnd(value);
        });
        btnCreate.onClick.AddListener(() => CreateNewGame());
        btnCancel.onClick.AddListener(() => Cancel());
        btnCargar.onClick.AddListener(() => Cargar());
    }

    void CreateNewGame()
    {
        MemorySystem.NewGame(gameNew);
    }

    void Cancel() => gameObject.SetActive(false);
    void Cargar() { 
        GameData gm= MemorySystem.LoadGame(gameNew);

        Debug.Log(gm.GameName);
    }

    void ChangeValueEnd(string gameNew)
    {
        Debug.Log(gameNew);
        this.gameNew = gameNew;
    }
}
