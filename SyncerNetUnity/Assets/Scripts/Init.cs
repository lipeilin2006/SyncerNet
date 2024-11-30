using HybridCLR;
using MemoryPack;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;
using UnityEngine.InputSystem;


public class Init : MonoBehaviour
{
    public string resourcesServer = "http://127.0.0.1:8000/";
    private string _packageVersion;

    public Slider slider;
    public Text message;

    private bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
        MemoryPackUnityFormatterProviderInitializer.RegisterInitialFormatters();
#if UNITY_EDITOR
        StartCoroutine(HotUpdate_EditorPlayMode());
#else
        StartCoroutine(HotUpdate());
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator LoadMetadataForAOTAssemblies()
    {
        var package = YooAssets.GetPackage("DefaultPackage");
        foreach (var dll in Assemblies.AotAssemblies)
        {
            Debug.Log($"Assets/HotfixAssemblies/Aot/{dll}.bytes");
            AssetHandle handle = package.LoadAssetAsync<TextAsset>($"Assets/HotUpdate/HotfixAssemblies/Aot/{dll}.bytes");
            yield return handle;
            TextAsset textAsset = handle.AssetObject as TextAsset;
            int err = (int)RuntimeApi.LoadMetadataForAOTAssembly(textAsset.bytes, HomologousImageMode.SuperSet);
            Debug.Log($"LoadMetadataForAOTAssembly:{dll}. ret:{err}");
        }
    }

    private IEnumerator LoadHotUpdateAssemblies()
    {
        var package = YooAssets.GetPackage("DefaultPackage");

        foreach (string dll in Assemblies.HotUpdateAssemblies)
        {
            Debug.Log($"Assets/HotfixAssemblies/Aot/{dll}.bytes");
            AssetHandle handle = package.LoadAssetAsync<TextAsset>($"Assets/HotUpdate/HotfixAssemblies/HotUpdate/{dll}.bytes");
            yield return handle;
            TextAsset textAsset = handle.AssetObject as TextAsset;
            Assembly.Load(textAsset.bytes);
        }
    }

    private IEnumerator HotUpdate()
    {
        YooAssets.Initialize();
        //创建默认的资源包
        var package = YooAssets.CreatePackage("DefaultPackage");
        //设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
        YooAssets.SetDefaultPackage(package);

        string defaultHostServer = resourcesServer;
        string fallbackHostServer = resourcesServer;
        IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
        IDecryptionServices decryptionServices = new FileOffsetDecryption();
        var cacheFileSystem = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices, decryptionServices);
        var initParameters = new HostPlayModeParameters();
        initParameters.BuildinFileSystemParameters = null;
        initParameters.CacheFileSystemParameters = cacheFileSystem;
        var initOperation = package.InitializeAsync(initParameters);
        yield return initOperation;

        if (initOperation.Status == EOperationStatus.Succeed)
            Debug.Log("资源包初始化成功！");
        else
            Debug.LogError($"资源包初始化失败：{initOperation.Error}");

        Debug.Log("获取版本号");
        yield return UpdatePackageVersion();
        Debug.Log("获取资源列表");
        yield return UpdatePackageManifest(_packageVersion);
        Debug.Log("下载资源");
        yield return DownloadResources();
        Debug.Log("修补程序集");
        yield return Assemblies.Refresh();
        yield return DllHotfix();
        slider.value = slider.maxValue;
        isReady = true;
        message.text = "点击进入游戏";
    }

    private IEnumerator HotUpdate_EditorPlayMode()
    {
        YooAssets.Initialize();
        //创建默认的资源包
        var package = YooAssets.CreatePackage("DefaultPackage");
        //设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
        YooAssets.SetDefaultPackage(package);

        //注意：如果是原生文件系统选择EDefaultBuildPipeline.RawFileBuildPipeline
        var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
        var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, "DefaultPackage");
        var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
        var initParameters = new EditorSimulateModeParameters();
        initParameters.EditorFileSystemParameters = editorFileSystem;
        var initOperation = package.InitializeAsync(initParameters);
        yield return initOperation;

        if (initOperation.Status == EOperationStatus.Succeed)
            Debug.Log("资源包初始化成功！");
        else
            Debug.LogError($"资源包初始化失败：{initOperation.Error}");

        Debug.Log("获取版本号");
        yield return UpdatePackageVersion();
        Debug.Log("获取资源列表");
        yield return UpdatePackageManifest(_packageVersion);
        Debug.Log("下载资源");
        yield return DownloadResources();
        slider.value = slider.maxValue;
        isReady = true;
        message.text = "点击进入游戏";
    }

    private IEnumerator UpdatePackageVersion()
    {
        var package = YooAssets.GetPackage("DefaultPackage");
        var operation = package.RequestPackageVersionAsync();
        yield return operation;

        if (operation.Status == EOperationStatus.Succeed)
        {
            //更新成功
            _packageVersion = operation.PackageVersion;
            Debug.Log($"Request package Version : {_packageVersion}");
        }
        else
        {
            //更新失败
            Debug.LogError(operation.Error);
        }
    }

    private IEnumerator UpdatePackageManifest(string packageVersion)
    {
        var package = YooAssets.GetPackage("DefaultPackage");
        var operation = package.UpdatePackageManifestAsync(packageVersion);
        yield return operation;

        if (operation.Status == EOperationStatus.Succeed)
        {
            //更新成功
        }
        else
        {
            //更新失败
            Debug.LogError(operation.Error);
        }
    }

    IEnumerator DownloadResources()
    {
        var package = YooAssets.GetPackage("DefaultPackage");

        int downloadingMaxNum = 10;
        int failedTryAgain = 3;
        var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);

        //没有需要下载的资源
        if (downloader.TotalDownloadCount == 0)
        {
            yield break;
        }

        //需要下载的文件总数和总大小
        int totalDownloadCount = downloader.TotalDownloadCount;
        long totalDownloadBytes = downloader.TotalDownloadBytes;

        //注册回调方法
        downloader.OnDownloadErrorCallback = OnDownloadError;
        downloader.OnDownloadProgressCallback = OnDownloadProgressUpdate;
        downloader.OnDownloadOverCallback = OnDownloadOver;
        downloader.OnStartDownloadFileCallback = OnStartDownloadFile;

        //开启下载
        downloader.BeginDownload();
        message.text = "下载中";
        yield return downloader;

        //检测下载结果
        if (downloader.Status == EOperationStatus.Succeed)
        {
            //下载成功
            message.text = "下载成功";
        }
        else
        {
            //下载失败
        }
    }

    IEnumerator DllHotfix()
    {
        yield return LoadMetadataForAOTAssemblies();
        yield return LoadHotUpdateAssemblies();
    }

    /// <summary>
    /// 使用协程加载热更资源包中的场景
    /// </summary>
    /// <returns></returns>
    IEnumerator EnterScene()
    {
        var package = YooAssets.GetPackage("DefaultPackage");

        message.text = "正在进入游戏";

        string location = "Assets/HotUpdate/Resources/Scenes/Lobby";
        var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
        SceneHandle handle = package.LoadSceneAsync(location, sceneMode);
        yield return handle;
        Debug.Log($"Scene name is {handle.SceneName}");
    }


    void OnDownloadError(string filename, string error)
    {
        Debug.LogError($"Error {error} downloading {filename}");
    }
    /// <summary>
    /// 更新下载进度条
    /// </summary>
    /// <param name="totalDownloadCount"></param>
    /// <param name="currentDownloadCount"></param>
    /// <param name="totalDownloadBytes"></param>
    /// <param name="currentDownloadBytes"></param>
    void OnDownloadProgressUpdate(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
    {
        slider.maxValue = totalDownloadBytes;
        slider.value = currentDownloadBytes;
        Debug.Log($"totalDownloadCount:{totalDownloadCount},currentDownloadCount:{currentDownloadCount},totalDownloadBytes:{totalDownloadBytes},currentDownloadBytes:{currentDownloadBytes}");
    }
    void OnDownloadOver(bool isSucceed)
    {
        Debug.Log(isSucceed ? "Succeed" : "Failed");
    }
    void OnStartDownloadFile(string fileName, long sizeBytes)
    {
        Debug.Log($"Start download {fileName} size :{sizeBytes}");
    }

    public void OnStartGameClick()
    {
        if (isReady)
        {
            Debug.Log("加载场景");
            StartCoroutine("EnterScene");
        }
    }

    /// <summary>
    /// 资源地址提供服务类
    /// </summary>
    class RemoteServices : IRemoteServices
    {
        private string _defaultHostServer, _fallbackHostServer;
        public RemoteServices(string defaultHostServer, string fallbackHostServer)
        {
            _defaultHostServer = defaultHostServer;
            _fallbackHostServer = fallbackHostServer;
        }
        public string GetRemoteFallbackURL(string fileName)
        {
            return _fallbackHostServer + fileName;
        }

        public string GetRemoteMainURL(string fileName)
        {
            return _defaultHostServer + fileName;
        }
    }

    /// <summary>
    /// 资源文件偏移加载解密类
    /// </summary>
    private class FileOffsetDecryption : IDecryptionServices
    {
        // AssetBundle解密方法
        AssetBundle IDecryptionServices.LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
        {
            managedStream = null;
            return AssetBundle.LoadFromFile(fileInfo.FileLoadPath, fileInfo.FileLoadCRC, GetFileOffset());
        }

        // AssetBundle解密方法
        AssetBundleCreateRequest IDecryptionServices.LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream)
        {
            managedStream = null;
            return AssetBundle.LoadFromFileAsync(fileInfo.FileLoadPath, fileInfo.FileLoadCRC, GetFileOffset());
        }

        // 原生文件解密方法
        byte[] IDecryptionServices.ReadFileData(DecryptFileInfo fileInfo)
        {
            throw new System.NotImplementedException();
        }

        // 原生文件解密方法
        string IDecryptionServices.ReadFileText(DecryptFileInfo fileInfo)
        {
            throw new System.NotImplementedException();
        }

        private static ulong GetFileOffset()
        {
            return 32;
        }
    }
}
