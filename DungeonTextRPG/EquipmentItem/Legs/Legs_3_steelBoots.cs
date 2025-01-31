using System;

public class Legs_3_steelBoots : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "강철 신발";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 6;
        _Data.Description = "무겁지만 발을 단단히 보호하는 신발.";
        _Data.Price = 150;
        _Data.IsSoldOut = false;
    }
}
