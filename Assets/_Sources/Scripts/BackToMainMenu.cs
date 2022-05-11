using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Sources.Scripts
{
    public class BackToMainMenu : MonoBehaviour
    {
        [SerializeField] private Image imageTransition;

        public void MainMenu()
        {
            Debug.Log("Works");
            Time.timeScale = 1;
            StartCoroutine(TransitionFade());
        }

        private IEnumerator TransitionFade()
        {
            imageTransition.DOFade(1, .7f);

            yield return new WaitForSeconds(.7f);

            SceneManager.LoadScene(0);
        }
    }
}