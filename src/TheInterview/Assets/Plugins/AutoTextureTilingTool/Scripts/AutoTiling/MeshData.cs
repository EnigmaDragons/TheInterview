using UnityEngine;
using System.Collections.Generic;
using System;

namespace AutoTiling {

    public class MeshData {

        private List<Vector3> vertices = new List<Vector3>();
        private List<Vector3> normals = new List<Vector3>();
        private List<int>[] triangles = new List<int>[1];
        private List<Vector2> uv = new List<Vector2>();
        private List<Vector2> uv2 = new List<Vector2>();
        private List<Vector4> tangents = new List<Vector4>();

        public List<Vector3> Vertices {

            get {
                return vertices;
            }

        }

        public List<Vector3> Normals {

            get {
                return normals;
            }

        }

        public List<int>[] Triangles {

            get {
                return triangles;
            }

        }

        public List<Vector2> UV {

            get {
                return uv;
            }

        }

        public List<Vector2> UV2 {
            get {
                return uv2;
            }
        }

        public List<Vector4> Tangents {

            get {
                return tangents;
            }

        }

        public int subMeshCount {
            get {
                return triangles.Length;
            }

            set {
                List<int>[] newTriangleList = new List<int>[value];
                for (int i = 0; i < newTriangleList.Length; i++) {
                    if (i < triangles.Length) {
                        newTriangleList[i] = triangles[i];
                    }
                    else {
                        newTriangleList[i] = new List<int>();
                    }
                }
                triangles = newTriangleList;
                //				Debug.Log ("Setting SubMeshCount to " + triangles.Length);
            }
        }

        public MeshData() {

            triangles = new List<int>[1];
            triangles[0] = new List<int>();

        }

        public MeshData(List<Vector3> vertices, List<Vector3> normals, List<int>[] triangles, List<Vector2> uv, List<Vector2> uv2, List<Vector4> tangents) {
            this.vertices = vertices;
            this.normals = normals;
            this.triangles = triangles;
            this.uv = uv;
            this.uv2 = uv2;
            this.tangents = tangents;
        }

        public MeshData Copy() {

            List<int>[] newTriangles = new List<int>[triangles.Length];
            for (int i = 0; i < triangles.Length; i++) {
                newTriangles[i] = new List<int>(triangles[i]);
            }
            return new MeshData(new List<Vector3>(vertices), new List<Vector3>(normals), newTriangles, new List<Vector2>(uv), new List<Vector2>(uv2), new List<Vector4>(tangents));

        }

        public void AddQuadTriangles() {

            if (triangles == null || triangles.Length < 1) {
                triangles = new List<int>[1];
            }
            if (triangles[0] == null) {
                triangles[0] = new List<int>();
            }
            if (triangles == null) {
                Debug.LogError("triangles were not set!");
                return;
            }
            if (triangles[0] == null) {
                Debug.LogError("triangles[0] was not set!");
                return;
            }
            if (vertices == null) {
                Debug.LogError("Vertices were not set!");
                return;
            }
            triangles[0].Add(vertices.Count - 4);
            triangles[0].Add(vertices.Count - 3);
            triangles[0].Add(vertices.Count - 2);

            triangles[0].Add(vertices.Count - 4);
            triangles[0].Add(vertices.Count - 2);
            triangles[0].Add(vertices.Count - 1);

        }

        public void AddTriangle(int tri) {

            if (triangles == null || triangles.Length < 1) {
                triangles = new List<int>[1];
                triangles[0] = new List<int>();
            }
            //			Debug.Log (GetType() + ".AddTriangle: Adding Triangle " + tri + " without specific material index.");
            triangles[0].Add(tri);

        }

        public void AddTriangle(int tri, int materialIndex) {

            if (materialIndex >= triangles.Length) {
                if (materialIndex > 0) {
                    Debug.LogError(GetType() + ".AddTriangle: the material index is too high, set subMeshCount first.");
                    return;
                }
                else {
                    AddTriangle(tri);
                    return;
                }
            }
            if (triangles[materialIndex] == null) {
                triangles[materialIndex] = new List<int>();
            }
            //			Debug.Log (GetType() + ".AddTriangle: Adding Triangle " + tri + " to material: " + materialIndex);
            triangles[materialIndex].Add(tri);

        }

        public void SetTriangles(Mesh mesh) {

            this.triangles = new List<int>[mesh.subMeshCount];
            if (mesh == null) {
                this.triangles[0] = new List<int>();
                return;
            }

            for (int i = 0; i < mesh.subMeshCount; i++) {
                int[] triangles = mesh.GetTriangles(i);
                if (triangles == null) {
                    this.triangles[i] = new List<int>();
                }
                else {
                    this.triangles[i] = new List<int>(mesh.GetTriangles(i));
                }
            }

        }

        public void SetTriangles(int[] newTriangles) {

            if (newTriangles == null) {
                this.triangles[0] = new List<int>();
                return;
            }
            this.triangles[0] = new List<int>(newTriangles);

        }

        public void AddVertex(Vector3 vertex) {

            vertices.Add(vertex);

        }

        private void RemoveVertices(Dictionary<int, int> vertexIndexDict) {

            //vertexIndexDict.Sort(
            //    delegate (VertexIndexReplacePair x, VertexIndexReplacePair y) {
            //        if (x.indexToReplace < y.indexToReplace) {
            //            return 1;
            //        }
            //        else if (x.indexToReplace > y.indexToReplace) {
            //            return -1;
            //        }
            //        else {
            //            return 0;
            //        }
            //    }
            //);
            for (int indexToReplace = vertices.Count - 1; indexToReplace >= 0; indexToReplace--) {
                if (vertexIndexDict.ContainsKey(indexToReplace)) {
                    //Debug.Log("Trying to replace double vertex " + indexToReplace + " with vertex index " + vertexIndexDict[indexToReplace]);
                    if (normals.Count > indexToReplace) {
                        normals.RemoveAt(indexToReplace);
                    }
                    if (vertices.Count > indexToReplace) {
                        vertices.RemoveAt(indexToReplace);
                    }
                    if (tangents.Count > indexToReplace) {
                        tangents.RemoveAt(indexToReplace);
                    }
                    if (uv.Count > indexToReplace) {
                        uv.RemoveAt(indexToReplace);
                    }
                    if (uv2.Count > indexToReplace) {
                        uv2.RemoveAt(indexToReplace);
                        //Debug.Log("Removed uv2 coordinate");
                    }
                    //List<int> triangleList = new List<int>(triangles);
                    for (int m = 0; m < triangles.Length; m++) {
                        for (int t = 0; t < triangles[m].Count; t++) {
                            if (triangles[m][t] == indexToReplace) {
                                triangles[m][t] = vertexIndexDict[indexToReplace];
                            }
                            else if (triangles[m][t] > indexToReplace) {
                                //Debug.Log("Reducing index: " + triangles[m][t] + " to " + (triangles[m][t] - 1));
                                triangles[m][t]--;
                            }
                        }
                    }
                }
            }

        }

        public void SetVertices(Vector3[] newVertices) {

            if (newVertices == null) {
                vertices = new List<Vector3>();
                return;
            }
            vertices = new List<Vector3>(newVertices);

        }

        public void AddNormal(Vector3 normal) {

            normals.Add(normal);

        }

        public void SetNormals(Vector3[] newNormals) {

            if (newNormals == null) {
                normals = new List<Vector3>();
                return;
            }
            normals = new List<Vector3>(newNormals);

        }

        public void AddTangent(Vector4 tangent) {

            tangents.Add(tangent);

        }

        public void SetTangents(Vector4[] newTangents) {

            if (newTangents == null) {
                tangents = new List<Vector4>();
                return;
            }
            tangents = new List<Vector4>(newTangents);

        }

        public void AddUVCoordinates(Vector2[] uvCoordinates) {

            uv.AddRange(uvCoordinates);

        }

        public void AddUVCoordinate(Vector2 uvCoordinate) {

            //			Debug.Log ("Adding UV Coordinate: " + uvCoordinate);
            uv.Add(uvCoordinate);

        }

        public void SetUV2Coordinates(Vector2[] uvCoordinates) {
            if (uvCoordinates == null) {
                uv2 = new List<Vector2>();
                return;
            }
            uv2 = new List<Vector2>(uvCoordinates);
        }

        public void AddUV2Coordinate(Vector2 coordinate) {
            uv2.Add(coordinate);
        }

        public void RemoveDoubles(bool checkForDifferingUV2coords = false) {

            Dictionary<int, int> doubleVertexIndices = new Dictionary<int, int>();
            for (int i = vertices.Count - 1; i >= 0; i--) {
                for (int j = 0; j < vertices.Count && j < i; j++) {
                    if (vertices[i] == vertices[j] && normals[i] == normals[j] && tangents[i] == tangents[j] && (!checkForDifferingUV2coords || (checkForDifferingUV2coords && uv2[i] == uv2[j]))) {
                        doubleVertexIndices[i] = j;
                        break;
                    }
                }
            }

            RemoveVertices(doubleVertexIndices);
            List<int>[] newTriangleList = new List<int>[1];
            newTriangleList[0] = new List<int>();
            for (int m = 0; m < triangles.Length; m++) {
                for (int t = 0; t < triangles[m].Count; t++) {
                    newTriangleList[0].Add(triangles[m][t]);
                }
            }
            triangles = newTriangleList;
            //Debug.Log("Triangles after removing submeshes: " + triangles[0].Count);
            List<IndexTriplet> triplets = new List<IndexTriplet>();
            for (int i = 0; i < triangles[0].Count; i += 3) {
                triplets.Add(new IndexTriplet(triangles[0][i], triangles[0][i + 1], triangles[0][i + 2]));
            }
            triplets.Sort();
            List<int> newTriangles = new List<int>();
            for (int i = 0; i < triplets.Count; i++) {
                newTriangles.Add(triplets[i].x);
                newTriangles.Add(triplets[i].y);
                newTriangles.Add(triplets[i].z);
            }
            triangles[0] = newTriangles;

        }

        private class IndexTriplet : System.IComparable {

            public int x;
            public int y;
            public int z;

            public IndexTriplet(int x, int y, int z) {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public int CompareTo(object otherObj) {

                IndexTriplet other = otherObj as IndexTriplet;
                if (other == null) {
                    return 1;
                }

                if (x < other.x) {
                    return -1;
                }
                else if (x == other.x) {
                    if (y < other.y) {
                        return -1;
                    }
                    else if (y == other.y) {
                        if (z < other.z) {
                            return -1;
                        }
                        else if (z == other.z) {
                            return 0;
                        }
                        else {
                            return 1;
                        }
                    }
                    else {
                        return 1;
                    }
                }
                else {
                    return 1;
                }

            }

        }

        /*
        public List<FaceData> RemoveDoubles(List<FaceData> faceDataList) {

            Dictionary<int, int> doubleVertexIndices = new Dictionary<int, int>();
            for (int i = 0; i < faceDataList.Count; i++) {
                for (int tIndex = 0; tIndex < faceDataList[i].Triangles.Length; tIndex++) {
                    if (faceDataList[i].Triangles[tIndex] >= vertices.Count) {
                        Debug.LogError(GetType() + ".RemoveDoubles: tried to get vertex " + faceDataList[i].Triangles[tIndex] + ", but vertices List contains only " + vertices.Count + " entries.");
                        continue;
                    }
                    int triangleVertexIndex = faceDataList[i].Triangles[tIndex];
                    List<int> faceTriangleList = new List<int>(faceDataList[i].Triangles);
                    //string faceTriangleString = "";
                    //foreach (var t in faceTriangleList) {
                    //    faceTriangleString += t + ", ";
                    //}
                    //Debug.Log("Triangle indices on face: " + faceTriangleString);
                    for (int v = 0; v < vertices.Count; v++) {
                        if (v != triangleVertexIndex) {
                            int indexToReplace = v;
                            int indexToReplaceWith = triangleVertexIndex;
                            if (indexToReplaceWith > indexToReplace) {
                                int change = indexToReplace;
                                indexToReplace = indexToReplaceWith;
                                indexToReplaceWith = change;
                            }
                            if (vertices[indexToReplace] == vertices[indexToReplaceWith] 
                                && normals[indexToReplace] == normals[indexToReplaceWith] 
                                && (tangents.Count <= indexToReplace || tangents[indexToReplace] == tangents[indexToReplaceWith])
                                && faceTriangleList.Contains(indexToReplace)) {
                                if (!doubleVertexIndices.ContainsKey(indexToReplace) || doubleVertexIndices[indexToReplace] > indexToReplaceWith) {
                                    //Debug.Log("Added Vertex #" + indexToReplace + " (" + vertices[indexToReplace] + ") to double list. Replace with Vertex #" + indexToReplaceWith + " (" + vertices[indexToReplaceWith] + ")");
                                    doubleVertexIndices[indexToReplace] = indexToReplaceWith;
                                }
                            }
                        }
                    }
                }
            }
            RemoveVertices(doubleVertexIndices);
            for (int i = 0; i < faceDataList.Count; i++) {
                faceDataList[i].ReplaceTriangleIndices(doubleVertexIndices);
            }
            return faceDataList;
        }
        */

    }

    //public struct VertexIndexReplacePair {

    //    public int indexToReplace;
    //    public int indexToReplaceWith;

    //    public VertexIndexReplacePair(int indexToReplace, int indexToReplaceWith) {
    //        this.indexToReplace = indexToReplace;
    //        this.indexToReplaceWith = indexToReplaceWith;
    //    }

    //}

}
