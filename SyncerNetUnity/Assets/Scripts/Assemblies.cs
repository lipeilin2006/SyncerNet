using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

/// <summary>
/// 记录热更新需要使用的dll
/// </summary>
public class Assemblies
{
    public static string[] AotAssemblies =
    {
        "mscorlib.dll",
        "System.dll",
        "System.Core.dll" ,
        "MemoryPack.Core.dll",
		"Unity.InputSystem.dll",
		"Unity.InputSystem.ForUI.dll"
	};
    public static string[] HotUpdateAssemblies =
    {
        "SyncerNet.Hotfix.dll",
        "HotUpdate.dll"
    };
    public static IEnumerator Refresh()
    {
        var package = YooAssets.GetPackage("DefaultPackage");
        AssetHandle handle = package.LoadAssetAsync<TextAsset>("Assets/HotUpdate/HotfixAssemblies/HotUpdateConfig.json");
        yield return handle;
        TextAsset textAsset = handle.AssetObject as TextAsset;
        Dictionary<string, string[]> hotUpdateConfig = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(textAsset.text);
        AotAssemblies = hotUpdateConfig["Aot"];
        HotUpdateAssemblies = hotUpdateConfig["HotUpdate"];
    }
}
