using System;

public class Armor_4_KnightArmor : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "기사 갑옷";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 10;
        _Data.Description = "반짝이는 금속으로 만든 기사들의 갑옷.";
        _Data.Price = 500;
        _Data.IsSoldOut = false;
    }
}
