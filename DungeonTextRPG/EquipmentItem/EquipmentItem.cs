using System;

public struct EquipmentData
{
    public string Name;
    public EquipmentType Type;
    public int PowerValue;
    public string Description;
    public int Price;
    public bool IsSoldOut;
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
    protected EquipmentData _Data;

    public abstract void InitSetting();

}
