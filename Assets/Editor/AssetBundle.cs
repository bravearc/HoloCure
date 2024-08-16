using System.IO;
using UnityEditor;


public class AssetBundle
{
    [MenuItem("Mytool/Assetbundle Build")]
    public static void AssetBundleBuild()
    {
        string directory = "./Bundle";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);


    }
}
