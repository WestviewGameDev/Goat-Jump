using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LevelLoader : MonoBehaviour
{

    [SerializeField] private Image _progressBar;

    void Start()
    {


        StartCoroutine(LoadAsynchronously());

    }
    
    IEnumerator LoadAsynchronously ()
    {

        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(3);

        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            _progressBar.fillAmount = progress;

            Debug.Log(progress);

            yield return new WaitForEndOfFrame();
        }
    }

}
