using System.Collections.Generic;
using Project.Seed.CricutApi;
using Project.Seed.Interfaces;
using System.Linq;

namespace Project.Seed.Services
{
    public class UserService : IUserService
    {
        public Dictionary<ApiUser, List<int>> GetUserDetails(List<int> projectIds)
        {
            var results = new Dictionary<ApiUser, List<int>>();

            foreach(var projectId in projectIds)
            {
                var user = GetUserDetails(projectId);
                ApiUser key = results.Keys.FirstOrDefault(k => k.Email == user.Email);
                if (key == null)
                {
                    var list = new List<int> { projectId };
                    results.Add(user, list);
                }
                else
                {
                    var list = results[key];
                    list.Add(projectId);
                }
            }

            return results;
        }

        public ApiUser GetUserDetails(int canvasId)
        {
            //using (var projectEngine = new CricutProjectEngineEntities())
            //{
            //    var query = from projects in projectEngine.Projects
            //                join profiles in projectEngine.Profiles on projects.ProfileID equals profiles.ID
            //                where projects.ID == projectId
            //                select profiles.UserName;

            //    return new ApiUser { Email = "impersonator135@cricut.com|" + query.SingleOrDefault(), Password = "Cr4c4t2016!" };
            //}

            using (var db = new KeplerEntities())
            {
                var query = from canvases in db.Canvases
                            join users in db.Users on canvases.UserId equals users.UserId
                            where canvases.CanvasId == canvasId
                            select new
                            {
                                Email = users.Username,
                                FirstName = users.FirstName,
                                LastName = users.LastName
                            };
                //var userInfo = query.FirstOrDefault();
                //detail.Email = userInfo.Email;
                //detail.FirstName = userInfo.FirstName;
                //detail.LastName = userInfo.LastName;
                return new ApiUser { Email = "impersonator135@cricut.com|" + query.SingleOrDefault().Email, Password = "Cr4c4t2016!" };
            }
        }
    }
}
