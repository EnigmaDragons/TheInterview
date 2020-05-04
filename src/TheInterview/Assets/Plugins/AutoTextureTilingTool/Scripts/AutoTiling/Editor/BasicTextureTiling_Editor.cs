using UnityEditor;

namespace AutoTiling {

    [CustomEditor(typeof(BasicTextureTiling))]
    public class BasicTextureTiling_Editor : AutoTextureTiling_Editor {

        protected override void DrawFaceUnwrapTypeSelection(AutoTextureTiling Target) {}
        //protected override void DrawMaterialSelector(AutoTextureTiling Target, FaceData faceData, string[] options, int index, bool changedAnythingStartValue, out bool changedAnything) { changedAnything = changedAnythingStartValue; }
        protected override void DrawNormalToleranceField(AutoTextureTiling Target) {}

    }

}