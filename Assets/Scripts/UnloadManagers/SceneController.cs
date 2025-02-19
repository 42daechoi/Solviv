using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private AsyncOperationHandle<SceneInstance> _currentSceneHandle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 씬 로드 메서드
    public async Task LoadSceneAsync(string sceneLabel)
    {
        // 이미 같은 씬이 로드되어 있는지 확인
        if (_currentSceneHandle.IsValid() && _currentSceneHandle.Result.Scene.name == sceneLabel)
        {
            Debug.LogWarning("이미 같은 씬이 로드되어 있습니다: " + sceneLabel);
            return;
        }

        // 이전 씬 언로드
        if (_currentSceneHandle.IsValid())
        {
            await Addressables.UnloadSceneAsync(_currentSceneHandle).Task;
        }

        // 새 씬 로드
        _currentSceneHandle = Addressables.LoadSceneAsync(sceneLabel, LoadSceneMode.Single);
        _currentSceneHandle.Completed += OnSceneLoaded;

        await _currentSceneHandle.Task;
    }

    // 씬 로드 완료 시 호출
    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("씬 로드 완료: " + handle.Result.Scene.name);
        }
        else
        {
            Debug.LogError("씬 로드 실패: " + handle.OperationException);
        }
    }

    // 씬 언로드 메서드
    public async Task UnloadCurrentSceneAsync()
    {
        if (_currentSceneHandle.IsValid())
        {
            await Addressables.UnloadSceneAsync(_currentSceneHandle).Task;
            _currentSceneHandle = default;
        }
    }
}