using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Events
{
    public class TriggerEvent : Event
    {
        private string str_Function;

        public string Function { get => str_Function; }

        public TriggerEvent(int _ID, string _Log, string _function)
        {
            this.int_ID = _ID;
            this.str_Log = _Log;
            this.str_Function = _function;
        }

        public TriggerEvent()
        {
            this.int_ID = -1;
            this.str_Log = "no inicialized";
            this.str_Function = "";
        }
    }
}