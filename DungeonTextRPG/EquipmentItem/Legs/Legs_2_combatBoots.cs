using System;

public class Legs_2_combatBoots : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "전투 신발";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 4;
        _Data.Description = "빠르게 움직이도록 설계된 튼튼한 신발.";
        _Data.Price = 80;
        _Data.IsSoldOut = false;
    }
}
