using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts._Shared.ScriptableObjects
{
    [ShowOdinSerializedPropertiesInInspector]
    public class SerializedScriptableObjectAutoInstaller<T> : ScriptableObjectAutoInstaller<T>, ISerializationCallbackReceiver
        where T : SerializedScriptableObjectAutoInstaller<T>
    {
        [SerializeField]
        [HideInInspector]
        private SerializationData serializationData;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (this.SafeIsUnityNull())
                return;
            UnitySerializationUtility.DeserializeUnityObject(this, ref serializationData);
            OnAfterDeserialize();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (this.SafeIsUnityNull())
                return;
            OnBeforeSerialize();
            UnitySerializationUtility.SerializeUnityObject(this, ref serializationData);
        }

        protected virtual void OnAfterDeserialize()
        {
        }

        protected virtual void OnBeforeSerialize()
        {
        }
        
#if UNITY_EDITOR
        [HideInTables]
        [OnInspectorGUI]
        [PropertyOrder(-2.1474836E+09f)]
        private void InternalOnInspectorGUI()
        {
            EditorOnlyModeConfigUtility.InternalOnInspectorGUI(this);
        }
#endif
    }
}