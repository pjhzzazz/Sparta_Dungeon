using System;
using System.Reflection.Emit;
using System.IO;

namespace SpartaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string saveFilePath = "savegame.txt";
            
            Character character = new Character();
            Inventory inventory = new Inventory();
            EquipManageMent equip = new EquipManageMent();
            Shop shop = new Shop();
            PurchaseItem pur = new PurchaseItem();
            SellItem sel = new SellItem();
            Dungeon dungeon = new Dungeon();

            Item IronArmor = new Item("무쇠 갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1000, ItemType.Armor);
            Item SpartaSpear = new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000, ItemType.Weapon);
            Item OldSword = new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon);
            Item NoviceArmor = new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 500, ItemType.Armor);
            Item SpartaArmor = new Item("스파르타 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor);
            Item BronzeAxe = new Item("청동 도끼", 5, 0, "어딘가 사용 됐던 거 같은 도끼입니다.", 1500, ItemType.Weapon);
            Item HolySword = new Item("성기사의 검", 30, 0, "던전을 정복했던 누군가의 검입니다.", 10000, ItemType.Weapon);
            Item HolyArmor = new Item("성기사의 갑옷", 0, 30, "던전을 정복했던 누군가의 갑옷입니다.", 10000, ItemType.Armor);


            if (File.Exists(saveFilePath))
            {
                Console.WriteLine("저장된 게임이 있습니다.");
                Console.WriteLine("1. 새로운 게임 시작");
                Console.WriteLine("2. 저장된 게임 불러오기");

                bool isChoice = true;
                while (isChoice)
                {
                    Console.WriteLine("원하는 선택을 하세요: ");
                    Console.Write(">>");

                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {

                        if (choice == 1)
                        {
                            Console.WriteLine("새로운 게임을 시작합니다.");
                            Console.Write("이름을 입력해주세요 : ");
                            character.name = Console.ReadLine();
                            character.StartNewGame(inventory, saveFilePath);
                            isChoice = false;
                        }
                        else if (choice == 2)
                        {
                            Console.WriteLine("저장된 게임을 불러옵니다.");
                            LoadGame(character, saveFilePath, inventory.GetInventoryItem());
                            isChoice = false;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("새로운 게임을 시작합니다.");
                Console.Write("이름을 입력해주세요 :");
                character.name = Console.ReadLine();
                character.StartNewGame(inventory, saveFilePath);
            }

            shop.AddItem(NoviceArmor);
            shop.AddItem(IronArmor);
            shop.AddItem(SpartaArmor);
            shop.AddItem(OldSword);
            shop.AddItem(BronzeAxe);
            shop.AddItem(SpartaSpear);
            shop.AddItem(HolySword);
            shop.AddItem(HolyArmor);

            bool gamePlay = true;       
            
            while (gamePlay)
            {                
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.Write("\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n6. 게임 저장\n7. 게임 불러오기\n8. 게임 종료\n\n원하시는 행동을 입력해주세요.\n>>");

                int input;
                if (int.TryParse(Console.ReadLine(), out input))

                    switch (input)
                    {
                        case 1:
                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("상태 보기");
                                Console.WriteLine("캐릭터 정보가 표시됩니다.");
                                character.Status(inventory.GetInventoryItem()); // inventory.GetItem()을 매개변수로 이용해 인벤토리의 아이템 능력치를 캐릭터 정보에 적용시킴!
                                Console.WriteLine();
                                Console.WriteLine("0.나가기");
                                Console.WriteLine();
                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                Console.Write(">>");

                                int input1;

                                if (int.TryParse(Console.ReadLine(), out input1))
                                {
                                    if (input1 == 0) break;
                                }

                                else
                                {
                                    Console.WriteLine("숫자를 입력해주세요");
                                }
                            }
                            break;

                        case 2:
                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("인벤토리");
                                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                                Console.WriteLine();

                                inventory.ShowInventory();

                                Console.WriteLine();
                                Console.WriteLine("1. 장착 관리");
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                                Console.Write(">>");

                                int input2;

                                if (int.TryParse(Console.ReadLine(), out input2))
                                {
                                    if (input2 == 0) break;
                                }
                                else
                                {
                                    Console.WriteLine("숫자를 입력해주세요");
                                }

                                switch (input2)
                                {
                                    case 1:
                                        Console.WriteLine();
                                        Console.WriteLine("인벤토리 - 장착 관리");
                                        Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
                                        Console.WriteLine();
                                        equip.EquipItem(inventory.GetInventoryItem()); // inventory.GetItem()을 매개변수로 이용해 인벤토리의 아이템 리스트를 장착관리창으로!
                                        Console.WriteLine();
                                        break;
                                }
                            }
                            break;

                        case 3:
                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("상점");
                                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                                Console.WriteLine();
                                Console.WriteLine("[보유골드]");
                                Console.WriteLine($"{character.Gold} G");

                                shop.DisplayShop(inventory);

                                Console.WriteLine();
                                Console.WriteLine("1. 아이템 구매");
                                Console.WriteLine("2. 아이템 판매");
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                                Console.Write(">>");

                                int input3;

                                if (int.TryParse(Console.ReadLine(), out input3))
                                {
                                    if (input3 == 0) break;
                                }

                                else
                                {
                                    Console.WriteLine("숫자를 입력해주세요");
                                }

                                switch (input3)
                                {
                                    case 1:
                                        Console.WriteLine();
                                        Console.WriteLine("상점 - 아이템 구매");
                                        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                                        Console.WriteLine();
                                        pur.DisplayPurchaseItem(shop.GetShopItem(), character, inventory); // shop.GetItem()을 매개변수로 이용해 상점의 아이템 리스트를 구매 관리창으로!


                                        break;
                                    case 2:
                                        Console.WriteLine();
                                        Console.WriteLine("상점 - 아이템 판매");
                                        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                                        Console.WriteLine();
                                        sel.DisplaySellItem(inventory.GetInventoryItem(), character, inventory);

                                        break;
                                }
                            }
                            break;

                        case 4:
                            
                                Console.WriteLine();
                                Console.WriteLine("던전입장");
                                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                                dungeon.DungeonInfo(character);
                            if (character.Hp <= 0) gamePlay = false;                           
                            break;

                        case 5:
                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("휴식하기");
                                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 {character.Gold} G)");
                                Console.WriteLine();
                                Console.WriteLine("1. 휴식하기");
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                                Console.Write(">>");

                                int input5;

                                if (int.TryParse(Console.ReadLine(), out input5))
                                {
                                    if (input5 == 0) break;
                                }

                                else
                                {
                                    Console.WriteLine("숫자를 입력해주세요");
                                }

                                switch (input5)
                                {
                                    case 1:
                                        character.Rest();
                                        break;
                                }
                            }
                            break;

                        case 6:
                            
                            SaveGame(character, saveFilePath, inventory.GetInventoryItem());                           
                            break;

                        case 7:

                           
                            LoadGame(character, saveFilePath, inventory.GetInventoryItem());
                            break;

                        case 8:
                            gamePlay = false;
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;

                            
                    }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요");
                }
                
            }
        }
        class Character
        {
            
            public int level { get; set; } = 1;
            public string job { get; set; } = "전사";
            public string name { get; set; }
            public float Hp { get; set; } = 100;
            public float Gold { get; set; } = 1500;
            public float Attack { get; set; } = 10;
            public float Armor { get; set; } = 5;
            
            
            public float bonusAtk;
            public float bonusDef;

            public void StartNewGame(Inventory inventory, string filePath )
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);  // 저장된 파일 삭제
                }
                level = 1;
                Gold = 1500;
                Hp = 100;
                Attack = 10;
                Armor = 5;
                inventory.clear();  // 인벤토리 비우기
               
            }
            public float ItemAtk(List<Item> inventory) //아이템 리스트를 받기 위해 매개변수 활용
            {
                float itemAtk = 0;

                foreach (var item in inventory)
                {
                    if (item.isEquipped)
                    {
                        itemAtk += item.Atk;
                    }

                }
                return itemAtk;
            }
            public float ItemDef(List<Item> inventory)
            {
                float ItemDef = 0;
                foreach (var item in inventory)
                {
                    if (item.isEquipped)
                        ItemDef += item.Def;
                }
                return ItemDef;
            }

            public void Status(List<Item> item) //인벤토리에 있는 아이템 리스트를 받기 위해 매개변수 활용
            {
                 bonusAtk = ItemAtk(item);
                 bonusDef = ItemDef(item) ;
                Console.WriteLine();
                Console.WriteLine($"Lv. :{level:D2}"); //보간문자열을 통해서 숫자를 두자리로 표현
                Console.WriteLine("{0} ({1})", name, job);
                Console.WriteLine($"공격력 : {Attack+bonusAtk} + ({bonusAtk})");
                Console.WriteLine($"방어력 : {Armor+bonusDef} + ({bonusDef})");
                Console.WriteLine($"체 력 : {Hp}");
                Console.WriteLine($"Gold : {Gold} G");
            }

            public bool Purchase(Item item)
            {
                if (!item.isPurchased)
                {
                    if (Gold >= item.Price)
                    {
                        Gold -= item.Price;
                        Console.WriteLine();
                        Console.WriteLine("구매를 완료했습니다.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Gold가 부족합니다.");
                        return false;
                    }
                }
                Console.WriteLine("이미 구매한 아이템 입니다.");
                return false;


            }
            public void Sell(Item item, Inventory inventory)
            {
                if (inventory.HasItem(item))
                {
                    Gold += (item.Price * 17 / 20);
                    inventory.RemoveItem(item);
                    Console.WriteLine($"{item.Name}이 판매 되었습니다.");
                    item.isPurchased = false; // 아이템 판매 시 상점에 구매완료 표시 제거
                }
            }
            public void Rest()
            {
                if (Gold >= 500)
                {
                    if (Hp < 100)
                    {
                        Hp = 100;
                        Gold -= 500;
                        Console.WriteLine("휴식을 완료했습니다.");
                        Console.WriteLine($"잔여 골드 : {Gold} G");
                    }
                    else
                    {
                        Console.WriteLine("더 이상 회복할 수 없습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
          
        }
        enum ItemType
        {
            Weapon,
            Armor
        }

        class Item
        {
            public ItemType ItemType { get; set; }
            public bool isPurchased { get; set; }
            public bool isEquipped { get; set; }
            public string Name { get; set; }
            public float Atk { get; set; }
            public float Def { get; set; }
            public string Desc { get; set; }
            public float Price { get; set; }

            public Item(string name, float atk, float def, string desc, float price, ItemType itemType) //생성자를 이용해서 한번에
            {
                Name = name;
                Atk = atk;
                Def = def;
                Desc = desc;
                Price = price;
                ItemType = itemType;
            }

            public void ShowItem()
            {
                string equipped = isEquipped ? "[E]" : " "; // 3항연산자를 이용해서 isEquipped가 참이면 [E} 거짓이면 " "
                Console.WriteLine($"{equipped} {Name} | 공격력 +{Atk} | 방어력 +{Def} | {Desc}");
            }
        }
        class Inventory //인벤토리창
        {
            List<Item> items = new List<Item>();
           
            public bool HasItem(Item item)
            {
                return items.Contains(item);
            }
            public void AddItem(Item item) //아이템 추가 기능
            {
                item.isPurchased = true;
                items.Add(item);            //인벤토리에 아이템 추가 시 상점에 구매완료 표시
            }
            public void RemoveItem(Item item) //아이템 제거 기능
            {
                items.Remove(item);            
            }
            public void clear()
            {
                items.Clear();
            }
           
            public List<Item> GetInventoryItem() // 인벤토리 아이템 정보를 다른 곳으로 넘기기 위해 메서드 사용
            {
                return items;
            }
            public void ShowInventory()
            {

                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < items.Count; i++)
                {
                    Console.Write($"- ");
                    items[i].ShowItem();
                }
            }
        }

        class EquipManageMent  //장착관리창
        {
            public void EquipItem(List<Item> InventoryItems)
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");

                    for (int i = 0; i < InventoryItems.Count; i++)
                    {
                        Console.Write($"- {i + 1} ");
                        InventoryItems[i].ShowItem();
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해 주세요.");
                    Console.Write(">>");

                    int num;
                    if (int.TryParse(Console.ReadLine(), out num))
                    {
                        if (num == 0)
                        {
                            break;
                        }
                        else if (num > 0 && num <= InventoryItems.Count)
                        {

                            Item selectedItem = InventoryItems[num - 1];

                            if (selectedItem.isEquipped)
                            {
                                // 이미 장착 중이면 해제
                                selectedItem.isEquipped = false;
                            }
                            else
                            {
                                // 같은 타입의 다른 아이템 해제
                                foreach (var item in InventoryItems)
                                {
                                    if (item.ItemType == selectedItem.ItemType && item.isEquipped)
                                    {
                                        item.isEquipped = false;
                                    }
                                }
                                // 새 아이템 장착
                                selectedItem.isEquipped = true;
                            }
                        }
                    }                                                     
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }
        }
            class Shop
            {
                List<Item> items = new List<Item>();

                public void AddItem(Item item) //아이템 추가 기능
                {
                    items.Add(item);
                }

                public List<Item> GetShopItem() // 상점 아이템 정보를 다른 곳으로 넘기기 위해 메서드 사용
                {
                    return items;
                }

                public void DisplayShop(Inventory inventory)
                {
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");

                    for (int i = 0; i < items.Count; i++)
                    {
                        Item item = items[i];
                        string purchased = inventory.HasItem(item) ? "구매 완료" : $"{item.Price} G";//구매 완료 표시

                        Console.WriteLine($"-{item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc} | {purchased}");
                    }
                }
            }

            class PurchaseItem
            {
                public void DisplayPurchaseItem(List<Item> ShopItems, Character character, Inventory inventory)
                {
                    while (true)
                    {
                        Console.WriteLine("[보유 골드]");
                        Console.WriteLine($"{character.Gold}G");
                        Console.WriteLine();
                        Console.WriteLine("[아이템 목록]");

                        for (int i = 0; i < ShopItems.Count; i++)
                        {
                            Item item = ShopItems[i];
                            string purchased = inventory.HasItem(item) ? "구매 완료" : $"{item.Price} G"; //구매 완료 표시
                            Console.WriteLine($"- {i + 1} {item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc} | {purchased}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해 주세요.");
                        Console.Write(">>");

                    int num;
                    if (int.TryParse(Console.ReadLine(), out num))
                    {
                        if (num == 0)
                        {
                            break;
                        }
                        else if (num > 0 && num <= ShopItems.Count)
                        {
                            num -= 1;
                            if (character.Purchase(ShopItems[num]))
                            {
                                inventory.AddItem(ShopItems[num]);
                                ShopItems[num].isPurchased = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력해주세요.");
                    }               
                    }                   
                }
            }
            class SellItem
            {
                public void DisplaySellItem(List<Item> InventoryItems, Character character, Inventory inventory)
                {
                    while (true)
                    {
                        Console.WriteLine("[보유 골드]");
                        Console.WriteLine($"{character.Gold}G");
                        Console.WriteLine();
                        Console.WriteLine("[아이템 목록]");

                        for (int i = 0; i < InventoryItems.Count; i++)
                        {
                            Item item = InventoryItems[i];
                            Console.WriteLine($"- {i + 1} {item.Name} | 공격력 +{item.Atk} | 방어력 +{item.Def} | {item.Desc} | {item.Price * 17 / 20} "); //팔때 85%로
                        }
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해 주세요.");
                        Console.Write(">>");

                    int num;                  
                    if (int.TryParse(Console.ReadLine(), out num))
                    {
                        if (num == 0)
                        {
                            break;
                        }
                        else if (num > 0 && num <= InventoryItems.Count)
                        {
                            num -= 1;
                            Item Item = InventoryItems[num];
                            character.Sell(Item, inventory);                         
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력해주세요.");
                    }                      
                    }
                }
            }
        enum DungeonType
        {
            Easy   =1,
            Normal,
            Hard
        }
        class Dungeon
        {
            
            public DungeonType DungeonType { get; set; }
            public float RecommendedArmor { get; set; }
            public bool isSuccess { get; set; }

            public int Clear { get; set; }

            public void DungeonInfo(Character character)
            {  bool isShow = true;
                while (isShow)
                {  
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("1.쉬운 던전    | 방어력 5 이상 권장");
                    Console.WriteLine("2.일반 던전    | 방어력 11 이상 권장");
                    Console.WriteLine("3.어려운 던전    | 방어력 17 이상 권장");
                    Console.WriteLine("0.나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");

                    int num;
                    if (int.TryParse(Console.ReadLine(), out num))
                    {
                        switch (num)
                        {
                            case 1:
                                EntranceDungeon(character, DungeonType.Easy);
                                DungeonReward(character, DungeonType.Easy);

                                break;
                            case 2:
                                EntranceDungeon(character, DungeonType.Normal);
                                DungeonReward(character, DungeonType.Normal);

                                break;
                            case 3:
                                EntranceDungeon(character, DungeonType.Hard);
                                DungeonReward(character, DungeonType.Hard);

                                break;
                            case 0:
                                return;

                            default:
                                Console.WriteLine("잘못된 숫자입니다. 0~3 사이 숫자를 입력해주세요.");
                                break;

                        }
                        if (character.Hp <= 0)
                        {
                            isShow = false;
                            Console.WriteLine();
                            Console.WriteLine("Game Over");
                            Console.WriteLine("게임이 종료됩니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력해주세요.");
                    }
               
                }
            }
            
            public void EntranceDungeon( Character character, DungeonType dungeonType)
            {
                Random rand = new Random();
                int percent = rand.Next(1, 101);

                DungeonType = dungeonType;
                           
                    switch (dungeonType)

                    {

                        case DungeonType.Easy:
                            RecommendedArmor = 5;
                            break;

                        case DungeonType.Normal:
                            RecommendedArmor = 11;
                            break;

                        case DungeonType.Hard:
                            RecommendedArmor = 17;
                            break;

                    }
                    
     
                if(character.Armor + character.bonusDef >= RecommendedArmor)
                {
                    isSuccess = true;                   
                }
                
                else
                {
                    if (percent < 41)
                    {
                        isSuccess = false;                       
                        float previousHp = character.Hp;
                        character.Hp /= 2;
                        Console.WriteLine();
                        Console.WriteLine("던전 클리어 실패");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"체력  {previousHp} -> {character.Hp}");
                        Console.WriteLine();
                    }
                    else
                    {
                        isSuccess = false;
                        Console.WriteLine();
                        Console.WriteLine("던전 클리어 실패");
                        Console.WriteLine();
                    }
                }
            }

            public void DungeonReward(Character character, DungeonType dungeonType)
            {

                if (!isSuccess) return;
                    
                    DungeonType = dungeonType;
                    float previousHp = character.Hp;
                    float previouGold = character.Gold;
                    float TotalAtk = character.Attack + character.bonusAtk;
                    float TotalArm = character.Armor + character.bonusDef;
                    Random random = new Random();
                    float bonus = random.Next(0, 3);
                    float HpDecrease = random.Next(20, 36);

                    switch (dungeonType)
                    {
                        case DungeonType.Easy:                          
                            character.Gold += (1000 + 10 * (TotalAtk * bonus));
                            character.Hp -= (HpDecrease + (RecommendedArmor - TotalArm));                         
                            Console.WriteLine();
                            Console.WriteLine("던전 클리어");
                            Console.WriteLine("축하 합니다!!");
                            Console.WriteLine($"쉬운 던전을 클리어 하셨습니다");                            
                            break;
                        case DungeonType.Normal:                           
                            character.Gold += (1700 + 17 *(TotalAtk * bonus));
                            character.Hp -= (HpDecrease + (RecommendedArmor - TotalArm));                          
                            Console.WriteLine();
                            Console.WriteLine("던전 클리어");
                            Console.WriteLine("축하 합니다!!");
                            Console.WriteLine($"일반 던전을 클리어 하셨습니다");                          
                            break;
                        case DungeonType.Hard:                          
                            character.Gold += (2500 + 25 * (TotalAtk * bonus));
                            character.Hp -= (HpDecrease + (RecommendedArmor - TotalArm));                            
                            Console.WriteLine();
                            Console.WriteLine("던전 클리어");
                            Console.WriteLine("축하 합니다!!");
                            Console.WriteLine($"어려운 던전을 클리어 하셨습니다");                         
                            break;                     
                    }

                    Clear++;
                    LevelUp(character);
                    Console.WriteLine();
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력  {previousHp} -> {previousHp - HpDecrease}");
                    Console.WriteLine($"Gold  {previouGold} -> {character.Gold}");
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");

                    int num;
                    if(int.TryParse(Console.ReadLine(),out num))
                    {
                        if (num == 0) return;

                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }                  
                    
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }

                

            }
            public void LevelUp(Character character)
            {

                if (Clear >= character.level)
                {
                    character.level++;
                    Clear = 0;
                    character.Attack += 0.5f;
                    character.Armor += 1f;
                }

            }
        }


        static void SaveGame(Character character, string filePath, List<Item> inventory)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // 기본 캐릭터 정보 저장
                    writer.WriteLine($"Name: {character.name}");
                    writer.WriteLine($"Level: {character.level}");
                    writer.WriteLine($"Gold: {character.Gold}");
                    writer.WriteLine($"Hp: {character.Hp}");
                    writer.WriteLine($"Attack: {character.Attack}");
                    writer.WriteLine($"Armor: {character.Armor}");
                    
                    // 아이템 목록 저장
                    writer.WriteLine("Items:");
                    foreach (var item in inventory)
                    {
                        writer.WriteLine($"{item.Name},{item.Atk},{item.Def},{item.Desc},{item.Price},{item.isEquipped},{item.ItemType}");
                    }

                    Console.WriteLine("게임이 저장되었습니다.");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"게임 저장 중 오류 발생: {ex.Message}");
            }
        }

        static void LoadGame(Character character, string filePath, List<Item> inventory)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    bool isReadingItems = false;

                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();

                            switch (key)
                            {
                                case "Name":
                                    character.name = value;
                                    break;
                                case "Level":
                                    character.level = int.Parse(value);
                                    break;
                                case "Gold":
                                    character.Gold = float.Parse(value);
                                    break;
                                case "Hp":
                                    character.Hp = float.Parse(value);
                                    break;
                                case "Attack":
                                    character.Attack = float.Parse(value);
                                    break;
                                case "Armor":
                                    character.Armor = float.Parse(value);
                                    break;
                                case "Items":
                                    isReadingItems = true;
                                    break;
                            }
                        }
                        else if (isReadingItems)
                        {

                            string[] itemParts = line.Split(',');

                            if (itemParts.Length == 7)
                            {
                                string name = itemParts[0].Trim();
                                float atk = float.Parse(itemParts[1].Trim());
                                float def = float.Parse(itemParts[2].Trim());
                                string desc = Convert.ToString(itemParts[3].Trim());
                                float price = float.Parse(itemParts[4].Trim());
                                bool isEquipped = bool.Parse(itemParts[5].Trim());
                                ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), itemParts[6].Trim());

                                // 아이템 객체 생성
                                Item item = new Item(name, atk, def, desc, price, itemType);
                                item.isEquipped = isEquipped;

                                // 중복 아이템 체크 후 추가
                                bool isDuplicate = false;
                                foreach (var existingItem in inventory)
                                {
                                    if (existingItem.Name == item.Name && existingItem.ItemType == item.ItemType)
                                    {
                                        isDuplicate = true;
                                        break;
                                    }
                                }

                                // 중복되지 않으면 아이템 추가
                                if (!isDuplicate)
                                {
                                    inventory.Add(item);
                                }
                            }
                        }
                    }
                    Console.WriteLine("게임이 로드되었습니다.");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"게임 로드 중 오류 발생: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("저장된 게임이 없습니다.");
            }
        }
    }
    }


