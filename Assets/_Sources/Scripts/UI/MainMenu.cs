using System.Collections;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Sources.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Image imageTransition;
        public void Continue()
        {
            Destroy( GameObject.FindObjectOfType<GameBootstrapper>());
            StartCoroutine(TransitionFade());
        }

        public void StartNew()
        {
            ES3DataManager.Instance.DeleteEnergy();
            ES3DataManager.Instance.DeleteHealth();
            ES3DataManager.Instance.DeleteLevelNumber();
            PlayerPrefs.DeleteAll();
            StartCoroutine(TransitionFade());
        }
        
        private IEnumerator TransitionFade()
        {
          
            imageTransition.DOFade(1, .7f);

            yield return new WaitForSeconds(.7f);

            SceneManager.LoadScene(1);
        }
        
    }
}