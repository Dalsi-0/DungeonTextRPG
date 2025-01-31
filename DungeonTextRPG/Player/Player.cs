using System;
using System.Xml.Linq;
public enum Job
{
    None,
    Warrior,    // 전사
    Knight,     // 기사
    Mercenary   // 용병
}


public class Player
{
    // 레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold
    public int Level { get; set; }
    public string Name { get; set; }
    public Job PlayerJob { get; set; }
    public float StatAttack { get; set; }
    public float StatDefense { get; set; }
    public float Health { get; set; }
    public int GoldAmount { get; set; }

    public Player(int level, string name, Job job, float attack, float defense, float health, int gold)
    {
        Level = level;
        Name = name;
        PlayerJob = job;
        StatAttack = attack;
        StatDefense = defense;
        Health = health;
        GoldAmount = gold;
    }
}
