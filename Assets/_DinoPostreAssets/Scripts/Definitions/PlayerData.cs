using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [System.Serializable]
    public class PlayerData
    {
        private enum RecordID
        {
            INGREDIENTS=1,
            CHANGE,
            DINODEFEAT,
            LOCATION
        }

        [SerializeField]
        private int int_recolectedMigas;
        [SerializeField]
        private int int_dinoChanges;
        [SerializeField]
        private int int_defetedDinoStage;
        [SerializeField]
        private int int_totalDefeatDinos;
        [SerializeField]
        private List<DinoCount> lst_defetedPerDino;
        [SerializeField]
        private List<LocationCount> lst_clearLcation;

        [SerializeField]
        private int int_migas;
        [SerializeField]
        private List<IngredientCount> lst_ingredientes;
        [SerializeField]
        private List<DinoSaveData> lst_dinoInventory;
        [SerializeField]
        private List<int> lst_unlockRecepies;
        [SerializeField]
        private List<int> lst_lockRecepies;
        public int _Migas { get => int_migas; }
        public int RecolectedMigas { get => int_recolectedMigas; }
        public int DinoChanges { get => int_dinoChanges; }
        public int DefetedDinoInStage { get => int_defetedDinoStage; }
        public int TotalDinoDefeted { get => int_totalDefeatDinos; }
        public List<IngredientCount> Inventory { get => lst_ingredientes; }
        public List<DinoSaveData> DinoInventory { get => lst_dinoInventory; }

        public PlayerData()
        {
            lst_defetedPerDino = new List<DinoCount>();
            lst_clearLcation = new List<LocationCount>();
            lst_ingredientes = new List<IngredientCount>();
            lst_dinoInventory = new List<DinoSaveData>();
            lst_unlockRecepies = new List<int>();
            lst_lockRecepies = new List<int>();

            lst_dinoInventory.Add(new DinoSaveData(DinoDef.DinoChar.Agujaceratops, GenerateID(), true));
        }

        public void RegisterNewDino(DinoDef.DinoChar _dino)
        {
            if (lst_dinoInventory != null)
                lst_dinoInventory = new List<DinoSaveData>();

            lst_dinoInventory.Add(new DinoSaveData(_dino,GenerateID()));
        }

        public void Purchase (List<IngredientCount> _coins)
        {

        }

        public int GetIngredientCount(IngredientDef.Sample _type)
        {
            try
            {
                return lst_ingredientes.First((x) => x._Ingredient == _type)._Count;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ingrdient {_type} not found in the player data");
                return -1;
            }
        }
        public DinoSaveData GetActive()
        {
            try
            {
                return lst_dinoInventory.Where((x) => x.IsSelected).First();
            }
            catch(System.Exception e) {
                Debug.LogError("No dino found as active dino");
                DinoSaveData info = new DinoSaveData(DinoDef.DinoChar.Agujaceratops, GenerateID(), true);
                lst_dinoInventory.Add(info);
                return info;

            }
        }

        private int GenerateID()
        {
            UnityEngine.Random.InitState(Convert.ToInt32(DateTime.Now.ToString("ddMMHHmm")));

            int id = 0;
            int uniqueCode = UnityEngine.Random.Range(0, 50)*10000000;
            id= System.Convert.ToInt32(DateTime.Today.ToString("ddMMyyyy"))+ uniqueCode;

            Debug.Log(id);

            if(lst_dinoInventory!= null)
            {
                if (lst_dinoInventory.Where((x) => x.ID == id).Any())
                    id = GenerateID();
            }

            return id;
        }

        public void Colectable(Events.Event _ev)
        {
            Events.RecordEvent ev = (Events.RecordEvent)_ev;
            string s = ev.Selector.ToString()[0].ToString();
            Debug.Log($"EVENT Trigger {(RecordID)(Convert.ToInt32(s))}");
            switch ((RecordID)(Convert.ToInt32(s)))
            {
                case RecordID.INGREDIENTS:
                    string v = (ev.Selector.ToString().Substring(1));
                    IngredientDef.Sample type=(IngredientDef.Sample)(Convert.ToInt32(v));
                    Debug.Log($"Register sample {type} from {v}");
                    if(type != IngredientDef.Sample.MIGAS)
                    {
                        if (lst_ingredientes.Any((x) => x._Ingredient == type))
                        {
                            lst_ingredientes.First((x) => x._Ingredient == type)._Count=1;
                        }
                        else
                        {
                            IngredientCount count = new IngredientCount(type, 1);
                            lst_ingredientes.Add(count);
                        }
                    }
                    else
                    {
                        int_recolectedMigas += 1;
                        int_migas += 1;
                    }
                    
                    break;
            }
        }
    }
}