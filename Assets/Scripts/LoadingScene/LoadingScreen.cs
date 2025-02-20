using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadingScreen : MonoBehaviourPunCallbacks
{
    public Slider loadingSlider;
    private AsyncOperationHandle<SceneInstance> sceneLoadHandle;

    private bool isAllPlayersLoaded = false;
    private bool isSceneLoaded = false;
    private const float TIMEOUT_DURATION = 60f;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartLoadingScene", RpcTarget.All, "GameScene");
        }
    }

    [PunRPC]
    private void StartLoadingScene(string sceneLabel)
    {
        StartCoroutine(LoadSceneAsync(sceneLabel));
    }

    private IEnumerator LoadSceneAsync(string sceneLabel)
    {
        sceneLoadHandle = Addressables.LoadSceneAsync(sceneLabel, LoadSceneMode.Single, activateOnLoad: false);

        while (!sceneLoadHandle.IsDone)
        {
            if (loadingSlider != null)
            {
                loadingSlider.value = sceneLoadHandle.PercentComplete;
            }
            yield return null;
        }

        isSceneLoaded = true;
        Debug.Log("3");
        Hashtable props = new Hashtable() { { "IsLoaded", true } };
        Debug.Log($"1");
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        Debug.Log($"2");
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsLoaded", out object isLoaded);

        Debug.Log($"hello : {(bool)isLoaded}");

    }

    private IEnumerator CheckAllPlayersLoadedCoroutine()
    {
        if (isAllPlayersLoaded) yield break;

        float elapsedTime = 0f;

        while (elapsedTime < TIMEOUT_DURATION)
        {
            isAllPlayersLoaded = true; 

            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (!player.CustomProperties.TryGetValue("IsLoaded", out object isLoaded) || !(bool)isLoaded)
                {
                    isAllPlayersLoaded = false;
                    break;
                }
            }

            if (isAllPlayersLoaded)
            {
                photonView.RPC("StartGame", RpcTarget.All);
                yield break;
            }

            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("LoadingScreen : 일부 플레이어가 로딩을 완료하지 못했습니다.");
    }

    [PunRPC]
    private void StartGame()
    {
        if (isSceneLoaded && sceneLoadHandle.IsValid())
        {
            sceneLoadHandle.Result.ActivateAsync();
        }
        else
        {
            Debug.Log("LoadingScreen : 씬이 아직 로드되지 않았거나 핸들이 유효하지 않습니다.");
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (PhotonNetwork.IsMasterClient && changedProps.ContainsKey("IsLoaded"))
        {
            StartCoroutine(CheckAllPlayersLoadedCoroutine());
        }
    }

    private void OnDestroy()
    {
        if (sceneLoadHandle.IsValid())
        {
            Addressables.Release(sceneLoadHandle);
        }
    }
}