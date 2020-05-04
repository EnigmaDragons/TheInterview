using UnityEngine;
using System.Collections;

namespace AutoTiling {

    /// <summary>
    /// Dynamic texture tiling.
    /// This class will allow updating of the Mesh and the UV coordinates at runtime.
    /// Every time your GameObject gets resized, it will adjust the Texture accordingly.
    /// </summary>
    public class DynamicTextureTiling : AutoTextureTiling {

#if UNITY_EDITOR
        public override void Awake() {

            if (gameObject.isStatic) {
                Debug.LogError(name + ": " + GetType() + ".Awake: this script won't work on static objects. Make the object non-static or use AutoTextureTiling.");
            }
            base.Awake();

        }
#endif

        void Update() {

            if (scaleX != transform.lossyScale.x || scaleY != transform.lossyScale.y || scaleZ != transform.lossyScale.z) {
                scaleX = transform.lossyScale.x;
                scaleY = transform.lossyScale.y;
                scaleZ = transform.lossyScale.z;
                CreateMeshAndUVs();
            }

        }

    }

}