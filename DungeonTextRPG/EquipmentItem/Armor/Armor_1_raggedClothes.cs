using System;

public class Armor_1_raggedClothes : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "누더기 옷";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 1;
        _Data.Description = "수차례 수선된 낡고 찢어진 옷";
        _Data.Price = 10;
        _Data.IsSoldOut = false;
        _Data.isEquiped = false;
    }
}
