using DevionGames;
using UnityEngine;

namespace DGExt
{
    [ComponentMenu("Blackboard/Set ToolTip")]
    public class SetBlackboardTooltip : Action
    {
        [HideInInspector]
        [SerializeField]
        protected string TitleName = "ToolTipTitle";
        [HideInInspector]
        [SerializeField]
        protected string InstructionName = "ToolTipInstruction";
        [SerializeField]
        protected string Title;
        [SerializeField]
        protected string Instruction;

        public override void OnSequenceStart()
        {
            base.OnSequenceStart();
            if (blackboard == null)
                Debug.LogWarning("Blackboard not found");
        }

        public override ActionStatus OnUpdate()
        {
            if (blackboard != null)
            {
                blackboard.SetValue<string>(TitleName, Title);
                blackboard.SetValue<string>(InstructionName, Instruction);
            }
            return ActionStatus.Success;
        }
    }
}
