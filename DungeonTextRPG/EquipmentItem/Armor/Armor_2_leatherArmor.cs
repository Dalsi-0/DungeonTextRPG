using System;

public class Armor_2_leatherArmor : EquipmentItem
{

    public override void InitSetting()
    {
        _Data.Code = this.GetType().Name;
        _Data.Name = "가죽 갑옷";
        _Data.Type = EquipmentType.Armor;
        _Data.PowerValue = 5;
        _Data.Description = "시간이 지나 자연스러운 색을 가진 질긴 가죽 갑옷";
        _Data.Price = 100;
        _Data.isSoldOut = false;
        _Data.isEquiped = false;
    }
}
