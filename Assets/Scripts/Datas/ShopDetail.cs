namespace Data
{
    [System.Serializable]
    public class ShopDetail
    {
        /// <summary>
        /// 売店の素材に数量がいらない場合はこのクラスもいらなくなる
        /// </summary>
        public MaterialData material;
        public int amount;//数量はやはりいる
        public int initAmount;//初期の数量
        public bool isSoldOut;//売り切れかどうか

        public ShopDetail(MaterialData shopData)//数量がいらない場合のコンストラクタ
        {
            this.material = shopData;
            this.isSoldOut = false;
            //this.amount = amount;
        }
        public ShopDetail(MaterialData shopData, int amount)//数量がある場合のコンストラクタ
        {
            this.material = shopData;
            this.amount = amount;
            this.initAmount = amount;
            this.isSoldOut = false;
        }
    }
}
