using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TravelRecordApp.Annotations;

namespace TravelRecordApp.Model
{
    public class Post: INotifyPropertyChanged
    {
        public string Id
        {
            get => Id;
            set { Id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Experience
        {
            get => Experience;
            set { Experience = value; OnPropertyChanged(nameof(Experience)); }
        }

        public string VenueName
        {
            get => VenueName;
            set { VenueName = value; OnPropertyChanged(nameof(VenueName)); }
        }

        public string CategoryId
        {
            get => CategoryId;
            set { CategoryId = value; OnPropertyChanged(nameof(CategoryId)); }
        }

        public string CategoryName
        {
            get => CategoryName;
            set { CategoryName = value; OnPropertyChanged(nameof(CategoryName)); }
        }

        public string Address
        {
            get => Address;
            set { Address = value; OnPropertyChanged(nameof(Address)); }
        }

        public double Latitude
        {
            get => Latitude;
            set { Latitude = value; OnPropertyChanged(nameof(Latitude)); }
        }

        public double Longitude
        {
            get => Longitude;
            set { Longitude = value; OnPropertyChanged(nameof(Longitude)); }
        }

        public double Distance
        {
            get => Distance;
            set { Distance = value; OnPropertyChanged(nameof(Distance)); }
        }

        public string UserId
        {
            get => UserId;
            set { UserId = value; OnPropertyChanged(nameof(UserId)); }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}