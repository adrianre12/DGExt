using DevionGames;
using DevionGames.QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGExt
{
    [Icon("Quest")]
    [ComponentMenu("Quest System/Quest Status To Blackboard")]
    [System.Serializable]
    public class QuestStatusToBlackboard : Action, ICondition
    {
        [QuestPicker(true)]
        [SerializeField]
        protected Quest _quest;
        [SerializeField]
        protected string _variableName = "QuestStatusId";
        [SerializeField]
        protected int _inactive = (int)Status.Inactive;
        [SerializeField]
        protected int _active = (int)Status.Active;
        [SerializeField]
        protected int _completed = (int)Status.Completed;
        [SerializeField]
        protected int _failed = (int)Status.Failed;
        [SerializeField]
        protected int _canceled = (int)Status.Canceled;

        public override void OnSequenceStart()
        {
            base.OnSequenceStart();
            if (blackboard == null)
                Debug.LogWarning("Blackboard not found");
        }

        public override ActionStatus OnUpdate()
        {
            int status = -1;
            if (QuestManager.current.HasQuest(_quest, out Quest quest))
            {
                switch (quest.Status)
                {
                    case Status.Inactive:
                        {
                            status = _inactive;
                            break;
                        }
                    case Status.Active:
                        {
                            status = _active;
                            break;
                        }
                    case Status.Completed:
                        {
                            status = _completed;
                            break;
                        }
                    case Status.Failed:
                        {
                            status = _failed;
                            break;
                        }
                    case Status.Canceled:
                        {
                            status = _canceled;
                            break;
                        }
                }
                    
                blackboard.SetValue<int>(_variableName, status);
            }
                
            return ActionStatus.Success;
        }
    }
}
