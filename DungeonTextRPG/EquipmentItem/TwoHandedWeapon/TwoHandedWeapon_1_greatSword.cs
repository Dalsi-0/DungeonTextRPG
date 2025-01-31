using System;

public class TwoHandedWeapon_1_greatSword : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "그레이트소드";
        _Data.Type = EquipmentType.Two_HandedWeapon;
        _Data.PowerValue = 10;
        _Data.Description = "거대한 칼날이 돋보이는 대검.";
        _Data.Price = 400;
        _Data.IsSoldOut = false;
    }
}
