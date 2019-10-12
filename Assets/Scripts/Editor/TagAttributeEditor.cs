using NewBark.Attributes;
using UnityEditor;
using UnityEngine;

namespace NewBark.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, "The property has to be a tag for TagAttribute to work!");
                return;
            }

            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
    }
}
