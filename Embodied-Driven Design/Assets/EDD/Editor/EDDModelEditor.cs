
using UnityEngine;
using UnityEditor;
using Klak.Wiring;

[CanEditMultipleObjects]
[CustomEditor(typeof(EDDModel))]
public class EDDPatchEditor : Editor
{
    [MenuItem("GameObject/EDD Model", false, 10)]
    static void CreatePatch()
    {
        var go = new GameObject("EDD Model");
		go.AddComponent<EDDModel>();
        Selection.activeGameObject = go;

    }

    Klak.Wiring.Patcher.PatcherWindow _window;

    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("Open Model", "LargeButton"))
			_window = Klak.Wiring.Patcher.PatcherWindow.OpenPatch((EDDModel)target);

    }

}

