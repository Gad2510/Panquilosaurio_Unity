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
        private List<int> lst_unlockTriggers;
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
            lst_unlockTriggers = new List<int>();
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

            bool isSet = lst_dinoInventory.Where((x) => x.ID == id).Any();
            while(isSet)
            {
                id++;
                isSet = lst_dinoInventory.Where((x) => x.ID == id).Any();
            }

            return id;
        }

        public bool CheckForUnlock(UnlockDef _lock)
        {
            if(lst_unlockTriggers.Any((x)=> _lock._ID == x))
            {
                return true;
            }

            bool isUnLock = true;
            if(_lock._TotalEnemies>0)
                isUnLock=isUnLock && _lock._TotalEnemies <= int_totalDefeatDinos;
            if (_lock._TotalLocation > 0)
                isUnLock =isUnLock && _lock._TotalLocation <= lst_clearLcation.Sum((x) => x._Value);
            if (_lock._TotalMigas > 0)
                isUnLock = isUnLock && _lock._TotalMigas <= int_recolectedMigas;
            if (_lock._RequireChanges > 0)
                isUnLock = isUnLock && _lock._RequireChanges <= int_dinoChanges;

            foreach (DinoCount dc in _lock._EnemyCount)
            {
                isUnLock = isUnLock && lst_defetedPerDino.Any((x) => dc.Value < x.Value && dc.Dino==x.Dino );
            }
            foreach (LocationCount loc in _lock._LocationCount)
            {
                isUnLock = isUnLock && lst_clearLcation.Any((x) => loc._Value < x._Value && x._Area==loc._Area && x._Rank==loc._Rank);
            }

            if (isUnLock)
                lst_unlockTriggers.Add(_lock._ID);

            return isUnLock;
        }

        public void Colectable(Events.Event _ev)
        {
            Events.RecordEvent ev = (Events.RecordEvent)_ev;
            string record = ev.Selector.ToString()[0].ToString();
            string nfoRef = (ev.Selector.ToString().Substring(1));
            Debug.Log($"EVENT Trigger {(RecordID)(Convert.ToInt32(record))}");
            switch ((RecordID)(Convert.ToInt32(record)))
            {
                case RecordID.INGREDIENTS:
                    
                    IngredientDef.Sample type=(IngredientDef.Sample)(Convert.ToInt32(nfoRef));
                    Debug.Log($"Register sample {type} from {nfoRef}");
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
                case RecordID.LOCATION:
                    LocationCount.Area _area=(LocationCount.Area)Convert.ToInt32(nfoRef.Substring(0,2));
                    LocationCount.Rank _rank = (LocationCount.Rank)Convert.ToInt32(nfoRef.Substring(3));

                    if(lst_clearLcation.Any((x) => x._Area==_area && x._Rank == _rank))
                    {
                        lst_clearLcation.First((x) => x._Area == _area && x._Rank == _rank)._Value=1;
                    }
                    else
                    {
                        lst_clearLcation.Add(new LocationCount(_area,_rank,1));
                    }
                    int_defetedDinoStage = 0;
                    break;
                case RecordID.DINODEFEAT:
                    int_defetedDinoStage ++;
                    int_totalDefeatDinos++;
                    DinoDef.DinoChar _dino= (DinoDef.DinoChar)Convert.ToInt32(nfoRef);

                    if (lst_defetedPerDino.Any((x) => x.Dino == _dino))
                    {
                        lst_defetedPerDino.First((x) => x.Dino == _dino).Value++;
                    }
                    else
                    {
                        lst_defetedPerDino.Add(new DinoCount(_dino, 1));
                    }

                    break;
                case RecordID.CHANGE:
                    int_dinoChanges++;
                    break;
            }
        }
    }
}