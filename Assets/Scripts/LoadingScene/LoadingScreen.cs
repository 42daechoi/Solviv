using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;

    void Start()
    {
        StartCoroutine(LoadAssets());
    }

    private IEnumerator LoadAssets()
    {
        // ��巹���� ���ҽ� �ε� ���⿡ �ۼ�
        // ex. var handle = Addressables.LoadAssetsAsync<GameObject>(...);

        // while (!handle.IsDone)
        // {
        //     loadingSlider.value = handle.PercentComplete;
        //     yield return null;
        // }

        // Addressables.Release(handle);

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }
        SceneManager.Instance.LoadScene("MainScene");
    }
}
