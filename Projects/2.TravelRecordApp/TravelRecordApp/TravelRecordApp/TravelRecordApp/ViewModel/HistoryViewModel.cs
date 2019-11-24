using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    public class HistoryViewModel
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryViewModel()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async Task UpdatePosts()
        {
            var posts = await Post.Read();

            Posts.Clear();
            foreach (var post in posts)
            {
                Posts.Add(post);
            }
        }
    }
}