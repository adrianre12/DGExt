using DevionGames;
using DevionGames.InventorySystem;
using DevionGames.UIWidgets;
using UnityEngine;

namespace DGExt
{
    public class ExamineTrigger : Trigger
    {
        [Header("Examine")]
        public KeyCode ExamineKey = KeyCode.E;
        [SerializeField]
        protected string _windowName = "Examine Window";
        protected ExamineWindow _ExamineWindow;
        [SerializeField]
        protected string _windowTitle = "Examine";
        [SerializeField]
        protected float _zoom = 1.0f;

        [HideInInspector]
        public bool WindowOpen;

        protected override void Start()
        {
            base.Start();

            _ExamineWindow = WidgetUtility.Find<ExamineWindow>(_windowName);
            if (_ExamineWindow == null)
                Debug.LogWarning("Missing window " + _windowName + " in scene!");
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(ExamineKey) && triggerType.HasFlag(TriggerInputType.Key) && InRange && IsBestTrigger())
            {
                if (WindowOpen)
                {
                    _ExamineWindow.Close();
                }
                else
                {
                    _ExamineWindow.RegisterListener("OnClose", (CallbackEventData eventData) =>
                    {
                        WindowOpen = false;
                    });
                    WindowOpen = true;
                    _ExamineWindow.Show(gameObject, _windowTitle, ExamineKey, _zoom);
                }
            }
        }
    }
}
