using System;

public class Legs_1_leatherBoots : EquipmentItem
{

    public override void InitSetting()
    {
        _Data.Name = "가죽 신발";
        _Data.Type = EquipmentType.Legs;
        _Data.PowerValue = 2;
        _Data.Description = "자주 마모되었지만 여전히 편안한 신발.";
        _Data.Price = 20;
        _Data.IsSoldOut = false;
    }
}
