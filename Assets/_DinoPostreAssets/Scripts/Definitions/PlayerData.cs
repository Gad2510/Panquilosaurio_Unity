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
        public int _Migas { get => int_migas; }
        public int RecolectedMigas { get => int_recolectedMigas; }
        public int DinoChanges { get => int_dinoChanges; }
        public int DefetedDinoInStage { get => int_defetedDinoStage; }
        public int TotalDinoDefeted { get => int_totalDefeatDinos; }
        public List<IngredientCount> Inventory { get => lst_ingredientes; }
        public List<DinoSaveData> DinoInventory { get => lst_dinoInventory; }
        public List<int> UnlockRecipies { get => lst_unlockRecepies; }

        public PlayerData()
        {
            lst_defetedPerDino = new List<DinoCount>();
            lst_clearLcation = new List<LocationCount>();
            lst_ingredientes = new List<IngredientCount>();
            lst_dinoInventory = new List<DinoSaveData>();
            lst_unlockRecepies = new List<int>();
            lst_unlockRecepies.Add((int)DinoDef.DinoChar.Agujaceratops);

            lst_dinoInventory.Add(new DinoSaveData(DinoDef.DinoChar.Agujaceratops, GenerateID(), true));
        }

        public void RegisterNewDino(DinoDef.DinoChar _dino)
        {
            if (lst_dinoInventory == null)
                lst_dinoInventory = new List<DinoSaveData>();

            lst_dinoInventory.Add(new DinoSaveData(_dino,GenerateID()));
            lst_dinoInventory.Count();
        }

        public bool CanBePurchase (List<IngredientCount> _coins)
        {
            bool canBuy = true;
            for(int i=0; i<_coins.Count() && canBuy; i++)
            {
                if(_coins[i]._Ingredient== IngredientDef.Sample.MIGAS)
                {
                    canBuy = _coins[i]._Count <= int_migas;
                }
                else
                {
                    canBuy = lst_ingredientes.Any((x) => x._Ingredient == _coins[i]._Ingredient && x._Count >= _coins[i]._Count);
                    
                }
            }
            return canBuy;
        }

        public void MakePurchase(List<IngredientCount> _coins)
        {
            for (int i = 0; i < _coins.Count(); i++)
            {
                if (_coins[i]._Ingredient == IngredientDef.Sample.MIGAS)
                {
                    int_migas -= _coins[i]._Count;
                }
                else
                {
                    IngredientCount ingre= lst_ingredientes.First((x) => x._Ingredient == _coins[i]._Ingredient);
                    ingre._Count = -_coins[i]._Count;
                }
            }
        }

        public int GetIngredientCount(IngredientDef.Sample _type)
        {
            try
            {
                if (_type != IngredientDef.Sample.MIGAS)
                    return lst_ingredientes.First((x) => x._Ingredient == _type)._Count;
                else
                    return int_migas;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Ingrdient {_type} not found in the player data");
                return 0;
            }
        }

        public bool GetRecipeUnlock(int _index)
        {
            return lst_unlockRecepies.Any((x) => x == _index);
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
            bool isSet = lst_dinoInventory.Where((x) => x.ID == id).Any();
            while(isSet)
            {
                id++;
                isSet = lst_dinoInventory.Where((x) => x.ID == id).Any();
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