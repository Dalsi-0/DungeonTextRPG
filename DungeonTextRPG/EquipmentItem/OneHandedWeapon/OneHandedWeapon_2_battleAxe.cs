using System;

public class OneHandedWeapon_2_battleAxe : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Code = this.GetType().Name;
        _Data.Name = "전투용 도끼";
        _Data.Type = EquipmentType.One_HandedWeapon;
        _Data.PowerValue = 5;
        _Data.Description = "강력한 타격을 주는 거친 도끼";
        _Data.Price = 70;
        _Data.isSoldOut = false;
    }
}
