using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Events
{
    public class ActionEvent : Event
    {
        List<object> lst_Parameter;


        public object GetParameterByIndex(int index)
        {
            if (lst_Parameter[index] != null)
            {
                return lst_Parameter[index];
            }
            return null;
        }

        public ActionEvent(int _ID, string _Log, List<object> _params)
        {
            this.int_ID = _ID;
            this.str_Log = _Log;
            this.lst_Parameter = _params;
        }

        public ActionEvent()
        {
            this.int_ID = -1;
            this.str_Log = "no inicialized";
            this.lst_Parameter = null;
        }
    }
}
