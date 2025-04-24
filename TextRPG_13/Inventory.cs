using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Inventory
    {
        [JsonIgnore]
        public Player owner { get; private set; }

        public List<ItemStack> items { get; set; } = new();
        public int Count => items.Count;
        public bool IsEmpty => !items.Any();

        public Inventory(){ }
        public Inventory(Player player)
        {
            owner = player;
        }

        public void SetOwner(Player player)
        {
            owner = player;
        }
        public List<Item> GetEquippedItems()
        {
            return items.Where(stack => stack.Item.IsEquipped).Select(stack => stack.Item).ToList();
        }

        public List<ItemStack> GetItems()
        {
            return items;
        }

        public void AddItem(Item newItem)
        {
            // 동일 아이템 존재 여부 확인
            var stack = items.FirstOrDefault(i => i.Item.Id == newItem.Id);
            if (stack != null)
            {
                stack.Add(1);  // 수량 증가
            }
            else
            {
                items.Add(new ItemStack(newItem, 1));
            }
        }

        public void RemoveItem(Item targetItem)
        {
            var stack = items.FirstOrDefault(i => i.Item.Id == targetItem.Id);
            if (stack != null)
            {
                stack.Remove(1);
                if (stack.Quantity <= 0)
                    items.Remove(stack);
            }
        }

        public void EquipItem(Item itemEquip)
        {
            // 이미 장착 중인 아이템을 다시 선택한 경우 → 해제
            if (itemEquip.IsEquipped)
            {
                itemEquip.IsEquipped = false;
                return;
            }
            //동일한 타입의 아이템은 한 개만 장착되게 처리
            foreach (var stack in items)
            {
                if (stack.Item.ItemCategory == itemEquip.ItemCategory && stack.Item.IsEquipped)
                {
                    stack.Item.IsEquipped = false;
                    break;
                }
            }
            itemEquip.IsEquipped = true;

            QuestManager.Instance.OnItemEquipped(itemEquip);
        }

        //기본적으로 존재하는 포션 생성
        public void AddInitialPotions()
        {
            for (int i = 0; i < 3; i++)
            {
                AddItem(ItemDatabase.Items.FirstOrDefault(item => item.Id == 100));
            }
        }

        //삭제 예정
        //초기 검  생성
        public void AddSword()
        {
            AddItem(ItemDatabase.Items.FirstOrDefault(item => item.Id == 4));
        }
    }
}
