using System.Xml.Linq;

public class ItemDatabase
{
    private static ItemDatabase _instance;

    public static ItemDatabase instance => _instance ??= new ItemDatabase();

    private ItemDatabase() { }

    public Dictionary<string, EquipmentItem> Items = new Dictionary<string, EquipmentItem>();

    public EquipmentItem GetItem(string itemKey)
    {
        // 원본 데이터를 복사해서 새 아이템 반환
        if (Items.ContainsKey(itemKey)) { return Items[itemKey].CopyItem(); }
        return null;
    }

    public void EquipmentFactory(EquipmentData _Data)
    {
        // 새로운 객체를 생성
        EquipmentItem clonedItem = new EquipmentItem();
        // 직접 _Data 필드 값을 설정
        clonedItem.SetEquipmentData(new EquipmentData()
        {
            Code = _Data.Code,
            Name = _Data.Name,
            Type = _Data.Type,
            PowerValue = _Data.PowerValue,
            Description = _Data.Description,
            Price = _Data.Price,
            isSoldOut = false,
            isEquiped = false
        });
        
        // 아이템을 Items 딕셔너리에 추가
        Items.Add(_Data.Code, clonedItem);
    }
}