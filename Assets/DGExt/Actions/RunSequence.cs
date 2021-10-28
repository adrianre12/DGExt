using DevionGames;
using UnityEngine;

namespace DGExt
{
    [ComponentMenu("Sequence/Run Sequence")]
    public class RunSequence : Action 
    {
        [Tooltip("If SequenceId < 0 the sequence will allways run. Usefull for nested sequences.")]
        [SerializeField]
        protected int SequenceId;
        [Tooltip("The blackboard variable name")]
        [SerializeField]
        protected string SequenceIdName = "SequenceId";
        [Tooltip("Increment the SequenceId if this sequence compleates successfully. Will only increment if SeqenceId >= 0")]
        [SerializeField]
        protected bool IncrementOnSuccess = false;
        [SerializeField]
        protected ActionTemplate actionTemplate;

        private bool initalised;
        private Sequence actionBehavior;

        /* To get arround that base.Initalise() is not virtual run this code in the OnSequenceStart.
         // When the parent sequence is instantiated (at Start()) Initalize is called so do the same for the child.
        public override void Initialize(GameObject gameObject, PlayerInfo playerInfo, Blackboard blackboard)
        {
            initalised = false;
            base.Initialize(gameObject, playerInfo, blackboard);
            if (actionTemplate == null || actionTemplate.actions.Count == 0)
            {               
                return;
            }
            if (blackboard == null)
            {
                Debug.LogWarning("No blackboard found for RunSequence Action");
                return;
            }
            actionTemplate = Object.Instantiate(actionTemplate);
            actionBehavior = new Sequence(gameObject, playerInfo, blackboard, actionTemplate.actions.ToArray());
            initalised = true;
        }*/

        public override void OnSequenceStart()
        {
            if (!initalised)
            { // work arround
                if (actionTemplate == null || actionTemplate.actions.Count == 0)
                {
                    return;
                }
                if (blackboard == null)
                {
                    blackboard = gameObject.AddComponent<Blackboard>();
                }
                ActionTemplate at = Object.Instantiate(actionTemplate);
                actionBehavior = new Sequence(gameObject, playerInfo, blackboard, at.actions.ToArray());
                initalised = true;
            }
 //               return;
            base.OnSequenceStart();
            actionBehavior.Start();
        }

        public override void OnInterrupt()
        {
            if (!initalised)
                return;
            base.OnInterrupt();
            actionBehavior.Interrupt();
        }

        public override ActionStatus OnUpdate()
        {
            if (!initalised)
                return ActionStatus.Success;

            int currentSequenceId = blackboard.GetValue<int>(SequenceIdName);
            if (SequenceId >= 0 && currentSequenceId != SequenceId)
                return ActionStatus.Success;

            actionBehavior.Tick();
            if (SequenceId >= 0 && IncrementOnSuccess && actionBehavior.Status == ActionStatus.Success)
            {
                currentSequenceId++;
                blackboard.SetValue<int>(SequenceIdName, currentSequenceId);
            }

            return actionBehavior.Status;
        }

        public override void OnSequenceEnd()
        {
            if (!initalised)
                return;
            base.OnSequenceEnd();
            actionBehavior.Stop();
            initalised = false; // this is for the work arround
        }

    }
}
