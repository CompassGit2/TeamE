using Data;//OrderData.cs�̒��̖��O���
using System.Collections.Generic;
using System.Linq;
using Data.Database;

public static class ShopManager
{
    public static MaterialData MaterialOnSale { get; set; }
    private readonly static ShopDatabase shopDatabase; 
    public static List<ShopDetail> ShopItemList =>shopDatabase.GetShopList();
    //�f�[�^�x�[�X����w���\�ȃA�C�e�����擾

    //�������͂����ł͊Ǘ����Ȃ�
    
    //������肽���̂͏��X�ŕ������ۂɐ��ʂ����߂�ۂɁA�������̍݌ɂ𒴂����Ȃ��d�g��

    //�w���y�є̔��͏����͂����ȊO�ł��������������������Ȃ��B
        
    //���̌�͒���Storage�N���X�̃��\�b�h���Ăяo���āu��n���q�Ɂv�ɓ���邾��

    ///�S��ShopUIManager�ɉ񂵂Ƃ���
    
} 