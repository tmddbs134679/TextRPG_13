using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Text.Json;

namespace TextRPG_13
{
    public class QuestManager
    {
        private static QuestManager instance;
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuestManager();
                return instance;
            }
        }

        private QuestManager()
        {
            
        }

        private Quest quest = new Quest();
        public Quest CurrentQuest { get; set; }

        public bool IsQuesting => CurrentQuest != null;

        public void AddQuest(Quest quest)
        {
            if (CurrentQuest == null)
                CurrentQuest = quest;
        }

        public void Reward(Player player)
        {
            if (CurrentQuest == null || !CurrentQuest.IsCompleted || CurrentQuest.IsRewarded)
                return;

            CurrentQuest.RewardPlayer(player);
            CurrentQuest = null;
        }

        public void OnItemEquipped(Item item)
        {
            if (CurrentQuest?.Task is TaskEquip task)
            {
                task.ProgressEquip(item);
            }
        }

        public void LoadFromFile()
        {
            string json = File.ReadAllText(Constants.QuestFilePath);

            using var doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            string questName = root.GetProperty("QuestName").GetString();
            string taskType = root.GetProperty("TaskType").GetString(); 

            // 보상 역직렬화
            QuestReward reward = JsonSerializer.Deserialize<QuestReward>(root.GetProperty("Reward").ToString());


            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            };


            // Task 수동으로 분기해서 역직렬화
            IQuestTask task = taskType switch
            {
                "Monster" => JsonSerializer.Deserialize<TaskMonster>(root.GetProperty("Task").ToString(), options),
                "Equip" => JsonSerializer.Deserialize<TaskEquip>(root.GetProperty("Task").ToString(), options),
                _ => throw new Exception($"알 수 없는 TaskType: {taskType}")
            };

            // 최종 퀘스트 구성
            Quest loaded = new Quest
            {
                QuestName = questName,
                Task = task,
                Reward = reward,
                TaskType = taskType
            };

            QuestManager.instance.CurrentQuest = loaded;
            }

    }
}