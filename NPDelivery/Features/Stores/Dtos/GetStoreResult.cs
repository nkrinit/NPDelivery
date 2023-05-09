namespace NPDelivery.Features.Stores.Dtos;

public sealed record GetStoreResult(int StoreId, string Name, string Description, List<StoreProductDto> Products);
