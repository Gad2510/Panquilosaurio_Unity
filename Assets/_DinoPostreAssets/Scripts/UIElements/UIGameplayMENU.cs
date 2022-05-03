using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Managers;
using Dinopostres.Events;
namespace Dinopostres.UIElements
{
    public class UIGameplayMENU : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txt_migas;
        [SerializeField]
        private GameObject go_lifesParent;
        [SerializeField]
        Sprite img_liveImage;
        [SerializeField]
        Sprite img_lostLiveImage;
        private Image[] arr_lifes;
        private void Awake()
        {
            arr_lifes = go_lifesParent.GetComponentsInChildren<Image>();
            UpdateMigas(null);

            GameManager._instance.OnRecordEvent += UpdateMigas;
            GameManager._instance.OnDead += LoseLife;
        }

        private void OnLevelWasLoaded(int level)
        {
            ResetLifes();
        }

        private void OnDestroy()
        {
            GameManager._instance.OnRecordEvent -= UpdateMigas;
            GameManager._instance.OnDead -= LoseLife;
        }

        private void ResetLifes()
        {
            foreach(Image i in arr_lifes)
            {
                i.sprite = img_liveImage;
            }
        }
        public void LoseLife(Events.Event _ev)
        {
            int index = GameManager.int_maxLives - GameManager._instance._Lives;
            if (index >= arr_lifes.Length)
                return;

            arr_lifes[index].sprite = img_lostLiveImage;
        }
        private void UpdateMigas(Events.Event _ev)
        {
            txt_migas.text= GameManager._instance.PD_gameData._Migas. ToString("0000");
        }
    }
}