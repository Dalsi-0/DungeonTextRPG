using Newtonsoft.Json;
using System;

public struct EquipmentData
{
    public string Code;
    public string Name;
    public EquipmentType Type;
    public int PowerValue;
    public string Description;
    public int Price;
    public bool isSoldOut;
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


public class EquipmentItem
{
    [JsonProperty] protected EquipmentData _Data; // JsonProperty 속성으로 직렬화 가능

    public EquipmentItem() { }

    public EquipmentData GetEquipmentData() { return _Data; }

    public void SetEquippedState(bool isEquipped) { _Data.isEquiped = isEquipped; }

    public void SetSoldOutState(bool isSoldOut) { _Data.isSoldOut = isSoldOut; }
    public void SetEquipmentData(EquipmentData data) {  _Data = data; }
    public string GetTypeToString()
    {
        string type = "";
        switch (_Data.Type)
        {
            case EquipmentType.One_HandedWeapon:
            case EquipmentType.Shield:
                type = "한손 장비";
                break;

            case EquipmentType.Two_HandedWeapon: type = "두손 장비";
                break;

            case EquipmentType.Armor: type = "갑옷";
                break;

            case EquipmentType.Legs: type = "하의";
                break;
        }
        return type;
    }

    public EquipmentItem CopyItem()
    {
        // 새로운 객체를 생성하고, 수동으로 복사
        EquipmentItem clonedItem = (EquipmentItem)MemberwiseClone(); 
        clonedItem._Data = new EquipmentData()
        {
            Code = _Data.Code,
            Name = _Data.Name,
            Type = _Data.Type,
            PowerValue = _Data.PowerValue,
            Description = _Data.Description,
            Price = _Data.Price,
            isSoldOut = _Data.isSoldOut,
            isEquiped = _Data.isEquiped
        };
        return clonedItem;
    }


}
