using DevionGames;
using DevionGames.QuestSystem;
using DevionGames.UIWidgets;
using UnityEngine;

namespace DGExt
{
    [Icon("Quest")]
    [ComponentMenu("Quest System/Open Quest Talk Window")]
    [System.Serializable]
    public class QuestTalkOpen : Action
    {
        [SerializeField]
        protected string _WindowName = "Quest Talk";
        [SerializeField]
        protected string _Title = "Talk";
        [SerializeField]
        protected string _Text = "";

        protected QuestTalkWindow _TalkQuestWindow;
        protected bool _inUse;
        protected ActionStatus status = ActionStatus.Inactive;

        public override void OnStart()
        {
            _TalkQuestWindow = WidgetUtility.Find<QuestTalkWindow>(_WindowName);
            if (_TalkQuestWindow == null)
                Debug.LogWarning("Missing window " + _WindowName + " in scene!");
            _TalkQuestWindow.RegisterListener("OnClose", (CallbackEventData eventData) =>
            {
                _inUse = false;
            });
            _TalkQuestWindow.Show(_Title, _Text);
            _inUse = true;
        }

        public override ActionStatus OnUpdate()
        {

            if (_TalkQuestWindow == null)
                return ActionStatus.Failure;

            return _inUse ? ActionStatus.Running : ActionStatus.Success;
        }

    }
}
