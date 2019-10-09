using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, "The property has to be a layer for LayerAttribute to work!");
            return;
        }

        property.intValue = EditorGUI.LayerField(position, label, property.intValue);
    }
}
