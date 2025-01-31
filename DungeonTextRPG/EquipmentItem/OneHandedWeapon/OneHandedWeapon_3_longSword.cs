using System;

public class OneHandedWeapon_3_longSword : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "롱소드";
        _Data.Type = EquipmentType.One_HandedWeapon;
        _Data.PowerValue = 6;
        _Data.Description = "군인들이 사용하기 좋은 긴 검";
        _Data.Price = 150;
        _Data.IsSoldOut = false;
    }
}
