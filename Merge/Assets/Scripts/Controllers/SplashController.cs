using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class SplashController : MonoBehaviour
{
    [SerializeField] Image _progressImage;
    [SerializeField] float _raiseTime = 0.5f;
    private Coroutine _raiseCoroutine;
    [SerializeField] private CanvasGroup crossFade;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(500, 50);
        //LevelLoadEnd()
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;



        //FirebaseManager.Instance.Initialize();



        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            LogUtils.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            if (_raiseCoroutine != null) StopCoroutine(_raiseCoroutine);
            _raiseCoroutine = StartCoroutine(RaiseProgress(asyncOperation.progress * (1f - _progressImage.fillAmount)));
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                LogUtils.Log("Press the space bar to continue");
                //Wait to you press the space key to activate the Scene
                //GameManager.Instance.BeginTransition();
                LevelLoadStart();
                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator RaiseProgress(float progress)
    {
        float currentAmount = _progressImage.fillAmount;
        var elapsedTime = 0f;
        while (elapsedTime < _raiseTime)
        {
            elapsedTime += Time.deltaTime;
            _progressImage.fillAmount = Mathf.MoveTowards(currentAmount, currentAmount + progress, elapsedTime / _raiseTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void LevelLoadEnd()
    {
        crossFade.alpha = 1;
        crossFade.DOFade(0, 0.2f).OnComplete(() =>
        {
            crossFade.interactable = false;
            crossFade.blocksRaycasts = false;
        });
    }

    private void LevelLoadStart()
    {
        crossFade.DOFade(1, 1f).OnComplete(() =>
        {
            crossFade.interactable = true;
            crossFade.blocksRaycasts = true;
        });
    }
}
