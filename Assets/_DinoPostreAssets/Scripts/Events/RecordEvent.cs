using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Events
{
    public class RecordEvent : Event
    {
        int int_Selector;

        public int Selector { get => int_Selector; }

        public RecordEvent(int _ID, string _Log, int _selector)
        {
            this.int_ID = _ID;
            this.str_Log = _Log;
            this.int_Selector = _selector;
        }

        public RecordEvent()
        {
            this.int_ID = -1;
            this.str_Log = "no inicialized";
            this.int_Selector = -1;
        }
    }
}
