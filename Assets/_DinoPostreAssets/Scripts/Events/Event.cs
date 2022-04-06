using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Events
{
    public class Event
    {
        protected string str_Log;
        protected int int_ID;

        public string Log { get => str_Log; }
        public int ID { get => int_ID; }

        public Event(int _ID,string _Log)
        {
            this.int_ID = _ID;
            this.str_Log = _Log;
        }

        public Event()
        {
            this.int_ID = -1;
            this.str_Log = "no inicialized";
        }
    }
}