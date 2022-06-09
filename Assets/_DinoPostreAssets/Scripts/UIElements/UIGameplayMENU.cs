using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dinopostres.Managers;
namespace Dinopostres.UIElements
{
    public class UIGameplayMENU : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txt_migas;
        [SerializeField]
        private GameObject go_lifesParent;
        [SerializeField]
        private Sprite img_liveImage;
        [SerializeField]
        private Sprite img_lostLiveImage;
        public Image[] arr_lifes;
        private void Awake()
        {
            arr_lifes = go_lifesParent.GetComponentsInChildren<Image>();
            arr_lifes = arr_lifes.OrderBy((x) => x.transform.GetSiblingIndex()).ToArray();
            UpdateMigas(null);

            GameManager._instance.OnRecordEvent += UpdateMigas;
            GameManager._instance.OnPlayerDead += LoseLife;
        }


        private void OnEnable()
        {
            LevelManager._Instance.SetLoadEvent(OnLoadLevel, true);
        }

        private void OnDisable()
        {
            LevelManager._Instance.SetLoadEvent(OnLoadLevel, false);
        }

        private void OnLoadLevel(UnityEngine.SceneManagement.Scene _scene, UnityEngine.SceneManagement.LoadSceneMode _mode)
        {
            ResetLifes();
        }

        private void OnDestroy()
        {
            GameManager._instance.OnRecordEvent -= UpdateMigas;
            GameManager._instance.OnPlayerDead -= LoseLife;
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
            int index = GameManager.int_maxLives - GameManager._instance._Lives+1;
            if (index >= arr_lifes.Length)
                return;

            arr_lifes[index].sprite = img_lostLiveImage;
        }
        private void UpdateMigas(Events.Event _ev)
        {
            txt_migas.text= GameManager._instance._GameData._Migas. ToString("0000");
        }
    }
}