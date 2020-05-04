using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
//using System.IO;
//using System.Linq;
//using UnityEngine.EventSystems;
#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace AutoTiling {

    [CustomEditor(typeof(AutoTextureTiling))]
    public class AutoTextureTiling_Editor : Editor {

        //		static Camera sceneCamera;
        //		static List<Vector3> currentSelectedTriangles;
        //		static Direction currentSelectedFace;
        //		static Material lineMaterial;
        //		static Tool lastUsedTool;
        //		float amount = 0f;

        public override void OnInspectorGUI() {

            AutoTextureTiling Target = target as AutoTextureTiling;
            if (!Target) {
                Debug.LogError("AutoTextureTiling_Editor.OnInspectorGUI: target was not of type AutoTextureTiling.");
                Destroy(target);
                return;
            }

            bool changedAnything = false;
            EditorGUILayout.BeginHorizontal();
            if (Target.useBakedMesh) {
                if (GUILayout.Button("Delete Mesh Asset", GUILayout.Width(200f))) {
                    Target.DeleteConnectedMesh();
                    changedAnything = true;
                }
            }
            else {
                if (GUILayout.Button("Save Mesh Asset", GUILayout.Width(200f))) {
                    Target.SaveMeshAsset();
                    changedAnything = true;
                }
            }
            if (GUILayout.Button("ATT Window")) {
                TextureTilingEditorWindow.ShowWindow();
                TextureTilingEditorWindow.window.Repaint();
            }
            EditorGUILayout.EndHorizontal();

            DrawFaceUnwrapTypeSelection(Target);
            //UnwrapType oldUnwrapType = Target.unwrapMethod;
            //EditorGUI.BeginChangeCheck();
            //UnwrapType unwrapType = (UnwrapType)EditorGUILayout.Popup("Unwrap Method", (int)oldUnwrapType, Enum.GetNames(typeof(UnwrapType)));
            //if (EditorGUI.EndChangeCheck() && oldUnwrapType != unwrapType) {
            //    Target.unwrapMethod = unwrapType;
            //}
            ////if (GUILayout.Button("Print Vertices")) {
            ////    for (int i = 0; i < Target.meshFilter.sharedMesh.vertices.Length; i++) {
            ////        Debug.Log("Vertex " + i + ": " + Target.meshFilter.sharedMesh.vertices[i]);
            ////    }
            ////}

            EditorGUILayout.Space();

            string[] options = new string[Target.Renderer.sharedMaterials.Length];

            for (int i = 0; i < Target.Renderer.sharedMaterials.Length; i++) {
                if (Target.Renderer.sharedMaterials[i] == null) {
                    options[i] = "[NULL]";
                }
                else {
                    options[i] = Target.Renderer.sharedMaterials[i].name;
                }
            }

            switch (Target.unwrapMethod) {
                case UnwrapType.CubeProjection:
                    #region CUBE_PROJECTION_EDITOR
                    bool oldUnifiedScaling = Target.useUnifiedScaling;
                    Target.useUnifiedScaling = EditorGUILayout.Toggle("Use Unified Scaling", Target.useUnifiedScaling);
                    if (Target.useUnifiedScaling) {
                        EditorGUI.BeginChangeCheck();
                        Vector2 newValue = EditorGUILayout.Vector2Field("Top Scale", Target.topScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.topScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                    }
                    else {
                        EditorGUI.BeginChangeCheck();
                        Vector2 newValue = EditorGUILayout.Vector2Field("Top Scale", Target.topScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.topScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newValue = EditorGUILayout.Vector2Field("Bottom Scale", Target.bottomScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.bottomScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newValue = EditorGUILayout.Vector2Field("Left Scale", Target.leftScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.leftScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newValue = EditorGUILayout.Vector2Field("Right Scale", Target.rightScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.rightScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newValue = EditorGUILayout.Vector2Field("Front Scale", Target.frontScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.frontScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newValue = EditorGUILayout.Vector2Field("Back Scale", Target.backScale);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedScaling != Target.useUnifiedScaling) {
                            Target.backScale = newValue;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                    }

                    bool oldUnifiedOffset = Target.useUnifiedOffset;
                    Target.useUnifiedOffset = EditorGUILayout.Toggle("Use Unified Offset", Target.useUnifiedOffset);
                    if (Target.useUnifiedOffset) {
                        EditorGUI.BeginChangeCheck();
                        Vector2 newOffset = EditorGUILayout.Vector2Field("Top Offset", Target.topOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.topOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                    }
                    else {
                        EditorGUI.BeginChangeCheck();
                        Vector2 newOffset = EditorGUILayout.Vector2Field("Top Offset", Target.topOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.topOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newOffset = EditorGUILayout.Vector2Field("Bottom Offset", Target.bottomOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.bottomOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newOffset = EditorGUILayout.Vector2Field("Left Offset", Target.leftOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.leftOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newOffset = EditorGUILayout.Vector2Field("Right Offset", Target.rightOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.rightOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newOffset = EditorGUILayout.Vector2Field("Front Offset", Target.frontOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.frontOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                        EditorGUI.BeginChangeCheck();
                        newOffset = EditorGUILayout.Vector2Field("Back Offset", Target.backOffset);
                        if (EditorGUI.EndChangeCheck() || oldUnifiedOffset != Target.useUnifiedOffset) {
                            Target.backOffset = newOffset;
                            EditorUtility.SetDirty(Target);
                            changedAnything = true;
                        }
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    float newRotation = EditorGUILayout.FloatField("Top Rotation", Target.topRotation);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.topRotation = newRotation;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUI.BeginChangeCheck();
                    newRotation = EditorGUILayout.FloatField("Bottom Rotation", Target.bottomRotation);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.bottomRotation = newRotation;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newRotation = EditorGUILayout.FloatField("Left Rotation", Target.leftRotation);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.leftRotation = newRotation;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUI.BeginChangeCheck();
                    newRotation = EditorGUILayout.FloatField("Right Rotation", Target.rightRotation);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.rightRotation = newRotation;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newRotation = EditorGUILayout.FloatField("Front Rotation", Target.frontRotation);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.frontRotation = newRotation;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUI.BeginChangeCheck();
                    newRotation = EditorGUILayout.FloatField("Back Rotation", Target.backRotation);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.backRotation = newRotation;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    bool newFlipValueX = EditorGUILayout.Toggle("Top Flip X", Target.topFlipX);
                    bool newFlipValueY = EditorGUILayout.Toggle("Top Flip Y", Target.topFlipY);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.topFlipX = newFlipValueX;
                        Target.topFlipY = newFlipValueY;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newFlipValueX = EditorGUILayout.Toggle("Bottom Flip X", Target.bottomFlipX);
                    newFlipValueY = EditorGUILayout.Toggle("Bottom Flip Y", Target.bottomFlipY);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.bottomFlipX = newFlipValueX;
                        Target.bottomFlipY = newFlipValueY;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newFlipValueX = EditorGUILayout.Toggle("Left Flip X", Target.leftFlipX);
                    newFlipValueY = EditorGUILayout.Toggle("Left Flip Y", Target.leftFlipY);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.leftFlipX = newFlipValueX;
                        Target.leftFlipY = newFlipValueY;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newFlipValueX = EditorGUILayout.Toggle("Right Flip X", Target.rightFlipX);
                    newFlipValueY = EditorGUILayout.Toggle("Right Flip Y", Target.rightFlipY);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.rightFlipX = newFlipValueX;
                        Target.rightFlipY = newFlipValueY;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newFlipValueX = EditorGUILayout.Toggle("Front Flip X", Target.frontFlipX);
                    newFlipValueY = EditorGUILayout.Toggle("Front Flip Y", Target.frontFlipY);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.frontFlipX = newFlipValueX;
                        Target.frontFlipY = newFlipValueY;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    newFlipValueX = EditorGUILayout.Toggle("Back Flip X", Target.backFlipX);
                    newFlipValueY = EditorGUILayout.Toggle("Back Flip Y", Target.backFlipY);
                    if (EditorGUI.EndChangeCheck()) {
                        Target.backFlipX = newFlipValueX;
                        Target.backFlipY = newFlipValueY;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    int newMaterialIndex = EditorGUILayout.Popup("Top Material", Target.topMaterialIndex, options);
                    if (newMaterialIndex != Target.topMaterialIndex && Target.Renderer.sharedMaterials[newMaterialIndex] != null) {
                        Target.topMaterialIndex = newMaterialIndex;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    newMaterialIndex = EditorGUILayout.Popup("Bottom Material", Target.bottomMaterialIndex, options);
                    if (newMaterialIndex != Target.bottomMaterialIndex && Target.Renderer.sharedMaterials[newMaterialIndex] != null) {
                        Target.bottomMaterialIndex = newMaterialIndex;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    newMaterialIndex = EditorGUILayout.Popup("Left Material", Target.leftMaterialIndex, options);
                    if (newMaterialIndex != Target.leftMaterialIndex && Target.Renderer.sharedMaterials[newMaterialIndex] != null) {
                        Target.leftMaterialIndex = newMaterialIndex;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    newMaterialIndex = EditorGUILayout.Popup("Right Material", Target.rightMaterialIndex, options);
                    if (newMaterialIndex != Target.rightMaterialIndex && Target.Renderer.sharedMaterials[newMaterialIndex] != null) {
                        Target.rightMaterialIndex = newMaterialIndex;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    newMaterialIndex = EditorGUILayout.Popup("Front Material", Target.frontMaterialIndex, options);
                    if (newMaterialIndex != Target.frontMaterialIndex && Target.Renderer.sharedMaterials[newMaterialIndex] != null) {
                        Target.frontMaterialIndex = newMaterialIndex;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    newMaterialIndex = EditorGUILayout.Popup("Back Material", Target.backMaterialIndex, options);
                    if (newMaterialIndex != Target.backMaterialIndex && Target.Renderer.sharedMaterials[newMaterialIndex] != null) {
                        Target.backMaterialIndex = newMaterialIndex;
                        EditorUtility.SetDirty(Target);
                        changedAnything = true;
                    }
                    #endregion
                    break;
                case UnwrapType.FaceDependent:
                default:
                    bool oldUseUnifiedScale = Target.useUnifiedScaling;

                    DrawNormalToleranceField(Target);
                    //EditorGUI.BeginChangeCheck();
                    //float newNormalTolerance = EditorGUILayout.FloatField("Normal Tolerance", Target.faceUnwrappingNormalTolerance);
                    //if (EditorGUI.EndChangeCheck() && newNormalTolerance != Target.faceUnwrappingNormalTolerance) {
                    //    Target.faceUnwrappingNormalTolerance = newNormalTolerance;
                    //    Target.CreateMeshAndUVs();
                    //}

                    bool oldUseUnifiedOffset = Target.useUnifiedOffset;
                    Target.useUnifiedScaling = EditorGUILayout.Toggle("Use Unified Scaling", Target.useUnifiedScaling);
                    Target.useUnifiedOffset = EditorGUILayout.Toggle("Use Unified Offset", Target.useUnifiedOffset);
                    if (Target.faceUnwrapData == null) {
                        EditorGUILayout.HelpBox("There was no face data. Maybe it was not gerated correctly. Try switching the Unwrap Method.", MessageType.Warning);
                    }
                    else {
                        for (int i = 0; i < Target.faceUnwrapData.Length; i++) {
                            FaceData faceData = Target.faceUnwrapData[i];
                            if (faceData == null) {
                                EditorGUILayout.HelpBox("Face " + i + " was not set.", MessageType.Error);
                                continue;
                            }
                            EditorGUILayout.BeginVertical(new GUIStyle("Box"));
                            EditorGUILayout.LabelField("Face " + i);

                            if (!Target.useUnifiedScaling || i == 0) {
                                EditorGUI.BeginChangeCheck();
                                Vector2 newValue = EditorGUILayout.Vector2Field("Scale", faceData.uvScale);
                                if (EditorGUI.EndChangeCheck() || oldUseUnifiedScale != Target.useUnifiedScaling) {
                                    Target.ApplyFaceScale(i, newValue);
                                    EditorUtility.SetDirty(Target);
                                    changedAnything = true;
                                }
                            }
                            else {
                                EditorGUI.BeginDisabledGroup(true);
                                EditorGUILayout.Vector2Field("Scale", faceData.uvScale);
                                EditorGUI.EndDisabledGroup();
                            }
                            if (!Target.useUnifiedOffset || i == 0) {
                                EditorGUI.BeginChangeCheck();
                                Vector2 newValue = EditorGUILayout.Vector2Field("Offset", faceData.uvOffset);
                                if (EditorGUI.EndChangeCheck() || oldUseUnifiedOffset != Target.useUnifiedOffset) {
                                    Target.ApplyFaceOffset(i, newValue);
                                    EditorUtility.SetDirty(Target);
                                    changedAnything = true; Debug.Log("Changed offset at face " + i);
                                }

                            }
                            else {
                                EditorGUI.BeginDisabledGroup(true);
                                EditorGUILayout.Vector2Field("Offset", faceData.uvOffset);
                                EditorGUI.EndDisabledGroup();
                            }
                            EditorGUI.BeginChangeCheck();
                            float newFaceRot = EditorGUILayout.FloatField("Rotation", faceData.rotation);
                            if (EditorGUI.EndChangeCheck()) {
                                Target.ApplyFaceRotation(i, newFaceRot);
                                EditorUtility.SetDirty(target);
                                changedAnything = true;
                            }

                            EditorGUI.BeginChangeCheck();
                            bool newFlipX = EditorGUILayout.Toggle("Flip UV X", faceData.flipUVx);
                            if (EditorGUI.EndChangeCheck()) {
                                Target.ApplyFlipUVX(i, newFlipX);
                                EditorUtility.SetDirty(target);
                                changedAnything = true;
                            }
                            EditorGUI.BeginChangeCheck();
                            bool newFlipY = EditorGUILayout.Toggle("Flip UV Y", faceData.flipUVy);
                            if (EditorGUI.EndChangeCheck()) {
                                Target.ApplyFlipUVY(i, newFlipY);
                                EditorUtility.SetDirty(target);
                                changedAnything = true;
                            }

                            DrawMaterialSelector(Target, faceData, options, i, changedAnything, out changedAnything);
                            //EditorGUI.BeginChangeCheck();
                            //int faceMaterialIndex = EditorGUILayout.Popup("Material", faceData.materialIndex, options);
                            //if (faceMaterialIndex >= Target.Renderer.sharedMaterials.Length) {
                            //    faceMaterialIndex = 0;
                            //}
                            //if (EditorGUI.EndChangeCheck() || faceMaterialIndex != faceData.materialIndex) {
                            //    Target.ApplyFaceMaterial(i, faceMaterialIndex);
                            //    EditorUtility.SetDirty(target);
                            //    changedAnything = true;
                            //}

                            EditorGUILayout.EndVertical();
                        }
                    }
                    break;
            }

            if (changedAnything) {
#if UNITY_5_3_OR_NEWER
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#else
                EditorApplication.MarkSceneDirty();
#endif
                if (Target.useBakedMesh) {
                    GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(Target.gameObject) as GameObject;
                    if (prefab) {
                        PrefabUtility.SaveAsPrefabAssetAndConnect(Target.gameObject, AssetDatabase.GetAssetPath(prefab), InteractionMode.AutomatedAction);
                    }
                }
            }

        }

        [ExecuteInEditMode]
        public void OnSceneGUI() {

            #region SELECT_FACE
            if ((GUIUtility.hotControl != 0 && (Event.current.type == EventType.MouseDown && Event.current.button == 0)) || (Selection.activeGameObject != null && TextureTilingEditorWindow.selected == null)) {
                TextureTilingEditorWindow.currentSelectedTriangles = new List<Vector3>();
                TextureTilingEditorWindow.currentSelectedTriangleNormal = Vector3.zero;
                RaycastHit hitInfo;
                // Check if scene camera is set
                if (!TextureTilingEditorWindow.sceneCamera) {
                    // If not try to find the standard scene view camera, by default called SceneCamera by Unity
                    GameObject sceneCameraObj = GameObject.Find("SceneCamera");
                    if (sceneCameraObj != null) {
                        TextureTilingEditorWindow.sceneCamera = sceneCameraObj.GetComponent<Camera>();
                    }
                }
                // Proceed with the found scene camera
                if (TextureTilingEditorWindow.sceneCamera) {
                    Vector3 mousePos = Event.current.mousePosition;
                    mousePos.y = TextureTilingEditorWindow.sceneCamera.pixelHeight - Event.current.mousePosition.y;
                    bool foundFace = false;
                    if (Physics.Raycast(TextureTilingEditorWindow.sceneCamera.ScreenPointToRay(mousePos), out hitInfo, float.PositiveInfinity)) {
                        AutoTextureTiling att = hitInfo.transform.GetComponent<AutoTextureTiling>();
                        if (!att) {
                            att = hitInfo.transform.GetComponent<DynamicTextureTiling>();
                        }
                        if (att) {
                            if (Tools.current != Tool.None) {
                                TextureTilingEditorWindow.lastUsedTool = Tools.current;
                            }
                            if (TextureTilingEditorWindow.currentTextureTool != Tool.None) {
                                Tools.current = Tool.None;
                            }
                            TextureTilingEditorWindow.selected = att;
                            switch (att.unwrapMethod) {
                                case UnwrapType.CubeProjection:
                                    TextureTilingEditorWindow.currentSelectedFace = AutoTextureTiling.GetCubeProjectionDirectionForNormal(Quaternion.Inverse(att.transform.rotation) * hitInfo.normal);
                                    foundFace = true;
                                    TextureTilingEditorWindow.SetSelectedTrianglesFor(att);
                                    break;
                                case UnwrapType.FaceDependent:
                                    if (hitInfo.triangleIndex >= 0) {
                                        for (int i = 0; i < att.faceUnwrapData.Length; i++) {
                                            bool foundMatch = false;
                                            for (int t = 0; t < att.faceUnwrapData[i].Triangles.Length; t += 3) {
                                                if (hitInfo.triangleIndex * 3 < att.meshFilter.sharedMesh.triangles.Length
                                                    && (hitInfo.triangleIndex * 3) + 1 < att.meshFilter.sharedMesh.triangles.Length
                                                    && (hitInfo.triangleIndex * 3) + 2 < att.meshFilter.sharedMesh.triangles.Length) {
                                                    if (att.meshFilter.sharedMesh.triangles[hitInfo.triangleIndex * 3] == att.faceUnwrapData[i].Triangles[t]
                                                        && att.meshFilter.sharedMesh.triangles[(hitInfo.triangleIndex * 3) + 1] == att.faceUnwrapData[i].Triangles[t + 1]
                                                        && att.meshFilter.sharedMesh.triangles[(hitInfo.triangleIndex * 3) + 2] == att.faceUnwrapData[i].Triangles[t + 2]) {
                                                        foundMatch = true;
                                                        TextureTilingEditorWindow.currentSelectedFaceIndex = i;
                                                        //Debug.Log("Selected face " + i);
                                                        TextureTilingEditorWindow.SetSelectedTrianglesFor(att);
                                                        break;
                                                    }
                                                }
                                            }
                                            if (foundMatch) {
                                                foundFace = true;
                                                break;
                                            }
                                        }
                                    }
                                    else {
                                        TextureTilingEditorWindow.currentSelectedFaceIndex = -1;
                                        //Debug.Log("Reset selected face.");
                                    }
                                    break;
                            }
                        }
                        else {
                            if (TextureTilingEditorWindow.window != null) {
                                Tools.current = TextureTilingEditorWindow.lastUsedTool;
                                if (Tools.current == Tool.None) {
                                    Debug.LogWarning(GetType() + ".OnSceneGUI: setting current tool. Fallback to Move tool.");
                                    Tools.current = Tool.Move;
                                }
                            }
                        }
                    }
                    if (!foundFace) {
                        //Debug.Log("No hit, trying without collider.");
                        AutoTextureTiling att = Selection.activeGameObject.GetComponent<AutoTextureTiling>();
                        if (!att) {
                            att = Selection.activeGameObject.GetComponent<DynamicTextureTiling>();
                        }
                        if (!att) {
                            att = Selection.activeGameObject.GetComponent<BasicTextureTiling>();
                        }
                        if (att) {
                            if (Tools.current != Tool.None) {
                                TextureTilingEditorWindow.lastUsedTool = Tools.current;
                            }
                            if (TextureTilingEditorWindow.currentTextureTool != Tool.None) {
                                Tools.current = Tool.None;
                            }
                            TextureTilingEditorWindow.selected = att;
                            //Debug.Log("Checking Auto Texture Tiling Tool object " + att.name);
                            switch (att.unwrapMethod) {
                                case UnwrapType.CubeProjection:
                                    for (int t = 0; t < att.meshFilter.sharedMesh.triangles.Length; t += 3) {
                                        int vertexIndex1 = att.meshFilter.sharedMesh.triangles[t];
                                        int vertexIndex2 = att.meshFilter.sharedMesh.triangles[t + 1];
                                        int vertexIndex3 = att.meshFilter.sharedMesh.triangles[t + 2];
                                        Vector2 p1 = TextureTilingEditorWindow.sceneCamera.WorldToScreenPoint(att.transform.position + ((att.transform.rotation * Vector3.Scale(att.meshFilter.sharedMesh.vertices[vertexIndex1], att.transform.lossyScale))));
                                        Vector2 p2 = TextureTilingEditorWindow.sceneCamera.WorldToScreenPoint(att.transform.position + ((att.transform.rotation * Vector3.Scale(att.meshFilter.sharedMesh.vertices[vertexIndex2], att.transform.lossyScale))));
                                        Vector2 p3 = TextureTilingEditorWindow.sceneCamera.WorldToScreenPoint(att.transform.position + ((att.transform.rotation * Vector3.Scale(att.meshFilter.sharedMesh.vertices[vertexIndex3], att.transform.lossyScale))));
                                        Vector2 center = (p1 + p2 + p3) / 3f;
                                        if (Vector3.Cross((p1 - center), (p2 - center)).z < 0f) {
                                            if (TextureTilingEditorWindow.IsScreenPointInTriangle(p1, p2, p3, mousePos)) {
                                                //Debug.Log("Triangle is clockwise: " + p1 + ", " + p2 + ", " + p3);
                                                //Debug.Log("Mouse Position: " + mousePos);
                                                //Debug.Log("Screen Width: " + Screen.width);
                                                //Debug.Log("Screen Height: " + Screen.height);
                                                Vector3 normal = (att.meshFilter.sharedMesh.normals[vertexIndex1] + att.meshFilter.sharedMesh.normals[vertexIndex2] + att.meshFilter.sharedMesh.normals[vertexIndex3]) / 3f;
                                                TextureTilingEditorWindow.currentSelectedFace = AutoTextureTiling.GetCubeProjectionDirectionForNormal(/*Quaternion.Inverse(att.transform.rotation) **/ normal);
                                                //Debug.Log("Found side " + TextureTilingEditorWindow.currentSelectedFace + " without collider.");
                                                foundFace = true;
                                                TextureTilingEditorWindow.SetSelectedTrianglesFor(att);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case UnwrapType.FaceDependent:
                                    if (att.faceUnwrapData != null) {
                                        for (int i = 0; i < att.faceUnwrapData.Length; i++) {
                                            //Debug.Log("Checking face " + i);
                                            for (int t = 0; t < att.faceUnwrapData[i].Triangles.Length; t += 3) {
                                                int vertexIndex1 = att.faceUnwrapData[i].Triangles[t];
                                                int vertexIndex2 = att.faceUnwrapData[i].Triangles[t + 1];
                                                int vertexIndex3 = att.faceUnwrapData[i].Triangles[t + 2];
                                                Vector2 p1 = Vector3.zero;
                                                Vector2 p2 = Vector3.zero;
                                                Vector2 p3 = Vector3.zero;
                                                if (vertexIndex1 >= att.meshFilter.sharedMesh.vertices.Length || vertexIndex2 >= att.meshFilter.sharedMesh.vertices.Length || vertexIndex3 >= att.meshFilter.sharedMesh.vertices.Length) {
                                                    Debug.Log("Mesh vertices count: " + att.meshFilter.sharedMesh.vertices.Length);
                                                    Debug.Log("Vertex Index 1: " + vertexIndex1);
                                                    Debug.Log("Vertex Index 2: " + vertexIndex2);
                                                    Debug.Log("Vertex Index 3: " + vertexIndex3);
                                                }
                                                else {
                                                    p1 = TextureTilingEditorWindow.sceneCamera.WorldToScreenPoint(att.transform.position + ((att.transform.rotation * Vector3.Scale(att.meshFilter.sharedMesh.vertices[vertexIndex1], att.transform.lossyScale))));
                                                    p2 = TextureTilingEditorWindow.sceneCamera.WorldToScreenPoint(att.transform.position + ((att.transform.rotation * Vector3.Scale(att.meshFilter.sharedMesh.vertices[vertexIndex2], att.transform.lossyScale))));
                                                    p3 = TextureTilingEditorWindow.sceneCamera.WorldToScreenPoint(att.transform.position + ((att.transform.rotation * Vector3.Scale(att.meshFilter.sharedMesh.vertices[vertexIndex3], att.transform.lossyScale))));
                                                }
                                                Vector2 center = (p1 + p2 + p3) / 3f;
                                                if (Vector3.Cross((p1 - center), (p2 - center)).z < 0f) {
                                                    if (TextureTilingEditorWindow.IsScreenPointInTriangle(p1, p2, p3, mousePos)) {
                                                        //Debug.Log("Triangle is clockwise: " + p1 + ", " + p2 + ", " + p3);
                                                        //Debug.Log("Mouse Position: " + mousePos);
                                                        //Debug.Log("Screen Width: " + Screen.width);
                                                        //Debug.Log("Screen Height: " + Screen.height);
                                                        TextureTilingEditorWindow.currentSelectedFaceIndex = i;
                                                        //Debug.Log("Found face " + TextureTilingEditorWindow.currentSelectedFaceIndex + " without collider.");
                                                        foundFace = true;
                                                        TextureTilingEditorWindow.SetSelectedTrianglesFor(att);
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    if (foundFace) {
                                        break;
                                    }
                                    break;
                            }
                            if (!foundFace) {
                                TextureTilingEditorWindow.currentSelectedFaceIndex = -1;
                            }
                        }
                    }
                }
                else {
                    // This should never happen. If this error comes up, try to restart Unity, or copy the contents of your scene in a new scene.
                    Debug.LogError(GetType() + ".OnSceneGUI: there was no editor SceneCamera. This should not be possible. Try to restart Unity");
                }
            }
            #endregion //SELECT_FACE

            if (TextureTilingEditorWindow.currentSelectedTriangles != null && TextureTilingEditorWindow.window != null) {
                AutoTextureTiling att = target as AutoTextureTiling;
                TextureTilingEditorWindow.CreateLineMaterial();
                TextureTilingEditorWindow.lineMaterial.SetPass(0);
                GL.PushMatrix();
                for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i += 3) {
                    GL.Begin(GL.TRIANGLES);
                    for (int j = 0; j < 3; j++) {
                        GL.Color(new Color(1f, 0.5f, 0.5f, .5f));
                        Vector3 currentVertex = TextureTilingEditorWindow.currentSelectedTriangles[i + j];
                        GL.Vertex(att.transform.rotation * (new Vector3(currentVertex.x * att.transform.lossyScale.x, currentVertex.y * att.transform.lossyScale.y, currentVertex.z * att.transform.lossyScale.z)) + att.transform.position);
                    }
                    GL.End();
                }
                GL.PopMatrix();

                if (TextureTilingEditorWindow.currentTextureTool != Tool.None && TextureTilingEditorWindow.selected != null 
                    && TextureTilingEditorWindow.selected.transform.lossyScale.x != 0f && TextureTilingEditorWindow.selected.transform.lossyScale.y != 0f && TextureTilingEditorWindow.selected.transform.lossyScale.z != 0f) {
                    bool changedAnything = false;
                    float amountX = 0f;
                    float amountY = 0f;
                    float scaleAmount = 0f;

                    float sizeModifier = 1f;

                    switch (TextureTilingEditorWindow.selected.unwrapMethod) {
                        case UnwrapType.CubeProjection:
                            #region CALC_HANDLE_POSITIONS_CUBE_UNWRAPPING
                            Vector3 handlePosition = TextureTilingEditorWindow.selected.transform.position;
                            float x = TextureTilingEditorWindow.currentSelectedFace == Direction.Left ? float.PositiveInfinity : 0f;
                            float y = TextureTilingEditorWindow.currentSelectedFace == Direction.Down ? float.PositiveInfinity : 0f;
                            float z = TextureTilingEditorWindow.currentSelectedFace == Direction.Back ? float.PositiveInfinity : 0f;
                            float currentTextureRotation = 0f;
                            switch (TextureTilingEditorWindow.currentSelectedFace) {
                                case Direction.Back:
                                    for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i++) {
                                        y += TextureTilingEditorWindow.currentSelectedTriangles[i].y;
                                        x += TextureTilingEditorWindow.currentSelectedTriangles[i].x;
                                        if (z > TextureTilingEditorWindow.currentSelectedTriangles[i].z) {
                                            z = TextureTilingEditorWindow.currentSelectedTriangles[i].z;
                                        }
                                    }
                                    y = (y / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.y;
                                    x = (x / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.x;
                                    z *= TextureTilingEditorWindow.selected.transform.lossyScale.z;
                                    currentTextureRotation = TextureTilingEditorWindow.selected.backRotation;
                                    break;
                                case Direction.Forward:
                                    for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i++) {
                                        y += TextureTilingEditorWindow.currentSelectedTriangles[i].y;
                                        x += TextureTilingEditorWindow.currentSelectedTriangles[i].x;
                                        if (z < TextureTilingEditorWindow.currentSelectedTriangles[i].z) {
                                            z = TextureTilingEditorWindow.currentSelectedTriangles[i].z;
                                        }
                                    }
                                    y = (y / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.y;
                                    x = (x / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.x;
                                    z *= TextureTilingEditorWindow.selected.transform.lossyScale.z;
                                    currentTextureRotation = TextureTilingEditorWindow.selected.frontRotation;
                                    break;
                                case Direction.Left:
                                    for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i++) {
                                        z += TextureTilingEditorWindow.currentSelectedTriangles[i].z;
                                        y += TextureTilingEditorWindow.currentSelectedTriangles[i].y;
                                        if (x > TextureTilingEditorWindow.currentSelectedTriangles[i].x) {
                                            x = TextureTilingEditorWindow.currentSelectedTriangles[i].x;
                                        }
                                    }
                                    z = (z / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.z;
                                    y = (y / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.y;
                                    x *= TextureTilingEditorWindow.selected.transform.lossyScale.x;
                                    currentTextureRotation = TextureTilingEditorWindow.selected.leftRotation;
                                    break;
                                case Direction.Right:
                                    for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i++) {
                                        z += TextureTilingEditorWindow.currentSelectedTriangles[i].z;
                                        y += TextureTilingEditorWindow.currentSelectedTriangles[i].y;
                                        if (x < TextureTilingEditorWindow.currentSelectedTriangles[i].x) {
                                            x = TextureTilingEditorWindow.currentSelectedTriangles[i].x;
                                        }
                                    }
                                    z = (z / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.z;
                                    y = (y / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.y;
                                    x *= TextureTilingEditorWindow.selected.transform.lossyScale.x;
                                    currentTextureRotation = TextureTilingEditorWindow.selected.rightRotation;
                                    break;
                                case Direction.Down:
                                    for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i++) {
                                        x += TextureTilingEditorWindow.currentSelectedTriangles[i].x;
                                        z += TextureTilingEditorWindow.currentSelectedTriangles[i].z;
                                        if (y > TextureTilingEditorWindow.currentSelectedTriangles[i].y) {
                                            y = TextureTilingEditorWindow.currentSelectedTriangles[i].y;
                                        }
                                    }
                                    x = (x / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.x;
                                    z = (z / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.z;
                                    y *= TextureTilingEditorWindow.selected.transform.lossyScale.y;
                                    currentTextureRotation = TextureTilingEditorWindow.selected.bottomRotation;
                                    break;
                                case Direction.Up:
                                    for (int i = 0; i < TextureTilingEditorWindow.currentSelectedTriangles.Count; i++) {
                                        x += TextureTilingEditorWindow.currentSelectedTriangles[i].x;
                                        z += TextureTilingEditorWindow.currentSelectedTriangles[i].z;
                                        if (y < TextureTilingEditorWindow.currentSelectedTriangles[i].y) {
                                            y = TextureTilingEditorWindow.currentSelectedTriangles[i].y;
                                        }
                                    }
                                    x = (x / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.x;
                                    z = (z / TextureTilingEditorWindow.currentSelectedTriangles.Count) * TextureTilingEditorWindow.selected.transform.lossyScale.z;
                                    y *= TextureTilingEditorWindow.selected.transform.lossyScale.y;
                                    currentTextureRotation = TextureTilingEditorWindow.selected.topRotation;
                                    break;
                            }
                            handlePosition = handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * new Vector3(x, y, z));
                            Quaternion rotationToCurrentSide = Quaternion.FromToRotation(Vector3.up, TextureTilingEditorWindow.currentSelectedTriangleNormal);
                            #endregion //CALC_HANDLE_POSITIONS_CUBE_UNWRAPPING
                            #region RENDER_HANDLES_FOR_CUBE_UNWRAPPING
                            sizeModifier = HandleUtility.GetHandleSize(handlePosition);
                            switch (TextureTilingEditorWindow.currentTextureTool) {
                                case Tool.Move:
                                    #region CUBE_PROJECTION_MOVE_TOOL
                                    switch (TextureTilingEditorWindow.currentSelectedFace) {
                                        case Direction.Back:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f + currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f + currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (att.useUnifiedOffset) {
                                                    att.backOffset = new Vector2(att.topOffset.x + amountX, att.topOffset.y + amountY);
                                                }
                                                else {
                                                    att.backOffset = new Vector2(att.backOffset.x + amountX, att.backOffset.y + amountY);
                                                }
                                            }
                                            Handles.color = Color.blue;
                                            if (ATTHandles.TextureAlignHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) { 
                                                att.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            Handles.color = Color.white;
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.up * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.down * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.left * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.right * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            break;
                                        case Direction.Down:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f + currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (att.useUnifiedOffset) {
                                                    att.bottomOffset = new Vector2(att.topOffset.x + amountX, att.topOffset.y + amountY);
                                                }
                                                else {
                                                    att.bottomOffset = new Vector2(att.bottomOffset.x + amountX, att.bottomOffset.y + amountY);
                                                }
                                            }
                                            Handles.color = Color.blue;
                                            if (ATTHandles.TextureAlignHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            Handles.color = Color.white;
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.forward * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.back * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.left * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.right * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFace);
                                            }

                                            break;
                                        case Direction.Forward:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f - currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(-currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (att.useUnifiedOffset) {
                                                    att.frontOffset = new Vector2(att.topOffset.x + amountX, att.topOffset.y + amountY);
                                                }
                                                else {
                                                    att.frontOffset = new Vector2(att.frontOffset.x + amountX, att.frontOffset.y + amountY);
                                                }
                                            }
                                            Handles.color = Color.blue;
                                            if (ATTHandles.TextureAlignHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            Handles.color = Color.white;
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.up * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.down * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.left * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.right * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFace);
                                            }

                                            break;
                                        case Direction.Left:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f - currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f - currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (att.useUnifiedOffset) {
                                                    att.leftOffset = new Vector2(att.topOffset.x + amountX, att.topOffset.y + amountY);
                                                }
                                                else {
                                                    att.leftOffset = new Vector2(att.leftOffset.x + amountX, att.leftOffset.y + amountY);
                                                }
                                            }
                                            Handles.color = Color.blue;
                                            if (ATTHandles.TextureAlignHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            Handles.color = Color.white;
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.up * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.down * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.back * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.forward * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFace);
                                            }

                                            break;
                                        case Direction.Right:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180 + currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(90f + currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (att.useUnifiedOffset) {
                                                    att.rightOffset = new Vector2(att.topOffset.x + amountX, att.topOffset.y + amountY);
                                                }
                                                else {
                                                    att.rightOffset = new Vector2(att.rightOffset.x + amountX, att.rightOffset.y + amountY);
                                                }
                                            }
                                            Handles.color = Color.blue;
                                            if (ATTHandles.TextureAlignHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            Handles.color = Color.white;
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.up * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.down * (att.transform.lossyScale.y / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.back * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.forward * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFace);
                                            }

                                            break;
                                        case Direction.Up:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f - currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TexturePositionHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f - currentTextureRotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                att.topOffset = new Vector2(att.topOffset.x + amountX, att.topOffset.y + amountY);
                                            }
                                            Handles.color = Color.blue;
                                            if (ATTHandles.TextureAlignHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            Handles.color = Color.white;
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.forward * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.back * (att.transform.lossyScale.z / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.left * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFace);
                                            }
                                            if (ATTHandles.TextureAlignHandle(handlePosition + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.right * (att.transform.lossyScale.x / 2f)), TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                )) {
                                                att.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFace);
                                            }

                                            break;
                                    }
#endregion //CUBE_PROJECTION_MOVE_TOOL
                                    break;
                                case Tool.Scale:
#region CUBE_PROJECTION_SCALE_TOOL
                                    switch (TextureTilingEditorWindow.currentSelectedFace) {
                                        case Direction.Back:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.white;
                                            scaleAmount =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(225f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                                Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                                Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                                if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (scaleAmount != 0f) {
                                                    float factor = (att.backScale.x + scaleAmount) / att.backScale.x;
                                                    amountX = scaleAmount;
                                                    amountY = (att.backScale.y * factor) - att.backScale.y;
                                                }
                                                if (att.useUnifiedScaling) {
                                                    att.backScale = new Vector2(att.topScale.x + amountX, att.topScale.y + amountY);
                                                }
                                                else {
                                                    att.backScale = new Vector2(att.backScale.x + amountX, att.backScale.y + amountY);
                                                }
                                            }
                                            break;
                                        case Direction.Down:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.white;
                                            scaleAmount =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(315f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                                Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (scaleAmount != 0f) {
                                                    float factor = (att.bottomScale.x + scaleAmount) / att.bottomScale.x;
                                                    amountX = scaleAmount;
                                                    amountY = (att.bottomScale.y * factor) - att.bottomScale.y;
                                                }
                                                if (att.useUnifiedScaling) {
                                                    att.bottomScale = new Vector2(att.topScale.x + amountX, att.topScale.y + amountY);
                                                }
                                                else {
                                                    att.bottomScale = new Vector2(att.bottomScale.x + amountX, att.bottomScale.y + amountY);
                                                }
                                            }
                                            break;
                                        case Direction.Forward:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.white;
                                            scaleAmount =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(315f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270 - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(-currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (scaleAmount != 0f) {
                                                    float factor = (att.frontScale.x + scaleAmount) / att.frontScale.x;
                                                    amountX = scaleAmount;
                                                    amountY = (att.frontScale.y * factor) - att.frontScale.y;
                                                }
                                                if (att.useUnifiedScaling) {
                                                    att.frontScale = new Vector2(att.topScale.x + amountX, att.topScale.y + amountY);
                                                }
                                                else {
                                                    att.frontScale = new Vector2(att.frontScale.x + amountX, att.frontScale.y + amountY);
                                                }
                                            }
                                            break;
                                        case Direction.Left:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.white;
                                            scaleAmount =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(225f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (scaleAmount != 0f) {
                                                    float factor = (att.leftScale.x + scaleAmount) / att.leftScale.x;
                                                    amountX = scaleAmount;
                                                    amountY = (att.leftScale.y * factor) - att.leftScale.y;
                                                }
                                                if (att.useUnifiedScaling) {
                                                    att.leftScale = new Vector2(att.topScale.x + amountX, att.topScale.y + amountY);
                                                }
                                                else {
                                                    att.leftScale = new Vector2(att.leftScale.x + amountX, att.leftScale.y + amountY);
                                                }
                                            }
                                            break;
                                        case Direction.Right:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.white;
                                            scaleAmount =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(135f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(90f + currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (scaleAmount != 0f) {
                                                    float factor = (att.rightScale.x + scaleAmount) / att.rightScale.x;
                                                    amountX = scaleAmount;
                                                    amountY = (att.rightScale.y * factor) - att.rightScale.y;
                                                }
                                                if (att.useUnifiedScaling) {
                                                    att.rightScale = new Vector2(att.topScale.x + amountX, att.topScale.y + amountY);
                                                }
                                                else {
                                                    att.rightScale = new Vector2(att.rightScale.x + amountX, att.rightScale.y + amountY);
                                                }
                                            }
                                            break;
                                        case Direction.Up:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.white;
                                            scaleAmount =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(225f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            Handles.color = Color.blue;
                                            amountY =
                                                ATTHandles.TextureScaleHandle(handlePosition, TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270f - currentTextureRotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                Handles.CubeHandleCap
#else
                                                Handles.CubeCap
#endif
                                                );
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                if (scaleAmount != 0f) {
                                                    float factor = (att.topScale.x + scaleAmount) / att.topScale.x;
                                                    amountX = scaleAmount;
                                                    amountY = (att.topScale.y * factor) - att.topScale.y;
                                                }
                                                att.topScale = new Vector2(att.topScale.x + amountX, att.topScale.y + amountY);
                                            }
                                            break;
                                    }
#endregion // CUBE_PROJECTION_SCALE_TOOL
                                    break;
                                case Tool.Rotate:
#region CUBE_PROJECTION_ROTATE_TOOL
                                    switch (TextureTilingEditorWindow.currentSelectedFace) {
                                        case Direction.Back:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureRotationHandle(handlePosition,
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(att.backRotation - 90f, Vector3.up),
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * TextureTilingEditorWindow.currentSelectedTriangleNormal,
                                                                                 att.transform.rotation * Vector3.up,
                                                                                 att.backRotation,
                                                                                 sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                                 Handles.SphereHandleCap);
#else
                                                                                 Handles.SphereCap);
#endif
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                att.backRotation = 90f - amountX;
                                            }
                                            break;
                                        case Direction.Down:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureRotationHandle(handlePosition,
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(att.bottomRotation, Vector3.up),
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * TextureTilingEditorWindow.currentSelectedTriangleNormal,
                                                                                 att.transform.rotation * Vector3.back,
                                                                                 att.bottomRotation,
                                                                                 sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                                 Handles.SphereHandleCap);
#else
                                                                                 Handles.SphereCap);
#endif
                                            if (EditorGUI.EndChangeCheck()) {
                                            changedAnything = true;
                                                att.bottomRotation = -amountX;
                                            }
                                            break;
                                        case Direction.Forward:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureRotationHandle(handlePosition,
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(270 - att.frontRotation, Vector3.up),
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * TextureTilingEditorWindow.currentSelectedTriangleNormal,
                                                                                 att.transform.rotation * Vector3.up,
                                                                                 att.frontRotation,
                                                                                 sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                                 Handles.SphereHandleCap);
#else
                                                                                 Handles.SphereCap);
#endif
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                att.frontRotation = amountX + 90;
                                            }
                                            break;
                                        case Direction.Left:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureRotationHandle(handlePosition,
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f - att.leftRotation, Vector3.up),
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * TextureTilingEditorWindow.currentSelectedTriangleNormal,
                                                                                 att.transform.rotation * Vector3.back,
                                                                                 att.leftRotation,
                                                                                 sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                                 Handles.SphereHandleCap);
#else
                                                                                 Handles.SphereCap);
#endif
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                att.leftRotation = amountX;
                                            }
                                            break;
                                        case Direction.Right:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureRotationHandle(handlePosition,
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f + att.rightRotation, Vector3.up),
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * TextureTilingEditorWindow.currentSelectedTriangleNormal,
                                                                                 att.transform.rotation * Vector3.back,
                                                                                 att.rightRotation,
                                                                                 sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                                 Handles.SphereHandleCap);
#else
                                                                                 Handles.SphereCap);
#endif
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                att.rightRotation = -amountX;
                                            }
                                            break;
                                        case Direction.Up:
                                            EditorGUI.BeginChangeCheck();
                                            Handles.color = Color.red;
                                            amountX =
                                                ATTHandles.TextureRotationHandle(handlePosition,
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * rotationToCurrentSide * Quaternion.AngleAxis(180f - att.topRotation, Vector3.up),
                                                                                 TextureTilingEditorWindow.selected.transform.rotation * TextureTilingEditorWindow.currentSelectedTriangleNormal,
                                                                                 att.transform.rotation * Vector3.back,
                                                                                 att.topRotation,
                                                                                 sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                                 Handles.SphereHandleCap);
#else
                                                                                 Handles.SphereCap);
#endif
                                            if (EditorGUI.EndChangeCheck()) {
                                                changedAnything = true;
                                                att.topRotation = amountX;
                                            }
                                            break;
                                    }
#endregion //CUBE_PROJECTION_ROTATE_TOOL
                                    break;
                            }
#endregion // RENDER_HANDLES_FOR_CUBE_UNWRAPPING
                            break;
                        case UnwrapType.FaceDependent:
                            if (TextureTilingEditorWindow.currentSelectedFaceIndex >= 0 && TextureTilingEditorWindow.currentSelectedFaceIndex < TextureTilingEditorWindow.selected.faceUnwrapData.Length) {
#region CALC_HANDLE_POSITION_FACE_UNWRAPPING
                                FaceData selectedFace = TextureTilingEditorWindow.selected.faceUnwrapData[TextureTilingEditorWindow.currentSelectedFaceIndex];
                                Vector3 normal = new Vector3(selectedFace.AverageNormal.x / TextureTilingEditorWindow.selected.transform.lossyScale.x, selectedFace.AverageNormal.y / TextureTilingEditorWindow.selected.transform.lossyScale.y, selectedFace.AverageNormal.z / TextureTilingEditorWindow.selected.transform.lossyScale.z);
                                //Vector3 side = Vector3.Cross(Vector3.up, selectedFace.AverageNormal);
                                Quaternion topToNormal = Quaternion.FromToRotation(Vector3.up, normal);
                                //Quaternion toRight = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
                                Vector3 center = Vector3.zero;
                                for (int i = 0; i < selectedFace.Triangles.Length; i++) {
                                    center += TextureTilingEditorWindow.selected.meshFilter.sharedMesh.vertices[selectedFace.Triangles[i]];
                                }
                                center = TextureTilingEditorWindow.selected.transform.position + (TextureTilingEditorWindow.selected.transform.rotation * Vector3.Scale((center / selectedFace.Triangles.Length), TextureTilingEditorWindow.selected.transform.lossyScale));
                                sizeModifier = HandleUtility.GetHandleSize(center);
                                Quaternion completeSideRotation = TextureTilingEditorWindow.selected.transform.rotation * topToNormal;
                                switch (TextureTilingEditorWindow.currentTextureTool) {
                                    case Tool.Move:
                                        EditorGUI.BeginChangeCheck();
                                        Handles.color = Color.red;
                                        amountX =
                                            ATTHandles.TexturePositionHandle(center, completeSideRotation * Quaternion.AngleAxis(270f + selectedFace.rotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                        Handles.color = Color.blue;
                                        amountY =
                                            ATTHandles.TexturePositionHandle(center, completeSideRotation * Quaternion.AngleAxis(180f + selectedFace.rotation, Vector3.up), sizeModifier,
#if UNITY_5_6_OR_NEWER
                                                Handles.ArrowHandleCap
#else
                                                Handles.ArrowCap
#endif
                                                );
                                        if (EditorGUI.EndChangeCheck()) {
                                            changedAnything = true;
                                            if (TextureTilingEditorWindow.selected.useUnifiedOffset) {
                                                FaceData faceZero = TextureTilingEditorWindow.selected.faceUnwrapData[0];
                                                TextureTilingEditorWindow.selected.ApplyFaceOffset(TextureTilingEditorWindow.currentSelectedFaceIndex, new Vector2(faceZero.uvOffset.x + amountX, faceZero.uvOffset.y + amountY));
                                            }
                                            else {
                                                TextureTilingEditorWindow.selected.ApplyFaceOffset(TextureTilingEditorWindow.currentSelectedFaceIndex, new Vector2(selectedFace.uvOffset.x + amountX, selectedFace.uvOffset.y + amountY));
                                            }
                                        }
                                        Handles.color = Color.blue;
                                        if (ATTHandles.TextureAlignHandle(center, completeSideRotation, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            )) { 
                                            TextureTilingEditorWindow.selected.AlignOffsetCenter(TextureTilingEditorWindow.currentSelectedFaceIndex);
                                        }
                                        Rect faceBounds = TextureTilingEditorWindow.selected.GetFaceBounds(selectedFace);
                                        Handles.color = Color.white;
                                        if (ATTHandles.TextureAlignHandle(center + (completeSideRotation * Vector3.back * (faceBounds.height / 2f)), completeSideRotation, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            )) {
                                            TextureTilingEditorWindow.selected.AlignOffsetTop(TextureTilingEditorWindow.currentSelectedFaceIndex);
                                        }
                                        if (ATTHandles.TextureAlignHandle(center + (completeSideRotation * Vector3.forward * (faceBounds.height / 2f)), completeSideRotation, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            )) {
                                            TextureTilingEditorWindow.selected.AlignOffsetBottom(TextureTilingEditorWindow.currentSelectedFaceIndex);
                                        }
                                        if (ATTHandles.TextureAlignHandle(center + (completeSideRotation * Vector3.left * (faceBounds.width / 2f)), completeSideRotation, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            )) {
                                            TextureTilingEditorWindow.selected.AlignOffsetLeft(TextureTilingEditorWindow.currentSelectedFaceIndex);
                                        }
                                        if (ATTHandles.TextureAlignHandle(center + (completeSideRotation * Vector3.right * (faceBounds.width / 2f)), completeSideRotation, sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            )) {
                                            TextureTilingEditorWindow.selected.AlignOffsetRight(TextureTilingEditorWindow.currentSelectedFaceIndex);
                                        }
                                        break;
                                    case Tool.Rotate:
                                        EditorGUI.BeginChangeCheck();
                                        Handles.color = Color.red;
                                        amountX =
                                            ATTHandles.TextureRotationHandle(center,
                                                                             completeSideRotation * Quaternion.AngleAxis(270f + selectedFace.rotation, Vector3.up),
                                                                             TextureTilingEditorWindow.selected.transform.rotation * selectedFace.AverageNormal,
                                                                             completeSideRotation * Quaternion.AngleAxis(90f + selectedFace.rotation, Vector3.up) * Vector3.back,
                                                                             selectedFace.rotation,
                                                                             sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                                                             Handles.SphereHandleCap);
#else
                                                                             Handles.SphereCap);
#endif
                                        ////if (amountX != 0f) Debug.Log("Rotation amount: " + amountX);
                                        if (EditorGUI.EndChangeCheck()) {
                                            changedAnything = true;
                                            TextureTilingEditorWindow.selected.ApplyFaceRotation(TextureTilingEditorWindow.currentSelectedFaceIndex, selectedFace.rotation - amountX);
                                        }
                                        break;
                                    case Tool.Scale:
                                        EditorGUI.BeginChangeCheck();
                                        Handles.color = Color.white;
                                        scaleAmount =
                                            ATTHandles.TextureScaleHandle(center, completeSideRotation * Quaternion.AngleAxis(225f + selectedFace.rotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            );
                                        Handles.color = Color.red;
                                        amountX =
                                            ATTHandles.TextureScaleHandle(center, completeSideRotation * Quaternion.AngleAxis(270f + selectedFace.rotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            );
                                        Handles.color = Color.blue;
                                        amountY =
                                            ATTHandles.TextureScaleHandle(center, completeSideRotation * Quaternion.AngleAxis(180f + selectedFace.rotation, Vector3.up), sizeModifier * 0.1f,
#if UNITY_5_6_OR_NEWER
                                            Handles.CubeHandleCap
#else
                                            Handles.CubeCap
#endif
                                            );
                                        if (EditorGUI.EndChangeCheck()) {
                                            changedAnything = true;
                                            if (scaleAmount != 0f) {
                                                float factor = (selectedFace.uvScale.x + scaleAmount) / selectedFace.uvScale.x;
                                                amountX = scaleAmount;
                                                amountY = (selectedFace.uvScale.y * factor) - selectedFace.uvScale.y;
                                            }
                                            if (att.useUnifiedScaling) {
                                                FaceData faceZero = TextureTilingEditorWindow.selected.faceUnwrapData[0];
                                                TextureTilingEditorWindow.selected.ApplyFaceScale(TextureTilingEditorWindow.currentSelectedFaceIndex, new Vector2(faceZero.uvScale.x + amountX, faceZero.uvScale.y + amountY));
                                            }
                                            else {
                                                TextureTilingEditorWindow.selected.ApplyFaceScale(TextureTilingEditorWindow.currentSelectedFaceIndex, new Vector2(selectedFace.uvScale.x + amountX, selectedFace.uvScale.y + amountY));
                                            }
                                        }
                                        break;
                                }
#endregion
                            }
                            //Debug.Log("TODO: Implement face dependend handles");
                            break;
                    }

                    if (changedAnything) {
#if UNITY_5_3_OR_NEWER
                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#else
                        EditorApplication.MarkSceneDirty();
#endif
                        if (att.useBakedMesh) {
                            GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(att.gameObject) as GameObject;
                            if (prefab) {
                                PrefabUtility.SaveAsPrefabAssetAndConnect(att.gameObject, AssetDatabase.GetAssetPath(prefab), InteractionMode.AutomatedAction);
                            }
                        }
                    }
                }
            }

        }

        protected virtual void DrawFaceUnwrapTypeSelection(AutoTextureTiling Target) {

            UnwrapType oldUnwrapType = Target.unwrapMethod;
            EditorGUI.BeginChangeCheck();
            UnwrapType unwrapType = (UnwrapType)EditorGUILayout.Popup("Unwrap Method", (int)oldUnwrapType, Enum.GetNames(typeof(UnwrapType)));
            if (EditorGUI.EndChangeCheck() && oldUnwrapType != unwrapType) {
                Target.unwrapMethod = unwrapType;
            }

        }

        protected virtual void DrawMaterialSelector(AutoTextureTiling Target, FaceData faceData, string[] options, int index, bool changedAnythingStartValue, out bool changedAnything) {

            EditorGUI.BeginChangeCheck();
            changedAnything = changedAnythingStartValue;
            int faceMaterialIndex = EditorGUILayout.Popup("Material", faceData.materialIndex, options);
            if (faceMaterialIndex >= Target.Renderer.sharedMaterials.Length) {
                faceMaterialIndex = 0;
            }
            if (EditorGUI.EndChangeCheck() || faceMaterialIndex != faceData.materialIndex) {
                Target.ApplyFaceMaterial(index, faceMaterialIndex);
                EditorUtility.SetDirty(target);
                changedAnything = true;
            }

        }

        protected virtual void DrawNormalToleranceField(AutoTextureTiling Target) {

            EditorGUI.BeginChangeCheck();
            float newNormalTolerance = EditorGUILayout.FloatField("Normal Tolerance", Target.faceUnwrappingNormalTolerance);
            if (EditorGUI.EndChangeCheck() && newNormalTolerance != Target.faceUnwrappingNormalTolerance) {
                Target.faceUnwrappingNormalTolerance = newNormalTolerance;
                Target.CreateMeshAndUVs();
            }

        }

        //private void SetSelectedTrianglesFor(AutoTextureTiling att) {
        //    switch (att.unwrapMethod) {
        //        case UnwrapType.CubeProjection:
        //            Vector3 currentNormalSum = Vector3.zero;
        //            int normalCount = 0;
        //            for (int i = 0; i < att.meshFilter.sharedMesh.triangles.Length; i += 3) {
        //                Vector3 normals = Vector3.zero;
        //                Vector3[] currentTriangle = new Vector3[3];
        //                for (int j = 0; j < 3; j++) {
        //                    int vertexIndex = att.meshFilter.sharedMesh.triangles[i + j];
        //                    Vector3 normal = att.meshFilter.sharedMesh.normals[vertexIndex];
        //                    normals += normal;
        //                    normal = new Vector3(normal.x / att.transform.lossyScale.x, normal.y / att.transform.lossyScale.y, normal.z / att.transform.lossyScale.z);
        //                    currentTriangle[j] = att.meshFilter.sharedMesh.vertices[vertexIndex] + (normal * 0.005f);
        //                }
        //                if (AutoTextureTiling.GetCubeProjectionDirectionForNormal(normals / 3f) == TextureTilingEditorWindow.currentSelectedFace) {
        //                    normalCount += 3;
        //                    currentNormalSum += normals;
        //                    TextureTilingEditorWindow.currentSelectedTriangles.AddRange(currentTriangle);
        //                }
        //            }
        //            TextureTilingEditorWindow.currentSelectedTriangleNormal = currentNormalSum / normalCount;
        //            //Debug.Log("Set triangles for side " + TextureTilingEditorWindow.currentSelectedFace);
        //            break;
        //        case UnwrapType.FaceDependent:
        //            if (TextureTilingEditorWindow.currentSelectedFaceIndex < 0 || TextureTilingEditorWindow.currentSelectedFaceIndex >= att.faceUnwrapData.Length) {
        //                Debug.LogWarning(GetType() + ".SetSelectedTrianglesFor: TextureTilingEditorWindow.currentSelectedFaceIndex out of bounds: " + TextureTilingEditorWindow.currentSelectedFaceIndex);
        //                return;
        //            }
        //            Vector3[] triangleArray = new Vector3[att.faceUnwrapData[TextureTilingEditorWindow.currentSelectedFaceIndex].Triangles.Length];
        //            for (int t = 0; t < att.faceUnwrapData[TextureTilingEditorWindow.currentSelectedFaceIndex].Triangles.Length; t++) {
        //                int vertexIndex = att.faceUnwrapData[TextureTilingEditorWindow.currentSelectedFaceIndex].Triangles[t];
        //                Vector3 normal = att.meshFilter.sharedMesh.normals[vertexIndex];
        //                normal = new Vector3(normal.x / att.transform.lossyScale.x, normal.y / att.transform.lossyScale.y, normal.z / att.transform.lossyScale.z);
        //                triangleArray[t] = att.meshFilter.sharedMesh.vertices[vertexIndex] + (normal * 0.001f);
        //            }
        //            TextureTilingEditorWindow.currentSelectedTriangles = new List<Vector3>(triangleArray);
        //            TextureTilingEditorWindow.currentSelectedTriangleNormal = att.faceUnwrapData[TextureTilingEditorWindow.currentSelectedFaceIndex].AverageNormal;
        //            //Debug.Log("Set triangles for face " + TextureTilingEditorWindow.currentSelectedFaceIndex);
        //            break;
        //    }
        //}

    }

}
