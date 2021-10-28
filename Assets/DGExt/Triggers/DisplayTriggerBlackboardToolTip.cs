using DevionGames;
using DevionGames.InventorySystem;
using DevionGames.UIWidgets;
using UnityEngine;

namespace DGExt
{
    [RequireComponent(typeof(Blackboard))]
    public class DisplayTriggerBlackboardToolTip : MonoBehaviour, ITriggerWentOutOfRange, ITriggerUsedHandler
    {
        [SerializeField]
        protected string m_Title;
        [SerializeField]
        protected string m_Instruction = "Pickup";

        protected TriggerTooltip m_Tooltip;
        protected BaseTrigger m_Trigger;
        protected string TitleName = "ToolTipTitle";
        protected string InstructionName = "ToolTipInstruction";
        protected Blackboard m_blackboard;

        private QuickOutline.Outline outline;

        private void Awake()
        {
            outline = GetComponent<QuickOutline.Outline>();
            if (outline is null)
            {
                outline = gameObject.AddComponent<QuickOutline.Outline>();
                outline.OutlineMode = QuickOutline.Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.red;
                outline.OutlineWidth = 1;
                outline.enabled = false;
            }
        }

        private void Start()
        {
            m_Trigger = GetComponentInChildren<BaseTrigger>(true);
            this.m_Tooltip = WidgetUtility.Find<TriggerTooltip>("Trigger Tooltip");
            m_blackboard = GetComponent<Blackboard>();
        }

        private void Update()
        {
            if (!this.m_Trigger.InUse && this.m_Trigger.InRange && InventoryManager.current.PlayerInfo.transform != null && this.m_Trigger.IsBestTrigger())
            {
                DoDisplayTooltip(true);
            }
        }

        protected virtual void DoDisplayTooltip(bool state)
        {
            outline.enabled = state;
            if (this.m_Tooltip == null) return;

            string title = m_blackboard.GetValue<string>(TitleName);
            if (title != null)
                this.m_Title = title;
            string instruction = m_blackboard.GetValue<string>(InstructionName);
            if (instruction != null)
                this.m_Instruction = instruction;

            if (state)
            {
                this.m_Tooltip.Show(this.m_Title, this.m_Instruction);
            }
            else
            {
                this.m_Tooltip.Close();
            }
        }

        private void OnDestroy()
        {
            DoDisplayTooltip(false);
        }

        public void OnWentOutOfRange(GameObject player)
        {
            DoDisplayTooltip(false);
        }

        public void OnTriggerUsed(GameObject player)
        {
            DoDisplayTooltip(false);
        }
    }
}