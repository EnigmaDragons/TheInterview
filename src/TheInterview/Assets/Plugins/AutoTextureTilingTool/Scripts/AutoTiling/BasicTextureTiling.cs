using UnityEngine;
using System.Collections.Generic;

namespace AutoTiling {

    public class BasicTextureTiling : AutoTextureTiling {

        protected override MeshData SplitMeshForFaceUnwrapping(MeshData meshData) {

            //Debug.Log("Setting mesh by basic parameters");
            MeshData oldMeshData = meshData.Copy();
            List<FaceData> faceDataList = new List<FaceData>();
            for (int submeshIndex = 0; submeshIndex < meshData.Triangles.Length; submeshIndex++) {
                if (_faceUnwrapData != null && submeshIndex < _faceUnwrapData.Length) {
                    faceDataList.Add(_faceUnwrapData[submeshIndex]);
                }
                else {
                    FaceData newFaceData = new FaceData();
                    newFaceData.Initialize();
                    newFaceData.materialIndex = submeshIndex;
                    for (int triangleIndex = 0; triangleIndex < meshData.Triangles[submeshIndex].Count; triangleIndex += 3) {
                        int[] triangleVertexIndices = new int[3];
                        Vector3 normal = Vector3.zero;
                        for (int singleTriangleVertexIndex = 0; singleTriangleVertexIndex < 3; singleTriangleVertexIndex++) {
                            int vertexIndex = triangleVertexIndices[singleTriangleVertexIndex] = meshData.Triangles[submeshIndex][triangleIndex + singleTriangleVertexIndex];
                            normal += meshData.Normals[vertexIndex];
                        }
                        normal /= 3f;
                        newFaceData.AddTriangle(triangleVertexIndices, normal);
                    }
                    faceDataList.Add(newFaceData);
                }
            }
            MeshData newMeshData = new MeshData();
            newMeshData.subMeshCount = oldMeshData.subMeshCount;
            for (int i = 0; i < faceDataList.Count; i++) {
                FaceData changedData = new FaceData();
                newMeshData = AddMeshDataForFaceData(faceDataList[i], newMeshData, oldMeshData, out changedData);
                faceDataList[i] = changedData;
            }
            _faceUnwrapData = faceDataList.ToArray();
            //Debug.Log("New face unwrap data count: " + _faceUnwrapData.Length);
            oldMeshData = newMeshData;
            return oldMeshData;

        }

    }

}