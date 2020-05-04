using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif
#endif

namespace AutoTiling {

    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    /// <summary>
    /// Auto texture tiling.
    /// --------------------
    /// The base class of the Auto Texture Tiling Tool.
    /// Just add this component to any GameObject with a Mesh (no Skinned Meshes yet, sorry), and it will keep the UV scaling relative to the object scaling.
    /// 
    /// This will NOT update the UV scaling while the game is running! Use DynamicTextureTiling instead.
    /// </summary>
    public class AutoTextureTiling : MonoBehaviour {

        public float faceUnwrappingNormalTolerance = 30f;

        public bool useUnifiedScaling = false;

        [SerializeField]
        private Vector2 _topScale = Vector2.one;
        [SerializeField]
        private Vector2 _bottomScale = Vector2.one;
        [SerializeField]
        private Vector2 _leftScale = Vector2.one;
        [SerializeField]
        private Vector2 _rightScale = Vector2.one;
        [SerializeField]
        private Vector2 _frontScale = Vector2.one;
        [SerializeField]
        private Vector2 _backScale = Vector2.one;

        public bool useUnifiedOffset = false;
        [SerializeField]
        private Vector2 _topOffset = Vector2.zero;
        [SerializeField]
        private Vector2 _bottomOffset = Vector2.zero;
        [SerializeField]
        private Vector2 _leftOffset = Vector2.zero;
        [SerializeField]
        private Vector2 _rightOffset = Vector2.zero;
        [SerializeField]
        private Vector2 _frontOffset = Vector2.zero;
        [SerializeField]
        private Vector2 _backOffset = Vector2.zero;

        [SerializeField]
        private float _topRotation = 0f;
        [SerializeField]
        private float _bottomRotation = 0f;
        [SerializeField]
        private float _leftRotation = 0f;
        [SerializeField]
        private float _rightRotation = 0f;
        [SerializeField]
        private float _frontRotation = 0f;
        [SerializeField]
        private float _backRotation = 0f;

        [SerializeField]
        private int _topMaterialIndex = 0;
        [SerializeField]
        private int _bottomMaterialIndex = 0;
        [SerializeField]
        private int _leftMaterialIndex = 0;
        [SerializeField]
        private int _rightMaterialIndex = 0;
        [SerializeField]
        private int _frontMaterialIndex = 0;
        [SerializeField]
        private int _backMaterialIndex = 0;

        [SerializeField]
        private bool _topFlipX = false;
        [SerializeField]
        private bool _topFlipY = false;
        [SerializeField]
        private bool _bottomFlipX = false;
        [SerializeField]
        private bool _bottomFlipY = false;
        [SerializeField]
        private bool _leftFlipX = false;
        [SerializeField]
        private bool _leftFlipY = false;
        [SerializeField]
        private bool _rightFlipX = false;
        [SerializeField]
        private bool _rightFlipY = false;
        [SerializeField]
        private bool _frontFlipX = false;
        [SerializeField]
        private bool _frontFlipY = false;
        [SerializeField]
        private bool _backFlipX = false;
        [SerializeField]
        private bool _backFlipY = false;

        [SerializeField]
        private bool _useBakedMesh;

        [SerializeField]
        protected FaceData[] _faceUnwrapData;

        [SerializeField]
        private UnwrapType _unwrapMethod = UnwrapType.FaceDependent;

        [SerializeField]
        private bool freshMesh = true;

        protected float scaleX;
        protected float scaleY;
        protected float scaleZ;

        private Mesh bakedSharedMeshAsset;
        private MeshFilter _meshFilter;
        private MeshRenderer meshRenderer;

        private static string extensionString = ".asset";
        private static string meshAssetPathString = "Assets/AutoTextureTilingTool/Meshes/";


        public MeshRenderer Renderer {

            get {
                if (!meshRenderer) {
                    meshRenderer = GetComponent<MeshRenderer>();
                }
                if (!meshRenderer) {
                    Debug.LogError(name + ": " + GetType() + ".Renderer_get: there was no MeshRenderer component attached.");
                }
                return meshRenderer;
            }

        }

        public MeshFilter meshFilter {

            get {
                if (!_meshFilter) {
                    _meshFilter = GetComponent<MeshFilter>();
                }
                if (!_meshFilter) {
                    Debug.LogError(name + ": " + GetType() + ".meshFilter_get: there was no MeshFilter component attached.");
                }
                return _meshFilter;
            }

        }

        public FaceData[] faceUnwrapData {
            get {
                return _faceUnwrapData;
            }
        }

        public UnwrapType unwrapMethod {
            get {
                return _unwrapMethod;
            }
            set {
                _unwrapMethod = value;
                CreateMeshAndUVs();
            }
        }

        #region CUBE_PROJECTION_PROPERTIES
        public Vector2 topScale {

            get {
                return _topScale;
            }

            set {
                _topScale = value;
                CreateMeshAndUVs();
            }

        }
        public Vector2 bottomScale {

            get {
                return _bottomScale;
            }

            set {
                _bottomScale = value;
                if (useUnifiedScaling) {
                    _topScale = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 leftScale {

            get {
                return _leftScale;
            }

            set {
                _leftScale = value;
                if (useUnifiedScaling) {
                    _topScale = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 rightScale {

            get {
                return _rightScale;
            }

            set {
                _rightScale = value;
                if (useUnifiedScaling) {
                    _topScale = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 frontScale {

            get {
                return _frontScale;
            }

            set {
                _frontScale = value;
                if (useUnifiedScaling) {
                    _topScale = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 backScale {

            get {
                return _backScale;
            }

            set {
                _backScale = value;
                if (useUnifiedScaling) {
                    _topScale = value;
                }
                CreateMeshAndUVs();
            }

        }

        public Vector2 topOffset {

            get {
                return _topOffset;
            }

            set {
                _topOffset = value;
                CreateMeshAndUVs();
            }

        }
        public Vector2 bottomOffset {

            get {
                return _bottomOffset;
            }

            set {
                _bottomOffset = value;
                if (useUnifiedOffset) {
                    _topOffset = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 leftOffset {

            get {
                return _leftOffset;
            }

            set {
                _leftOffset = value;
                if (useUnifiedOffset) {
                    _topOffset = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 rightOffset {

            get {
                return _rightOffset;
            }

            set {
                _rightOffset = value;
                if (useUnifiedOffset) {
                    _topOffset = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 frontOffset {

            get {
                return _frontOffset;
            }

            set {
                _frontOffset = value;
                if (useUnifiedOffset) {
                    _topOffset = value;
                }
                CreateMeshAndUVs();
            }

        }
        public Vector2 backOffset {

            get {
                return _backOffset;
            }

            set {
                _backOffset = value;
                if (useUnifiedOffset) {
                    _topOffset = value;
                }
                CreateMeshAndUVs();
            }

        }

        public float topRotation {
            get {
                return _topRotation;
            }
            set {
                _topRotation = value;
                CreateMeshAndUVs();
            }
        }
        public float bottomRotation {
            get {
                return _bottomRotation;
            }
            set {
                _bottomRotation = value;
                CreateMeshAndUVs();
            }
        }
        public float leftRotation {
            get {
                return _leftRotation;
            }
            set {
                _leftRotation = value;
                CreateMeshAndUVs();
            }
        }
        public float rightRotation {
            get {
                return _rightRotation;
            }
            set {
                _rightRotation = value;
                CreateMeshAndUVs();
            }
        }
        public float frontRotation {
            get {
                return _frontRotation;
            }
            set {
                _frontRotation = value;
                CreateMeshAndUVs();
            }
        }
        public float backRotation {
            get {
                return _backRotation;
            }
            set {
                _backRotation = value;
                CreateMeshAndUVs();
            }
        }

        public int topMaterialIndex {
            get {
                return _topMaterialIndex;
            }
            set {
                _topMaterialIndex = value;
                CreateMeshAndUVs();
            }
        }
        public int bottomMaterialIndex {
            get {
                return _bottomMaterialIndex;
            }
            set {
                _bottomMaterialIndex = value;
                CreateMeshAndUVs();
            }
        }
        public int leftMaterialIndex {
            get {
                return _leftMaterialIndex;
            }
            set {
                _leftMaterialIndex = value;
                CreateMeshAndUVs();
            }
        }

        public int rightMaterialIndex {
            get {
                return _rightMaterialIndex;
            }
            set {
                _rightMaterialIndex = value;
                CreateMeshAndUVs();
            }
        }
        public int frontMaterialIndex {
            get {
                return _frontMaterialIndex;
            }
            set {
                _frontMaterialIndex = value;
                CreateMeshAndUVs();
            }
        }
        public int backMaterialIndex {
            get {
                return _backMaterialIndex;
            }
            set {
                _backMaterialIndex = value;
                CreateMeshAndUVs();
            }
        }

        public bool topFlipX {
            get {
                return _topFlipX;
            }
            set {
                _topFlipX = value;
                CreateMeshAndUVs();
            }
        }
        public bool topFlipY {
            get {
                return _topFlipY;
            }
            set {
                _topFlipY = value;
                CreateMeshAndUVs();
            }
        }
        public bool bottomFlipX {
            get {
                return _bottomFlipX;
            }
            set {
                _bottomFlipX = value;
                CreateMeshAndUVs();
            }
        }
        public bool bottomFlipY {
            get {
                return _bottomFlipY;
            }
            set {
                _bottomFlipY = value;
                CreateMeshAndUVs();
            }
        }
        public bool leftFlipX {
            get {
                return _leftFlipX;
            }
            set {
                _leftFlipX = value;
                CreateMeshAndUVs();
            }
        }
        public bool leftFlipY {
            get {
                return _leftFlipY;
            }
            set {
                _leftFlipY = value;
                CreateMeshAndUVs();
            }
        }
        public bool rightFlipX {
            get {
                return _rightFlipX;
            }
            set {
                _rightFlipX = value;
                CreateMeshAndUVs();
            }
        }
        public bool rightFlipY {
            get {
                return _rightFlipY;
            }
            set {
                _rightFlipY = value;
                CreateMeshAndUVs();
            }
        }
        public bool frontFlipX {
            get {
                return _frontFlipX;
            }
            set {
                _frontFlipX = value;
                CreateMeshAndUVs();
            }
        }
        public bool frontFlipY {
            get {
                return _frontFlipY;
            }
            set {
                _frontFlipY = value;
                CreateMeshAndUVs();
            }
        }
        public bool backFlipX {
            get {
                return _backFlipX;
            }
            set {
                _backFlipX = value;
                CreateMeshAndUVs();
            }
        }
        public bool backFlipY {
            get {
                return _backFlipY;
            }
            set {
                _backFlipY = value;
                CreateMeshAndUVs();
            }
        }
        #endregion // CUBE_PROJECTION_PROPERTIES

        public bool useBakedMesh {
            get {
                return _useBakedMesh;
            }
            set {
                _useBakedMesh = value;
            }
        }

        public virtual void Awake() {

            _meshFilter = GetComponent<MeshFilter>();
            if (!_meshFilter) {
                Debug.LogError(name + ": " + GetType() + ".Awake: there was no MeshFilter component attached.");
            }
            meshRenderer = GetComponent<MeshRenderer>();
            if (!meshRenderer) {
                Debug.LogError(name + ": " + GetType() + ".Awake: there was no MeshRenderer component attached.");
            }
            scaleX = transform.lossyScale.x;
            scaleY = transform.lossyScale.y;
            scaleZ = transform.lossyScale.z;
#if UNITY_EDITOR
            if (Application.isPlaying && gameObject.isStatic) {
                DestroyImmediate(this);
                return;
            }
            else {
                if (meshFilter.sharedMesh != null) {
                    if (!_useBakedMesh || !MeshPrefabExists()) {
                        Mesh meshCopy = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh;
                        meshCopy.name = meshFilter.sharedMesh.name;
                        meshFilter.sharedMesh = meshCopy;
                    }
                }
                else {
                    _useBakedMesh = false;
                    Debug.Log(name + ": " + GetType() + ".CreateMeshAndUVs: there was no mesh, adding a default cube mesh.");
                    meshFilter.sharedMesh = new Mesh();
                    EditorUtility.SetDirty(this);
                    if (!Application.isPlaying) {
#if UNITY_5_3_OR_NEWER
                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#else
                        EditorApplication.MarkSceneDirty();
#endif
                    }
                }
            }
#endif
            CreateMeshAndUVs();

        }

        // Updated only in the Editor, only if not in play mode. For Dynamic tiling at runtime use the DynamicTextureTiling component.
#if UNITY_EDITOR
        void Update() {

            if (!Application.isPlaying) {
                if (scaleX != transform.lossyScale.x || scaleY != transform.lossyScale.y || scaleZ != transform.lossyScale.z) {
                    scaleX = transform.lossyScale.x;
                    scaleY = transform.lossyScale.y;
                    scaleZ = transform.lossyScale.z;
                    CreateMeshAndUVs();
                }
                if (meshFilter.sharedMesh == null) {
                    _useBakedMesh = false;
                    Debug.Log(name + ": " + GetType() + ".Update: there was no mesh, adding a default cube mesh.");
                    meshFilter.sharedMesh = new Mesh();
                    EditorUtility.SetDirty(this);
#if UNITY_5_3_OR_NEWER
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#else
                    EditorApplication.MarkSceneDirty();
#endif
                    CreateMeshAndUVs();
                }

            }

        }
#endif
        public void AlignOffsetCenter(Direction side) {

            switch (side) {
                case Direction.Back:
                    backOffset = Vector2.zero;
                    break;
                case Direction.Down:
                    bottomOffset = Vector2.zero;
                    break;
                case Direction.Forward:
                    frontOffset = Vector2.zero;
                    break;
                case Direction.Left:
                    leftOffset = Vector2.zero;
                    break;
                case Direction.Right:
                    rightOffset = Vector2.zero;
                    break;
                case Direction.Up:
                    topOffset = Vector2.zero;
                    break;
            }

        }

        public void AlignOffsetCenter(int faceIndex) {

            ApplyFaceOffset(faceIndex, Vector2.zero);

        }

        public void AlignOffsetTop(Direction side) {
            switch (side) {
                case Direction.Back:
                    backOffset = new Vector2(backOffset.x, 1f - (((transform.lossyScale.y - backScale.y) / backScale.y) * .5f));
                    break;
                case Direction.Down:
                    bottomOffset = new Vector2(bottomOffset.x, 1f - (((transform.lossyScale.x - bottomScale.y) / bottomScale.y) * .5f));
                    break;
                case Direction.Forward:
                    frontOffset = new Vector2(frontOffset.x, 1f - (((transform.lossyScale.y - frontScale.y) / frontScale.y) * .5f));
                    break;
                case Direction.Left:
                    leftOffset = new Vector2(leftOffset.x, 1f - (((transform.lossyScale.y - leftScale.y) / leftScale.y) * .5f));
                    break;
                case Direction.Right:
                    rightOffset = new Vector2(rightOffset.x, 1f - (((transform.lossyScale.y - rightScale.y) / rightScale.y) * .5f));
                    break;
                case Direction.Up:
                    topOffset = new Vector2(topOffset.x, 1f - (((transform.lossyScale.x - topScale.y) / topScale.y) * .5f));
                    break;
            }
        }

        public void AlignOffsetTop(int faceIndex) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            FaceData face = _faceUnwrapData[faceIndex];
            Rect faceBounds = GetFaceBounds(face);
            face.uvOffset = new Vector2(face.uvOffset.x, faceBounds.yMax / face.uvScale.y);
            CreateMeshAndUVs();
        }

        public void AlignOffsetBottom(Direction side) {
            switch (side) {
                case Direction.Back:
                    backOffset = new Vector2(backOffset.x, ((transform.lossyScale.y - backScale.y) / backScale.y) * .5f);
                    break;
                case Direction.Down:
                    bottomOffset = new Vector2(bottomOffset.x, ((transform.lossyScale.x - bottomScale.y) / bottomScale.y) * .5f);
                    break;
                case Direction.Forward:
                    frontOffset = new Vector2(frontOffset.x, ((transform.lossyScale.y - frontScale.y) / frontScale.y) * .5f);
                    break;
                case Direction.Left:
                    leftOffset = new Vector2(leftOffset.x, ((transform.lossyScale.y - leftScale.y) / leftScale.y) * .5f);
                    break;
                case Direction.Right:
                    rightOffset = new Vector2(rightOffset.x, ((transform.lossyScale.y - rightScale.y) / rightScale.y) * .5f);
                    break;
                case Direction.Up:
                    topOffset = new Vector2(topOffset.x, ((transform.lossyScale.x - topScale.y) / topScale.y) * .5f);
                    break;
            }
        }

        public void AlignOffsetBottom(int faceIndex) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            FaceData face = _faceUnwrapData[faceIndex];
            Rect faceBounds = GetFaceBounds(face);
            face.uvOffset = new Vector2(face.uvOffset.x, faceBounds.y / face.uvScale.y);
            CreateMeshAndUVs();
        }

        public void AlignOffsetLeft(Direction side) {
            switch (side) {
                case Direction.Back:
                    backOffset = new Vector2(((transform.lossyScale.x - backScale.x) / backScale.x) * .5f, backOffset.y);
                    break;
                case Direction.Down:
                    bottomOffset = new Vector2(((1f - (transform.lossyScale.z - bottomScale.x) / bottomScale.x) * .5f), bottomOffset.y);
                    break;
                case Direction.Forward:
                    frontOffset = new Vector2(((transform.lossyScale.x - frontScale.x) / frontScale.x) * .5f, frontOffset.y);
                    break;
                case Direction.Left:
                    leftOffset = new Vector2(((transform.lossyScale.z - leftScale.x) / leftScale.x) * .5f, leftOffset.y);
                    break;
                case Direction.Right:
                    rightOffset = new Vector2(((transform.lossyScale.z - rightScale.x) / rightScale.x) * .5f, rightOffset.y);
                    break;
                case Direction.Up:
                    topOffset = new Vector2(((1f - (transform.lossyScale.z - topScale.x) / topScale.x) * .5f), topOffset.y);
                    break;
            }
        }

        public void AlignOffsetLeft(int faceIndex) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            FaceData face = _faceUnwrapData[faceIndex];
            Rect faceBounds = GetFaceBounds(face);
            face.uvOffset = new Vector2(faceBounds.xMax / face.uvScale.x, face.uvOffset.y);
            CreateMeshAndUVs();
        }

        public void AlignOffsetRight(Direction side) {
            switch (side) {
                case Direction.Back:
                    backOffset = new Vector2((1f - ((transform.lossyScale.x - backScale.x) / backScale.x) * .5f), backOffset.y);
                    break;
                case Direction.Down:
                    bottomOffset = new Vector2((((transform.lossyScale.z - bottomScale.x) / bottomScale.x) * .5f), bottomOffset.y);
                    break;
                case Direction.Forward:
                    frontOffset = new Vector2((1f - ((transform.lossyScale.x - frontScale.x) / frontScale.x) * .5f), frontOffset.y);
                    break;
                case Direction.Left:
                    leftOffset = new Vector2((1f - ((transform.lossyScale.z - leftScale.x) / leftScale.x) * .5f), leftOffset.y);
                    break;
                case Direction.Right:
                    rightOffset = new Vector2((1f - ((transform.lossyScale.z - rightScale.x) / rightScale.x) * .5f), rightOffset.y);
                    break;
                case Direction.Up:
                    topOffset = new Vector2((((transform.lossyScale.z - topScale.x) / topScale.x) * .5f), topOffset.y);
                    break;
            }
        }

        public void AlignOffsetRight(int faceIndex) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            FaceData face = _faceUnwrapData[faceIndex];
            Rect faceBounds = GetFaceBounds(face);
            face.uvOffset = new Vector2(faceBounds.x / face.uvScale.x, face.uvOffset.y);
            CreateMeshAndUVs();
        }

        public void SetTextureToFit(Direction side) {

            switch (side) {
                case Direction.Back:
                    backOffset = Vector2.zero;
                    backRotation = 0f;
                    backScale = new Vector2(transform.lossyScale.x, transform.lossyScale.y);
                    break;
                case Direction.Down:
                    bottomOffset = Vector2.zero;
                    bottomRotation = 0f;
                    bottomScale = new Vector2(transform.lossyScale.z, transform.lossyScale.x);
                    break;
                case Direction.Forward:
                    frontOffset = Vector2.zero;
                    frontRotation = 0f;
                    frontScale = new Vector2(transform.lossyScale.x, transform.lossyScale.y);
                    break;
                case Direction.Left:
                    leftOffset = Vector2.zero;
                    leftRotation = 0f;
                    leftScale = new Vector2(transform.lossyScale.z, transform.lossyScale.y);
                    break;
                case Direction.Right:
                    rightOffset = Vector2.zero;
                    rightRotation = 0f;
                    rightScale = new Vector2(transform.lossyScale.z, transform.lossyScale.y);
                    break;
                case Direction.Up:
                    topOffset = Vector2.zero;
                    topRotation = 0f;
                    topScale = new Vector2(transform.lossyScale.z, transform.lossyScale.x);
                    break;
            }

        }

        public void SetTextureToFit(int faceIndex) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".SetTextureToFit: faceIndex out of range: " + faceIndex);
                return;
            }
            FaceData face = _faceUnwrapData[faceIndex];
            Rect faceBounds = GetFaceBounds(face);
            face.rotation = 0f;
            face.uvScale = new Vector2(faceBounds.width, faceBounds.height);
            face.uvOffset = new Vector2(faceBounds.center.x / face.uvScale.x, faceBounds.center.y / face.uvScale.y);
            CreateMeshAndUVs();
        }

        public Rect GetFaceBounds(FaceData face) {
            if (face == null) {
                Debug.LogError(name + ": " + GetType() + ".GetFaceBounds: face was null.");
            }
            float minY = float.PositiveInfinity;
            float maxY = float.NegativeInfinity;
            float minX = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;
            Quaternion rotationToTop = Quaternion.FromToRotation(Vector3.Scale(face.AverageNormal, transform.lossyScale), Vector3.up);
            for (int t = 0; t < face.Triangles.Length; t++) {
                Vector3 vector = meshFilter.sharedMesh.vertices[face.Triangles[t]];
                Vector3 rotatedVector = rotationToTop * new Vector3(vector.x * transform.lossyScale.x, vector.y * transform.lossyScale.y, vector.z * transform.lossyScale.z);
                if (rotatedVector.x < minX) {
                    minX = rotatedVector.x;
                }
                else if (rotatedVector.x > maxX) {
                    maxX = rotatedVector.x;
                }
                if (rotatedVector.z < minY) {
                    minY = rotatedVector.z;
                }
                else if (rotatedVector.z > maxY) {
                    maxY = rotatedVector.z;
                }
            }
            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        public void ApplyFlipUVX(int faceIndex, bool newFlipX) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            _faceUnwrapData[faceIndex].flipUVx = newFlipX;
            CreateMeshAndUVs();
        }

        public void ApplyFlipUVY(int faceIndex, bool newFlipY) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            _faceUnwrapData[faceIndex].flipUVy = newFlipY;
            CreateMeshAndUVs();
        }

        public void ApplyFaceMaterial(int faceIndex, int faceMaterialIndex) {
            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            _faceUnwrapData[faceIndex].materialIndex = faceMaterialIndex;
            CreateMeshAndUVs();
        }

        public void ApplyFaceOffset(int faceIndex, Vector2 offset) {

            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }

            _faceUnwrapData[useUnifiedOffset ? 0 : faceIndex].uvOffset = offset;
            CreateMeshAndUVs();

        }

        public void ApplyFaceRotation(int faceIndex, float rotation) {

            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }
            _faceUnwrapData[faceIndex].rotation = rotation;
            CreateMeshAndUVs();

        }

        public void ApplyFaceScale(int faceIndex, Vector2 scale) {

            if (faceIndex < 0 || faceIndex >= _faceUnwrapData.Length) {
                Debug.LogError(name + ": " + GetType() + ".ApplyFaceScale: faceIndex out of range: " + faceIndex);
                return;
            }

            _faceUnwrapData[useUnifiedScaling ? 0 : faceIndex].uvScale = scale;
            CreateMeshAndUVs();

        }

        public void CreateMeshAndUVs() {

            if (meshFilter == null) {
                Debug.LogError(GetType() + ".CreateMeshAndUVs: meshFilter was not set, there is no MeshFilter component attached.");
                return;
            }

#if UNITY_EDITOR
            if (!meshFilter.sharedMesh)
            {
                Debug.LogError(GetType() + ".CreateMeshAndUVs: shared mesh was null. This should not happen.");
                return;
            }
            if (!meshFilter.sharedMesh.isReadable) {
                Debug.LogError(GetType() + ".CreateMeshAndUVs: could not edit mesh. Please make sure that 'Read/Write Enabled' is checked in the import settings.");
                return;
            }
#endif
            MeshData meshData = new MeshData();
#if UNITY_EDITOR
            Mesh meshToEdit;
            if (_useBakedMesh)
            {
                if (!bakedSharedMeshAsset)
                {
                    bakedSharedMeshAsset = AssetDatabase.LoadAssetAtPath(GetMeshAssetPath() + meshFilter.sharedMesh.name + extensionString, typeof(Mesh)) as Mesh;
                }
                meshToEdit = bakedSharedMeshAsset;
            }
            else
            {
                meshToEdit = Instantiate(meshFilter.sharedMesh);
            }
            meshToEdit.name = meshFilter.sharedMesh.name;
            if (gameObject.isStatic && this as BasicTextureTiling == null)
            {
                Unwrapping.GenerateSecondaryUVSet(meshToEdit);
            }
#else
            Mesh meshToEdit = meshFilter.mesh;
            if (meshToEdit == null) 
            {
                Debug.LogWarning(GetType() + ".CreateMeshAndUVs: mesh was null. Automatically created a new one. Was this intended?");
                meshToEdit = new Mesh();
            }
#endif
            if (meshToEdit.vertices.Length < 1) {
                meshData = CreateStandardCubeMesh();
                meshToEdit.subMeshCount = meshData.subMeshCount;
                meshToEdit.vertices = meshData.Vertices.ToArray();
                for (int i = 0; i < meshData.subMeshCount; i++) {
                    meshToEdit.SetTriangles(meshData.Triangles[i].ToArray(), i);
                }
                meshToEdit.uv = meshData.UV.ToArray();
                meshToEdit.RecalculateBounds();
                meshToEdit.RecalculateNormals();
#if UNITY_EDITOR
                EditorUtility.SetDirty(meshFilter);
                EditorUtility.SetDirty(this);
#endif
            }
            else {
                Vector3[] oldVertices = meshToEdit.vertices;
                Vector3[] oldNormals = meshToEdit.normals;
                if (oldVertices.Length < 3) {
                    Debug.LogError(name + ": " + GetType() + ".CreateMeshAndUVs: there was something wrong with the mesh, not enough vertices: " + oldVertices.Length + ".");
                    return;
                }
                meshData.SetVertices(oldVertices);
                if (oldNormals.Length != oldVertices.Length) {
                    Debug.LogError(name + ": " + GetType() + ".CreateMeshAndUVs: there was something wrong with the mesh, there were " + oldNormals.Length + " normals, but " + oldVertices.Length + " vertices. They need to have the same count.");
                    return;
                }
                meshData.SetNormals(oldNormals);
                meshData.SetTriangles(meshToEdit);
                meshData.SetTangents(meshToEdit.tangents);
                meshData.SetUV2Coordinates(meshToEdit.uv2);
                switch (_unwrapMethod) {
                    case UnwrapType.CubeProjection:
                        meshData = SplitMeshForCubeProjection(meshData);
                        break;
                    case UnwrapType.FaceDependent:
                        meshData = SplitMeshForFaceUnwrapping(meshData);
                        break;
                    default:
                        meshData = SplitMeshForFaceUnwrapping(meshData);
                        break;
                }
                // Copy modified meshdata to the object's mesh
                meshToEdit.subMeshCount = meshData.subMeshCount;
                if (meshData.Vertices.Count < meshToEdit.vertices.Length) {
                    for (int i = 0; i < meshData.subMeshCount; i++) {
                        if (meshData.Triangles[i].Count > 0 && meshData.Triangles[i].Count % 3 != 0) {
                            Debug.LogError(name + ": " + GetType() + ".CreateMeshAndUVs: there was something wrong with the mesh, triangles not divisible by 3. Triangles Count for material index " + i + ": " + meshData.Triangles[i].Count);
                            return;
                        }
                        meshToEdit.SetTriangles(meshData.Triangles[i].ToArray(), i);
                    }
                    meshToEdit.vertices = meshData.Vertices.ToArray();
                }
                else {
                    meshToEdit.vertices = meshData.Vertices.ToArray();
                    for (int i = 0; i < meshData.subMeshCount; i++) {
                        if (meshData.Triangles[i] == null) {
                            Debug.LogError(name + ": " + GetType() + ".CreateMeshAndUVs: there was something wrong with the mesh, triangles at " + i + " were null.");
                            continue;
                        }
                        else if (meshData.Triangles[i].Count > 0 && meshData.Triangles[i].Count % 3 != 0) {
                            Debug.LogError(name + ": " + GetType() + ".CreateMeshAndUVs: there was something wrong with the mesh, triangles not divisible by 3. Triangles Count for material index " + i + ": " + meshData.Triangles[i].Count);
                            continue;
                        }
                        meshToEdit.SetTriangles(meshData.Triangles[i].ToArray(), i);
                    }
                }
                meshToEdit.normals = meshData.Normals.ToArray();
                meshToEdit.tangents = meshData.Tangents.ToArray();
                meshToEdit.uv = meshData.UV.ToArray();
                meshToEdit.uv2 = meshData.UV2.ToArray();
            }
#if UNITY_EDITOR
            if (!_useBakedMesh) {
#endif
                meshToEdit.name = "Mesh " + name;
#if UNITY_EDITOR
            }
#endif
#if UNITY_EDITOR
            if (_useBakedMesh)
            {
                EditorUtility.SetDirty(meshToEdit);
            }
            else {
                meshFilter.sharedMesh = meshToEdit;
            }
#else
            meshFilter.mesh = meshToEdit;
#endif
            if (freshMesh) {
                freshMesh = false;
            }
        }

        private MeshData CreateStandardCubeMesh() {

            MeshData meshData = new MeshData();

            meshData.AddVertex(new Vector3(-0.5f, 0.5f, 0.5f));
            meshData.AddVertex(new Vector3(0.5f, 0.5f, 0.5f));
            meshData.AddVertex(new Vector3(0.5f, 0.5f, -0.5f));
            meshData.AddVertex(new Vector3(-0.5f, 0.5f, -0.5f));

            meshData.AddQuadTriangles();
            meshData.AddUVCoordinates(QuadFaceUVs(Direction.Up));

            meshData.AddVertex(new Vector3(-0.5f, -0.5f, -0.5f));
            meshData.AddVertex(new Vector3(0.5f, -0.5f, -0.5f));
            meshData.AddVertex(new Vector3(0.5f, -0.5f, 0.5f));
            meshData.AddVertex(new Vector3(-0.5f, -0.5f, 0.5f));

            meshData.AddQuadTriangles();
            meshData.AddUVCoordinates(QuadFaceUVs(Direction.Down));

            meshData.AddVertex(new Vector3(0.5f, -0.5f, 0.5f));
            meshData.AddVertex(new Vector3(0.5f, 0.5f, 0.5f));
            meshData.AddVertex(new Vector3(-0.5f, 0.5f, 0.5f));
            meshData.AddVertex(new Vector3(-0.5f, -0.5f, 0.5f));

            meshData.AddQuadTriangles();
            meshData.AddUVCoordinates(QuadFaceUVs(Direction.Forward));

            meshData.AddVertex(new Vector3(-0.5f, -0.5f, -0.5f));
            meshData.AddVertex(new Vector3(-0.5f, 0.5f, -0.5f));
            meshData.AddVertex(new Vector3(0.5f, 0.5f, -0.5f));
            meshData.AddVertex(new Vector3(0.5f, -0.5f, -0.5f));

            meshData.AddQuadTriangles();
            meshData.AddUVCoordinates(QuadFaceUVs(Direction.Back));

            meshData.AddVertex(new Vector3(-0.5f, -0.5f, 0.5f));
            meshData.AddVertex(new Vector3(-0.5f, 0.5f, 0.5f));
            meshData.AddVertex(new Vector3(-0.5f, 0.5f, -0.5f));
            meshData.AddVertex(new Vector3(-0.5f, -0.5f, -0.5f));

            meshData.AddQuadTriangles();
            meshData.AddUVCoordinates(QuadFaceUVs(Direction.Left));

            meshData.AddVertex(new Vector3(0.5f, -0.5f, -0.5f));
            meshData.AddVertex(new Vector3(0.5f, 0.5f, -0.5f));
            meshData.AddVertex(new Vector3(0.5f, 0.5f, 0.5f));
            meshData.AddVertex(new Vector3(0.5f, -0.5f, 0.5f));

            meshData.AddQuadTriangles();
            meshData.AddUVCoordinates(QuadFaceUVs(Direction.Right));

            return meshData;
        }

        private Vector2[] QuadFaceUVs(Direction dir) {

            Vector2[] UVs = new Vector2[4];

            float x = 1f;
            float y = 1f;

            switch (dir) {
                case Direction.Up:
                    x = (transform.lossyScale.z / topScale.x);
                    y = (transform.lossyScale.x / topScale.y);
                    UVs[0] = new Vector2(x + topOffset.x, 0f + topOffset.y);
                    UVs[1] = new Vector2(x + topOffset.x, y + topOffset.y);
                    UVs[2] = new Vector2(0f + topOffset.x, y + topOffset.y);
                    UVs[3] = new Vector2(0f + topOffset.x, 0f + topOffset.y);
                    break;
                case Direction.Down:
                    x = (transform.lossyScale.z / (useUnifiedScaling ? topScale.x : bottomScale.x));
                    y = (transform.lossyScale.x / (useUnifiedScaling ? topScale.y : bottomScale.y));
                    UVs[0] = new Vector2(x + (useUnifiedOffset ? topOffset.x : bottomOffset.x), 0f + (useUnifiedOffset ? topOffset.y : bottomOffset.y));
                    UVs[1] = new Vector2(x + (useUnifiedOffset ? topOffset.x : bottomOffset.x), y + (useUnifiedOffset ? topOffset.y : bottomOffset.y));
                    UVs[2] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : bottomOffset.x), y + (useUnifiedOffset ? topOffset.y : bottomOffset.y));
                    UVs[3] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : bottomOffset.x), 0f + (useUnifiedOffset ? topOffset.y : bottomOffset.y));
                    break;
                case Direction.Left:
                    x = (transform.lossyScale.z / (useUnifiedScaling ? topScale.x : leftScale.x));
                    y = (transform.lossyScale.y / (useUnifiedScaling ? topScale.y : leftScale.y));
                    UVs[0] = new Vector2(x + (useUnifiedOffset ? topOffset.x : leftOffset.x), 0f + (useUnifiedOffset ? topOffset.y : leftOffset.y));
                    UVs[1] = new Vector2(x + (useUnifiedOffset ? topOffset.x : leftOffset.x), y + (useUnifiedOffset ? topOffset.y : leftOffset.y));
                    UVs[2] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : leftOffset.x), y + (useUnifiedOffset ? topOffset.y : leftOffset.y));
                    UVs[3] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : leftOffset.x), 0f + (useUnifiedOffset ? topOffset.y : leftOffset.y));
                    break;
                case Direction.Right:
                    x = (transform.lossyScale.z / (useUnifiedScaling ? topScale.x : rightScale.x));
                    y = (transform.lossyScale.y / (useUnifiedScaling ? topScale.y : rightScale.y));
                    UVs[0] = new Vector2(x + (useUnifiedOffset ? topOffset.x : rightOffset.x), 0f + (useUnifiedOffset ? topOffset.y : rightOffset.y));
                    UVs[1] = new Vector2(x + (useUnifiedOffset ? topOffset.x : rightOffset.x), y + (useUnifiedOffset ? topOffset.y : rightOffset.y));
                    UVs[2] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : rightOffset.x), y + (useUnifiedOffset ? topOffset.y : rightOffset.y));
                    UVs[3] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : rightOffset.x), 0f + (useUnifiedOffset ? topOffset.y : rightOffset.y));
                    break;
                case Direction.Forward:
                    x = (transform.lossyScale.x / (useUnifiedScaling ? topScale.x : frontScale.x));
                    y = (transform.lossyScale.y / (useUnifiedScaling ? topScale.y : frontScale.y));
                    UVs[0] = new Vector2(x + (useUnifiedOffset ? topOffset.x : frontOffset.x), 0f + (useUnifiedOffset ? topOffset.y : frontOffset.y));
                    UVs[1] = new Vector2(x + (useUnifiedOffset ? topOffset.x : frontOffset.x), y + (useUnifiedOffset ? topOffset.y : frontOffset.y));
                    UVs[2] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : frontOffset.x), y + (useUnifiedOffset ? topOffset.y : frontOffset.y));
                    UVs[3] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : frontOffset.x), 0f + (useUnifiedOffset ? topOffset.y : frontOffset.y));
                    break;
                case Direction.Back:
                    x = (transform.lossyScale.x / (useUnifiedScaling ? topScale.x : backScale.x));
                    y = (transform.lossyScale.y / (useUnifiedScaling ? topScale.y : backScale.y));
                    UVs[0] = new Vector2(x + (useUnifiedOffset ? topOffset.x : backOffset.x), 0f + (useUnifiedOffset ? topOffset.y : backOffset.y));
                    UVs[1] = new Vector2(x + (useUnifiedOffset ? topOffset.x : backOffset.x), y + (useUnifiedOffset ? topOffset.y : backOffset.y));
                    UVs[2] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : backOffset.x), y + (useUnifiedOffset ? topOffset.y : backOffset.y));
                    UVs[3] = new Vector2(0f + (useUnifiedOffset ? topOffset.x : backOffset.x), 0f + (useUnifiedOffset ? topOffset.y : backOffset.y));
                    break;
            }

            return UVs;

        }

        private MeshData SplitMeshForCubeProjection(MeshData data) {

            //			Debug.Log ("Old count vertices: " + data.Vertices.Count + ", old count UVs: " + data.UV.Count);
            List<int> topTriangles = new List<int>();
            List<int> bottomTriangles = new List<int>();
            List<int> leftTriangles = new List<int>();
            List<int> rightTriangles = new List<int>();
            List<int> frontTriangles = new List<int>();
            List<int> backTriangles = new List<int>();

            for (int m = 0; m < data.Triangles.Length; m++) {
                for (int i = 0; i < data.Triangles[m].Count; i += 3) {
                    //				Debug.Log ("Handling triangle entry: " + i);
                    Vector3 triangleNormal = new Vector3();
                    List<int> triangleVertIds = new List<int>();
                    for (int tvId = 0; tvId < 3; tvId++) {
                        int index = data.Triangles[m][i + tvId];
                        triangleNormal += data.Normals[index];
                        triangleVertIds.Add(index);
                    }
                    Direction triangleNormalDirection = GetCubeProjectionDirectionForNormal(triangleNormal.normalized);
                    switch (triangleNormalDirection) {
                        case Direction.Back:
                            backTriangles.AddRange(triangleVertIds);
                            break;
                        case Direction.Down:
                            bottomTriangles.AddRange(triangleVertIds);
                            break;
                        case Direction.Forward:
                            frontTriangles.AddRange(triangleVertIds);
                            break;
                        case Direction.Left:
                            leftTriangles.AddRange(triangleVertIds);
                            break;
                        case Direction.Right:
                            rightTriangles.AddRange(triangleVertIds);
                            break;
                        case Direction.Up:
                            topTriangles.AddRange(triangleVertIds);
                            break;
                    }
                }
            }

            MeshData newMeshData = new MeshData();
            if (freshMesh) {
                HashSet<Vector3> backTrianglesHashset = new HashSet<Vector3>();
                for (int i = 0; i < backTriangles.Count; i++) {
                    backTrianglesHashset.Add(data.Vertices[backTriangles[i]]);
                }
                HashSet<Vector3> bottomTrianglesHashset = new HashSet<Vector3>();
                for (int i = 0; i < bottomTriangles.Count; i++) {
                    bottomTrianglesHashset.Add(data.Vertices[bottomTriangles[i]]);
                }
                HashSet<Vector3> frontTrianglesHashset = new HashSet<Vector3>();
                for (int i = 0; i < frontTriangles.Count; i++) {
                    frontTrianglesHashset.Add(data.Vertices[frontTriangles[i]]);
                }
                HashSet<Vector3> leftTrianglesHashset = new HashSet<Vector3>();
                for (int i = 0; i < leftTriangles.Count; i++) {
                    leftTrianglesHashset.Add(data.Vertices[leftTriangles[i]]);
                }
                HashSet<Vector3> rightTrianglesHashset = new HashSet<Vector3>();
                for (int i = 0; i < rightTriangles.Count; i++) {
                    rightTrianglesHashset.Add(data.Vertices[rightTriangles[i]]);
                }
                HashSet<Vector3> topTrianglesHashset = new HashSet<Vector3>();
                for (int i = 0; i < topTriangles.Count; i++) {
                    topTrianglesHashset.Add(data.Vertices[topTriangles[i]]);
                }
                for (int i = 0; i < data.subMeshCount; i++) {
                    HashSet<Vector3> dataTrianglesHashset = new HashSet<Vector3>();
                    for (int j = 0; j < data.Triangles[i].Count; j++) {
                        dataTrianglesHashset.Add(data.Vertices[data.Triangles[i][j]]);
                    }
                    if (backTrianglesHashset.IsSubsetOf(dataTrianglesHashset)) {
                        _backMaterialIndex = i;
                    }
                    if (bottomTrianglesHashset.IsSubsetOf(dataTrianglesHashset)) {
                        _bottomMaterialIndex = i;
                    }
                    if (frontTrianglesHashset.IsSubsetOf(dataTrianglesHashset)) {
                        _frontMaterialIndex = i;
                    }
                    if (leftTrianglesHashset.IsSubsetOf(dataTrianglesHashset)) {
                        _leftMaterialIndex = i;
                    }
                    if (rightTrianglesHashset.IsSubsetOf(dataTrianglesHashset)) {
                        _rightMaterialIndex = i;
                    }
                    if (topTrianglesHashset.IsSubsetOf(dataTrianglesHashset)) {
                        _topMaterialIndex = i;
                    }
                }
                freshMesh = false;
            }
            newMeshData.subMeshCount = Mathf.Max(new int[] { _backMaterialIndex, _bottomMaterialIndex, _frontMaterialIndex, _leftMaterialIndex, _rightMaterialIndex, _topMaterialIndex }) + 1;
            newMeshData = AddMeshDataForTriangleList(backTriangles, Vector3.back, newMeshData, data, _backMaterialIndex);
            newMeshData = AddMeshDataForTriangleList(bottomTriangles, Vector3.down, newMeshData, data, _bottomMaterialIndex);
            newMeshData = AddMeshDataForTriangleList(frontTriangles, Vector3.forward, newMeshData, data, _frontMaterialIndex);
            newMeshData = AddMeshDataForTriangleList(leftTriangles, Vector3.left, newMeshData, data, _leftMaterialIndex);
            newMeshData = AddMeshDataForTriangleList(rightTriangles, Vector3.right, newMeshData, data, _rightMaterialIndex);
            newMeshData = AddMeshDataForTriangleList(topTriangles, Vector3.up, newMeshData, data, _topMaterialIndex);

            return newMeshData;

        }

        protected virtual MeshData SplitMeshForFaceUnwrapping(MeshData meshData) {

            MeshData oldMeshData = meshData.Copy();
            meshData.RemoveDoubles(gameObject.isStatic);
            List<FaceData> faceDataList = new List<FaceData>();

            #region SORT_BY_FACE_NORMAL
            for (int m = 0; m < meshData.Triangles.Length; m++) {
                for (int i = 0; i < meshData.Triangles[m].Count; i += 3) {
                    bool addedToExistingFaceData = false;
                    int[] triangleVertexIndices = new int[3];
                    Vector3 triangleNormal = Vector3.zero;
                    for (int t = 0; t < 3; t++) {
                        triangleVertexIndices[t] = meshData.Triangles[m][i + t];
                        triangleNormal += meshData.Normals[triangleVertexIndices[t]];
                    }
                    triangleNormal /= 3f; 
                    triangleNormal = new Vector3(triangleNormal.x / transform.lossyScale.x, triangleNormal.y / transform.lossyScale.y, triangleNormal.z / transform.lossyScale.z).normalized;
                    for (int faceIndex = 0; faceIndex < faceDataList.Count; faceIndex++) {
                        if (triangleNormal != Vector3.zero && faceDataList[faceIndex].IsWithinNormalAngleRange(triangleNormal, faceUnwrappingNormalTolerance)) {
                            faceDataList[faceIndex].AddTriangle(triangleVertexIndices, triangleNormal);
                            addedToExistingFaceData = true;
                            break;
                        }
                    }
                    if (!addedToExistingFaceData) {
                        FaceData newFaceData = new FaceData();
                        newFaceData.AddTriangle(triangleVertexIndices, triangleNormal);
                        faceDataList.Add(newFaceData);
                    }
                }
            }
            #endregion //SORT_BY_FACE_NORMAL

            #region SPLIT_FACES_BY_UV_EDGES
            List<FaceData> splitFaceDataList = new List<FaceData>();
            for (int i = 0; i < faceDataList.Count; i++) {
                List<int> triangles = new List<int>(faceDataList[i].Triangles);
                List<List<int>> splitTriangles = new List<List<int>>();
                int currentIndex = 0;
                int currentSplitTriangleIndex = 0;
                splitTriangles.Add(new List<int>());
                while (triangles.Count > 0) {
                    if (splitTriangles[currentSplitTriangleIndex].Count < 1 
                        || splitTriangles[currentSplitTriangleIndex].Contains(triangles[currentIndex]) 
                        || splitTriangles[currentSplitTriangleIndex].Contains(triangles[currentIndex + 1]) 
                        || splitTriangles[currentSplitTriangleIndex].Contains(triangles[currentIndex + 2])) 
                    {
                        splitTriangles[currentSplitTriangleIndex].Add(triangles[currentIndex]);
                        splitTriangles[currentSplitTriangleIndex].Add(triangles[currentIndex + 1]);
                        splitTriangles[currentSplitTriangleIndex].Add(triangles[currentIndex + 2]);
                        triangles.RemoveRange(currentIndex, 3);
                        currentIndex = 0;
                    }
                    else {
                        currentIndex += 3;
                    }
                    if (triangles.Count > 0 && currentIndex >= triangles.Count) {
                        currentIndex = 0;
                        splitTriangles.Add(new List<int>());
                        currentSplitTriangleIndex++;
                    }
                }
                for (int splitI = 0; splitI < splitTriangles.Count; splitI++) {
                    if (splitTriangles[splitI].Count < 1) {
                        continue;
                    }
                    FaceData newData = new FaceData();
                    newData.CopySettingsFrom(faceDataList[i]);
                    for (int t = 0; t < splitTriangles[splitI].Count; t += 3) {
                        int[] triangleIndices = new int[3] { splitTriangles[splitI][t], splitTriangles[splitI][t + 1], splitTriangles[splitI][t + 2] };
                        Vector3 normal = Vector3.zero;
                        for (int tI = 0; tI < 3; tI++) {
                            normal += meshData.Normals[triangleIndices[tI]];
                        }
                        normal /= 3f;
                        newData.AddTriangle(triangleIndices, normal);
                    }
                    splitFaceDataList.Add(newData);
                }
            }
            faceDataList = splitFaceDataList;
            #endregion //SPLIT_FACES_BY_UV_EDGES

            #region ADD_MESHDATA_FOR_FACEDATA
            MeshData newMeshData = new MeshData();
            newMeshData.subMeshCount = 1;
            for (int newFaceDataIndex = 0; newFaceDataIndex < faceDataList.Count; newFaceDataIndex++) {
                FaceData changedData = faceDataList[newFaceDataIndex];
                newMeshData.subMeshCount = Mathf.Max(newMeshData.subMeshCount, faceDataList[newFaceDataIndex].materialIndex + 1);
                newMeshData = AddMeshDataForFaceData(faceDataList[newFaceDataIndex], newMeshData, meshData, out changedData);
                faceDataList[newFaceDataIndex] = changedData;
            }
            #endregion //ADD_MESHDATA_FOR_FACEDATA

            #region ADD_UVDATA_FOR_FACEDATA
            FaceData[] orderedFaceDataArray = new FaceData[faceDataList.Count];
            List<FaceData> leftOverFaceData = new List<FaceData>();
            for (int newFaceDataIndex = 0; newFaceDataIndex < faceDataList.Count; newFaceDataIndex++) {
                if (_faceUnwrapData != null) {
                    for (int oldFaceDataIndex = 0; oldFaceDataIndex < _faceUnwrapData.Length; oldFaceDataIndex++) {
                        bool faceEqualsOldData = true;
                        if (_faceUnwrapData[oldFaceDataIndex] == null) {
                            _faceUnwrapData[oldFaceDataIndex] = new FaceData();
                            faceEqualsOldData = false;
                        }
                        else {
                            bool hadSimilarFace = false;
                            if (_faceUnwrapData[oldFaceDataIndex].Initialized && _faceUnwrapData[oldFaceDataIndex].Triangles != null && _faceUnwrapData[oldFaceDataIndex].Triangles.Length == faceDataList[newFaceDataIndex].Triangles.Length) {
                                hadSimilarFace = true;
                                List<Vector3> oldVertices = new List<Vector3>();
                                for (int i = 0; i < _faceUnwrapData[oldFaceDataIndex].Triangles.Length; i++) {
                                    if (_faceUnwrapData[oldFaceDataIndex].Triangles[i] < newMeshData.Vertices.Count) {
                                        oldVertices.Add(newMeshData.Vertices[_faceUnwrapData[oldFaceDataIndex].Triangles[i]]);
                                    }
                                }
                                for (int iT = 0; iT < faceDataList[newFaceDataIndex].Triangles.Length; iT++) {
                                    if (!oldVertices.Contains(newMeshData.Vertices[faceDataList[newFaceDataIndex].Triangles[iT]])) {
                                        faceEqualsOldData = false;
                                        break;
                                    }
                                }
                            }
                            if (!hadSimilarFace) {
                                faceEqualsOldData = false;
                            }
                        }
                        if (faceEqualsOldData) {
                            faceDataList[newFaceDataIndex].Initialize(_faceUnwrapData[oldFaceDataIndex]);
                            if (oldFaceDataIndex < orderedFaceDataArray.Length) {
                                orderedFaceDataArray[oldFaceDataIndex] = faceDataList[newFaceDataIndex];
                            }
                            else {
                                leftOverFaceData.Add(faceDataList[newFaceDataIndex]);
                            }
                            break;
                        }
                    }
                }
                if (!faceDataList[newFaceDataIndex].Initialized) {
                    #region COPY_CUBE_PROJECTION_SETTINGS
                    switch (GetCubeProjectionDirectionForNormal(faceDataList[newFaceDataIndex].AverageNormal.normalized)) {
                        case Direction.Back:
                            faceDataList[newFaceDataIndex].flipUVx = backFlipX;
                            faceDataList[newFaceDataIndex].flipUVy = backFlipY;
                            faceDataList[newFaceDataIndex].materialIndex = backMaterialIndex;
                            faceDataList[newFaceDataIndex].rotation = backRotation;
                            faceDataList[newFaceDataIndex].uvOffset = backOffset;
                            faceDataList[newFaceDataIndex].uvScale = backScale;
                            break;
                        case Direction.Down:
                            faceDataList[newFaceDataIndex].flipUVx = bottomFlipX;
                            faceDataList[newFaceDataIndex].flipUVy = bottomFlipY;
                            faceDataList[newFaceDataIndex].materialIndex = bottomMaterialIndex;
                            faceDataList[newFaceDataIndex].rotation = bottomRotation;
                            faceDataList[newFaceDataIndex].uvOffset = bottomOffset;
                            faceDataList[newFaceDataIndex].uvScale = bottomScale;
                            break;
                        case Direction.Forward:
                            faceDataList[newFaceDataIndex].flipUVx = frontFlipX;
                            faceDataList[newFaceDataIndex].flipUVy = frontFlipY;
                            faceDataList[newFaceDataIndex].materialIndex = frontMaterialIndex;
                            faceDataList[newFaceDataIndex].rotation = frontRotation;
                            faceDataList[newFaceDataIndex].uvOffset = frontOffset;
                            faceDataList[newFaceDataIndex].uvScale = frontScale;
                            break;
                        case Direction.Left:
                            faceDataList[newFaceDataIndex].flipUVx = leftFlipX;
                            faceDataList[newFaceDataIndex].flipUVy = leftFlipY;
                            faceDataList[newFaceDataIndex].materialIndex = leftMaterialIndex;
                            faceDataList[newFaceDataIndex].rotation = leftRotation;
                            faceDataList[newFaceDataIndex].uvOffset = leftOffset;
                            faceDataList[newFaceDataIndex].uvScale = leftScale;
                            break;
                        case Direction.Right:
                            faceDataList[newFaceDataIndex].flipUVx = rightFlipX;
                            faceDataList[newFaceDataIndex].flipUVy = rightFlipY;
                            faceDataList[newFaceDataIndex].materialIndex = rightMaterialIndex;
                            faceDataList[newFaceDataIndex].rotation = rightRotation;
                            faceDataList[newFaceDataIndex].uvOffset = rightOffset;
                            faceDataList[newFaceDataIndex].uvScale = rightScale;
                            break;
                        case Direction.Up:
                            faceDataList[newFaceDataIndex].flipUVx = topFlipX;
                            faceDataList[newFaceDataIndex].flipUVy = topFlipY;
                            faceDataList[newFaceDataIndex].materialIndex = topMaterialIndex;
                            faceDataList[newFaceDataIndex].rotation = topRotation;
                            faceDataList[newFaceDataIndex].uvOffset = topOffset;
                            faceDataList[newFaceDataIndex].uvScale = topScale;
                            break;
                    }
                    #endregion
                    faceDataList[newFaceDataIndex].Initialize();
                    leftOverFaceData.Add(faceDataList[newFaceDataIndex]);
                }
            }
            while (leftOverFaceData.Count > 0) {
                for (int i = 0; i < orderedFaceDataArray.Length; i++) {
                    if (orderedFaceDataArray[i] == null) {
                        orderedFaceDataArray[i] = leftOverFaceData[0];
                        leftOverFaceData.RemoveAt(0);
                        break;
                    }
                }
            }
            if (_faceUnwrapData != null) {
                faceDataList = new List<FaceData>(orderedFaceDataArray);
            }

            // Check for old material indices and re-apply them where fitting
            if (freshMesh) {
                for (int subMeshIndex = 0; subMeshIndex < oldMeshData.subMeshCount; subMeshIndex++) {
                    HashSet<Vector3> subMeshTrianglesHash = new HashSet<Vector3>();
                    for (int i = 0; i < oldMeshData.Triangles[subMeshIndex].Count; i++) {
                        subMeshTrianglesHash.Add(oldMeshData.Vertices[oldMeshData.Triangles[subMeshIndex][i]]);
                    }
                    for (int fdIndex = 0; fdIndex < faceDataList.Count; fdIndex++) {
                        HashSet<Vector3> faceVerticesHash = new HashSet<Vector3>();
                        for (int i = 0; i < faceDataList[fdIndex].Triangles.Length; i++) {
                            faceVerticesHash.Add(newMeshData.Vertices[faceDataList[fdIndex].Triangles[i]]);
                        }
                        if (faceVerticesHash.IsSubsetOf(subMeshTrianglesHash)) {
                            //Debug.Log("Found fitting triangle mesh: setting material index to " + subMeshIndex);
                            faceDataList[fdIndex].materialIndex = subMeshIndex;
                        }
                    }
                }
                freshMesh = false;
            }

            MeshData updatedMeshData = new MeshData();
            updatedMeshData.subMeshCount = 1;
            for (int newFaceDataIndex = 0; newFaceDataIndex < faceDataList.Count; newFaceDataIndex++) {
                FaceData changedData = new FaceData();
                if (faceDataList[newFaceDataIndex] == null) {
                    faceDataList[newFaceDataIndex] = new FaceData();
                    faceDataList[newFaceDataIndex].Initialize();
                }
                updatedMeshData.subMeshCount = Mathf.Max(updatedMeshData.subMeshCount, faceDataList[newFaceDataIndex].materialIndex + 1);
                updatedMeshData = AddMeshDataForFaceData(faceDataList[newFaceDataIndex], updatedMeshData, newMeshData, out changedData);
            }
            newMeshData = updatedMeshData;
            #endregion //ADD_UVDATA_FOR_FACEDATA

            _faceUnwrapData = faceDataList.ToArray();
            return newMeshData;

        }

        private void LogFaceDataVertices(FaceData faceData, MeshData meshData) {

            string vertexString = "";
            if (faceData == null) {
                vertexString += "(ERROR: faceData was null)";
            }
            else {
                if (faceData.Triangles == null) {
                    vertexString = "(ERROR: faceData triangles were null)";
                }
                else {
                    for (int i = 0; i < faceData.Triangles.Length; i++) {
                        int index = faceData.Triangles[i];
                        if (index < meshData.Vertices.Count) {
                            vertexString += meshData.Vertices[index].ToString();
                        }
                        else {
                            vertexString += "(ERROR:" + index + " out of bound)";
                        }
                    }
                }
            }
            Debug.Log(vertexString);
        }

        private MeshData AddMeshDataForTriangleList(List<int> triangleIds, Vector3 normalDirection, MeshData newData, MeshData oldData, int materialIndex) {

            Dictionary<int, int> oldIdNewIdMapping = new Dictionary<int, int>();
            foreach (int vertId in triangleIds) {
                if (!oldIdNewIdMapping.ContainsKey(vertId)) {
                    oldIdNewIdMapping[vertId] = newData.Vertices.Count;
                    newData.AddTriangle(newData.Vertices.Count, materialIndex);
                    newData.AddVertex(oldData.Vertices[vertId]);
                    if (vertId < oldData.Tangents.Count) {
                        newData.AddTangent(oldData.Tangents[vertId]);
                    }
                    newData.AddNormal(oldData.Normals[vertId]);
                    newData.AddUVCoordinate(VerticeUVByNormal(oldData.Vertices[vertId], normalDirection));
                    if (vertId < oldData.UV2.Count) {
                        newData.AddUV2Coordinate(oldData.UV2[vertId]);
                    }
                }
                else {
                    newData.AddTriangle(oldIdNewIdMapping[vertId], materialIndex);
                }
            }
            return newData;

        }

        protected MeshData AddMeshDataForFaceData(FaceData faceData, MeshData newData, MeshData oldData, out FaceData updatedFaceData) {

            Dictionary<int, int> oldIdNewIdMapping = new Dictionary<int, int>();
            List<int> newFaceTriangleIndices = new List<int>();
            foreach (int vertId in faceData.Triangles) {
                if (!oldIdNewIdMapping.ContainsKey(vertId)) {
                    oldIdNewIdMapping[vertId] = newData.Vertices.Count;
                    newData.AddTriangle(newData.Vertices.Count, faceData.materialIndex);
                    newFaceTriangleIndices.Add(newData.Vertices.Count);
                    newData.AddVertex(oldData.Vertices[vertId]);
                    if (vertId < oldData.Tangents.Count) {
                        newData.AddTangent(oldData.Tangents[vertId]);
                    }
                    newData.AddNormal(oldData.Normals[vertId]);
                    newData.AddUVCoordinate(VerticeUVByFace(oldData.Vertices[vertId], faceData));
                    if (vertId < oldData.UV2.Count) {
                        newData.AddUV2Coordinate(oldData.UV2[vertId]);
                    }
                }
                else {
                    int index = oldIdNewIdMapping[vertId];
                    newData.AddTriangle(index, faceData.materialIndex);
                    newFaceTriangleIndices.Add(index);
                }
            }
            faceData.SetTriangles(newFaceTriangleIndices.ToArray());
            updatedFaceData = faceData;
            return newData;

        }

        public static Direction GetCubeProjectionDirectionForNormal(Vector3 normal) {

            Direction uvDir = Direction.Up;
            float angle = Vector3.Angle(normal, Vector3.up);
            float newAngle = Vector3.Angle(normal, Vector3.down);
            if (newAngle < angle) {
                angle = newAngle;
                uvDir = Direction.Down;
            }
            newAngle = Vector3.Angle(normal, Vector3.left);
            if (newAngle < angle) {
                angle = newAngle;
                uvDir = Direction.Left;
            }
            newAngle = Vector3.Angle(normal, Vector3.right);
            if (newAngle < angle) {
                angle = newAngle;
                uvDir = Direction.Right;
            }
            newAngle = Vector3.Angle(normal, Vector3.forward);
            if (newAngle < angle) {
                angle = newAngle;
                uvDir = Direction.Forward;
            }
            newAngle = Vector3.Angle(normal, Vector3.back);
            if (newAngle < angle) {
                angle = newAngle;
                uvDir = Direction.Back;
            }

            return uvDir;

        }

        private Vector2 VerticeUVByNormal(Vector3 vertex, Vector3 normal) {

            Direction uvDir = GetCubeProjectionDirectionForNormal(normal);
            Vector2 uvCoord = new Vector2(1f, 1f);

            switch (uvDir) {
                case Direction.Up:
                    uvCoord = Quaternion.Euler(0f, 0f, topRotation) * new Vector2(transform.lossyScale.z * vertex.z, transform.lossyScale.x * vertex.x);
                    uvCoord.x = (uvCoord.x / topScale.x) + topOffset.x;
                    uvCoord.y = (uvCoord.y / topScale.y) + topOffset.y;
                    if (topFlipX) {
                        uvCoord.x = 1 - uvCoord.x;
                    }
                    if (topFlipY) {
                        uvCoord.y = 1 - uvCoord.y;
                    }
                    break;
                case Direction.Down:
                    uvCoord = Quaternion.Euler(0f, 0f, bottomRotation) * new Vector2(transform.lossyScale.z * vertex.z, transform.lossyScale.x * vertex.x);
                    uvCoord.x = (uvCoord.x / (useUnifiedScaling ? topScale.x : bottomScale.x)) + (useUnifiedOffset ? topOffset.x : bottomOffset.x);
                    uvCoord.y = (uvCoord.y / (useUnifiedScaling ? topScale.y : bottomScale.y)) + (useUnifiedOffset ? topOffset.y : bottomOffset.y);
                    if (bottomFlipX) {
                        uvCoord.x = 1 - uvCoord.x;
                    }
                    if (bottomFlipY) {
                        uvCoord.y = 1 - uvCoord.y;
                    }
                    break;
                case Direction.Left:
                    uvCoord = Quaternion.Euler(0f, 0f, leftRotation) * new Vector2(transform.lossyScale.z * vertex.z, transform.lossyScale.y * vertex.y);
                    uvCoord.x = (uvCoord.x / (useUnifiedScaling ? topScale.x : leftScale.x)) + (useUnifiedOffset ? topOffset.x : leftOffset.x);
                    uvCoord.y = (uvCoord.y / (useUnifiedScaling ? topScale.y : leftScale.y)) + (useUnifiedOffset ? topOffset.y : leftOffset.y);
                    if (leftFlipX) {
                        uvCoord.x = 1 - uvCoord.x;
                    }
                    if (leftFlipY) {
                        uvCoord.y = 1 - uvCoord.y;
                    }
                    break;
                case Direction.Right:
                    uvCoord = Quaternion.Euler(0f, 0f, rightRotation) * new Vector2(transform.lossyScale.z * vertex.z, transform.lossyScale.y * vertex.y);
                    uvCoord.x = (uvCoord.x / (useUnifiedScaling ? topScale.x : rightScale.x)) + (useUnifiedOffset ? topOffset.x : rightOffset.x);
                    uvCoord.y = (uvCoord.y / (useUnifiedScaling ? topScale.y : rightScale.y)) + (useUnifiedOffset ? topOffset.y : rightOffset.y);
                    if (rightFlipX) {
                        uvCoord.x = 1 - uvCoord.x;
                    }
                    if (rightFlipY) {
                        uvCoord.y = 1 - uvCoord.y;
                    }
                    break;
                case Direction.Forward:
                    uvCoord = Quaternion.Euler(0f, 0f, frontRotation) * new Vector2(transform.lossyScale.x * vertex.x, transform.lossyScale.y * vertex.y);
                    uvCoord.x = (uvCoord.x / (useUnifiedScaling ? topScale.x : frontScale.x)) + (useUnifiedOffset ? topOffset.x : frontOffset.x);
                    uvCoord.y = (uvCoord.y / (useUnifiedScaling ? topScale.y : frontScale.y)) + (useUnifiedOffset ? topOffset.y : frontOffset.y);
                    if (frontFlipX) {
                        uvCoord.x = 1 - uvCoord.x;
                    }
                    if (frontFlipY) {
                        uvCoord.y = 1 - uvCoord.y;
                    }
                    break;
                case Direction.Back:
                    uvCoord = Quaternion.Euler(0f, 0f, backRotation) * new Vector2(transform.lossyScale.x * vertex.x, transform.lossyScale.y * vertex.y);
                    uvCoord.x = (uvCoord.x / (useUnifiedScaling ? topScale.x : backScale.x)) + (useUnifiedOffset ? topOffset.x : backOffset.x);
                    uvCoord.y = (uvCoord.y / (useUnifiedScaling ? topScale.y : backScale.y)) + (useUnifiedOffset ? topOffset.y : backOffset.y);
                    if (backFlipX) {
                        uvCoord.x = 1 - uvCoord.x;
                    }
                    if (backFlipY) {
                        uvCoord.y = 1 - uvCoord.y;
                    }
                    break;
            }
            return uvCoord;

        }

        private Vector2 VerticeUVByFace(Vector3 vertex, FaceData faceData) {

            Vector3 normal = Vector3.Scale(faceData.AverageNormal, transform.lossyScale);
            Vector3 localVertexCoord = Quaternion.FromToRotation(normal, Vector3.up) * new Vector3(vertex.x * transform.lossyScale.x, vertex.y * transform.lossyScale.y, vertex.z * transform.lossyScale.z);
            Vector2 uvCoord = Quaternion.Euler(0f, 0f, faceData.rotation) * new Vector2(localVertexCoord.x, localVertexCoord.z);
            uvCoord.x = (uvCoord.x / (useUnifiedScaling && _faceUnwrapData.Length > 0 && _faceUnwrapData[0] != null ? _faceUnwrapData[0].uvScale.x : faceData.uvScale.x))
                + (useUnifiedOffset && _faceUnwrapData.Length > 0 && _faceUnwrapData[0] != null ? _faceUnwrapData[0].uvOffset.x : faceData.uvOffset.x);
            uvCoord.y = (uvCoord.y / (useUnifiedScaling && _faceUnwrapData.Length > 0 && _faceUnwrapData[0] != null ? _faceUnwrapData[0].uvScale.y : faceData.uvScale.y))
                + (useUnifiedOffset && _faceUnwrapData.Length > 0 && _faceUnwrapData[0] != null ? _faceUnwrapData[0].uvOffset.y : faceData.uvOffset.y);
            if (faceData.flipUVx) {
                uvCoord.x = 1 - uvCoord.x;
            }
            if (faceData.flipUVy) {
                uvCoord.y = 1 - uvCoord.y;
            }
            return uvCoord;

        }

#if UNITY_EDITOR
        public void SaveMeshAsset() {

            if (!meshFilter.sharedMesh) {
                Debug.LogError(name + ": " + GetType() + ".SaveMeshAsset: there was no mesh set.");
                return;
            }
            string currentMeshAssetPath = GetMeshAssetPath();
            string[] pathParts = currentMeshAssetPath.Split('/');

            if (pathParts == null || pathParts.Length < 1) {
                Debug.LogError(GetType() + ".SaveMeshAsset: mesh asset path was set incorrectly.");
                return;
            }
            if (pathParts[0] != "Assets") {
                Debug.LogError(GetType() + ".SaveMeshAsset: mesh asset path has to start with \"Assets\".");
                return;
            }

            for (int i = 1; i < pathParts.Length; i++) {
                if (!string.IsNullOrEmpty(pathParts[i])) {
                    string currentPath = pathParts[0];
                    for (int curPathId = 1; curPathId < i; curPathId++) {
                        currentPath += "/" + pathParts[curPathId];
                    }
                    if (!AssetDatabase.IsValidFolder(currentPath + "/" + pathParts[i])) {
                        AssetDatabase.CreateFolder(currentPath, pathParts[i]);
                    }
                }
            }
            Mesh meshPrefab = AssetDatabase.LoadAssetAtPath(currentMeshAssetPath + meshFilter.sharedMesh.name + extensionString, typeof(Mesh)) as Mesh;
            if (meshPrefab) {
                if (meshPrefab != meshFilter.sharedMesh) {
                    string[] meshNameParts = meshPrefab.name.Split('_');
                    if (meshNameParts.Length > 1) {
                        int numberSuffix = 0;
                        if (int.TryParse(meshNameParts[meshNameParts.Length - 1], out numberSuffix)) {
                            numberSuffix++;
                            string prefabName = currentMeshAssetPath + name + "_" + numberSuffix + extensionString;
                            //							string prefabName = meshAssetPathString + EditorApplication.currentScene.Replace('/', '_').Replace('\\', '_') + "_" + name + "_" + numberSuffix + extensionString;
                            meshPrefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(Mesh)) as Mesh;
                            while (meshPrefab != null) {
                                prefabName = currentMeshAssetPath + name + "_" + (++numberSuffix) + extensionString;
                                //								prefabName = meshAssetPathString + EditorApplication.currentScene.Replace('/', '_').Replace('\\', '_') + "_" + name + "_" + (++numberSuffix) + extensionString;
                                meshPrefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(Mesh)) as Mesh;
                            }
                            _useBakedMesh = true;
                            Mesh meshToSave = Mesh.Instantiate(meshFilter.sharedMesh);
                            meshToSave.name = meshFilter.sharedMesh.name;
                            AssetDatabase.CreateAsset(meshToSave, prefabName);
                            AssetDatabase.SaveAssets();
                            meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath(prefabName, typeof(Mesh)) as Mesh;
                            EditorUtility.SetDirty(meshFilter.sharedMesh);
                            EditorUtility.SetDirty(meshFilter);
                            EditorUtility.SetDirty(this);
                        }
                    }
                    else {
                        Debug.LogError(name + ": " + GetType() + ".SaveMeshAsset: prefab name " + meshPrefab.name + " has no number suffix.");
                    }
                }
            }
            else {
                int numberSuffix = 0;
                string prefabName = currentMeshAssetPath + name + "_" + numberSuffix + extensionString;
                //				string prefabName = meshAssetPathString + EditorApplication.currentScene.Replace('/', '_').Replace('\\', '_') + "_" + name + "_" + numberSuffix + extensionString;
                meshPrefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(Mesh)) as Mesh;
                while (meshPrefab != null) {
                    prefabName = currentMeshAssetPath + name + "_" + (++numberSuffix) + extensionString;
                    //					prefabName = meshAssetPathString + EditorApplication.currentScene.Replace('/', '_').Replace('\\', '_') + "_" + name + "_" + (++numberSuffix) + extensionString;
                    meshPrefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(Mesh)) as Mesh;
                }
                _useBakedMesh = true;
                Mesh meshToSave = Mesh.Instantiate(meshFilter.sharedMesh);
                meshToSave.name = meshFilter.sharedMesh.name;
                AssetDatabase.CreateAsset(meshToSave, prefabName);
                AssetDatabase.SaveAssets();
                meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath(prefabName, typeof(Mesh)) as Mesh;
                EditorUtility.SetDirty(meshFilter.sharedMesh);
                EditorUtility.SetDirty(meshFilter);
                EditorUtility.SetDirty(this);
            }

        }

        public void DeleteConnectedMesh() {

            string currentMeshAssetPath = GetMeshAssetPath();
            Mesh meshPrefab = AssetDatabase.LoadAssetAtPath(currentMeshAssetPath + meshFilter.sharedMesh.name + extensionString, typeof(Mesh)) as Mesh;
            if (!meshPrefab && meshFilter.sharedMesh.name.EndsWith("(Clone)")) {
                meshPrefab = AssetDatabase.LoadAssetAtPath(currentMeshAssetPath + meshFilter.sharedMesh.name.Substring(0, meshFilter.sharedMesh.name.Length - 7) + extensionString, typeof(Mesh)) as Mesh;
            }
            if (meshPrefab) {
                AutoTextureTiling[] listOfTextureTilingToolObjects = FindObjectsOfType<AutoTextureTiling>();
                for (int i = 0; i < listOfTextureTilingToolObjects.Length; i++) {
                    if (listOfTextureTilingToolObjects[i].meshFilter.sharedMesh == meshPrefab) {
                        listOfTextureTilingToolObjects[i].BreakMeshAssetConnection();
                    }
                }
                if (!AssetDatabase.DeleteAsset(currentMeshAssetPath + meshPrefab.name + extensionString)) {
                    Debug.LogError(name + ": " + GetType() + ".SaveMeshAsset: could not delete " + meshPrefab.name + ": failed to execute AssetDatabase.DeleteAsset.");
                }
                else {
                    GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(this.gameObject) as GameObject;
                    if (prefab) {
                        Debug.LogWarning(GetType() + ".BreakMeshAssetConnection: mesh asset was deleted, but object was instance of a prefab. It is recommended to delete the prefab " + prefab.name + ".");
                    }
                }
                AssetDatabase.SaveAssets();
            }
            else {
                _useBakedMesh = false;
                EditorUtility.SetDirty(this);
                Debug.LogError(name + ": " + GetType() + ".SaveMeshAsset: could not delete " + meshFilter.sharedMesh.name + ": Did not find the asset.");
            }
        }

        public void BreakMeshAssetConnection() {

            if (_useBakedMesh) {
                if (meshFilter.sharedMesh) {
                    Mesh meshCopy = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh;
                    meshCopy.name = meshFilter.sharedMesh.name;
                    meshFilter.sharedMesh = meshCopy;
                    meshFilter.sharedMesh.name = "Mesh " + name;
                }
                _useBakedMesh = false;
                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(this.gameObject) as GameObject;
                if (prefab)
                {
#if UNITY_2018
                    PrefabUtility.UnpackPrefabInstance(this.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
#else
                    PrefabUtility.DisconnectPrefabInstance(this.gameObject);
#endif
                }
                EditorUtility.SetDirty(this);
            }

        }

        public bool MeshPrefabExists()
        {
            string currentMeshAssetPath = GetMeshAssetPath();
            return AssetDatabase.LoadAssetAtPath(currentMeshAssetPath + meshFilter.sharedMesh.name + extensionString, typeof(Mesh)) as Mesh != null;
        }

        private string GetMeshAssetPath()
        {
            string currentMeshAssetPath = meshAssetPathString;
            string[] pathGUIDs = AssetDatabase.FindAssets("AutoTextureTilingTool");
            string foundPath = "";
            if (pathGUIDs == null || pathGUIDs.Length < 1)
            {
                Debug.LogError("No asset \"AutoTextureTilingTool\" was found.");
            }
            else
            {
                if (pathGUIDs.Length > 1)
                {
                    Debug.LogWarning(GetType() + ".SaveMeshAsset: there is more than one path or asset called \"AutoTextureTilingTool\". There should only be one single path named \"AutoTextureTilingTool\"");
                }
                for (int i = 0; i < pathGUIDs.Length; i++)
                {
                    foundPath = AssetDatabase.GUIDToAssetPath(pathGUIDs[i]);
                    if (!string.IsNullOrEmpty(foundPath))
                    {
                        currentMeshAssetPath = foundPath + "/Meshes/";
                        break;
                    }
                }
            }
            return currentMeshAssetPath;
        }
#endif

#if FALSE // UNITY_EDITOR //
        public void OnDrawGizmosSelected() {

            if (faceUnwrapData == null) {
                return;
            }
            if (!_meshFilter || !meshFilter.sharedMesh) {
                return;
            }

            //for (int i = 0; i < meshFilter.sharedMesh.normals.Length; i++) {
            //    Vector3 startPos = meshFilter.sharedMesh.vertices[i];
            //    startPos = transform.position + transform.rotation * new Vector3(transform.lossyScale.x * startPos.x, transform.lossyScale.y * startPos.y, transform.lossyScale.z * startPos.z);
            //    Gizmos.color = Color.white;
            //    Gizmos.DrawLine(startPos, startPos + transform.rotation * meshFilter.sharedMesh.normals[i]);
            //}

            //for (int i = 0; i < _meshFilter.sharedMesh.triangles.Length; i += 3) {
            //    GL.PushMatrix();
            //    GL.Begin(GL.TRIANGLES);
            //    for (int j = 0; j < 3; j++) {
            //        GL.Color(Color.blue);
            //        int index = _meshFilter.sharedMesh.triangles[i + j];
            //        Vector3 currentVertex = _meshFilter.sharedMesh.vertices[index] + (_meshFilter.sharedMesh.normals[index] * 0.01f);
            //        GL.Vertex(transform.rotation * (new Vector3(currentVertex.x * transform.lossyScale.x, currentVertex.y * transform.lossyScale.y, currentVertex.z * transform.lossyScale.z)) + transform.position);
            //    }
            //    GL.End();
            //    GL.PopMatrix();
            //}

            for (int i = 0; i < faceUnwrapData.Length; i++) {
                Vector3 averageFacePosition = Vector3.zero;
                Vector3 averageNormal = Vector3.zero;
                for (int v = 0; v < faceUnwrapData[i].Triangles.Length; v += 3) {
                    //GL.PushMatrix();
                    //GL.Begin(GL.TRIANGLES);
                    for (int j = 0; j < 3; j++) {
                        //GL.Color(new Color(((i) % 6) / 6f, ((2 + i) % 6) / 6f, ((4 + i) % 6) / 6f, 1f));
                        int index = faceUnwrapData[i].Triangles[v + j];
                        //Vector3 currentVertex = _meshFilter.sharedMesh.vertices[index] + (_meshFilter.sharedMesh.normals[index] * 0.01f);
                        //GL.Vertex(transform.rotation * (new Vector3(currentVertex.x * transform.lossyScale.x, currentVertex.y * transform.lossyScale.y, currentVertex.z * transform.lossyScale.z)) + transform.position);
                        if (index < _meshFilter.sharedMesh.vertices.Length) {
                            Vector3 newPos = _meshFilter.sharedMesh.vertices[index];
                            averageFacePosition += (transform.rotation *
                                new Vector3(newPos.x * transform.lossyScale.x, newPos.y * transform.lossyScale.y, newPos.z * transform.lossyScale.z));
                            averageNormal += _meshFilter.sharedMesh.normals[index];
                        }
                        else {
                            Debug.LogError("Index out of bound.");
                        }
                    }
                    //GL.End();
                    //GL.PopMatrix();
                }
                averageFacePosition = (averageFacePosition / (faceUnwrapData[i].Triangles.Length)) + transform.position;
                averageNormal = transform.rotation * (averageNormal / (faceUnwrapData[i].Triangles.Length));
                //Gizmos.DrawSphere(averageFacePosition, 0.1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(averageFacePosition, averageFacePosition + averageNormal);
            }

            //for (int i = 0; i < faceUnwrapData.Length; i++) {
            //    Vector3 averageFacePosition = Vector3.zero;
            //    for (int v = 0; v < faceUnwrapData[i].Triangles.Length; v++) {
            //        int index = faceUnwrapData[i].Triangles[v];
            //        if (index < _meshFilter.sharedMesh.vertices.Length) {
            //            Vector3 newPos = _meshFilter.sharedMesh.vertices[index];
            //            averageFacePosition += (transform.rotation *
            //                new Vector3(newPos.x * transform.lossyScale.x, newPos.y * transform.lossyScale.y, newPos.z * transform.lossyScale.z));
            //        }
            //        else {
            //            Debug.LogError("Index out of bound.");
            //        }
            //    }
            //    averageFacePosition = (averageFacePosition / (faceUnwrapData[i].Triangles.Length)) + transform.position;
            //    Vector3 normal = new Vector3(faceUnwrapData[i].AverageNormal.x / transform.lossyScale.x, faceUnwrapData[i].AverageNormal.y / transform.lossyScale.y, faceUnwrapData[i].AverageNormal.z / transform.lossyScale.z).normalized;
            //    Vector3 normalEnd = averageFacePosition + (transform.rotation * normal);
            //    //Gizmos.DrawSphere(averageFacePosition, 0.1f);
            //    Gizmos.DrawLine(averageFacePosition, normalEnd);
            //}

        }

#endif

    }

    public enum Direction {

        Up,
        Down,
        Left,
        Right,
        Forward,
        Back,

    }

    [System.Serializable]
    public class FaceData {

        public Vector2 uvScale = Vector2.one;
        public Vector2 uvOffset = Vector2.zero;
        public float rotation = 0f;
        public bool flipUVx = false;
        public bool flipUVy = false;
        public int materialIndex = 0;

        [HideInInspector]
        [SerializeField]
        private int[] triangles;
        [HideInInspector]
        [SerializeField]
        public Vector3[] normals;
        [HideInInspector]
        [SerializeField]
        private Vector3 averageNormal;
        [HideInInspector]
        [SerializeField]
        private bool initialized;

        public Vector3 AverageNormal {
            get {
                return averageNormal;
            }
        }

        public bool Initialized {
            get {
                return initialized;
            }
        }

        public int[] Triangles {
            get {
                return triangles;
            }
        }

        public FaceData() {
            triangles = new int[0];
            normals = new Vector3[0];
        }

        public void Initialize() {
            initialized = true;
        }

        public void Initialize(FaceData dataForCopyingSettings) {
            CopySettingsFrom(dataForCopyingSettings);
            initialized = true;
        }

        public void CopySettingsFrom(FaceData dataForCopyingSettings) {
            uvScale = dataForCopyingSettings.uvScale;
            uvOffset = dataForCopyingSettings.uvOffset;
            rotation = dataForCopyingSettings.rotation;
            flipUVx = dataForCopyingSettings.flipUVx;
            flipUVy = dataForCopyingSettings.flipUVy;
            materialIndex = dataForCopyingSettings.materialIndex;
        }

        public void AddTriangle(int[] triangleVertexIndices, Vector3 normal) {

            if (triangleVertexIndices == null) {
                Debug.LogError(GetType() + ".AddTriangle: triangleVertexIndices was null.");
                return;
            }
            if (triangleVertexIndices.Length != 3) {
                Debug.LogError(GetType() + ".AddTriangle: triangle vertex index array has to have exactly 3 entries.");
                return;
            }
            //Debug.Log("Adding Triangle: " + triangleVertexIndices[0] + "|" + triangleVertexIndices[1] + "|" + triangleVertexIndices[2]);
            for (int i = 0; i < triangles.Length; i += 3) {
                int sameIndexCount = 0;
                for (int t = 0; t < 3; t++) {
                    if (triangles[i + t] == triangleVertexIndices[t]) {
                        sameIndexCount++;
                    }
                }
                if (sameIndexCount == 3) {
                    Debug.LogWarning(GetType() + ".AddTriangle: triangle " + triangleVertexIndices[0] + "|" + triangleVertexIndices[1] + "|" + triangleVertexIndices[2] + " already existed. Check your meshData.");
                    return;
                }
            }

            int[] newVertexList = new int[triangles.Length + triangleVertexIndices.Length];
            for (int i = 0; i < triangles.Length; i++) {
                newVertexList[i] = triangles[i];
            }
            for (int i = 0; i < triangleVertexIndices.Length; i++) {
                newVertexList[triangles.Length + i] = triangleVertexIndices[i];
            }
            triangles = newVertexList;

            Vector3[] newNormals = new Vector3[normals.Length + 1];
            averageNormal = Vector3.zero;
            for (int i = 0; i < normals.Length; i++) {
                newNormals[i] = normals[i];
                averageNormal += normals[i];
            }
            newNormals[normals.Length] = normal;
            averageNormal += normal;
            averageNormal /= newNormals.Length;
            normals = newNormals;

        }

        public bool IsWithinNormalAngleRange(Vector3 triangleNormal, float faceUnwrappingNormalTolerance) {

            return Vector3.Angle(triangleNormal, averageNormal) <= faceUnwrappingNormalTolerance;

        }

        public void SetTriangles(int[] newFaceTriangleIndices) {

            if (newFaceTriangleIndices == null) {
                Debug.LogError(GetType() + ".SetTriangles: triangle index array can't be null.");
                return;
            }
            if (newFaceTriangleIndices.Length % 3 != 0) {
                Debug.LogError(GetType() + ".SetTriangles: triangle index array has to have a length devisable by 3. Array length: " + newFaceTriangleIndices.Length);
                return;
            }

            triangles = newFaceTriangleIndices;

        }

        public string TrianglesToString() {

            string returnString = "";
            for (int i = 0; i < triangles.Length; i++) {
                returnString += triangles[i].ToString();
                if (i < triangles.Length - 1) {
                    returnString += ", ";
                }
            }
            return returnString;

        }

    }

    public enum UnwrapType {
        CubeProjection,
        FaceDependent,
    }

}

