using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Managers;

namespace Dinopostres.UIElements
{
    public class UIKeyboard : MonoBehaviour
    {
        [SerializeField]
        private Transform trns_keysparent;
        [SerializeField]
        private Button btn_spaceBr;
        [SerializeField]
        private Button btn_createBtn;
        [SerializeField]
        private Button btn_returnBtn;
        [SerializeField]
        private TMP_InputField intxt_name;

        UILetter[] arr_letters;
        string str_name;

        bool isSimbols;
        // Start is called before the first frame update
        void Start()
        {
            str_name = "";
            arr_letters = trns_keysparent.GetComponentsInChildren<UILetter>();
            btn_spaceBr.onClick.AddListener(() => AddLetter2Name(" "));
            btn_createBtn.onClick.AddListener(() =>
            {
                MemoryManager.NewGame(str_name);
                GameManager._instance.LoadGame(str_name);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.newGame, false);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, true);
                LevelManager._Instance.LoadLevel("Criadero");
            });
            btn_returnBtn.onClick.AddListener(() => {
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.newGame, false);
                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, true);
            });
            initBtnEvents();
        }

        private void initBtnEvents()
        {
            foreach (UILetter letter in arr_letters)
            {
                letter.AddBtnEvent(() =>
                {
                    if (!letter._IsConmmand)
                    {
                        AddLetter2Name(letter._Letter);
                    }
                    else
                    {
                        switch (letter._Letter)
                        {
                            case "enter":
                                MemoryManager.NewGame(str_name);
                                GameManager._instance.LoadGame(str_name);
                                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.newGame, false);
                                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, true);
                                LevelManager._Instance.LoadLevel("Criadero");
                                break;
                            case "return":
                                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.newGame, false);
                                LevelManager._Instance._GameMode.OpenCloseSpecicficMenu(GameMode.MenuDef.menu, true);
                                break;
                            case "altNum":
                                if (!isSimbols)
                                    ChangeBoardSimbols();
                                break;
                            case "altLet":
                                if (isSimbols)
                                    ChangeBoardLetter();
                                break;
                            case "upper":
                                if (isSimbols)
                                    break;
                                ChangeBoardCase();
                                break;
                            case "back":
                                RemoveLetter2Name();
                                break;
                            default:
                                Debug.Log(letter._Letter);
                                Debug.Log($"Command {letter._Letter} not registered yet");
                                break;
                        }
                    }
                });
            }

            arr_letters[0].SelectLetter();
        }

        private void AddLetter2Name(string _letter)
        {
            if (str_name.Length >= 16 || (int)_letter[0]==0)
                return;

            str_name += _letter;
            intxt_name.text = str_name;
        }
        private void RemoveLetter2Name()
        {
            if (str_name.Length == 0)
                return;

            str_name = str_name.Substring(0, str_name.Length - 1);
            intxt_name.text = str_name;
        }


        private void ChangeBoardCase()
        {
            foreach (UILetter letter in arr_letters)
            {
                letter.ChangeCase();
            }
        }

        private void ChangeBoardSimbols()
        {
            foreach (UILetter letter in arr_letters)
            {
                letter.ChangeToSimbol();
            }
            isSimbols = true;
        }

        private void ChangeBoardLetter()
        {
            foreach (UILetter letter in arr_letters)
            {
                letter.ChangeToLetter();
            }
            isSimbols = false;
        }
    }
}