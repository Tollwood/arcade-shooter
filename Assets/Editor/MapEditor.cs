using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (MapGenerator))]
public class MapEditor : Editor {

    public override void OnInspectorGUI()
    {
        MapGenerator map = target as MapGenerator;
        if(DrawDefaultInspector()){
            map.GenerateMap(FindObjectOfType<Game>().levels[0]);    
        }

        if(GUILayout.Button("Generate Map")){
            map.GenerateMap(FindObjectOfType<Game>().levels[0]);
        }
    }
}
