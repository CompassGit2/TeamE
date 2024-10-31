using Data;//OrderData.csの中の名前空間
using System.Collections.Generic;
using System.Linq;
using Data.Database;

public static class ShopManager
{
    public static MaterialData MaterialOnSale { get; set; }
    private readonly static ShopDatabase shopDatabase; 
    public static List<ShopDetail> ShopItemList =>shopDatabase.GetShopList();
    //データベースから購入可能なアイテムを取得

    //所持金はここでは管理しない
    
    //※今作りたいのは商店で物買う際に数量を決める際に、今現存の在庫を超えられない仕組み

    //購入及び販売は処理はここ以外でやった方がいいかもしれない。
        
    //その後は直接Storageクラスのメソッドを呼び出すて「基地内倉庫」に入れるだけ

    ///全部ShopUIManagerに回しといた
    
} 