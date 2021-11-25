using DevionGames;
using UnityEngine;

namespace DGExt
{
    [ComponentMenu("Blackboard/If by bool set int")]
    public class IfBoolSetInt : Action
    {
        [SerializeField]
        protected string checkVariableName;
        [SerializeField]
        protected bool toggle;
        [SerializeField]
        protected string setIntName = "IntId";
        [SerializeField]
        protected int trueValue = 0;
        [SerializeField]
        protected int falseValue = 0;
        private bool OK;

        public override void OnSequenceStart()
        {
            OK = true;
            if (blackboard == null)
            {
                Debug.LogWarning("Blackboard not found");
                OK = false;
            }
            if (checkVariableName == null || checkVariableName == "")
                OK = false;
            if (setIntName == null || setIntName == "")
                OK = false;
            if (!OK)
                return;
            base.OnSequenceStart();
        }

        public override ActionStatus OnUpdate()
        {
            if (!OK)
                return ActionStatus.Success;
            bool value = blackboard.GetValue<bool>(checkVariableName);

            blackboard.SetValue<int>(setIntName, value ? trueValue : falseValue);

            if (toggle)
            {
                blackboard.SetValue<bool>(checkVariableName,!value);
            }
            return ActionStatus.Success;
        }
    }
}
