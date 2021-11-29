using DevionGames;
using DevionGames.InventorySystem;
using DevionGames.UIWidgets;
using UnityEngine;


namespace DGExt
{
    public class DisplayExamineTriggerTooltip : MonoBehaviour, ITriggerWentOutOfRange, ITriggerUsedHandler
    {
        [SerializeField]
        protected string _title;
        [SerializeField]
        protected string _actionPrompt = "Pickup";
        [SerializeField]
        protected string _examinePrompt = "Examine";

        protected ExamineTriggerTooltip _tooltip;
        protected ExamineTrigger _trigger;


        private void Start()
        {
            _trigger = GetComponentInChildren<ExamineTrigger>(true);
            _tooltip = WidgetUtility.Find<ExamineTriggerTooltip>("Examine Trigger Tooltip");
        }

        private void Update()
        {
            if (!_trigger.InUse && _trigger.InRange && _trigger.IsBestTrigger())
            {
                DoDisplayTooltip(!_trigger.Viewing);
            }
        }

        protected virtual void DoDisplayTooltip(bool state)
        {
            if (_tooltip == null) return;

            if (state)
            {
                _tooltip.Show(_title,_trigger.key, _actionPrompt, _trigger.ExamineKey, _examinePrompt);
            }
            else
            {
                _tooltip.Close();
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