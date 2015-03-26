using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
    [TaskCategory("Basic/Transform")]
    [TaskDescription("Stores the euler angles of the Transform. Returns Success.")]
    public class GetEulerAngles : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The euler angles of the Transform")]
        [RequiredField]
        public SharedVector3 storeValue;

        private Transform targetTransform;

        public override void OnStart()
        {
            targetTransform = GetDefaultGameObject(targetGameObject.Value).GetComponent<Transform>();
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform == null) {
                Debug.LogWarning("Transform is null");
                return TaskStatus.Failure;
            }

            storeValue.Value = targetTransform.eulerAngles;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            storeValue = Vector3.zero;
        }
    }
}