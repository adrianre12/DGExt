using DevionGames.UIWidgets;
using UnityEngine;
using UnityEngine.UI;

namespace DGExt
{
    public class ExamineTriggerTooltip : UIWidget
    {
        [Header("References")]
        [SerializeField]
        protected Text _title;
        [SerializeField]
        protected Text _actionKey;
        [SerializeField]
        protected Text _actionPrompt;
        [SerializeField]
        protected Text _examineKey;
        [SerializeField]
        protected Text _examinePrompt;

        public void Show(string title, KeyCode actionKey, string instruction, KeyCode examineKey, string examinePrompt) {
            _title.text = title;
            _actionKey.text = actionKey.ToString();
            _actionPrompt.text = instruction;
            _examineKey.text = examineKey.ToString();
            _examinePrompt.text = examinePrompt;
            base.Show();
        }
    }
}