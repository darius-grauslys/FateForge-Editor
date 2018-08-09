using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge.Managers
{
    public static class QuestManager
    {
        private static List<QuestEditor> _questEditors = new List<QuestEditor>();

        /// <summary>
        /// All control references to user made Quests.
        /// No need to add quest control to the manager, automatically done in constructor.
        /// </summary>
        private static List<QuestEditor> QuestEditors { get => _questEditors; set => _questEditors = value; }

        /// <summary>
        /// Get a quest name based on quest count and avoids conflicting names.
        /// </summary>
        /// <returns></returns>
        public static string GetNewQuestName()
        {
            string newNameAttempt = "quest_" + QuestEditors.Count;
            while (IsNameExisting(newNameAttempt))
            {
                newNameAttempt = "_" + newNameAttempt;
            }
            return newNameAttempt;
        }

        public static bool IsNameExisting(string testName)
        {
            return QuestEditors.Exists( (q) => { return q.QuestName == testName; });
        }

        public static bool IsOtherNameExisting(QuestEditor _q)
        {
            return QuestEditors.Exists((q) => { return (q.QuestName == _q.QuestName && q != _q); });
        }

        /// <summary>
        /// Do not call this function. It has handled by constructors.
        /// </summary>
        /// <param name="_q"></param>
        public static void AddNewQuest(QuestEditor _q)
        {
            if (IsNameExisting(_q.QuestName))
                throw new ArgumentException("Quest was already added to manager. Do not invoke QuestManager.AddNewQuest!");

            QuestEditors.Add(_q);
        }
    }
}
