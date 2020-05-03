using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    #region private field
    [SerializeField]
    GameObject loadingPanel;
    Animator loadingAnimator;
    Coroutine loadingCrtn;
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // 컴포넌트 할당
        loadingAnimator = loadingPanel.GetComponent<Animator>();
    }
    /**
    *   지정한 씬을 비동기 로드한다.
    *   @param sceneName    씬 이름
    */
    void Update()
    {
        // 임시 로딩
        if (Input.GetKeyDown(KeyCode.A))
        {
            loadingCrtn = StartCoroutine(loadScene("HS_Main"));
        }
    }
    IEnumerator loadScene(string sceneName)
    {
        // 로딩 패널 켜기
        if (loadingPanel != null)
            loadingPanel.SetActive(true);
        // 비동기 작업 지정
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // 씬이 로딩이 완료 될 때까지 비활성화
        operation.allowSceneActivation = false;
        float time = 0;
        while (!operation.isDone)
        {
            time += Time.deltaTime;
            // 만약 로딩이 완료됬을 경우 씬을 활성화 한다
            if (operation.progress >= 0.9f && time >= 1.0f)
            {
                operation.allowSceneActivation = true;
                loadingAnimator.SetTrigger("TrgLoaded");
                break ;
            }
            yield return null;
        }
        while (loadingAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        loadingPanel.SetActive(false);
        yield return null;
    }
}
