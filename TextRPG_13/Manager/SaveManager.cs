using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class SaveManager
    {
        private static readonly string path_ = "player.json";
        private static readonly string questpath_ = "quest.json";

        public static void Save(Player player, string path = null)
        {
            if (path == null)
                path = path_;

            var op = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
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
                File.WriteAllText(questpath_, questJson);
            }
            else
            { 
                if (File.Exists(questpath_))
                        File.Delete(questpath_);
            }
        }


        public static Player Load(string path = null)
        {
            if (path == null)
                path = path_;

            if (!File.Exists(path_))
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

            if (File.Exists(questpath_))
            {
                QuestManager.Instance.LoadFromFile();
            }
            return player;
        }

        public static void Reset(string path = null)
        {
            if (path == null)
                path = path_;


            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool Exists(string path = null)
        {
            if (path == null)
                path = path_;

            //if (File.Exists(questpath_))
            //    File.Delete("quest.json");

            return File.Exists(path);
        }

        public static void ExitGame()
        {
            Console.WriteLine("");

            Console.ReadLine();
        }
    }
}
