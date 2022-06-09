using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dinopostres.Definitions
{
    [Serializable]
    public class PlayerData
    {
        private enum RecordID
        {
            INGREDIENTS=1,
            CHANGE,
            DINODEFEAT,
            LOCATION
        }
        private string str_ID;
        private int int_recolectedMigas;
        private int int_dinoChanges;
        private int int_defetedDinoStage;
        private int int_totalDefeatDinos;
        private List<DinoCount> lst_defetedPerDino;
        private List<LocationCount> lst_clearLcation;

        private int int_migas;
        private List<IngredientCount> lst_ingredientes;
        private List<DinoSaveData> lst_dinoInventory;
        private List<int> lst_unlockRecepies;
        private List<int> lst_unlockTriggers;
        public int _Migas { get => int_migas; }
        public List<IngredientCount> _Inventory { get => lst_ingredientes; }
        public List<DinoSaveData> _DinoInventory { get => lst_dinoInventory; }
        public List<int> _UnlockRecipies { get => lst_unlockRecepies; }
        public string _ID => str_ID;
        public PlayerData()
        {
            lst_defetedPerDino = new List<DinoCount>();
            lst_clearLcation = new List<LocationCount>();
            lst_ingredientes = new List<IngredientCount>();
            lst_dinoInventory = new List<DinoSaveData>();
            lst_unlockRecepies = new List<int>();
            lst_unlockTriggers = new List<int>();
        }

        public PlayerData(string _ID)
        {
            lst_defetedPerDino = new List<DinoCount>();
            lst_clearLcation = new List<LocationCount>();
            lst_ingredientes = new List<IngredientCount>();
            lst_dinoInventory = new List<DinoSaveData>();
            lst_unlockRecepies = new List<int>();
            lst_unlockTriggers = new List<int>();
            lst_unlockRecepies.Add((int)DinoDef.DinoChar.Agujaceratops);

            lst_dinoInventory.Add(new DinoSaveData(DinoDef.DinoChar.Protarchaeopteryx, GenerateID(), true,5));

            str_ID = _ID;
        }

        public void RegisterNewDino(DinoDef.DinoChar _dino)
        {
            if (lst_dinoInventory == null)
                lst_dinoInventory = new List<DinoSaveData>();

            lst_dinoInventory.Add(new DinoSaveData(_dino,GenerateID()));
            lst_dinoInventory.Count();
        }

        public void DeleteDino(int _ID)
        {
            if (lst_dinoInventory == null)
                return;

            try
            {
                if(lst_dinoInventory.Any((x)=> x._ID == _ID))
                {
                    lst_dinoInventory.Remove(lst_dinoInventory.First((x) => x._ID == _ID));
                }
            }
            catch
            {
                Debug.LogWarning("N dino found to delete");
            }
        }

        public int CalculateRegisterDinos()
        {
            return lst_dinoInventory.Select((x)=>x._Dino).Distinct().Count();
        }

        public void RestoreHP()
        {
            List<DinoSaveData> lst_dino2Restore;
            try
            {
                lst_dino2Restore = lst_dinoInventory.Where((x) => x._CurrentHealth < x._MaxHealth).ToList();

                foreach(DinoSaveData dsd in lst_dino2Restore)
                {
                    dsd._CurrentHealth = dsd._MaxHealth;
                }
            }
            catch
            {
                Debug.Log("No dino to restore");
            }
        }

        public bool IsBossDefeated(LocationCount.Rank _rank)
        {
            return lst_clearLcation.Any(x=> x._Area==LocationCount.Area.volcan && x._Rank==_rank && x._Value>0);
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
            catch
            {
                //Debug.LogWarning($"Ingrdient {_type} not found in the player data");
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
                return lst_dinoInventory.Where((x) => x._IsSelected).First();
            }
            catch {
                Debug.LogWarning("No dino found as active dino");
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

            bool isSet = lst_dinoInventory.Where((x) => x._ID == id).Any();
            while(isSet)
            {
                id++;
                isSet = lst_dinoInventory.Where((x) => x._ID == id).Any();
            }

            return id;
        }

        public bool CheckForUnlock(UnlockDef _lock,int _constumID=-1)
        {
            if(_constumID >= 0)
            {
                if (lst_unlockRecepies.Any((x) => _constumID == x))
                {
                    return true;
                }
            }
            else
            {
                if (lst_unlockTriggers.Any((x) => _lock._ID == x))
                {
                    return true;
                }
                
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
                isUnLock = isUnLock && lst_defetedPerDino.Any(x=> x._Dino==dc._Dino && x._Value>dc._Value);
            }
            foreach (LocationCount loc in _lock._LocationCount)
            {
                isUnLock = isUnLock && lst_clearLcation.Any(x=> x._Area==loc._Area && x._Rank==loc._Rank && x._Value>loc._Value);
            }

            if (isUnLock)
            {
                if (_constumID>=0)
                {
                    lst_unlockRecepies.Add(_constumID);
                    lst_unlockRecepies=lst_unlockRecepies.OrderBy((x) => x).ToList();
                }
                else
                {
                    lst_unlockTriggers.Add(_lock._ID);
                }
               
            }

            return isUnLock;
        }

        public void Colectable(Events.Event _ev)
        {
            if (_ev == null)
                return;

            Events.RecordEvent ev = (Events.RecordEvent)_ev;
            string record = ev.Selector.ToString()[0].ToString();
            string nfoRef = (ev.Selector.ToString().Substring(1));
            switch ((RecordID)(Convert.ToInt32(record)))
            {
                case RecordID.INGREDIENTS:
                    
                    IngredientDef.Sample type=(IngredientDef.Sample)(Convert.ToInt32(nfoRef));
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
                    //string selector = String.Format("{0}|{1}", (int)_area, (int)_rank);
                    if (lst_clearLcation.Any(x=> x._Area==_area && x._Rank==_rank))
                    {
                        lst_clearLcation.First(x => x._Area == _area && x._Rank == _rank)._Value=1;
                    }
                    else
                    {
                        lst_clearLcation.Add(new LocationCount(_area,_rank, 1));
                    }
                    int_defetedDinoStage = 0;
                    break;
                case RecordID.DINODEFEAT:
                    int_defetedDinoStage ++;
                    int_totalDefeatDinos++;
                    DinoDef.DinoChar _dino= (DinoDef.DinoChar)Convert.ToInt32(nfoRef);

                    if (lst_defetedPerDino.Any(x=> x._Dino==_dino))
                    {
                        lst_defetedPerDino.First(x => x._Dino == _dino)._Value+=1;
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