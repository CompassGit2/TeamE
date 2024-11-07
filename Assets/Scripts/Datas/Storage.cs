using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Data{
    public static class Storage
    {
        /* 素材倉庫 -------------------------------------------------------------------------------- */
        public static List<MaterialStack> Materials
        {
            get => materials;
        }
        
        private static List<MaterialStack> materials = new List<MaterialStack>();


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


        /* 武器倉庫 -------------------------------------------------------------------------------- */
        public static List<Weapon> Weapons
        {
            get => weapons;
        }
        private static List<Weapon> weapons = new List<Weapon>();


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


        /* お金倉庫 -------------------------------------------------------------------------------- */
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
        private static int gold = 0;


        /* 商店在庫 -------------------------------------------------------------------------------- */
        public static List<MaterialStack> ShopMaterials
        {
            get => shopMaterials;
        }
        private static List<MaterialStack> shopMaterials = new List<MaterialStack>();

        public static void AddShopMaterial(MaterialData materialData, int amount)
        {
            // アイテムがすでにあるか確認
            MaterialStack stack = shopMaterials.Find(i => i.material == materialData);
            
            if (stack != null)
            {
                // 既存のアイテムがあれば所持数を増やす
                stack.amount += amount;
            }
            else
            {
                // なければ新しいアイテムを追加
                shopMaterials.Add(new MaterialStack(materialData, amount));
            }
        }

        public static void RemoveShopMaterial(MaterialData materialData, int amount)
        {
            MaterialStack stack = shopMaterials.Find(i => i.material == materialData);

            if (stack != null)
            {
                stack.amount -= amount;

                if (stack.amount <= 0)
                {
                    shopMaterials.Remove(stack); // 所持数が0以下になったら削除
                }
            }
        }
    }
}