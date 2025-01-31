using Newtonsoft.Json;
using System;

public struct EquipmentData
{
    public string Name;
    public EquipmentType Type;
    public int PowerValue;
    public string Description;
    public int Price;
    public bool IsSoldOut;
    public bool isEquiped;
}
public enum EquipmentType
{
    None,
    One_HandedWeapon, // 한손 무기
    Two_HandedWeapon, // 양손 무기
    Shield, // 방패
    Armor, // 갑옷
    Legs, // 하의
}


public abstract class EquipmentItem
{
    [JsonProperty] // JsonProperty 속성으로 직렬화 가능
    protected EquipmentData _Data;

    public EquipmentItem()
    {
        InitSetting();
    }

    public abstract void InitSetting();

    public EquipmentData GetEquipmentData()
    {
        return _Data;
    }

    public void SetEquippedState(bool isEquipped)
    {
        _Data.isEquiped = isEquipped;
    }


    public EquipmentItem CopyItem()
    {
        // 새로운 객체를 생성하고, 수동으로 복사
        EquipmentItem clonedItem = (EquipmentItem)MemberwiseClone(); 
        clonedItem._Data = new EquipmentData()
        {
            Name = _Data.Name,
            Type = _Data.Type,
            PowerValue = _Data.PowerValue,
            Description = _Data.Description,
            Price = _Data.Price,
            IsSoldOut = _Data.IsSoldOut,
            isEquiped = _Data.isEquiped
        };

        return clonedItem;
    }


}
