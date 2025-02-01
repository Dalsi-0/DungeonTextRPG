using System;

public class Armor_3_battleArmor : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "전투용 갑옷";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 8;
        _Data.Description = "전쟁터에서 사용되던 흠집 많은 갑옷";
        _Data.Price = 300;
        _Data.isSoldOut = false;
    }
}
