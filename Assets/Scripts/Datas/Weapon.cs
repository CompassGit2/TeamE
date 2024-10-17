namespace Data
{
    [System.Serializable]
    public class Weapon
    {
        public WeaponData weapon;
        public int bonus;
        public int attack;
        public int price;

        public Weapon(WeaponData weaponData, int bonus, int attack, int price)
        {
            this.weapon = weaponData;
            this.bonus = bonus;
            this.attack = attack;
            this.price = price;
        }
    }

}
