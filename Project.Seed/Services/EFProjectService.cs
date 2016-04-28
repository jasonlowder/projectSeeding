using System;
using System.Collections.Generic;
using Project.Seed.CricutApi;
using Project.Seed.Interfaces;
using Project.Seed.Mongo;
using System.Linq;

namespace Project.Seed.Services
{
    public class EFProjectService : IProjectService
    {
        int _resultCount = 10000;
        int _startIndex = 0;

        public EFProjectService()
        {
        }

        public EFProjectService(int resultCount, int startIndex)
        {
            _resultCount = resultCount;
            _startIndex = startIndex;
        }

        public List<MongoProject> Get(MongoUser user)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ApiUser, List<MongoProject>> GetAll()
        {
            var list = new List<MongoProject>();
            var projectIds = GetProjectIds();
            foreach (var id in projectIds)
            {
                var detailsList = GetProjectDetails(id);
                foreach (var details in detailsList)
                {
                    list.Add(new MongoProject
                    {
                        CanvasId = details.CanvasId.HasValue ? details.CanvasId.Value : -1,
                        Complexity = new MongoComplexity
                        {
                            Difficulty = new Difficulty
                            {
                                SkillLevel = details.DifficultyID
                            },
                            OverallTimeRequired = details.TimeRequired
                        },
                        CreatedDate = details.CreationDate,
                        Quote = details.OverviewDescription,
                        MaterialsUsed = new MaterialsUsed(details.OtherMaterials),
                        ModifiedDate = details.UpdateDate,
                        ProfileId = details.ProfileID.ToString(),
                        PublishDate = details.PublishDate,
                        Published = true,
                        Shared = true,
                        ProjectImages = new List<MongoImage>
                        {
                            new MongoImage
                            {
                                ImageOrder = 1,
                                ImageUrl = details.ImageURL
                            }
                        },
                        Title = details.Title
                    });
                }
            }
            return GetProfileInfo(list);
        }

        public Dictionary<ApiUser, List<MongoProject>> GetProfileInfo(List<MongoProject> details)
        {
            using (var db = new KeplerEntities())
            {
                var map = new Dictionary<ApiUser, List<MongoProject>>();
                foreach (var detail in details)
                {
                    var query = from canvases in db.Canvases
                                join users in db.Users on canvases.UserId equals users.UserId
                                where canvases.CanvasId == detail.CanvasId
                                select new
                                {
                                    Email = users.Username,
                                    FirstName = users.FirstName,
                                    LastName = users.LastName
                                };
                    var userInfo = query.FirstOrDefault();
                    var user = map.Keys.FirstOrDefault(u => u.Email.Contains(userInfo.Email));
                    if (user == null)
                    {
                        user = new ApiUser { Email = "impersonator135@cricut.com|" + userInfo.Email, Password = "Cr4c4t2016!" };
                        map.Add(user, new List<MongoProject>());
                    }

                    map[user].Add(detail);
                }
                return map;
            }
        }

        private List<int> GetProjectIds()
        {
            var list = new List<int>();
            using (var db = new KeplerEntities())
            {
                var projects = db.ProjectsGetFeaturedLatestByCategory(0, _startIndex, _resultCount, "", "");
                foreach (var project in projects)
                {
                    list.Add(project.ProjectID);
                }
            }
            return list;
        }

        private IEnumerable<ProjectGetDetailsByIDV2_Result> GetProjectDetails(int id)
        {
            using (var db = new KeplerEntities())
            {
                return db.ProjectGetDetailsByIDV2(id, 1, 0, 2).ToList();
            }
        }

        public List<CricutApi.ProjectTag> GetProjectTags(int canvasId)
        {
            using (var db = new KeplerEntities())
            {
                var query = from c in db.Canvas_ImageCategories
                            join i in db.ImageCategories on c.ImageCategoryID equals i.ImageCategoryID
                            where c.CanvasID == canvasId
                            select new
                            {
                                Id = i.ImageCategoryID,
                                TagName = i.CategoryName
                            };
                var tags = new List<CricutApi.ProjectTag>();

                foreach (var tag in query.ToList())
                {
                    tags.Add(new CricutApi.ProjectTag { Id = tag.Id, TagName = tag.TagName });
                }
                return tags;
            }
        }
    }
}
