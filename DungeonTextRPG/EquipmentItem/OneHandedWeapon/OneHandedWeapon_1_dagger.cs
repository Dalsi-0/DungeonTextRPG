using System;

public class OneHandedWeapon_1_dagger : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "단검";
        _Data.Type = EquipmentType.One_HandedWeapon;
        _Data.PowerValue = 3;
        _Data.Description = "날카롭고 은밀한 처치를 위한 작은 검";
        _Data.Price = 30;
        _Data.IsSoldOut = false;
    }
}
