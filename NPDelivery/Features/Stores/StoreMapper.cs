using NPDelivery.Domain;
using NPDelivery.Features.Stores.Dtos;

using Riok.Mapperly.Abstractions;

namespace NPDelivery.Features.Stores;

[Mapper]
public partial class StoreMapper
{
    [MapProperty(nameof(Store.Id), nameof(GetStoreResult.StoreId))]
    public partial GetStoreResult StoreToGetStoreResult(Store store);

    public partial StoreProductDto ProductToStoreProductDto(Product product);

    [MapProperty(nameof(StoreKeeper.Id), nameof(GetStoreKeeperResult.StoreKeeperId))]
    public partial GetStoreKeeperResult StoreKeeperToGetStoreKeeperResult(StoreKeeper storeKeeper);

    public partial StoreDto StoreToStoreDto(Store store);
}