using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecordApp.Annotations;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string experience;

        public string Experience
        {
            get => experience;
            set
            {
                experience = value;
                OnPropertyChanged(nameof(Experience));
            }
        }

        private string venueName;

        public string VenueName
        {
            get => venueName;
            set
            {
                venueName = value;
                OnPropertyChanged(nameof(VenueName));
            }
        }

        private string categoryId;

        public string CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        private string categoryName;

        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private double latitude;

        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }

        private double longitude;

        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }

        private int distance;

        public int Distance
        {
            get => distance;
            set
            {
                distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private string userId;

        public string UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        private Venue venue;

        [JsonIgnore]
        public Venue Venue
        {
            get => venue;
            set
            {
                venue = value;

                var firstCategory = venue?.categories?.FirstOrDefault();

                CategoryId = firstCategory?.id;
                CategoryName = firstCategory?.name;
                Address = venue?.location?.address;
                Distance = venue?.location?.distance ?? default;
                Latitude = venue?.location?.lat ?? default;
                Longitude = venue?.location?.lng ?? default;
                VenueName = venue?.name;
                UserId = App.User.Id;

                OnPropertyChanged(nameof(Venue));
            }
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