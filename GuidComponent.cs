using System;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class GuidComponent : MonoBehaviour
{
    [SerializeField]
    private UniqueID _id;

    public string ID
    {
        get { return _id.Value; }
    }
    
    [Serializable]
    private struct UniqueID
    {
        public string Value;
    }

#if UNITY_EDITOR
    [ContextMenu("Force reset ID")]
    private void ResetId()
    {
        _id.Value = Guid.NewGuid().ToString();
        UpdateID();
    }

    //Need to check for duplicates when copying a gameobject/component
    public static bool IsUnique(string ID)
    {
        return Resources.FindObjectsOfTypeAll<GuidComponent>().Count(x => x.ID == ID) == 1;
    }

    protected void UpdateID()
    {
        //If scene is not valid, the gameobject is most likely not instantiated (ex. prefabs)
        if (!gameObject.scene.IsValid())
        {
            _id.Value = string.Empty;
            return;
        }

        if (string.IsNullOrEmpty(ID) || !IsUnique(ID))
        {
            ResetId();
        }
    }



    [UnityEditor.CustomPropertyDrawer(typeof(UniqueID))]
    private class UniqueIdDrawer : UnityEditor.PropertyDrawer
    {
        private const float buttonWidth = 120;
        private const float padding = 2;

        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = UnityEditor.EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            GUI.enabled = false;
            Rect valueRect = position;
            valueRect.width -= buttonWidth + padding;
            UnityEditor.SerializedProperty idProperty = property.FindPropertyRelative("Value");
            UnityEditor.EditorGUI.PropertyField(valueRect, idProperty, GUIContent.none);

            GUI.enabled = true;

            Rect buttonRect = position;
            buttonRect.x += position.width - buttonWidth;
            buttonRect.width = buttonWidth;
            if (GUI.Button(buttonRect, "Generate New ID"))
            {
                (property.serializedObject.targetObject as GuidComponent).UpdateID();
            }

            UnityEditor.EditorGUI.EndProperty();
        }
    }
#endif
}