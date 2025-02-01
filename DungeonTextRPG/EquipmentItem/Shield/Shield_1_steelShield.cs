using System;

public class Shield_1_steelShield : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Code = this.GetType().Name;
        _Data.Name = "강철 방패";
        _Data.Type = EquipmentType.Shield;
        _Data.PowerValue = 2;
        _Data.Description = "튼튼하고 반짝이는 강철 방패";
        _Data.Price = 50;
        _Data.isSoldOut = false;
    }
}
