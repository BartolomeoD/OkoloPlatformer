using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static string _targetScene;
        private float _startTime;
        public float minimalLoadTime = 1f;
        private const string LoadingSceneName = "LoadingScene";

        private void Start()
        {
            _startTime = Time.time;
            ChangeScene();
        }

        private void ChangeScene()
        {
            var previousScene = SceneManager.GetActiveScene();
            var unloadOperation = SceneManager.UnloadSceneAsync(previousScene);
            unloadOperation.completed += _ =>
            {
                var loadSceneOperation = SceneManager.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);
                loadSceneOperation.completed += _ =>
                {
                    var resourcesClearOperation = Resources.UnloadUnusedAssets();
                    resourcesClearOperation.completed += _ =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_targetScene));
                        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true);
                        StartCoroutine(WaitShowScene());
                    };
                };
            };
        }

        private IEnumerator WaitShowScene()
        {
            var levelReadyChecker = FindObjectsOfType<MonoBehaviour>().OfType<ILevelReady>().FirstOrDefault();
            if (levelReadyChecker != null)
                while (!levelReadyChecker.IsLevelReady)
                    yield return new WaitForEndOfFrame();

            var loadingTime = Time.time - _startTime;

            if (loadingTime < minimalLoadTime)
                yield return new WaitForSeconds(minimalLoadTime - loadingTime);

            SceneManager.UnloadSceneAsync(LoadingSceneName);
        }

        public static void LoadScene(string scene)
        {
            _targetScene = scene;
            var loader = SceneManager.GetSceneByName(LoadingSceneName);
            if (!loader.isLoaded)
                SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            else
                SceneManager.LoadScene(scene);
        }
    }
}