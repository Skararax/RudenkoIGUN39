class Unit
{
    private string name;
    private float health;
    private int damage;
    private float armor;

    public string Name { get { return name; } }
    public float Health { get { return health; } }
    public int Damage { get { return damage; } }
    public float Armor { get { return armor; } }

    public Unit() : this("Unknown Unit") 
    { 
    
    }

    public Unit(string unitName) 
    { 
        name = unitName;
        health = 100f;
        damage = 5;
        armor = 0.6f;
    }

    public float HealthArmor() 
    {
        return health * (1f + armor);
    }

    public bool SetDamage(int damage) 
    { 
        health -= damage * armor;

        if (health <= 0) 
        { 
            return true;
        }
        return false;
    }
}