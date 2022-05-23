using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Train_Way))]
public class Train_Way_Editor : Editor
{
    #region SerializedProperty'ler
    private SerializedProperty railWays;
    #endregion
    private void OnEnable()
    {
        railWays = serializedObject.FindProperty("railWays");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Train_Way train_Way = (Train_Way)target;
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(railWays);
        if (EditorGUI.EndChangeCheck())
        {
            train_Way.SetRailWayActivited();
        }
        if (GUILayout.Button("Move Last Point For Station", GUILayout.Height(20)))
        {
            if (train_Way.RailWays[0].controlPoint.name == "Forward")
            {
                train_Way.RailWays[0].controlPoint.GetChild(1).localPosition = new Vector3(0, 0.6f, 0);
                train_Way.RailWays[0].controlPoint.GetChild(2).localPosition = new Vector3(0, 0.6f, 0.5f);
                train_Way.RailWays[0].controlPoint.GetChild(3).localPosition = new Vector3(0, 0.6f, 1);
            }
            else if (train_Way.RailWays[0].controlPoint.name == "RightTurn")
            {
                train_Way.RailWays[0].controlPoint.GetChild(3).localPosition = new Vector3(1, 0.6f, 0);
            }
            else if (train_Way.RailWays[0].controlPoint.name == "LeftTurn")
            {
                train_Way.RailWays[0].controlPoint.GetChild(3).localPosition = new Vector3(-1, 0.6f, 0);
            }
        }
        if (GUILayout.Button("Delete Unnecessery Object", GUILayout.Height(20)))
        {
            try
            {
                train_Way.DeleteUnnecessaryObject();
            }
            catch
            {
                Debug.LogWarning("Prefab'deki bir objeyi DESTROY edemezsin.");
            }
        }
        AssetDatabase.SaveAssets();
        serializedObject.ApplyModifiedProperties();
    }
}