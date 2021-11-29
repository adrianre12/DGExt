using DevionGames;
using DevionGames.InventorySystem;
using DevionGames.UIWidgets;
using UnityEngine;

namespace DGExt
{
    public class ExamineTrigger : Trigger
    {
        [Header("Examine")]
        [SerializeField]
        protected string _WindowName = "Examine Window";
        protected ExamineWindow _ExamineWindow;
        
        [HideInInspector]
        public bool Viewing;

        public KeyCode ExamineKey = KeyCode.E;

        protected override void Start()
        {
            base.Start();

            _ExamineWindow = WidgetUtility.Find<ExamineWindow>(_WindowName);
            if (_ExamineWindow == null)
                Debug.LogWarning("Missing window " + _WindowName + " in scene!");
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(ExamineKey) && triggerType.HasFlag(TriggerInputType.Key) && InRange) // && IsBestTrigger())
            {
                if (Viewing)
                {
                    _ExamineWindow.Close();
                }
                else
                {
                    _ExamineWindow.RegisterListener("OnClose", (CallbackEventData eventData) =>
                    {
                        Viewing = false;
                    });
                    _ExamineWindow.Show(gameObject);
                    Viewing = true;
                }
            }
        }
    }
}
