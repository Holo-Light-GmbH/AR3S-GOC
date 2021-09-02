#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[ExecuteInEditMode]
public static class MeshSaverEditor
{
#if UNITY_EDITOR

	[MenuItem("HoloLight/Export Grouped Objects to ARES Basic")]
	public static void SaveGroupedMeshesToARESBasic()
    {
		// Save Combined Meshes
		SaveCombinedMeshes();

		// Create Assetbundles
		BuildAllAssetBundles(BuildTarget.WSAPlayer, "ARES Basic", BuildAssetBundleOptions.AssetBundleStripUnityVersion);
	}

	[MenuItem("HoloLight/Export Grouped Objects to ARES Pro")]
	public static void SaveGroupedMeshesToARESPro()
    {
		// Save Combined Meshes
		SaveCombinedMeshes();

		// Create Assetbundles
		BuildAllAssetBundles(BuildTarget.StandaloneWindows, "ARES Pro", BuildAssetBundleOptions.AssetBundleStripUnityVersion);
	}

	public static void SaveCombinedMeshes()
    {
		// Choose Selected Gameobject
		var gameObject = Selection.activeGameObject;

		if (gameObject == null)
		{
			ShowPopup window = ScriptableObject.CreateInstance<ShowPopup>();
			
			window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
			
			window.ShowPopup();

			return;
		}

		//Create SubMeshes Directory
		string subMeshesDirectory = "Assets/Meshes/SubMeshes/";

		if (!Directory.Exists(subMeshesDirectory))
		{
			Directory.CreateDirectory(subMeshesDirectory);
		}

		// Generate Combined Meshes
		var combinedMeshes = gameObject.GetComponentsInChildren<MeshCombiner>();

		foreach (var combinedMesh in combinedMeshes)
        {
			// Combine Meshes
			combinedMesh.CombineMeshes();

			// Save Mesh
			var mf = combinedMesh.GetComponent<MeshFilter>();
			
			// Filter for critical characters in gameobject names
			combinedMesh.name.Replace(" ", "_");
			combinedMesh.name.Replace(".", "_");
			combinedMesh.name.Replace("/", "_");
			combinedMesh.name.Replace("\n", "_");
			combinedMesh.name.Replace(",", "_");
			combinedMesh.name.Replace("-", "_");
			combinedMesh.name.Replace(":", "_");
			combinedMesh.name.Replace(";", "_");

			var meshPath = Path.Combine(subMeshesDirectory, combinedMesh.name + ".asset");

			SaveMesh(meshPath, mf.sharedMesh, combinedMesh.name, true, true);

			// Reassign saved Mesh
			mf.mesh = AssetDatabase.LoadAssetAtPath(meshPath, typeof(Mesh)) as Mesh;
		}

		// Create Prefab Directory
		string prefabsDirectory = "Assets/Meshes/Prefabs/";

		if (!Directory.Exists(prefabsDirectory))
		{
			Directory.CreateDirectory(prefabsDirectory);
		}

		var prefabPath = Path.Combine(prefabsDirectory, gameObject.name + ".prefab");

		// Create Prefab
		PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, prefabPath, InteractionMode.AutomatedAction);

		// Set Asset Name
		AssetImporter.GetAtPath(prefabPath).SetAssetBundleNameAndVariant(gameObject.name, "");
	}

	public static void SaveMesh(string FilePath, Mesh mesh, string name, bool makeNewInstance, bool optimizeMesh)
	{
		Mesh meshToSave = (makeNewInstance) ? Object.Instantiate(mesh) as Mesh : mesh;

		if (optimizeMesh)
        {
			MeshUtility.Optimize(meshToSave);
		}

		AssetDatabase.CreateAsset(meshToSave, FilePath);

		AssetDatabase.SaveAssets();
	}

	static void BuildAllAssetBundles(BuildTarget target, string folderName, BuildAssetBundleOptions options)
	{
		string assetBundleDirectory = "Assets/ARES.Export/" + folderName;

		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}

		BuildPipeline.BuildAssetBundles(assetBundleDirectory, options, target);
	}
#endif
}

#if UNITY_EDITOR
public class ShowPopup : EditorWindow
{
	void OnGUI()
	{
		EditorGUILayout.LabelField("No GameObject is selected. Please select a GameObject!", EditorStyles.wordWrappedMiniLabel);

		GUILayout.Space(70);
		
		if (GUILayout.Button("OK")) this.Close();
	}
}
#endif
