using System;

public class TwoHandedWeapon_2_battleSpear : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Code = this.GetType().Name;
        _Data.Name = "전투용 창";
        _Data.Type = EquipmentType.Two_HandedWeapon;
        _Data.PowerValue = 7;
        _Data.Description = "긴 창 끝에 날카로운 철이 달린 무기";
        _Data.Price = 200;
        _Data.isSoldOut = false;
    }
}
