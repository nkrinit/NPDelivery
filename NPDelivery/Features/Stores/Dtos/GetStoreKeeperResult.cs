namespace NPDelivery.Features.Stores.Dtos;

public sealed record CreateStoreKeeperResult(int StoreKeeperId);
public sealed record GetStoreKeeperResult(int StoreKeeperId, List<StoreDto> Stores);
