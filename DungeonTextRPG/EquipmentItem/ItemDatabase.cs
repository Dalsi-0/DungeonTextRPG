using DungeonTextRPG.Manager.Inventory;
using System;

public class ItemDatabase
{
    private static ItemDatabase _instance;

    public static ItemDatabase instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ItemDatabase();
            }
            return _instance;
        }
    }

    private ItemDatabase()
    {
    }

    public Dictionary<string, EquipmentItem> Items = new Dictionary<string, EquipmentItem>
    {
        {"Armor_1_raggedClothes", new Armor_1_raggedClothes() },
        {"Armor_2_leatherArmor", new Armor_2_leatherArmor() },
        {"Armor_3_battleArmor", new Armor_3_battleArmor() },
        {"Armor_4_KnightArmor", new Armor_4_KnightArmor() },
        {"Legs_1_leatherBoots", new Legs_1_leatherBoots() },
        {"Legs_2_combatBoots", new Legs_2_combatBoots() },
        {"Legs_3_steelBoots", new Legs_3_steelBoots() },
        {"OneHandedWeapon_1_dagger", new OneHandedWeapon_1_dagger() },
        {"OneHandedWeapon_2_battleAxe", new OneHandedWeapon_2_battleAxe() },
        {"OneHandedWeapon_3_longSword", new OneHandedWeapon_3_longSword() },
        {"Shield_1_steelShield", new Shield_1_steelShield() },
        {"TwoHandedWeapon_1_greatSword", new TwoHandedWeapon_1_greatSword() },
        {"TwoHandedWeapon_2_battleSpear", new TwoHandedWeapon_2_battleSpear() }
    };

    public EquipmentItem GetItem(string itemKey)
    {
        if (Items.ContainsKey(itemKey))
        {
            // 원본 데이터를 복사해서 새 아이템 반환
            return Items[itemKey].CopyItem();
        }
        return null;
    }
}