using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class SaveManager
    {
        //private static readonly string path_ = "player.json";
        //private static readonly string questpath_ = "quest.json";
        //private static readonly string stagepath_ = "stage.json";
        public static void Save(Player player, string path = null)
        {
            if (path == null)
                path = Constants.PlayerFilePath;

            var op = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never
            };

            string json = JsonSerializer.Serialize(player, op);
            File.WriteAllText(path, json);

            Quest quest = QuestManager.Instance.CurrentQuest;

            if (quest != null && !quest.IsRewarded)
            {
                if (quest.Task is TaskMonster)
                    quest.TaskType = "Monster";
                else if (quest.Task is TaskEquip)
                    quest.TaskType = "Equip";

                string questJson = JsonSerializer.Serialize(quest, op);
                File.WriteAllText(Constants.QuestFilePath, questJson);
            }
            else
            {
                if (File.Exists(Constants.QuestFilePath))
                    File.Delete(Constants.QuestFilePath);
            }

            File.WriteAllText("stage.json", GameManager.Stage.CurrentStage.ToString());

        }
        public static void LoadStage()
        {
            if (File.Exists(Constants.StageFilePath))
            {
                string text = File.ReadAllText(Constants.StageFilePath);

                if (int.TryParse(text, out int savedStage))
                {
                    GameManager.Stage.SetStage(savedStage);
                }
            }
        }

        public static Player Load(string path = null)
        {
            if (path == null)
                path = Constants.PlayerFilePath;

            if (!File.Exists(Constants.PlayerFilePath))
            {
                return null;
            }

            string json = File.ReadAllText(path);

            Player player = JsonSerializer.Deserialize<Player>(json);

            if (player?.Stats != null)
            {
               player.Stats.SetOwner(player);
            }

            if (player?.Inven != null)
            {
                player.Inven.SetOwner(player);
            }

            player?.RestoreReferences();
            player?.ReStats();

            if (File.Exists(Constants.QuestFilePath))
            {
                QuestManager.Instance.LoadFromFile();
            }
            return player;
        }

        public static void Reset(string path = null)
        {
            if (path == null)
                path = Constants.PlayerFilePath;

            string[] files = {
                                         Constants.PlayerFilePath,              
                                         Constants.StageFilePath,      
                                         Constants.QuestFilePath,  
                                       
                                     };

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }

        public static bool Exists(string path = null)
        {
            if (path == null)
                path = Constants.PlayerFilePath;

            return File.Exists(path);
        }

        public static void ExitGame()
        {
            Console.WriteLine("");

            Console.ReadLine();
        }
    }
}
