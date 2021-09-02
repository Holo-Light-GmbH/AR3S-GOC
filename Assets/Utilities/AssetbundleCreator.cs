#if UNITY_EDITOR
using UnityEditor;
using System.IO;

namespace HoloLight.AssetbundleCreation
{
    public class AssetbundleCreator
    {
        [MenuItem("Assets/Build AssetBundles/Standalone Windows")]
        public static void BuildAllAssetBundlesStandaloneWindows()
        {
            BuildAllAssetBundles(BuildTarget.StandaloneWindows, BuildAssetBundleOptions.AssetBundleStripUnityVersion);
        }

        [MenuItem("Assets/Build AssetBundles/Android")]
        public static void BuildAllAssetBundlesAndroid()
        {
            BuildAllAssetBundles(BuildTarget.Android, BuildAssetBundleOptions.AssetBundleStripUnityVersion);
        }

        [MenuItem("Assets/Build AssetBundles/IOS")]
        public static void BuildAllAssetBundlesIOS()
        {
            BuildAllAssetBundles(BuildTarget.iOS, BuildAssetBundleOptions.AssetBundleStripUnityVersion);
        }

        [MenuItem("Assets/Build AssetBundles/WSA")]
        public static void BuildAllAssetBundlesWSA()
        {
            BuildAllAssetBundles(BuildTarget.WSAPlayer, BuildAssetBundleOptions.AssetBundleStripUnityVersion);
        }

        static void BuildAllAssetBundles(BuildTarget target, BuildAssetBundleOptions options)
        {
            string assetBundleDirectory = "Assets/AssetBundles/" + target.ToString();
            
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, options, target);
        }
    }
}
#endif
