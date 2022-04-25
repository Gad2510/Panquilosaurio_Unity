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

        public int RecolectedMigas { get => int_recolectedMigas; }
        public int DinoChanges { get => int_dinoChanges; }
        public int DefetedDinoInStage { get => int_defetedDinoStage; }
        public int TotalDinoDefeted { get => int_totalDefeatDinos; }
        

        public List<DinoSaveData> DinoInventory { get => lst_dinoInventory; }

        public void RegisterNewDino(DinoDef.DinoChar _dino)
        {
            if (lst_dinoInventory != null)
                lst_dinoInventory = new List<DinoSaveData>();

            lst_dinoInventory.Add(new DinoSaveData(_dino,GenerateID()));
        }

        private int GenerateID()
        {
            UnityEngine.Random.InitState(Convert.ToInt32(DateTime.Now.ToString("ddMMyyyyHHmmss")));

            int id = 0;
            int uniqueCode = UnityEngine.Random.Range(0, 5000)*100000000;
            id= System.Convert.ToInt32(DateTime.Today.ToString("ddMMyyyy"))+ uniqueCode;
            if(lst_dinoInventory!= null)
            {
                if (lst_dinoInventory.Where((x) => x.ID == id).Any())
                    id = GenerateID();
            }

            return id;
        }
    }
}