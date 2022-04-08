using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dinopostres.Events
{
    public class ActionEvent : Event
    {
        public enum GameActions
        {
            HIT, none
        };

        List<object> lst_Parameter;
        GameActions enm_action;

        public GameActions _Action { get => enm_action; }

        public object GetParameterByIndex(int index)
        {
            if (lst_Parameter[index] != null)
            {
                return lst_Parameter[index];
            }
            return null;
        }

        public ActionEvent(int _ID, string _Log,GameActions _action ,List<object> _params)
        {
            this.int_ID = _ID;
            this.str_Log = _Log;
            this.lst_Parameter = _params;
            this.enm_action = _action;
        }

        public ActionEvent()
        {
            this.int_ID = -1;
            this.str_Log = "no inicialized";
            this.lst_Parameter = null;
            this.enm_action = GameActions.none;
        }
    }
}
