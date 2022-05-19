using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


namespace Dinopostres.UIElements
{
    public class UILetter : MonoBehaviour
    {
        enum SwitchLetter
        {
            _97 = '~',
            _98 = '"',
            _99 = ';',
            _100 = '=',
            _101 = '$',
            _102 = (char)92,
            _103 = '+',
            _104 = '{',
            _105 = '*',
            _106 = '}',
            _107 = '[',
            _108 = ']',
            _109 = ',',
            _110 = (char)39,
            _111 = '(',
            _112 = ')',
            _113 = '!',
            _114 = '$',
            _115 = '´',
            _116 = '%',
            _117 = '&',
            _118 = ':',
            _119 = '@',
            _120 = '>',
            _121 = '^',
            _122 = '<',
            _48 = '0',
            _49 = '1',
            _50 = '2',
            _51 = '3',
            _52 = '4',
            _53 = '5',
            _54 = '6',
            _55 = '7',
            _56 = '8',
            _57 = '9',
            _44 = '.',
            _45= '-',
            _46= '?',
            _47= '_',
            _58= ']',
            _63 = '/'
        }

        private char ch_mainLetter;
        private char ch_swiftSimbol;
        private bool canUpperCase;
        private bool isCommand;
        private bool isLower;

        private Button btn_executer;
        private TextMeshProUGUI txt_currentLetter;


        public string _Letter=> txt_currentLetter.text;
        public bool _IsConmmand => isCommand;

        private void Awake()
        {
            btn_executer = GetComponent<Button>();
            txt_currentLetter = GetComponentInChildren<TextMeshProUGUI>(true);

            int asc = Encoding.ASCII.GetBytes(txt_currentLetter.text.ToLower())[0];
            canUpperCase = txt_currentLetter.text.Trim().Length == 1 && (asc > 96 && asc < 123);
            isCommand = txt_currentLetter.text.Trim().Length > 1;

            if (!isCommand)
            {
                isLower = true;
                ch_mainLetter = txt_currentLetter.text.ToLower()[0];
                try
                {
                    ch_swiftSimbol = (Char)((SwitchLetter)Enum.Parse(typeof(SwitchLetter), "_" + asc.ToString()));
                }
                catch
                {
                    ch_swiftSimbol = default;
                }
            }

        }

        public void ChangeCase()
        {
            if (!canUpperCase )
                return;

            if(isLower)
                txt_currentLetter.text= txt_currentLetter.text.ToUpper();
            else
                txt_currentLetter.text = txt_currentLetter.text.ToLower();

            isLower = !isLower;
        }

        public void ChangeToSimbol()
        {
            if (isCommand)
                return;

            txt_currentLetter.text = ch_swiftSimbol.ToString();
        }

        public void ChangeToLetter()
        {
            if (isCommand)
                return;

            txt_currentLetter.text = ch_mainLetter.ToString();
        }

        public void AddBtnEvent(UnityAction _ev)
        {
            btn_executer.onClick.AddListener(_ev);
        }

        public void SelectLetter()
        {
            btn_executer.Select();
        }
    }
}