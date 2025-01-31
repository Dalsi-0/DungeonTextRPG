﻿using System;

public class OneHandedWeapon_5_steelShield : EquipmentItem
{
    public override void InitSetting()
    {
        _Data.Name = "강철 방패";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 2;
        _Data.Description = "튼튼하고 반짝이는 강철 방패.";
        _Data.Price = 50;
        _Data.IsSoldOut = false;
    }
}
