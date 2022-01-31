using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
    
namespace _Sources.Scripts.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            // if loaded scene is already initial - do nothing;
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            
            while (!waitNextScene.isDone)
            {
                yield return null;
            }
            onLoaded?.Invoke();
        }
    }
}