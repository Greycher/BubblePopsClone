using System.Reflection;
using BubblePopsClone.Runtime;
using SceneGUIAttributes.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace BubblePopsClone.Editor
{
    public class LocalBoundsHandleFieldAttributeDrawer : GenericSceneGUIFieldAttributeDrawer<LocalBoundsHandleAttribute>
    {
        private BoxBoundsHandle _boundsHandle = new BoxBoundsHandle();
        
        protected override bool ShouldDraw(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType != typeof(Bounds))
            {
                Debug.LogError($"{nameof(LocalBoundsHandleAttribute)} should only be used on bounds typed fields!");
                return false;
            }

            return true;
        }

        protected override void InternalDuringSceneGui(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, LocalBoundsHandleAttribute attribute)
        {
            var bounds = (Bounds)fieldInfo.GetValue(monoBehaviour);
            var tr = monoBehaviour.transform;
            _boundsHandle.center = tr.TransformPoint(bounds.center);
            _boundsHandle.size = tr.TransformVector(bounds.size);
            
            var oldColor = Handles.color;
            Handles.color = attribute.GetColor();
            _boundsHandle.DrawHandle();
            Handles.color = oldColor;
            
            bounds.center = tr.InverseTransformPoint(_boundsHandle.center);
            bounds.size = tr.InverseTransformVector(_boundsHandle.size);
            fieldInfo.SetValue(monoBehaviour, bounds);
        }
    }
}