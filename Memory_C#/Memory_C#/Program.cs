class Unit
{
    private string name;
    private float health;
    private Interval damageInterval;
    private float armor;

    public string Name { get { return name; } }
    public float Health { get { return health; } }
    public Interval DamageInterval { get { return damageInterval; } }
    public float Armor { get { return armor; } }

    public Unit() : this("Unknown Unit")
    {
    }

    public Unit(string unitName)
    {
        name = unitName;
        health = 100f;
        damageInterval = new Interval(0, 5);
        armor = 0.6f;
    }

    public Unit(string unitName, int minDamage, int maxDamage)
    {
        name = unitName;
        health = 100f;
        damageInterval = new Interval(minDamage, maxDamage);
        armor = 0.6f;
    }

    public float HealthArmor()
    {
        return health * (1f + armor);
    }

    public bool TakeDamage(int damage)
    {
        health -= damage * (1f - armor);

        if (health <= 0)
        {
            return true;
        }
        return false; 
    }

    public int Attack()
    {
        return damageInterval.Get(); 
    }
}

class Weapon
{

    private string _name;
    private Interval _damageInterval;
    private float _duranilaty;

    public string Name { get { return _name; } }
    public Interval DamageInterval { get { return _damageInterval; } }
    public float Duranilaty { get { return _duranilaty; } }

    public Weapon() : this("Unknown Weapon")
    {

    }

    public Weapon(string name)
    {
        _name = name;
        _damageInterval = new Interval(1, 7);
        _duranilaty = 1f;
    }

    public Weapon(string name, int minDamage, int maxDamage) : this(name)
    {
        _damageInterval = new Interval(minDamage, maxDamage);
    }

    public int GetDamage()
    {   
        return _damageInterval.Get();
    }
}

struct Interval
{
    private int _min;
    private int _max;
    private Random _random;

    public int Min { get { return _min; } }
    public int Max { get { return _max; } }

    public Interval(int minValue, int maxValue)
    {
        int validatedMin = minValue;
        int validatedMax = maxValue;

        if (validatedMin < 0)
        {
            validatedMin = 0;
            Console.WriteLine("Некорректные данные! Число не может быть отрицательным!");
        }

        if (validatedMax < 0)
        {
            validatedMax = 0;
            Console.WriteLine("Некорректные данные! Число не может быть отрицательным!");
        }

        if (validatedMin > validatedMax)
        {
            int temp = validatedMin;
            validatedMin = validatedMax;
            validatedMax = temp;
            Console.WriteLine("Некорректные данные! Минимальное больше максимального!");
        }

        if (validatedMin == validatedMax)
        {
            validatedMax += 10;
            Console.WriteLine("Некорректные данные! Числа не могут быть равны!");
        }

        _min = validatedMin;
        _max = validatedMax;
        _random = new Random();
    }

    public int Get()
    {
        return _random.Next(_min, _max + 1);
    }
}

struct Room
{
    public Unit _unit;
    public Weapon _weapon;

    public Room(Unit unit, Weapon weapon)
    {
        _unit = unit;
        _weapon = weapon;
    }
}

class Dungeon
{
    private Room[] _rooms;

    public Dungeon()
    {
        _rooms = new Room[]
        {
            new Room(new Unit("Скелет"), new Weapon("Лук", 3,7)),
            new Room(new Unit("Орк"), new Weapon("Дубина", 5,10)),
            new Room(new Unit("Человек"), new Weapon("Меч"))
        };
    }

    public void ShowRooms()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            var room = _rooms[i];
            Console.WriteLine("Unit of room " + room._unit.Name);
            Console.WriteLine("Weapon of room " + room._weapon.Name);
            Console.WriteLine("---");
        }
    }
}

class Program 
{
    static void Main() 
    { 
        Dungeon dungeon = new Dungeon();

        dungeon.ShowRooms();
    }
}