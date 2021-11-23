using DevionGames;
using UnityEngine;

namespace DGExt
{
    [ComponentMenu("Blackboard/Set Int Variable")]
    public class SetIntVariable : Action
    {
        [SerializeField]
        private string variableName = "IntId";
        [SerializeField]
        private int value = 0;

        public override void OnSequenceStart()
        {
            base.OnSequenceStart();
            if (blackboard == null)
                Debug.LogWarning("Blackboard not found");
        }

        public override ActionStatus OnUpdate()
        {
            blackboard.SetValue<int>(variableName, value);
            return ActionStatus.Success;
        }
    }
}
