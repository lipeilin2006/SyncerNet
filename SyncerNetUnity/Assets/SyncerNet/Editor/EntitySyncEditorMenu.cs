using HybridCLR.Editor.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SyncerNet
{
    public class SyncerNetEditorMenu : MonoBehaviour
    {
        [MenuItem("SyncerNet/Copy HotUpdate Dlls")]
        static void CopyHotfixDll()
        {
            string hotfixDir = HybridCLRSettings.Instance.hotUpdateDllCompileOutputRootDir;
            string metadataDir = HybridCLRSettings.Instance.strippedAOTDllOutputRootDir;
            string target = EditorUserBuildSettings.activeBuildTarget.ToString();

            foreach (string dll in Assemblies.AotAssemblies)
            {
                File.Copy($"{metadataDir}/{target}/{dll}", $"Assets/HotUpdate/HotfixAssemblies/Aot/{dll}.bytes", true);
            }
            foreach (string dll in Assemblies.HotUpdateAssemblies)
            {
                File.Copy($"{hotfixDir}/{target}/{dll}", $"Assets/HotUpdate/HotfixAssemblies/HotUpdate/{dll}.bytes", true);
            }
            AssetDatabase.Refresh();
            Debug.Log("Finished");
        }

        [MenuItem("SyncerNet/Clear HotUpdate Dlls")]
        static void ClearHotfixDll()
        {
            foreach (string dll in Directory.GetFiles("Assets/HotUpdate/HotfixAssemblies/Aot/"))
            {
                File.Delete(dll);
            }
            foreach (string dll in Directory.GetFiles("Assets/HotUpdate/HotfixAssemblies/HotUpdate/"))
            {
                File.Delete(dll);
            }
            AssetDatabase.Refresh();
            Debug.Log("Finished");
        }

        [MenuItem("SyncerNet/Generate HotUpdate Config")]
        static void GenHotUpdateConfig()
        {
            File.WriteAllText("Assets/HotUpdate/HotfixAssemblies/HotUpdateConfig.json", JsonConvert.SerializeObject(new Dictionary<string, string[]>() { { "Aot", Assemblies.AotAssemblies }, { "HotUpdate", Assemblies.HotUpdateAssemblies } }));
            AssetDatabase.Refresh();
            Debug.Log("Finished");
        }
    }
}
