using System.Collections.Generic;
using UnityEngine;
using SmartCombine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
	public void CombineMeshes()
    {
		var meshRenderers = GetComponentsInChildren<MeshRenderer>(true);

		var smartMeshDatas = new List<SmartMeshData>();

		for (int i = 0; i < meshRenderers.Length; i++)
		{
			if (i == 0) { continue; }

			var renderer = meshRenderers[i];

			var mesh = renderer.GetComponent<MeshFilter>()?.sharedMesh;
			var materials = renderer.sharedMaterials;
			var position = renderer.transform.position; // - transform.position;
			var rotation = renderer.transform.rotation; // * Quaternion.Inverse(transform.rotation);
			var scale = renderer.transform.lossyScale;
			var smartMeshData = new SmartMeshData(mesh, materials, position, rotation, scale);
			smartMeshDatas.Add(smartMeshData);
		}

		GenerateMergedMeshes(smartMeshDatas);
	}

    private void GenerateMergedMeshes(List<SmartMeshData> smartMeshDatas)
	{
		var data = smartMeshDatas.ToArray();

		var combinedMesh = new Mesh();

		combinedMesh.name = this.name;

		combinedMesh.CombineMeshesSmart(data, out var combinedMaterials, false, false);

		transform.GetComponent<MeshRenderer>().sharedMaterials = combinedMaterials;

		transform.GetComponent<MeshFilter>().sharedMesh = combinedMesh;

		transform.position = Vector3.zero;

		transform.rotation = Quaternion.identity;

		while(transform.childCount > 0)
        {
			var child = transform.GetChild(0);
			if (child != null)
			{
				DestroyImmediate(child.gameObject);
			}
		}
	}
}