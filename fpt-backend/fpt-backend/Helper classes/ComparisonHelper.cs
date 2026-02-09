using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models;

namespace fpt_backend.Helper_classes;

public static class ComparisonHelper<T> where T: BaseModel
{
    public static List<T> GetRemoved(List<T> currentList, IEnumerable<int?> updatedListIds)
    {
        //Hashset of IDs to check against current entities in db
        var idsToCheck = updatedListIds.Where(id => id.HasValue).Select(id => id!.Value)
            .ToHashSet();

        return currentList
            .Where(e => !idsToCheck.Contains(e.Id))
            .ToList();
    }
}