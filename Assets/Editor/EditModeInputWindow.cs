using UnityEditor;
using UnityEngine;

public class EditModeInputWindow : EditorWindow
{
    [MenuItem("Window/Edit Mode Input")]
    public static void ShowWindow()
    {
        GetWindow<EditModeInputWindow>("Edit Mode Input");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Perform Action"))
        {
            Debug.Log("Button clicked in Edit Mode!");
            
        }

        if(GUILayout.Button("Turn constrains ON")){
            PositionConstrains.redact = true;
            if(PositionConstrains.redact){
                Debug.Log("Edit On");
            }
        }

        if(GUILayout.Button("Turn constrains OFF")){
            PositionConstrains.redact = false;
            if(PositionConstrains.redact){
                Debug.Log("Edit false");
            }
        }

        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Space)
            {
                PositionConstrains.redact = !PositionConstrains.redact;
            if(PositionConstrains.redact){
                Debug.Log("Edit On");
            }else{
                Debug.Log("Edit Off");
            }
                Debug.Log("Space bar pressed in Edit Mode through Editor Window!");
            }
        }
    }
}
