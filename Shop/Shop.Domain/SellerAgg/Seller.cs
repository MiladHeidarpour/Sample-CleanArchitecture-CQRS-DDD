using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Enums;
using Shop.Domain.SellerAgg.Services;
using System;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    public long UserId { get; private set; }
    public string ShopName { get; private set; }
    public string NationalCode { get; private set; }
    public SellerStatus Status { get; private set; }
    public DateTime? LastUpdate { get; private set; }
    public List<SellerInventory> Inventories { get; private set; }

    private Seller()
    {
        
    }
    public Seller(long userId, string shopName, string nationalCode,ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Inventories = new List<SellerInventory>();
        if (domainService.IsValidSellerInformation(this)==false)
        {
            throw new InvalidDomainDataException("اطلاعات نامعتبر است");
        }

    }
    public void ChangeStatus(SellerStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }
    public void Edit(string shopName, string nationalCode, SellerStatus status, ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        if (nationalCode!=NationalCode)
        {
            if (domainService.NationalCodeIsExist(nationalCode)==true)
            {
                throw new InvalidDomainDataException("کد ملی مطعلق به شخص دیگری است");
            }
        }
        ShopName = shopName;
        NationalCode = nationalCode;
        Status = status;
    }
    public void AddInventory(SellerInventory inventory)
    {
        if (Inventories.Any(f=>f.ProductId==inventory.ProductId))
        {
            throw new InvalidDomainDataException("این محصول قبلا ثبت شده است");
        }
        Inventories.Add(inventory);
    }
    public void EditInventory(long inventoryId,int price,int count,int? discountPercentage)
    {
        var currentInventory = Inventories.FirstOrDefault(f => f.Id == inventoryId);
        if (currentInventory == null)
        {
            return;
        }

        //TODO Check Inventory
        currentInventory.Edit(price, count, discountPercentage);

    }
    //public void DeleteInventory(long inventoryId)
    //{
    //    var currentInventory = Inventories.FirstOrDefault(f => f.Id == inventoryId);
    //    if (currentInventory == null)
    //    {
    //        throw new NullOrEmptyDomainDataException("محصول یافت نشد");
    //    }
    //    Inventories.Remove(currentInventory);
    //}
    public void Guard(string shopName, string nationalCode)
    {
        NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));
        if (IranianNationalIdChecker.IsValid(nationalCode) == false)
        {
            throw new InvalidDomainDataException("کد ملی نامعتبر است");
        }
    }
}
