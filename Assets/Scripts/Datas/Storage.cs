using System;
using System.Collections.Generic;

namespace Data{
    public static class Storage
    {
        public static List<MaterialStack> Materials
        {
            get => materials;
        }
        
        private static List<MaterialStack> materials = new List<MaterialStack>();

        public static List<Weapon> Weapons
        {
            get => weapons;
        }
        private static List<Weapon> weapons = new List<Weapon>();

        public static int Gold
        {
            get => gold;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Goldは負数になれません.");
                }

                gold = value;
            }
        }
        private static int gold = 500;

        /// <summary>
        /// 倉庫に素材を格納する
        /// </summary>
        /// <param name="materialData">格納する素材のデータ</param>
        /// <param name="amount">格納する個数</param>
        public static void AddMaterial(MaterialData materialData, int amount)
        {
            // アイテムがすでにあるか確認
            MaterialStack stack = materials.Find(i => i.material == materialData);
            
            if (stack != null)
            {
                // 既存のアイテムがあれば所持数を増やす
                stack.amount += amount;
            }
            else
            {
                // なければ新しいアイテムを追加
                materials.Add(new MaterialStack(materialData, amount));
            }
        }
        
        /// <summary>
        /// 倉庫から素材を削除する
        /// </summary>
        /// <param name="materialData">削除する素材のデータ</param>
        /// <param name="amount">削除する個数</param>
        public static void RemoveMaterial(MaterialData materialData, int amount)
        {
            MaterialStack stack = materials.Find(i => i.material == materialData);

            if (stack != null)
            {
                stack.amount -= amount;

                if (stack.amount <= 0)
                {
                    materials.Remove(stack); // 所持数が0以下になったら削除
                }
            }
        }

        public static void AddWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
        }

        public static void RemoveWeapon(Weapon weapon)
        {
            weapons.Remove(weapon);
        }

        public static void RemoveWeaponById(int id)
        {
            Weapon weapon = weapons.Find(i => i.Id == id);
            if(weapon != null)
            {
                weapons.Remove(weapon);
            }
        }
    }
}