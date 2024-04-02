using System;
using System.Collections;
using Dacodelaac.Core;
using Dacodelaac.Events;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dacodelaac.UI
{
    public class LoadingScreen : BaseMono
    {
        [SerializeField] Canvas canvas;
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Launching")]
        [SerializeField] GameObject launching;
        [SerializeField] Slider progress;
        [Header("Loading")]
        [SerializeField] GameObject loading;
        [SerializeField] RectTransform logo;
        [SerializeField] TextMeshProUGUI progressText;


        public bool Loading { get; private set; }
        float timeScale;
        Vector2 logoOriginalSize;

        public override void Initialize()
        {
            base.Initialize();
            logoOriginalSize = logo.rect.size;
        }

        public void Load(LoadingScreenData data)
        {
            DontDestroyOnLoad(gameObject);

            pools.DespawnAll();
            pools.DestroyAll();

            if (data.IsLaunching && !data.IsLoadScene)
            {
                Launching(data.Scene, data.MinLoadTime, data.LaunchCondition);
            }
            else if(!data.IsLaunching && data.IsLoadScene)
            {
                Switch(data.Scene, data.MinLoadTime, data.LaunchCondition);
            }
            else
            {
                LoadScene(data.Scene, data.MinLoadTime, data.LaunchCondition);

            }
        }

        void Launching(string sceneName, float minLoadTime, Func<bool> launchCondition = null)
        {
            StartCoroutine(LaunchingRoutine(sceneName, minLoadTime, launchCondition));
        }

        public void Switch(string sceneName, float minLoadTime, Func<bool> launchCondition = null)
        {
            StartCoroutine(SwitchScene(sceneName, minLoadTime, launchCondition));
        }


        IEnumerator LaunchingRoutine(string sceneName, float minLoadTime, Func<bool> launchCondition)
        {
            if (Loading) yield break;
            Loading = true;
            canvas.gameObject.SetActive(true);
            launching.gameObject.SetActive(true);
            loading.gameObject.SetActive(false);

            var ao = SceneManager.LoadSceneAsync(sceneName);
            ao.allowSceneActivation = false;

            var t = 0f;

            while (t < minLoadTime || ao.progress < 0.9f)
            {
                t += Time.unscaledDeltaTime;
                progress.value = Mathf.Min(t / minLoadTime, ao.progress / 0.9f);
                progressText.text = $"LOADING {Mathf.Floor(progress.value * 100f)}%...";

                yield return null;
            }

            if (launchCondition != null)
            {
                yield return new WaitUntil(launchCondition);
            }

            ao.allowSceneActivation = true;
        }

        IEnumerator SwitchScene(string sceneName, float minLoadTime, Func<bool> launchCondition)
        {
            if (Loading) yield break;
            Loading = true;

            canvasGroup.alpha = 1f;

            canvas.gameObject.SetActive(true);
            launching.gameObject.SetActive(true);
            loading.gameObject.SetActive(false);
            var ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            ao.allowSceneActivation = false;

            var t = 0f;

            while (t < minLoadTime || ao.progress < 0.9f)
            {
                t += Time.deltaTime;
                progress.value = Mathf.Min(t / minLoadTime, ao.progress / 0.9f);
                progressText.TryGetComponent<LocalizationParamsManager>(out LocalizationParamsManager progressTextParam);
                progressTextParam.SetParameterValue("Value", $"{Mathf.Floor(progress.value * 100f)}");
                yield return null;
            }

            if (launchCondition != null)
            {
                yield return new WaitUntil(launchCondition);
            }

            ao.allowSceneActivation = true;
        }


        void LoadScene(string sceneName, float minLoadTime, Func<bool> launchCondition = null)
        {
            StartCoroutine(LoadSceneRoutine(sceneName, minLoadTime, launchCondition));
        }

        IEnumerator LoadSceneRoutine(string sceneName, float minLoadTime, Func<bool> launchCondition)
        {
            if (Loading) yield break;
            Loading = true;

            timeScale = Time.timeScale;
            Time.timeScale = 0;
            DOTween.KillAll();

            canvas.gameObject.SetActive(true);
            launching.gameObject.SetActive(false);
            loading.gameObject.SetActive(true);

            DOTween.Kill(this);

            logo.sizeDelta = logoOriginalSize;
            canvasGroup.alpha = 0f;

            var zoomIn = false;

            DOTween.To(() => 0f, x =>
            {
                canvasGroup.alpha = x;
                logo.sizeDelta = Vector2.Lerp(logoOriginalSize, Vector2.zero, x);
            }, 1f, 0.5f).SetTarget(this).SetUpdate(true).OnComplete(() =>
            {
                zoomIn = true;
            }).Play();

            yield return new WaitUntil(() => zoomIn);

            var ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            ao.allowSceneActivation = false;

            var t = 0f;

            while (t < minLoadTime || ao.progress < 0.9f)
            {
                t += Time.unscaledDeltaTime;

                yield return null;
            }

            if (launchCondition != null)
            {
                yield return new WaitUntil(launchCondition);
            }

            ao.allowSceneActivation = true;
            Time.timeScale = timeScale;
        }

        public void OnLoadingFinished()
        {
            if (loading.activeSelf)
            {
                DOTween.Kill(this);

                DOTween.To(() => 1f, x =>
                {
                    canvasGroup.alpha = x;
                    logo.sizeDelta = Vector2.Lerp(logoOriginalSize, Vector2.zero, x);
                }, 0f, 1f).SetDelay(0.1f).SetTarget(this).SetUpdate(true).OnComplete(() =>
                {
                    Loading = false;
                    canvas.gameObject.SetActive(false);
                }).Play();
            }
            else
            {
                Loading = false;
                canvas.gameObject.SetActive(false);
            }
        }

        public void OnShowConsentForm()
        {
            canvas.sortingOrder = -1;
        }

        public void OnDoneConsentForm()
        {
            canvas.sortingOrder = 1000;
        }
    }
}