using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post
    {
        public string Id { get; set; }

        public string Experience { get; set; }

        public string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }

        public static async void Insert(Post post)
        {
            await App.MobileServiceClient.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<IEnumerable<Post>> Read()
        {
            return await App.MobileServiceClient.GetTable<Post>().Where(p => p.UserId == App.User.Id).ToListAsync();
        }

        public static Dictionary<string, int> PostedCategories(List<Post> posts)
        {
            var postsCategories = (from p in posts
                                   orderby p.CategoryId
                select p.CategoryName).Distinct().ToList();

            var categoriesWithCount = postsCategories.Select(category => new
            {
                categoryName = category,
                categoryCount = posts.Count(post => post.CategoryName == category)
            }).ToDictionary(k => k.categoryName, v => v.categoryCount);

            return categoriesWithCount;
        }
    }
}