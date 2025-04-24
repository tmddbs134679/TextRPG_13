using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Inventory
    {
        private static List<Item> items;
        public int Count => items.Count;
        public bool IsEmpty => !items.Any();

        public Inventory()
        {
            items = new List<Item>();
        }

        public List<Item> GetEquippedItems()
        {
            return items.Where(i => i.IsEquipped).ToList();
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
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
            foreach (var item in items)
            {
                if (item.ItemCategory == itemEquip.ItemCategory && item.IsEquipped)
                {
                    item.IsEquipped = false;
                    break;
                }
            }
            itemEquip.IsEquipped = true;
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
