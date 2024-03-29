using DevionGames;
using DevionGames.UIWidgets;
using DevionGames.QuestSystem;
using System.Linq;
using UnityEngine;  

namespace DGExt
{
    [Icon("Quest")]
    [ComponentMenu("Quest System/Open Quest Window")]
    public class QuestOpen : Action
    {
        public static QuestWindow currentUsedWindow;

        private PlayerInfo PlayerInfo => QuestManager.current.PlayerInfo;

        [Header("Selection")]
        [SerializeField]
        protected string m_Title = "Availible Quests";
        [SerializeField]
        protected string m_Text = "Select a quest to continue.";
        [SerializeField]
        protected bool SelectFirstQuest = false;

        protected QuestCollection m_QuestCollection;
        protected bool InUse;
        protected bool SelectInUse;

        public override void OnSequenceStart()
        {
            this.m_QuestCollection = gameObject.GetComponent<QuestCollection>();
        }

        public override void OnStart()
        {
            //Can the trigger be used?
            if (!CanUse())
                return;

            currentUsedWindow = QuestManager.UI.questWindow;

            for (int i = 0; i < this.m_QuestCollection.Count; i++)
            {
                Quest quest = this.m_QuestCollection[i];
                if (quest.CanComplete())
                {
                    currentUsedWindow.RegisterListener("OnClose", (CallbackEventData eventData) =>
                    {
                        InUse = false;
                    });
                    currentUsedWindow.Show(quest);
                    InUse = true;
                    return;
                }
            }

            if (SelectFirstQuest) // dont go further if we have an active quest.
            {
                for (int i = 0; i < this.m_QuestCollection.Count; i++)
                {
                    Quest quest = this.m_QuestCollection[i];
                    if (quest.Status == Status.Active)
                        return;
                }
            }

            string[] quests = this.m_QuestCollection.Where(x => x.CanActivate()).Select(y => y.Name).ToArray();
            if (quests.Length == 0)
                return;

            if (quests.Length == 1 || SelectFirstQuest)
            {
                currentUsedWindow.RegisterListener("OnClose", (CallbackEventData eventData) =>
                {
                    InUse = false;
                });
                currentUsedWindow.Show(this.m_QuestCollection.FirstOrDefault(x => x.Name == quests[0]));
                InUse = true;
                return;
            }
            else // if (quests.Length > 1)
            {
                DialogBox questSelection = QuestManager.UI.questSelectionWindow;
                //Debug.Log(questSelection);
                questSelection.RegisterListener("OnClose", (CallbackEventData eventData) =>
                {
                    SelectInUse = false;
                });

                SelectInUse = true;
                questSelection.Show(this.m_Title, this.m_Text, (int result) =>
                {
                    currentUsedWindow.Show(this.m_QuestCollection.FirstOrDefault(x => x.Name == quests[result]));
                    InUse = true;
                }, quests);
            }
        }

        public override ActionStatus OnUpdate()
        {
            return InUse ? ActionStatus.Running : ActionStatus.Success;
        }

        private bool CanUse()
        {
            Quest quest = GetNextQuest();
            return !InUse && !SelectInUse && quest != null;
        }

        private Quest GetNextQuest()
        {
            //First check a quest can be completed.
            for (int i = 0; i < this.m_QuestCollection.Count; i++)
            {
                Quest quest = this.m_QuestCollection[i];
                if (quest.CanComplete())
                    return quest;
            }
            //Check if a quest can be activated.
            for (int i = 0; i < this.m_QuestCollection.Count; i++)
            {
                Quest quest = this.m_QuestCollection[i];
                if (quest.CanActivate())
                    return quest;
            }
            return null;
        }
    } 
}
