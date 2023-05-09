namespace NPDelivery.Features.Stores.Dtos;

public sealed record GetStoreKeeperResult(int StoreKeeperId, List<StoreDto> Stores);
