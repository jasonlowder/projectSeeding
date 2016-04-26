using System.Collections.Generic;

namespace Project.Seed.Interfaces
{
    public interface IUserService
    {
        CricutApi.ApiUser GetUserDetails(int projectId);
        Dictionary<CricutApi.ApiUser, List<int>> GetUserDetails(List<int> projectIds);
    }
}
